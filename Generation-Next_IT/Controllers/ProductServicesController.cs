using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Generation_Next_IT.Data;
using Generation_Next_IT.Models;

namespace Generation_Next_IT.Controllers
{
    public class ProductServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductServices
        public async Task<IActionResult> Index()
        {
              return _context.Products_Service_Tbl != null ? 
                          View(await _context.Products_Service_Tbl.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Products_Service_Tbl'  is null.");
        }

        // GET: ProductServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products_Service_Tbl == null)
            {
                return NotFound();
            }

            var productService = await _context.Products_Service_Tbl
                .FirstOrDefaultAsync(m => m.ProductServiceID == id);
            if (productService == null)
            {
                return NotFound();
            }

            return View(productService);
        }

        // GET: ProductServices/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductServiceID,ProductServiceName,Unit")] ProductService productService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productService);
        }

        // GET: ProductServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products_Service_Tbl == null)
            {
                return NotFound();
            }

            var productService = await _context.Products_Service_Tbl.FindAsync(id);
            if (productService == null)
            {
                return NotFound();
            }
            return View(productService);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductServiceID,ProductServiceName,Unit")] ProductService productService)
        {
            if (id != productService.ProductServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductServiceExists(productService.ProductServiceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productService);
        }

        // GET: ProductServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products_Service_Tbl == null)
            {
                return NotFound();
            }

            var productService = await _context.Products_Service_Tbl
                .FirstOrDefaultAsync(m => m.ProductServiceID == id);
            if (productService == null)
            {
                return NotFound();
            }

            return View(productService);
        }

        // POST: ProductServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products_Service_Tbl == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products_Service_Tbl'  is null.");
            }
            var productService = await _context.Products_Service_Tbl.FindAsync(id);
            if (productService != null)
            {
                _context.Products_Service_Tbl.Remove(productService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductServiceExists(int id)
        {
          return (_context.Products_Service_Tbl?.Any(e => e.ProductServiceID == id)).GetValueOrDefault();
        }
    }
}
