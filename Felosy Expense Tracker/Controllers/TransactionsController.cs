using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Felosy_Expense_Tracker.Data.Contexts;
using Felosy_Expense_Tracker.Models;

namespace Felosy_Expense_Tracker.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Transactions.Include(t => t.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Transactions/AddOrEdit
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();
            if (id == 0)
                return View(new Transaction());
            else
                return View(_context.Transactions.Find(id));
        }

        // POST: Transactions/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Description,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                    await _context.AddAsync(transaction);
                else
                    _context.Update(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(transaction);
        }

        // POST: Transactions/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'AppDbContext.Transactions' is null.");
            }
            var transaction = _context.Transactions.Find(id);
            if (transaction != null)     
                _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    }
}
