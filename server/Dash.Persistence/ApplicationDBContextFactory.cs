using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Dash.Persistence
{
    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            optionsBuilder.UseSqlite("DashDatabase", options => options.MigrationsAssembly(typeof(ApplicationDBContext).GetTypeInfo().Assembly.GetName().Name));

            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }
}
