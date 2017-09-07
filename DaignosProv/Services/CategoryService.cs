using DaignosProv.Data;
using DaignosProv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaignosProv.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ProductCategory> GetSelectList()
        {
            return _context.Categories.ToList();
        }
    }
}
