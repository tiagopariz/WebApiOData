using System.Data.Entity;
using WebApiOData.Entities;

namespace WebApiOData.Context
{
    public class Context : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}