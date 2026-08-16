using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Responses.DailyExpensesReponses;
using ExpensesControl.Models;
using ExpensesControl.Repositories.RepositoriesInterface;
using ExpensesControl.Repositories.RepositoryInterfaces;
using ExpensesControl.Service.ServiceInterfaces;

namespace ExpensesControl.Service.ServiceImplementations;

public class DailyExpensesService(IGenericOperationsRepository GenericOperationsRepository, IDailyExpensesRepository dailyExpensesRepository, ICurrentUserService currentUser) : IDailyExpensesService
{
    public async Task<ResultPattern<string>> CreateDailyExpensesEntriesAsync(List<DailyExpenseEntryRequest> request)
    {
        if (request.Count <= 0)
            return ResultPattern<string>.Failure("request is empty");

        var dailyExpenseModel = request.Select(x => new DailyExpense
        {
            ExpenseDate = x.ExpenseDate,
            Amount = x.Amount,
            Note = x.Note,
            TransactionCategoryId = x.CategoryId,
            IsDeleted = false,
            UserId = currentUser.UserId!
        }).ToList();

        await GenericOperationsRepository.CreateAsync(dailyExpenseModel);

        return ResultPattern<string>.Success("Expense was registered!");
    }
    public async Task<ResultPattern<PagedResult<DailyExpenseResponse>>> GetDailyExpensesAsync(DailyExpensesRequest request)
    {
        if (request.BeginningOfPeriod > request.EndOfPeriod)
            return ResultPattern<PagedResult<DailyExpenseResponse>>.Failure("The start period cannot be latter than the end period");

        if (request.Page < 1)
            return ResultPattern<PagedResult<DailyExpenseResponse>>.Failure("Page cannot be smaller than 1");

        var (items, totalItems) = await dailyExpensesRepository.GetDailyExpensesPaginated(request);

        if (items.Count <= 0)
            return ResultPattern<PagedResult<DailyExpenseResponse>>.Failure("No expense was found");

        var DailyExpenseDto = items.Select(x => new DailyExpenseResponse
        {
            DailyExpenseId = x.DailyExpenseId,
            ExpenseDate = x.ExpenseDate,
            ExpenseValue = x.Amount,
            Note = x.Note ?? "",
            CategoryName = x.TransactionCategory?.Name ?? "",
            UserId = x.UserId,
            User = x.User!
        }).ToList();

        var PaginatedResult = (DailyExpenseDto, totalItems).ToPagedResult(request.Page, request.QTY);

        return ResultPattern<PagedResult<DailyExpenseResponse>>.Success(PaginatedResult);
    }
    public async Task<ResultPattern<string>> UpdateDailyExpensesEntriesAsync(List<PutDailyExpensesRequest> request)
    {
        if (request.Count <= 0)
            return ResultPattern<string>.Failure("request is empty");

        var dailyExpenseModel = new List<DailyExpense>();

        foreach (var item in request)
        {
            var result = await dailyExpensesRepository.GetDailyExpensesById(item.DailyExpenseId);
            if (result == null)
                return ResultPattern<string>.Failure($"Expense of id: {item.DailyExpenseId} was not found");

            if(result.UserId != currentUser.UserId)
                return ResultPattern<string>.Failure($"User not allow to change this entry");

            result.ExpenseDate = item.ExpenseDate == DateOnly.MinValue ? result.ExpenseDate : item.ExpenseDate;
            result.Amount = item.ExpenseValue <= 0 ? result.Amount : item.ExpenseValue;
            result.Note = string.IsNullOrEmpty(item.Note) ? result.Note : item.Note;
            result.TransactionCategoryId = item.CategoryId <= 0 ? result.TransactionCategoryId : item.CategoryId;

            dailyExpenseModel.Add(result);
        }

        await GenericOperationsRepository.UpdateAsync(dailyExpenseModel);

        return ResultPattern<string>.Success("items updated!");
    }
    public async Task<ResultPattern<string>> DeleteDailyExpenseEntryByIdAsync(int id)
    {
        var entryForFakeDelete = await dailyExpensesRepository.GetDailyExpensesById(id);

        if (entryForFakeDelete == null)
            return ResultPattern<string>.Failure($"No expense of id: {id} was found");

        if (entryForFakeDelete.UserId != currentUser.UserId)
            return ResultPattern<string>.Failure($"User not allow to change this entry");

        entryForFakeDelete.IsDeleted = true;

        await GenericOperationsRepository.UpdateAsync(entryForFakeDelete);

        return ResultPattern<string>.Success("Item deleted!");
    }
}
