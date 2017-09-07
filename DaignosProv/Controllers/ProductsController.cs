using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DaignosProv.Data;
using DaignosProv.Models;
using DaignosProv.Services;
using Microsoft.Extensions.Logging;

namespace DaignosProv.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryService _category;
        private readonly ILogger _logger;

        public ProductsController(ApplicationDbContext context,CategoryService category,ILogger<ProductsController> logger)
        {
            _context = context;
            _category = category;
            _logger = logger;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var product = await _context.Categories.Include(p => p.Products).ToListAsync();
            _logger.LogWarning("With great powers comes great responsibilities");
            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CatList"] = new SelectList(_context.Categories, "ProductCategoryId", "Name");
            return View();
            
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see 0http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Price,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                foreach (var cat in _category.GetSelectList())
                {
                    var products = new Product
                    {
                        ProductCategoryId = cat.ProductCategoryId,
                       Name= product.Name,
                        Price = product.Price,
                    };
                    //xnncx,
                    _context.Products.Add(products);
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Catlist"] = new SelectList(_context.Categories, "ProductCategoryId", "Name", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Price")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var cat in _category.GetSelectList())
                    {
                        var newproduct = new Product
                        {
                            ProductId=product.ProductId,
                            ProductCategoryId = cat.ProductCategoryId,
                            Name = product.Name,
                            Price = product.Price,
                        };
                        _context.Update(newproduct);
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
