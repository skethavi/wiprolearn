using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmployCrud.Models;

namespace EmployCrud.Pages.Employ
{
    public class IndexModel : PageModel
    {
        private readonly EmployCrud.Models.EFCoreDbContext _context;

        public IndexModel(EmployCrud.Models.EFCoreDbContext context)
        {
            _context = context;
        }

        public IList<EmployCrud.Models.Employ> Employ { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Employ = await _context.Employees.ToListAsync();
        }
    }
}