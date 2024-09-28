using Dash.Application.Contracts;
using Dash.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dash.Infrastructure.Repositories.Sql
{
    public class UserRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : IUserRepository
    {
    }
}
