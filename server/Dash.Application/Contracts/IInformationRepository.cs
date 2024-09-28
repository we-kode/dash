using Dash.Persistence.Models;

namespace Dash.Application.Contracts;

public interface IInformationRepository
{
    void CreateOrUpdate(Information data);
    void Delete(List<Guid> id);
    List<Information> Get();
    List<Information> GetList();
    Information? Get(Guid infoId);

}
