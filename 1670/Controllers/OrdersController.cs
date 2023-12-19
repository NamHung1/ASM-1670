using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1670.Data;
using _1670.Models;

namespace _1670.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Order.Include(o => o.Product);
            //return View(await applicationDbContext.ToListAsync());
            var user = await _context.Users.ToListAsync();
            ViewBag.User = user;
            return View(await _context.Order.ToListAsync());
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> ApproveOrder(int id)
        //{
        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> RejectOrder(int id)
        //{
        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        // GET: Orders/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Order == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .Include(o => o.Product)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        // GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
        //    return View();
        //}

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Qty,Price,Phone,OrderTime,ProductId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", order.ProductId);
        //    return View(order);
        //}

        // GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Order == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", order.ProductId);
        //    return View(order);
        //}

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Qty,Price,Phone,OrderTime,ProductId")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", order.ProductId);
        //    return View(order);
        //}

        // GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Order == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .Include(o => o.Product)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Order == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
        //    }
        //    var order = await _context.Order.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Order.Remove(order);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {

        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
