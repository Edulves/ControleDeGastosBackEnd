using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;
using ExpensesControl.Repositories.RepositoriesInterface;
using ExpensesControl.Repositories.RepositoryInterfaces;
using ExpensesControl.Service.ServiceInterfaces;

namespace ExpensesControl.Service.ServiceImplementations;

public class FixedExpensesService(IGenericOperationsRepository GenericOperationsRepository, IFixedExpensesRepository fixedExpensesRepository, ICurrentUserService currentUser) : IFixedExpensesService
{
    public async Task<ResultPattern<PagedResult<FixedExpense>>> GetFixedExpensesAsync(GetFixedExpensesRequest request)
    {
        if (request.BeginningOfPeriod > request.EndOfPeriod)
            return ResultPattern<PagedResult<FixedExpense>>.Failure("The start period cannot be latter than the end period");

        if (request.Page < 1)
            return ResultPattern<PagedResult<FixedExpense>>.Failure("Page cannot be smaller than 1");

        var (items, totalItems) = await fixedExpensesRepository.GetFixedExpenses(request);

        var respostaPaginada = (items, totalItems).ToPagedResult(request.Page, request.QTY);

        return ResultPattern<PagedResult<FixedExpense>>.Success(respostaPaginada);
    }
    public async Task<ResultPattern<string>> PostFixedExpenseAsync(List<PostFixedExpensesDto> request)
    {
        var fixedExpenseModel = request.Select(x => new FixedExpense
        {
            Description = x.Description,
            Amount = x.Amount,
            FixedExpenseDate = x.FixedExpenseDate,
            UserId = currentUser.UserId!
        }).ToList();

        foreach (var item in fixedExpenseModel)
        {
            if (item.FixedExpenseDate == DateOnly.MinValue)
                return ResultPattern<string>.Failure($"Invalid date {item.FixedExpenseDate}", "Invalid date");
        }

        await GenericOperationsRepository.CreateAsync(fixedExpenseModel);

        return ResultPattern<string>.Success("Fixed expenses created!", StatusCodes.Status201Created);
    }
    public async Task<ResultPattern<string>> PutFixedExpensesAsync(List<PutFixedExpensesRequest> request)
    {
        if (request.Count <= 0)
            return ResultPattern<string>.Failure($"request is empty");

        var FixedExpenseModel = new List<FixedExpense>();

        foreach (var item in request)
        {
            var result = await fixedExpensesRepository.GetFixedExpensesById(item.FixedExpensesId);
            if (result == null)
            {
                FixedExpenseModel.Add(new FixedExpense()
                {
                    Description = item.Description,
                    Amount = item.Amount,
                    FixedExpenseDate = item.FixedExpenseDate
                });

                continue;
            }

            if(result.UserId != currentUser.UserId)
                return ResultPattern<string>.Failure($"User not allow to change this entry");

            result.Description = string.IsNullOrEmpty(item.Description) ? result.Description : item.Description;
            result.Amount = item.Amount <= 0 ? result.Amount : item.Amount;
            result.IsPaid = item.IsPaid;
            result.FixedExpenseDate = item.FixedExpenseDate == DateOnly.MinValue ? result.FixedExpenseDate : item.FixedExpenseDate;

            FixedExpenseModel.Add(result);
        }

        await GenericOperationsRepository.UpdateAsync(FixedExpenseModel);

        return ResultPattern<string>.Success("Fixed expenses updated!");
    }
    public async Task<ResultPattern<string>> DeleteFixedExpensesAsync(int id)
    {
        var result = await fixedExpensesRepository.GetFixedExpensesById(id);

        if (result == null)
            return ResultPattern<string>.Failure($"Not entry for id: {id}");

        if (result.UserId != currentUser.UserId)
            return ResultPattern<string>.Failure($"User not allow to change this entry");

        result.IsDeleted = true;

        await GenericOperationsRepository.UpdateAsync(result);

        return ResultPattern<string>.Success("Item deleted!");
    }
}
