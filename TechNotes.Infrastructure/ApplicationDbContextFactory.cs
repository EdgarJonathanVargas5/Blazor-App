using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechNotes.Infrastructure;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=localhost;Database=TechNotes_Db;User ID=SA;Password=MyStrongPass123;TrustServerCertificate=true;MultipleActiveResultSets=true";
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString, 
                                    b => b.MigrationsAssembly("TechNotes.Infrastructure"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
