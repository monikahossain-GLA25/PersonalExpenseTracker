using PersonalExpenseTracker.Enums;
using PersonalExpenseTracker.Models;
using PersonalExpenseTracker.Services;
using System;
using System.Linq;

namespace PersonalExpenseTracker.UI
{
    public  class ExpenseMenu
    {

        private readonly ExpenseService _expenseService;

        public ExpenseMenu(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        public void ShowMenu()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("==============================");
                Console.WriteLine("   PERSONAL EXPENSE TRACKER");
                Console.WriteLine("==============================");

                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. View All Expenses");
                Console.WriteLine("3. Search by Category");
                Console.WriteLine("4. Update Expense");
                Console.WriteLine("5. Delete Expense");
                Console.WriteLine("6. View Total Expense");
                Console.WriteLine("0. Exit");

                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddExpense();
                        break;
                    //case "1":
                    //    Console.WriteLine("Add Expense selected");
                    //    break;

                    case "2":
                        ViewAllExpenses();
                        break;

                    case "3":
                        SearchByCategory();
                        break;

                    case "4":
                        UpdateExpense();
                        break;

                    case "5":
                        // Delete
                        DeleteExpense();
                        break;

                    case "6":
                        // Total
                        Console.WriteLine("Total Expense selected");
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }



            }
        }


        private void AddExpense()
        {
            Console.WriteLine();
            Console.WriteLine("--- Add Expense ---");

            // 1. Title
            Console.Write("Title: ");
            string? title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            // 2. Amount
            decimal amount = ReadPositiveAmount();

            // 3. Category  ← You are probably missing this line
            ExpenseCategory category = ReadCategory();

            // 4. Date
            DateTime expenseDate = ReadExpenseDate();

            // 5. Note
            Console.Write("Note: ");
            string? note = Console.ReadLine();

            // 6. ID
            int id = _expenseService.GetNextId();

            // 7. Create object
            Expense expense = new Expense(
                id,
                title,
                amount,
                category,
                expenseDate,
                note
            );

            // 8. Add
            _expenseService.AddExpense(expense);
        }
        private void ViewAllExpenses()
        {
            List<Expense> expenses =
                _expenseService.GetAllExpenses();

            Console.WriteLine();
            Console.WriteLine("--- ALL EXPENSES ---");

            if (expenses.Count == 0)
            {
                Console.WriteLine(
                    "No expenses found.");

                return;
            }

            foreach (Expense expense in expenses)
            {
                Console.WriteLine(
                    $"ID: {expense.Id}");

                Console.WriteLine(
                    $"Title: {expense.Title}");

                Console.WriteLine(
                    $"Amount: {expense.Amount:F2}");

                Console.WriteLine(
                    $"Category: {expense.Category}");

                Console.WriteLine(
                    $"Date: " +
                    $"{expense.ExpenseDate:dd MMM yyyy}");

                Console.WriteLine(
                    $"Note: {expense.Note ?? "No note"}");

                Console.WriteLine(
                    "------------------------------");
            }
        }

        private void SearchByCategory()
        {
            Console.WriteLine();
            Console.WriteLine(
                "--- SEARCH BY CATEGORY ---");

            ExpenseCategory category =
                ReadCategory();

            List<Expense> expenses =
                _expenseService
                    .GetByCategory(category)
                    .ToList();

            if (expenses.Count == 0)
            {
                Console.WriteLine(
                    "No expenses found in this category.");

                return;
            }

            foreach (Expense expense in expenses)
            {
                Console.WriteLine(
                    $"{expense.Id} | " +
                    $"{expense.Title} | " +
                    $"{expense.Amount:F2} | " +
                    $"{expense.ExpenseDate:dd MMM yyyy}");
            }
        }

        private int ReadPositiveInteger(
    string message)
        {
            while (true)
            {
                Console.Write(message);

                string? input =
                    Console.ReadLine();

                if (int.TryParse(
                        input,
                        out int number)
                    &&
                    number > 0)
                {
                    return number;
                }

                Console.WriteLine(
                    "Please enter a valid positive number.");
            }
        }

        private void UpdateExpense()
        {
            Console.WriteLine();
            Console.WriteLine("--- UPDATE EXPENSE ---");

            int id = ReadPositiveInteger(
                "Enter expense ID: ");

            Expense? expense =
                _expenseService.GetById(id);

            if (expense == null)
            {
                Console.WriteLine(
                    "Expense not found.");

                return;
            }

            Console.WriteLine(
                $"Current Title: {expense.Title}");

            Console.Write("New Title: ");
            string? title =
                Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine(
                    "Title cannot be empty.");

                return;
            }

            decimal amount =
                ReadPositiveAmount();

            ExpenseCategory category =
                ReadCategory();

            DateTime expenseDate =
                ReadExpenseDate();

            Console.Write(
                "New Note (optional): ");

            string? note =
                Console.ReadLine();

            bool updated =
                _expenseService.UpdateExpense(
                    id,
                    title,
                    amount,
                    category,
                    expenseDate,
                    note);

            if (updated)
            {
                Console.WriteLine(
                    "Expense updated successfully.");
            }
            else
            {
                Console.WriteLine(
                    "Expense could not be updated.");
            }
        }
        private void DeleteExpense()
        {
            Console.WriteLine();
            Console.WriteLine("--- DELETE EXPENSE ---");

            int id =
                ReadPositiveInteger(
                    "Enter expense ID: ");

            Expense? expense =
                _expenseService.GetById(id);

            if (expense == null)
            {
                Console.WriteLine(
                    "Expense not found.");

                return;
            }

            Console.WriteLine(
                $"{expense.Title} | " +
                $"{expense.Amount:F2}");

            Console.Write(
                "Are you sure you want to delete? (y/n): ");

            string? confirmation =
                Console.ReadLine();

            if (confirmation?.ToLower() != "y")
            {
                Console.WriteLine(
                    "Delete cancelled.");

                return;
            }

            bool deleted =
                _expenseService.DeleteExpense(id);

            if (deleted)
            {
                Console.WriteLine(
                    "Expense deleted successfully.");
            }
        }
        private decimal ReadPositiveAmount()
        {
            while (true)
            {
                Console.Write("Amount: ");

                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal amount)
                    && amount > 0)
                {
                    return amount;
                }

                Console.WriteLine(
                    "Please enter a valid amount greater than 0.");
            }
        }

        private ExpenseCategory ReadCategory()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Expense Categories");

                foreach (ExpenseCategory category
                         in Enum.GetValues<ExpenseCategory>())
                {
                    Console.WriteLine(
                        $"{(int)category}. {category}");
                }

                Console.Write("Select category: ");

                string? input = Console.ReadLine();

                if (int.TryParse(input, out int categoryNumber)
                    &&
                    Enum.IsDefined(
                        typeof(ExpenseCategory),
                        categoryNumber))
                {
                    return (ExpenseCategory)categoryNumber;
                }

                Console.WriteLine(
                    "Invalid category. Please try again.");
            }
        }

        private DateTime ReadExpenseDate()
        {
            while (true)
            {
                Console.Write(
                    "Expense date (yyyy-MM-dd) " +
                    "or press Enter for today: ");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return DateTime.Today;
                }

                if (DateTime.TryParse(
                    input,
                    out DateTime expenseDate))
                {
                    return expenseDate;
                }

                Console.WriteLine(
                    "Invalid date. Please try again.");
            }
        }
    }
}
