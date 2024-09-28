using Dash.Application.Contracts;
using Dash.Persistence;
using Dash.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Dash.Infrastructure.Repositories.Sql;

public class DisplayRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : IDisplayRepository
{
    public void CreateOrUpdate(Display display)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var newDisplay = ctx.Displays.FirstOrDefault(d => d.DisplayId == display.DisplayId);
        if (newDisplay != null)
        {
            newDisplay.Name = display.Name;
            newDisplay.Description = display.Description;
            newDisplay.Icon = display.Icon;
            newDisplay.Columns = display.Columns;
            newDisplay.Rows = display.Rows;
        }

        ctx.SaveChanges();
    }

    public bool Exists(Guid displayId)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.Displays.Any(d => d.DisplayId == displayId);
    }

    public Display? GetDisplay(Guid displayId)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.Displays.FirstOrDefault(d => d.DisplayId == displayId);
    }

    public Display? GetDisplayByShareId(Guid shareId)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.Displays.FirstOrDefault(d => d.ShareId == shareId);
    }

    public async Task Init()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var newDisplay = ctx.Displays.FirstOrDefault(d => d.DisplayId == IDisplayRepository.DefaultDisplayId);
        if (newDisplay != null) return;
        newDisplay = new Display
        {
            DisplayId = IDisplayRepository.DefaultDisplayId,
            ShareId = Guid.NewGuid(),
            Columns = 4,
            Rows = 8,
            Name = "Default"
        };
        ctx.Add(newDisplay);
        await ctx.SaveChangesAsync();
    }
}
