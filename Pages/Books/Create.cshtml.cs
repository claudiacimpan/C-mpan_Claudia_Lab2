using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cîmpan_Claudia_Lab2.Data;
using Cîmpan_Claudia_Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Cîmpan_Claudia_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Cîmpan_Claudia_Lab2.Data.Cîmpan_Claudia_Lab2Context _context;

        public CreateModel(Cîmpan_Claudia_Lab2.Data.Cîmpan_Claudia_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
         //   ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");

             ViewData["FirstName"] = new SelectList(_context.Set<Author>(), "ID","FirstName");
            ViewData["LastName"] = new SelectList(_context.Set<Author>(), "ID","LastName");
            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        /* public async Task<IActionResult> OnPostAsync()
         {
           if (!ModelState.IsValid)
             {
                 return Page();
             }

             _context.Book.Add(Book);
             await _context.SaveChangesAsync();

             return RedirectToPage("./Index");
         }*/
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newBook = new Book();
            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Book>(
            newBook,
            "Book",
            i => i.Title, i => i.Author,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                _context.Book.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newBook);
            return Page();
        }
    } 
} 
