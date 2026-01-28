using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContactsApp.Data;
using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IO;

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
    Console.WriteLine(" POST Create вызван!");
    
    /*if (!ModelState.IsValid)
    {
        Console.WriteLine("ОШИБКИ ВАЛИДАЦИИ!");
        Contacts = await _context.Contacts.ToListAsync();
        return Page();
    }
    */
    //  ДОБАВЛЯЕМ НОВЫЙ КОНТАКТ
    var newContact = new Contact
    {
        Name = ContactInput.Name,
        MobilePhone = ContactInput.MobilePhone,
        JobTitle = ContactInput.JobTitle,
        BirthDate = ContactInput.BirthDate
    };
    
    _context.Contacts.Add(newContact);
    await _context.SaveChangesAsync();
    
    Console.WriteLine($" МАРИЯ ДОБАВЛЕНА! БД: {new FileInfo("contacts.db").Length}б");
    
    // 🔥 ОБНОВЛЯЕМ СПИСОК
    Contacts = await _context.Contacts.ToListAsync();
    
    // ОЧИЩАЕМ ФОРМУ
    ContactInput = new Contact();
    
    return Page();  // ОСТАЁМСЯ НА СТРАНИЦЕ
}
    }
}
