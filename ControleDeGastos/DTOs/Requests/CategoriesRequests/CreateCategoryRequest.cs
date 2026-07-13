using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.CategoriesRequests
{
    public class CreateCategoryRequest
    {
        [Required]
        [MinLength(3)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
