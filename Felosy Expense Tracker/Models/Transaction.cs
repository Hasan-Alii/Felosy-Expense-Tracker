﻿using Felosy_Expense_Tracker.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Felosy_Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; } // PK
        
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; } // FK
        public Category? Category { get; set; } // Navigational Property
        
        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }
        
        [Column(TypeName = "nvarchar(75)")]
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        
        [NotMapped]
        public string? CategoryTitleIcon 
        { 
            get
            {
                return Category == null ? "no category data" : Category.TitleIcon;
            }
        }


        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0", Cultures.EGY);
            }
        }
    }
}
