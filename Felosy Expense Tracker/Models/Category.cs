using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Felosy_Expense_Tracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; } // PK

        [Required(ErrorMessage = "Title is required.")]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; } = "Expense";

        [NotMapped]
        public string? TitleIcon { 
            get
            {
                return this.Icon + " " + this.Title;
            }
        }
    }
}
