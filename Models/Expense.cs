using PersonalExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalExpenseTracker.Models
{
    public  class Expense
    {

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public ExpenseCategory Category { get; set; }

        public DateTime ExpenseDate { get; set; }

        public string? Note { get; set; }

        public Expense(int id, string title , decimal amount , ExpenseCategory category , DateTime expenseDate , string? note)
        {
            Id = id;
            Title = title;
            Amount = amount;
            Category = category;
            ExpenseDate = expenseDate;
            Note = note;
        }
       
    }
}
