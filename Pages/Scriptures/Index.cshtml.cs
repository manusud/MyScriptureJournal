using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public string BookSort { get; set; }
        public string DateSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; }

        public IList<Journal> Journal { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Notes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookSSelect { get; set; }
        
        public async Task OnGetAsync(string sortOrder)
        {

            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            // Use LINQ to get list of books.
            IQueryable<string> noteQuery = from m in _context.Journal
                                            orderby m.Book
                                            select m.Book;

            var books = from m in _context.Journal
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.Notes.Contains(SearchString));
                CurrentFilter = SearchString;
            }
            
            if (!string.IsNullOrEmpty(BookSSelect))
            {
                books = books.Where(x => x.Book == BookSSelect);
                CurrentFilter = BookSSelect;
            }

            // Sort
            switch (sortOrder)
            {
                case "book_desc":
                    books = books.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    books = books.OrderBy(s => s.EditionDate);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(s => s.EditionDate);
                    break;
                default:
                    books = books.OrderBy(s => s.Book);
                    break;
            }

            
            Notes = new SelectList(await noteQuery.Distinct().ToListAsync());
            Journal = await books.AsNoTracking().ToListAsync();
        }
    }
}
