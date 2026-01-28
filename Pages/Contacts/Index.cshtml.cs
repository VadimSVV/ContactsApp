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
    
    var newContact = new Contact
    {
        Name = ContactInput.Name,
        MobilePhone = ContactInput.MobilePhone,
        JobTitle = ContactInput.JobTitle,
        BirthDate = ContactInput.BirthDate
    };
    
    _context.Contacts.Add(newContact);
    await _context.SaveChangesAsync();
    
    Console.WriteLine($" ✅ ДОБАВЛЕН! БД: {new FileInfo("contacts.db").Length}б");
    
    Contacts = await _context.Contacts.ToListAsync();
    ContactInput = new Contact();
    
    return Page();
}

// 🔥 UPDATE (ИЗМЕНЕНИЕ КОНТАКТА)
public async Task<IActionResult> OnGetEditAsync(int id)
{
    Console.WriteLine($"🔥 Edit GET: ID={id}");
    
    // 1. ГРУЗИМ ВСЕ КОНТАКТЫ ПЕРВЫМ ДЕЛОМ!
    Contacts = await _context.Contacts.ToListAsync();
    
    // 2. НАХОДИМ РЕДАКТИРУЕМЫЙ
    var contact = await _context.Contacts.FindAsync(id);
    if (contact == null) return NotFound();
    
    // 3. ЗАПОЛНЯЕМ ФОРМУ
    ContactInput = contact;
    
    return Page();
}

public async Task<IActionResult> OnPostUpdateAsync()
{
    Console.WriteLine("🔥 POST Update вызван!");
    
    var contact = await _context.Contacts.FindAsync(ContactInput.Id);
    if (contact == null) return NotFound();
    
    // 🔥 ОБНОВЛЯЕМ ПОЛЯ
    contact.Name = ContactInput.Name;
    contact.MobilePhone = ContactInput.MobilePhone;
    contact.JobTitle = ContactInput.JobTitle;
    contact.BirthDate = ContactInput.BirthDate;
    
    await _context.SaveChangesAsync();
    Console.WriteLine("✅ КОНТАКТ ОБНОВЛЁН!");
    
    Contacts = await _context.Contacts.ToListAsync();
    ContactInput = new Contact();
    return Page();
}
    }
}
