using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWAF.Models
{
    // code-first approach: create model classes that represent your database
    // the database fields are properties in the model class
    // all model classes can be placed in the same file
    // even the database context can be placed in that same file
    // add the connection string and register the database context in the Startup.cs class when you have the model and the dbcontext
    // open the Package Manager Console and type "add-migration <migration name>" then "update-database" and you should have the database
    // a migration should be done every time the model is modified in order the keep it synchronized with the database structure
    public class Movie
    {
        public int ID { get; set; }           // obligatory field, automatically increments
        public string Title { get; set; }

        [Display(Name = "Release Date")]      // data annotations are used for validation and formatting
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
    }
}
