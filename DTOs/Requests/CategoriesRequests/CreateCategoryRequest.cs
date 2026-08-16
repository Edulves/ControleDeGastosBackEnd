using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.CategoriesRequests
{
    public class CreateCategoryRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
