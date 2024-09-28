using Dash.Persistence.Models;

namespace Dash.Application.Contracts;

public interface ICalendarRepository
{
    void Create(string name, string color);
    void CreateEvent(string? summary, string? description, string? location, DateTime dtStart, DateTime dtEnd, Guid calendarId);
    void DeleteCalendar(Guid id);
    void DeleteCalendarEvent(Guid id);
    bool Exists(Guid id);
    bool ExistsEvent(Guid id);
    List<Calendar> Get();
    Calendar? Get(Guid id);
    CalendarEvent? GetEvent(Guid id);
    Task Init();
    List<CalendarEvent> ListEvents(DateTime start, DateTime end);
    void Update(Guid id, string name, string color);
    void UpdateEvent(Guid id, string? summary, string? description, string? location, DateTime dtStart, DateTime dtEnd, Guid calendarId);
}
