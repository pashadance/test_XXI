using Microsoft.EntityFrameworkCore;
using static stoneXXI.Models;

namespace stoneXXI;

public class Repository : DbContext
{
    public Repository()
    {
        Database.EnsureCreated();
    }

    public DbSet<HrSpecialist> Hrs { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            if (entityType.FindProperty(nameof(Models.IId.Id)) != null)
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(Models.IId.Id))
                    .ValueGeneratedOnAdd();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=stoneXxi_test;User Id=postgres;Password=54321postgree");
       
        base.OnConfiguring(optionsBuilder);
    }
}

public static class DbExt
{
    public static bool TableExists(this string table)
    {
        using var context = new Repository();
        var isExist = context.Database.SqlQuery<bool>($@"SELECT EXISTS (SELECT  FROM information_schema.tables WHERE table_name = '{table}')").ToList();
        return isExist.Any();
    }

    public static bool IndexExists(this string table, string indexName)
    {
        using var context = new Repository();
        var isExist = context.Database.SqlQuery<bool>($@"SELECT EXISTS (SELECT FROM pg_indexes where tablename = '{table}' and indexname='{indexName}')").ToList();
        return isExist.Any();
    }
}