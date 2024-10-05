using Dash.Application.Contracts;
using Dash.Persistence;
using Dash.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Dash.Infrastructure.Repositories.Sql;

public class ElementRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : IElementRepository
{
    public Guid Create(string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate, int x, int y, int rows, int cols)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = new Element
        {
            Config = config,
            Content = content,
            DisplayId = displayId,
            Display = ctx.Displays.Find(displayId) ?? throw new ArgumentNullException(nameof(displayId)),
            ExpireDate = expireDate,
            ComponentId = componentId,
            Component = ctx.Components.Find(componentId) ?? throw new ArgumentNullException(nameof(componentId)),
            X = x,
            Y = y,
            Cols = cols,
            Rows = rows
        };
        ctx.Add(data);
        ctx.SaveChanges();
        return data.ElementId;
    }

    public void Delete(Guid id)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = ctx.Elements.FirstOrDefault(d => d.ElementId == id);
        if (data != null)
        {
            ctx.Remove(data);
            ctx.SaveChanges();
        }
    }

    public List<Element> GetByDisplay(Guid displayId)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return [.. ctx.Elements.Include(e => e.Component).Where(d => d.DisplayId == displayId)];
    }

    public List<Component> GetComponents()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return [.. ctx.Components.OrderBy(c => c.Name)];
    }

    public async Task Init()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var isCCal = ctx.Components.Any(c => c.Identifier == "app-calendar");
        if (!isCCal)
        {
            ctx.Add(new Component
            {
                Identifier = "app-calendar",
                Image = "calendar_month",
                Name = "Calendar",
                Config = "{}"
            });
        }

        var isCPinboard = ctx.Components.Any(c => c.Identifier == "app-pinboard");
        if (!isCPinboard)
        {
            ctx.Add(new Component
            {
                Identifier = "app-pinboard",
                Image = "dashboard",
                Name = "Pinboard",
                Config = "{}"
            });
        }

        var isCClock = ctx.Components.Any(c => c.Identifier == "app-clock");
        if (!isCClock)
        {
            ctx.Add(new Component
            {
                Identifier = "app-clock",
                Image = "schedule",
                Name = "Clock",
                Config = "{type:\"analog|digital\"}"
            });
        }

        var isCDate = ctx.Components.Any(c => c.Identifier == "app-date");
        if (!isCDate)
        {
            ctx.Add(new Component
            {
                Identifier = "app-date",
                Image = "today",
                Name = "Date",
                Config = "{}"
            });
        }

        var isCText = ctx.Components.Any(c => c.Identifier == "app-text");
        if (!isCText)
        {
            ctx.Add(new Component
            {
                Identifier = "app-text",
                Image = "abc",
                Name = "Text",
                Config = "{}"
            });
        }

        var isCImage = ctx.Components.Any(c => c.Identifier == "app-image");
        if (!isCText)
        {
            ctx.Add(new Component
            {
                Identifier = "app-image",
                Image = "image",
                Name = "Image",
                Config = "{}"
            });
        }

        await ctx.SaveChangesAsync();
    }

    public void Update(Guid id, string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate, int x, int y, int rows, int cols)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = ctx.Elements.FirstOrDefault(e => e.ElementId == id);
        if (data != null)
        {
            data.ExpireDate = expireDate;
            data.Content = content;
            data.ComponentId = componentId;
            data.DisplayId = displayId;
            data.Config = config;
            data.X = x;
            data.Y = y;
            data.Cols = cols;
            data.Rows = rows;
            ctx.SaveChanges();
        }
    }
}
