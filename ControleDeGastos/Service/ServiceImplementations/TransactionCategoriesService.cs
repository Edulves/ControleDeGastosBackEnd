using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.CategoriesRequests;
using ExpensesControl.Models;
using ExpensesControl.Repositories.InterfaceRepositories;
using ExpensesControl.Repositories.RepositoriesInterface;
using ExpensesControl.Service.ServiceInterfaces;

namespace ExpensesControl.Service.ServiceImplementations;

public class TransactionCategoriesService(IExpensesControlRepository expensesControlRepository, IGenericOperationsRepository GenericOperationsRepository) : ITransactionCategoriesService
{
    public async Task<ResultPattern<List<TransactionCategory>>> GetEntryCategoriesAsync()
    {
        var result = await expensesControlRepository.GetTransactionCategories();

        return ResultPattern<List<TransactionCategory>>.Success(result);
    }
    public async Task<ResultPattern<string>> CreateCategoriesAsync(List<CreateCategoryRequest> request)
    {
        var modelTransactionCategory = request.Select(x => new TransactionCategory()
        {
            Name = x.Name.ToLower(),
        }).ToList();

        await GenericOperationsRepository.CreateAsync(modelTransactionCategory);

        return ResultPattern<string>.Success($"Categories created!");
    }
    public async Task<ResultPattern<string>> PutCategoriesAsync(List<TransactionCategory> request)
    {
        foreach (var item in request)
        {
            item.Name = item.Name.ToLower();
        }

        await GenericOperationsRepository.UpdateAsync(request);

        return ResultPattern<string>.Success($"Categories updated!");
    }
    public async Task<ResultPattern<string>> DeleteCategoryByIdAsync(int id)
    {
        var result = await expensesControlRepository.GetTransactionCategoryById(id);

        if (result == null)
            return ResultPattern<string>.Failure($"No category found with id: {id}");

        result.IsDeleted = true;

        await GenericOperationsRepository.UpdateAsync(result);

        return ResultPattern<string>.Success($"Category deleted!");
    }
}
