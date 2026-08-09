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
                        // View expenses
                        Console.WriteLine("View Expense selected");
                        break;

                    case "3":
                        // Search
                        Console.WriteLine("Search Expense selected");
                        break;

                    case "4":
                        // Update
                        Console.WriteLine("Update Expense selected");
                        break;

                    case "5":
                        // Delete
                        Console.WriteLine("Delete Expense selected");
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
