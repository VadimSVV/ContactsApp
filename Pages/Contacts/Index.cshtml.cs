using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContactsApp.Data;
using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Contact> Contacts { get; set; } = default!;

        [BindProperty]
        public Contact ContactInput { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Contacts = await _context.Contacts.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                Contacts = await _context.Contacts.ToListAsync();
                return Page();
            }

            _context.Contacts.Add(ContactInput);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
