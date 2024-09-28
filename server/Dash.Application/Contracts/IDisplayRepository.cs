using Dash.Persistence.Models;

namespace Dash.Application.Contracts;
public interface IDisplayRepository
{
    // We have only one display now. So always return the id.
    public static Guid DefaultDisplayId => Guid.Parse("5e02c38b-fff2-4991-85dc-693aa2a82b6d");
    Task Init();
    void CreateOrUpdate(Display display);
    bool Exists(Guid displayId);
    Display? GetDisplay(Guid displayId);
    Display? GetDisplayByShareId(Guid shareId);
}
