
using Dash.Persistence.Models;

namespace Dash.Application.Contracts;

public interface IElementRepository
{
    Guid Create(string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate, int x, int y, int rows, int cols);
    void Delete(Guid id);
    List<Element> GetByDisplay(Guid displayId);
    List<Component> GetComponents();
    Task Init();
    void Update(Guid id, string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate, int x, int y, int rows, int cols);
}
