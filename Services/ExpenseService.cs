using PersonalExpenseTracker.Enums;

using System.Linq;
using PersonalExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalExpenseTracker.Services
{
    public class ExpenseService
    {
        private readonly List<Expense> _expenses = new();

        private int _nextId = 1;
        public int GetNextId()
        {
            return _nextId++;
        }
        public bool AddExpense(Expense expense)
        {
            if (string.IsNullOrWhiteSpace(expense.Title))
            {
                return false;
            }

            if (expense.Amount <= 0)
            {
                return false;
            }

            _expenses.Add(expense);

            return true;
        }


        public List<Expense> GetAllExpenses()
        {
            return _expenses;
        }
        public int GetExpenseCount()
        {
            return _expenses.Count;
        }

        public decimal CalculateTotalExpense()
        {
            decimal total = 0;

            foreach (Expense expense in _expenses)
            {
                total = total + expense.Amount;
            }

            return total;
        }
        
        public IEnumerable<Expense> GetByCategory(
    ExpenseCategory category)
        {
            return _expenses.Where(
                expense =>
                    expense.Category == category);
        }
        public Expense? GetById(int id)
        {
            return _expenses.FirstOrDefault(
                expense => expense.Id == id);
        }
        public bool UpdateExpense(
                int id,
                string title,
                decimal amount,
                ExpenseCategory category,
                DateTime expenseDate,
                string? note)
        {
            Expense? expense =
                GetById(id);

            if (expense == null)
            {
                return false;
            }

            expense.Title = title;
            expense.Amount = amount;
            expense.Category = category;
            expense.ExpenseDate = expenseDate;
            expense.Note = note;

            return true;
        }
        public bool DeleteExpense(int id)
        {
            Expense? expense =
                GetById(id);

            if (expense == null)
            {
                return false;
            }

            _expenses.Remove(expense);

            return true;
        }

        
    }
}
