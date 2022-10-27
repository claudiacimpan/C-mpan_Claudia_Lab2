using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cîmpan_Claudia_Lab2.Data;
using Cîmpan_Claudia_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cîmpan_Claudia_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Cîmpan_Claudia_Lab2.Data.Cîmpan_Claudia_Lab2Context _context;

        public DetailsModel(Cîmpan_Claudia_Lab2.Data.Cîmpan_Claudia_Lab2Context context)
        {
            _context = context;
        }

      public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
                ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
                ViewData["FirstName"] = new SelectList(_context.Set<Author>(), "ID","FirstName");
                ViewData["LastName"] = new SelectList(_context.Set<Author>(), "ID","LastName");
            }
            return Page();
        }
    }
}
