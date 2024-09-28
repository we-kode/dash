
using Dash.Persistence.Models;

namespace Dash.Application.Contracts;

public interface IElementRepository
{
    void Create(string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate);
    void Delete(Guid id);
    List<Element> GetByDisplay(Guid displayId);
    List<Component> GetComponents();
    Task Init();
    void Update(Guid id, string config, string? content, Guid componentId, Guid displayId, DateTime? expireDate);
}
