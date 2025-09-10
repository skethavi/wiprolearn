using System.ComponentModel.DataAnnotations;

namespace RoleProductApp.ViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
    }
}