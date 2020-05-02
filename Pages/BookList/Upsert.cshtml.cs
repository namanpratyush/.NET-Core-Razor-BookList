using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazorProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazorProject
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }


        [BindProperty]
        public Book Book { get; set; }


        public async Task<IActionResult> OnGet(int? Id)
        {
            Book = new Book();
            if(Id == null)
            {
                //create
                return Page();
            }
           
            //update
            Book = await _db.Book.FindAsync(Id);
            if(Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

    }
}