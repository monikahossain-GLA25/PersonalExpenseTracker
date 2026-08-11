using PersonalExpenseTracker.Services;
using PersonalExpenseTracker.UI;

public class Program
{
    public static void Main(string[] args)
    {
        ExpenseService expenseService =
            new ExpenseService();

        ExpenseMenu expenseMenu =
            new ExpenseMenu(expenseService);

        expenseMenu.ShowMenu();
    }
}