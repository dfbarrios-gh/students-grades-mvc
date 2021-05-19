using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StudentGrades.Repository
{
    public class SchoolContext: DbContext
    {
        public SchoolContext() : base("SchoolContext")
        {

        }

        public DbSet<StudentGrades.Models.StudentGrades> StudentGrades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}