using Microsoft.EntityFrameworkCore;

//“Entity Framework is an object-relational mapper (O/RM) that enables .NET developers to work with a database using .NET objects. 
//It eliminates the need for most of the data-access code that developers usually need to write.”

namespace SampleWAF.Models
{
    public class MovieContext : DbContext             // any database context must be derived from the DbContext class
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {

        }

        public DbSet<SampleWAF.Models.Movie> Movie { get; set; }     // representation of database table
    }
}
