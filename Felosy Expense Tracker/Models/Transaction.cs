using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Felosy_Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; } // PK
        //TODO
        public int CategoryId { get; set; } // FK
        public Category Category { get; set; } // Navigational Property

        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        
        public string? Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
