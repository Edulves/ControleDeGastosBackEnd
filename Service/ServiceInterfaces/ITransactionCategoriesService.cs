using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.CategoriesRequests;
using ExpensesControl.Models;

namespace ExpensesControl.Service.ServiceInterfaces;

public interface ITransactionCategoriesService
{
    Task<ResultPattern<List<TransactionCategory>>> GetEntryCategoriesAsync();
    Task<ResultPattern<string>> CreateCategoriesAsync(List<CreateCategoryRequest> requisicao);
    Task<ResultPattern<string>> PutCategoriesAsync(List<TransactionCategory> requisicao);
    Task<ResultPattern<string>> DeleteCategoryByIdAsync(int id);
}
