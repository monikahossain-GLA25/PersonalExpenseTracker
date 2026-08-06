using PersonalExpenseTracker.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
                        Console.WriteLine("Add Expense selected");
                        break;

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
    }
}
