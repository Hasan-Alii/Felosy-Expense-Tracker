using Felosy_Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Felosy_Expense_Tracker.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
