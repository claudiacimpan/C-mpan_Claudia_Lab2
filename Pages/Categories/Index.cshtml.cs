using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cîmpan_Claudia_Lab2.Data;
using Cîmpan_Claudia_Lab2.Models;
using Cîmpan_Claudia_Lab2.Models.ViewModels;

namespace Cîmpan_Claudia_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Cîmpan_Claudia_Lab2.Data.Cîmpan_Claudia_Lab2Context _context;

        public IndexModel(Cîmpan_Claudia_Lab2.Data.Cîmpan_Claudia_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;
        public CategoriesIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoriesIndexData();
            CategoryData.Categories = await _context.Category
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Book)
                .ThenInclude(c => c.Author)
                .OrderBy(i => i.CategoryName)
                .ToListAsync();
            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                .Where(i => i.ID == id.Value).Single();
                CategoryData.BookCategories = category.BookCategories;
            }
        }
        /* public async Task OnGetAsync()
         {
             if (_context.Category != null)
             {
                 Category = await _context.Category.ToListAsync();
             }
         }*/
        /* public PublisherIndexData PublisherData { get; set; }
         public int PublisherID { get; set; }
         public int BookID { get; set; }
         public async Task OnGetAsync(int? id, int? bookID)
         {
             PublisherData = new PublisherIndexData();
             PublisherData.Publishers = await _context.Publisher
             .Include(i => i.Books)
             .ThenInclude(c => c.Author)
             .OrderBy(i => i.PublisherName)
             .ToListAsync();
             if (id != null)
             {
                 PublisherID = id.Value;
                 Publisher publisher = PublisherData.Publishers
                 .Where(i => i.ID == id.Value).Single();
                 PublisherData.Books = publisher.Books;
             }

         }
     }*/
    }
    }