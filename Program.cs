using PersonalExpenseTracker.Enums;
using PersonalExpenseTracker.Models;

using PersonalExpenseTracker.Services;
public class Program
{
    public static void Main(string[] args)
    {
        Expense objExpense = new Expense(1, "Groceries from Unimart : ", 5000, ExpenseCategory.Food, DateTime.Now, "Weekly grocery shopping");
        Expense objExpense2 = new Expense(2, "Metro Rapid Pass: ", 400, ExpenseCategory.Transport, DateTime.Now, "Metro Rapid Pass for daily commute");

        Console.WriteLine($"ID: {objExpense.Id}");
        Console.WriteLine($"Title: {objExpense.Title}");
        Console.WriteLine($"Amount: {objExpense.Amount}");
        Console.WriteLine($"Category: {objExpense.Category}");
        Console.WriteLine($"Date: {objExpense.ExpenseDate:dd MMM yyyy}");
        Console.WriteLine($"Note: {objExpense.Note}");



        Console.WriteLine($"ID: {objExpense2.Id}");

        Expense invalidExpense1 = new Expense(
                        3,
                        "",
                        5000,
                        ExpenseCategory.Shopping,
                        DateTime.Today,
                        null);


        ExpenseService expenseService = new ExpenseService();
        expenseService.AddExpense(objExpense);
        expenseService.AddExpense(objExpense2);

        List<Expense> expenses = expenseService.GetAllExpenses();
        expenseService.DisplayAllExpenses();

        Console.WriteLine($"Total expense records: {expenseService.GetExpenseCount()}");


        decimal totalExpense = expenseService.CalculateTotalExpense();


        Console.WriteLine($"Total amount: {totalExpense:F2}");
    }
}