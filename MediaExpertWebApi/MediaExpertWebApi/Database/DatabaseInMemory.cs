using MediaExpertWebApi.Database;
using MediaExpertWebApi.Models;

namespace MediaExpertWebApi;

public class DatabaseInMemory
{
    public DbSet<Product> Products = new DbSet<Product>();
}
