using Felosy_Expense_Tracker.Data.Contexts;
using Felosy_Expense_Tracker.Helper;
using Felosy_Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Felosy_Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            // Fetch transactions for the last 7 days
            List<Transaction> SelectedTransactions = await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.Date >= StartDate && t.Date <= EndDate)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            // Calculate the total income and expense for the selected transactions
            int TotalIncome = SelectedTransactions
                .Where(t => t.Category != null && t.Category.Type == "Income")
                .Sum(t => t.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0", Cultures.EGY);

            int TotalExpense = SelectedTransactions
                .Where(t => t.Category != null && t.Category.Type == "Expense")
                .Sum(t => t.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0", Cultures.EGY);

            // Calculate the balance
            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = Balance.ToString("C0", Cultures.EGY);

            //Doughnut Chart 
            //Expense By Category
            ViewBag.DoughnutChartData = SelectedTransactions
                .Where(t => t.Category?.Type == "Expense")
                .GroupBy(t => t.CategoryId)
                .Select(t => new
                {
                    categoryTitleIcon = t.First().Category?.TitleIcon,
                    amount = t.Sum(i => i.Amount),
                    percentageAmount = t.Sum(i => i.Amount).ToString("c0", Cultures.EGY)
                })
                .ToList();

            //Spline Chart
            //Income
            List<SplineChartData> IncomeSummary = SelectedTransactions
                .Where(t => t.Category?.Type == "Income")
                .GroupBy(t => t.Date.ToString("dd-MMM"))
                .Select(t => new SplineChartData
                {
                    day = t.Key,
                    income = t.Sum(i => i.Amount),
                })
                .ToList();


            //Expense
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(t => t.Category?.Type == "Expense")
                .GroupBy(t => t.Date.ToString("dd-MMM"))
                .Select(t => new SplineChartData
                {
                    day = t.Key,
                    expense = t.Sum(i => i.Amount),
                })
                .ToList();

            //Income and Expense Summary
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = Last7Days
                .Select(day => new SplineChartData
                {
                    day = day,
                    income = IncomeSummary.FirstOrDefault(i => i.day == day)?.income ?? 0,
                    expense = ExpenseSummary.FirstOrDefault(e => e.day == day)?.expense ?? 0
                })
                .ToList();

            //Recent 10 Transactions
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date)
                .Take(10)
                .ToListAsync();

            return View();
        }

    }
    public class SplineChartData
    {
        public string day = "";
        public int income;
        public int expense;
    }
}