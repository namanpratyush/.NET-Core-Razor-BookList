using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazorProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazorProject.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var bookFromDB = await _db.Book.FirstOrDefaultAsync(x => x.Id == Id);

            if(bookFromDB == null)
            {
                return Json(new { success = false, message ="Error while deleting, Not found" });
            }

            _db.Book.Remove(bookFromDB);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Deleted" });
        }
    }
}