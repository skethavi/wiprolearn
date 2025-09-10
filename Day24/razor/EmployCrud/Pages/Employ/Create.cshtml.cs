using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployCrud.Models;

namespace EmployCrud.Pages.Employ
{
    public class CreateModel : PageModel
    {
        private readonly EmployCrud.Models.EFCoreDbContext _context;

        public CreateModel(EmployCrud.Models.EFCoreDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EmployCrud.Models.Employ Employ { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Employees.Add(Employ);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
