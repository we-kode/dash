using Dash.Application.Contracts;
using Dash.Persistence;
using Dash.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Dash.Infrastructure.Repositories.Sql;

public class CalendarRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : ICalendarRepository
{
    public void Create(string name, string color)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = new Calendar
        {
            HexColor = color,
            Name = name
        };
        ctx.Add(data);
        ctx.SaveChanges();
    }

    public void CreateEvent(string? summary, string? description, string? location, DateTime dtStart, DateTime dtEnd, Guid calendarId)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = new CalendarEvent
        {
            Summary = summary,
            Description = description,
            CalendarId = calendarId,
            Calendar = ctx.Calendars.Find(calendarId) ?? throw new ArgumentNullException(nameof(calendarId)),
            DtStart = dtStart,
            DtEnd = dtEnd,
            DtStamp = DateTime.UtcNow,
            Location = location,
        };
        ctx.Add(data);
        ctx.SaveChanges();
    }

    public void DeleteCalendar(Guid id)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = ctx.Calendars.FirstOrDefault(d => d.Id == id);
        if (data != null)
        {
            ctx.Remove(data);
            ctx.SaveChanges();
        }
    }

    public void DeleteCalendarEvent(Guid id)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = ctx.CalendarEvents.FirstOrDefault(d => d.Id == id);
        if (data != null)
        {
            ctx.Remove(data);
            ctx.SaveChanges();
        }
    }

    public bool Exists(Guid id)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.Calendars.Any(c => c.Id == id);
    }

    public bool ExistsEvent(Guid id)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.CalendarEvents.Any(c => c.Id == id);
    }

    public List<Calendar> Get()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return [.. ctx.Calendars];
    }

    public Calendar? Get(Guid id)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.Calendars.FirstOrDefault(c => c.Id == id);
    }

    public CalendarEvent? GetEvent(Guid id)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.CalendarEvents.Include(c => c.Calendar).FirstOrDefault(c => c.Id == id);
    }

    public async Task Init()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        if (!ctx.Calendars.Any())
        {
            var cal = new Calendar
            {
                Name = "Default",
                HexColor = "#3b6a00"
            };
            ctx.Add(cal);
            await ctx.SaveChangesAsync();
        }
    }

    public List<CalendarEvent> ListEvents(DateTime start, DateTime end)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return [.. ctx.CalendarEvents.Include(c => c.Calendar).Where(c => c.DtStart >= start && c.DtEnd <= end)];
    }

    public void Update(Guid id, string name, string color)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = ctx.Calendars.FirstOrDefault(c => c.Id == id);
        if (data != null)
        {
            data.Name = name;
            data.HexColor = color;
            ctx.SaveChanges();
        }
    }

    public void UpdateEvent(Guid id, string? summary, string? description, string? location, DateTime dtStart, DateTime dtEnd, Guid calendarId)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = ctx.CalendarEvents.FirstOrDefault(c => c.Id == id);
        if (data != null)
        {
            data.Summary = summary;
            data.CalendarId = calendarId;
            data.Description = description;
            data.Location = location;
            data.DtStart = dtStart;
            data.DtEnd = dtEnd;
            ctx.SaveChanges();
        }
    }
}
