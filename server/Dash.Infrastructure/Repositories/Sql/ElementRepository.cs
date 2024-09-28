using Dash.Application.Contracts;
using Dash.Persistence;
using Dash.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Dash.Infrastructure.Repositories.Sql;

public class ElementRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : IElementRepository
{
    public void Create(string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate)
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
            Component = ctx.Components.Find(displayId) ?? throw new ArgumentNullException(nameof(componentId)),
        };
        ctx.Add(data);
        ctx.SaveChanges();
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
        return [.. ctx.Elements.Where(d => d.DisplayId == displayId)];
    }

    public List<Component> GetComponents()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return [.. ctx.Components];
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
                Image = "calendar-month",
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

        var isCInfo = ctx.Components.Any(c => c.Identifier == "app-info");
        if (!isCInfo)
        {
            ctx.Add(new Component
            {
                Identifier = "app-info",
                Image = "image",
                Name = "Information",
                Config = "{text?:\"string\"image?:\"img\"}"
            });
        }

        await ctx.SaveChangesAsync();
    }

    public void Update(Guid id, string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate)
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
            ctx.SaveChanges();
        }
    }
}
