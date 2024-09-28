using Dash.Application.Contracts;
using Dash.Persistence;
using Dash.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Dash.Infrastructure.Repositories.Sql;

public class InformationRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : IInformationRepository
{
    public void CreateOrUpdate(Information data)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var newData = ctx.Informations.FirstOrDefault(d => d.Id == data.Id);
        if (newData == null)
        {
            newData = new Information
            {
                Id = data.Id,
                CreationDate = DateTime.UtcNow
            };
            ctx.Add(newData);
        }

        newData.Name = data.Name;
        newData.Text = data.Text;
        newData.ExpiredDate = data.ExpiredDate;
        newData.Image = data.Image;
        newData.Config = data.Config;
        newData.IsFocused = data.IsFocused;

        ctx.SaveChanges();
    }

    public void Delete(List<Guid> ids)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        var data = ctx.Informations.Where(d => ids.Contains(d.Id));
        ctx.RemoveRange(data);
        ctx.SaveChanges();
    }

    public List<Information> Get()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return [.. ctx.Informations
                    .Where(i => !i.ExpiredDate.HasValue || i.ExpiredDate.Value < DateTime.UtcNow)
               ];
    }

    public Information? Get(Guid infoId)
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return ctx.Informations.FirstOrDefault(i => i.Id == infoId);
    }

    public List<Information> GetList()
    {
        using var ctx = dbContextFactory.CreateDbContext();
        return [.. ctx.Informations.OrderBy(i => i.Name)];
    }
}
