using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.CategoriesRequests;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.DataConsolidationRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.DTOs.Responses.DailyExpensesReponses;
using ExpensesControl.DTOs.Responses.DataConsolidationResponses;
using ExpensesControl.Models;
using ExpensesControl.Repositories.InterfaceRepositories;
using ExpensesControl.Repositories.RepositoriesInterface;
using ExpensesControl.Service.ServiceInterfaces;

namespace ExpensesControl.Service.ServiceImplementations;

public class ExpensesControlService(IExpensesControlRepository expensesControlRepository, IGenericOperationsRepository GenericOperationsRepository) : IExpensesControlService
{
    #region DailyExpenses
    public async Task<ResultPattern<string>> CreateDailyExpensesEntriesAsync(List<DailyExpenseEntryRequest> request)
    {
        if (request.Count <= 0)
            return ResultPattern<string>.Failure("request is empty");

        var dailyExpenseModel = request.Select(x => new DailyExpense
        {
            InputDate = x.InputDate,
            ExpenseValue = x.ExpenseValue,
            Note = x.Note,
            CategoryId = x.CategoryId,
            Deleted = "",
        }).ToList();

        await GenericOperationsRepository.CreateAsync(dailyExpenseModel);

        return ResultPattern<string>.Success("Expense was registered!");
    }
    public async Task<ResultPattern<PagedResult<DailyExpenseResponse>>> GetDailyExpensesAsync(GetDailyExpensesRequest request)
    {
        if(request.BeginningOfPeriod > request.EndOfPeriod)
            return ResultPattern<PagedResult<DailyExpenseResponse>>.Failure("The start period cannot be latter than the end period");

        if (request.Page < 1)
            return ResultPattern<PagedResult<DailyExpenseResponse>>.Failure("Page cannot be smaller than 1");

        var result = await expensesControlRepository.GetDailyExpensesPaginated(request);

        if (result.items.Count <= 0)
            return ResultPattern<PagedResult<DailyExpenseResponse>>.Failure("No expense was found");

        var DailyExpenseDto = result.items.Select(x => new DailyExpenseResponse
        {
            DailyExpenseId = x.DailyExpensesId,
            InputDate = x.InputDate,
            ExpenseValue = x.ExpenseValue,
            Note = x.Note ?? "",
            CategoryName = x.Category?.CategoryName ?? "",
        }).ToList();

        var PaginatedResult = (DailyExpenseDto, result.totalItems).ToPagedResult(request.Page, request.QTY);

        return ResultPattern<PagedResult<DailyExpenseResponse>>.Success(PaginatedResult);
    }
    public async Task<ResultPattern<string>> UpdateDailyExpensesEntriesAsync(List<PutDailyExpensesRequest> request)
    {
        if (request.Count <= 0)
            return ResultPattern<string>.Failure("request is empty");

        var dailyExpenseModel = new List<DailyExpense>();

        foreach (var item in request)
        {
            var result = await expensesControlRepository.GetDailyExpensesById(item.DailyExpenseId);
            if (result == null)
                return ResultPattern<string>.Failure($"Expense of id: {item.DailyExpenseId} was not found");

            result.InputDate = item.InputDate == DateTime.MinValue ? result.InputDate : item.InputDate;
            result.ExpenseValue = item.ExpenseValue <= 0 ? result.ExpenseValue : item.ExpenseValue;
            result.Note = string.IsNullOrEmpty(item.Note) ? result.Note : item.Note;
            result.CategoryId = item.CategoryId <= 0 ? result.CategoryId : item.CategoryId;

            dailyExpenseModel.Add(result);
        }

        await GenericOperationsRepository.UpdateAsync(dailyExpenseModel);

        return ResultPattern<string>.Success("items updated!");
    }
    public async Task<ResultPattern<string>> DeleteDailyExpenseEntryByIdAsync(int id)
    {
        var entryForFakeDelete = await expensesControlRepository.GetDailyExpensesById(id);

        if(entryForFakeDelete == null)
            return ResultPattern<string>.Failure($"No expense of id: {id} was found");

        entryForFakeDelete.Deleted = "*";

        await GenericOperationsRepository.UpdateAsync(entryForFakeDelete);

        return ResultPattern<string>.Success("Item deleted!");
    }
    #endregion

    #region ExpenseCategories
    public async Task<ResultPattern<List<TransactionCategory>>> GetEntryCategoriesAsync()
    {
        var result = await expensesControlRepository.GetTransactionCategories();

        return ResultPattern<List<TransactionCategory>>.Success(result);
    }
    public async Task<ResultPattern<string>> CreateCategoriesAsync(List<CreateCategoryRequest> request)
    {
        var modelTransactionCategory = request.Select(x => new TransactionCategory()
        {
            CategoryName = x.CategoryName.ToLower(),
        }).ToList();

        await GenericOperationsRepository.CreateAsync(modelTransactionCategory);

        return ResultPattern<string>.Success($"Categories created!");
    }
    public async Task<ResultPattern<string>> PutCategoriesAsync(List<TransactionCategory> request)
    {
        foreach (var item in request)
        {
            item.CategoryName = item.CategoryName.ToLower();
        }
        
        await GenericOperationsRepository.UpdateAsync(request);

        return ResultPattern<string>.Success($"Categories updated!");
    }
    public async Task<ResultPattern<string>> DeleteCategoryByIdAsync(int id)
    {
        var result = await expensesControlRepository.GetTransactionCategoryById(id);

        if (result == null)
            return ResultPattern<string>.Failure($"No category found with id: {id}");

        result.Deleted = "*";

        await GenericOperationsRepository.UpdateAsync(result);

        return ResultPattern<string>.Success($"Category deleted!");
    }
    #endregion

    #region FixedExpenses
    public async Task<ResultPattern<PagedResult<FixedExpense>>> GetFixedExpensesAsync(GetFixedExpensesRequest request)
    {
        if (request.BeginningOfPeriod > request.EndOfPeriod)
            return ResultPattern<PagedResult<FixedExpense>>.Failure("The start period cannot be latter than the end period");

        if (request.Page < 1)
            return ResultPattern<PagedResult<FixedExpense>>.Failure("Page cannot be smaller than 1");

        var (items, totalItems) = await expensesControlRepository.GetFixedExpenses(request);

        var respostaPaginada = (items, totalItems).ToPagedResult(request.Page, request.QTY);

        return ResultPattern<PagedResult<FixedExpense>>.Success(respostaPaginada);
    }
    public async Task<ResultPattern<string>> PostFixedExpenseAsync(List<PostFixedExpensesDto> request)
    {

        var fixedExpenseModel = request.Select(x => new FixedExpense
        {
            FixedExpenseDescription = x.FixedExpenseDescription,
            FixedExpenseValue = x.FixedExpenseValue,
            InputDate = x.InputDate
        }).ToList();

        foreach (var item in fixedExpenseModel)
        {
            if (item.InputDate == DateTime.MinValue)
                return ResultPattern<string>.Failure($"Invalid date {item.InputDate}", "Invalid date");
        }

        await GenericOperationsRepository.CreateAsync(fixedExpenseModel);

        return ResultPattern<string>.Success("Fixed expenses created!", StatusCodes.Status201Created);
    }
    public async Task<ResultPattern<string>> PutFixedExpensesAsync(List<PutFixedExpensesRequest> request)
    {
        if(request.Count <= 0)
            return ResultPattern<string>.Failure($"request is empty");

        var FixedExpenseModel = new List<FixedExpense>();

        foreach (var item in request)
        {
            var result = await expensesControlRepository.GetFixedExpensesById(item.FixedExpensesId);
            if (result == null){
                FixedExpenseModel.Add(new FixedExpense()
                {
                    FixedExpenseDescription = item.FixedExpenseDescription,
                    FixedExpenseValue = item.FixedExpenseValue,
                    InputDate = item.InputDate
                });

                continue;
            }

            result.FixedExpenseDescription = string.IsNullOrEmpty(item.FixedExpenseDescription) ? result.FixedExpenseDescription : item.FixedExpenseDescription;
            result.FixedExpenseValue = item.FixedExpenseValue <= 0 ? result.FixedExpenseValue : item.FixedExpenseValue;
            result.Paid = item.Paid;
            result.InputDate = item.InputDate == DateTime.MinValue ? result.InputDate : item.InputDate;

            FixedExpenseModel.Add(result);
        }

        await GenericOperationsRepository.UpdateAsync(FixedExpenseModel);

        return ResultPattern<string>.Success("Fixed expenses updated!");
    }
    public async Task<ResultPattern<string>> DeleteFixedExpensesAsync(int id)
    {
        var result = await expensesControlRepository.GetFixedExpensesById(id);

        if (result == null)
            return ResultPattern<string>.Failure($"Not entry for id: {id}");

        result.Deleted = "*";

        await GenericOperationsRepository.UpdateAsync(result);

        return ResultPattern<string>.Success("Item deleted!");
    }
    #endregion

    #region Consolidation
    public async Task<ResultPattern<DailyExpensesPerCategoryResult>> GetExpensesSumPerCategoryAsync(GetByFullDateOrMothAndYearRequest request)
    {
        var filtro = new GetDailyExpensesRequest()
        {
            BeginningOfPeriod = request.BeginningOfPeriod,
            EndOfPeriod = request.EndOfPeriod,
            Year = request.Year,
            Month = request.Month
        };

        var result = await expensesControlRepository.GetListDailyExpenses(filtro);
        
        if(result.Count <= 0)
            return ResultPattern<DailyExpensesPerCategoryResult>.Failure("No expense found with the current filter");

        var groupedResult = result.GroupBy(x => x.CategoryId);

        var GastosPorCategoria = groupedResult.Select(x => new GetDailyExpensesByCategoryReponse()
        {
            CategoryName = x.FirstOrDefault()?.Category?.CategoryName ?? "No category",
            ExpenseValue = x.Sum(x => x.ExpenseValue),
        }).OrderByDescending(x => x.ExpenseValue).ToList();

        var dailyExpensesResult = new DailyExpensesPerCategoryResult();
        dailyExpensesResult.DailyExpensesByCategoryList.AddRange(GastosPorCategoria);
        dailyExpensesResult.Total = dailyExpensesResult.DailyExpensesByCategoryList.Sum(x => x.ExpenseValue);

        return ResultPattern<DailyExpensesPerCategoryResult>.Success(dailyExpensesResult);
    }

    public async Task<ResultPattern<DailyExpensesConsolidationResult>> GetExpensesSumPerDayAsync(ExpensesByMothAndYearRequest request)
    {
        var filter = new GetDailyExpensesRequest() {
            Year = request.Year,
            Month = request.Month
        };

        var result = await expensesControlRepository.GetListDailyExpenses(filter);

        if (result.Count <= 0)
            return ResultPattern<DailyExpensesConsolidationResult>.Failure("No daily expense found");

        var groupedResult = result.GroupBy(x => x.InputDate.Date);
       
        var expensesByCategory = groupedResult.Select(x => new GetDailyExpensesByDayResponse()
        {
            InputDate =  x.Key,
            ExpenseValuePerDay = x.Sum(x => x.ExpenseValue)
        }).OrderBy(x => x.InputDate).ToList();

        var DailyExpensesResult = new DailyExpensesConsolidationResult();
        DailyExpensesResult.DailyExpensesList.AddRange(expensesByCategory);
        DailyExpensesResult.Total = DailyExpensesResult.DailyExpensesList.Sum(x => x.ExpenseValuePerDay);

        return ResultPattern<DailyExpensesConsolidationResult>.Success(DailyExpensesResult);
    }

    public async Task<ResultPattern<TotalFixedExpensesComparasionResponse>> GetTotalFixedExpensesComparasionAsync(ExpensesByMothAndYearRequest request)
    {
        var filter = new GetFixedExpensesRequest()
        {
            Year = request.Year,
            Month = request.Month,
        };

        var consultaGastosFixos = await expensesControlRepository.GetFixedExpensesList(filter);

        if (consultaGastosFixos.Count <= 0)
            return ResultPattern<TotalFixedExpensesComparasionResponse>.Failure("No fixed expense found");

        var resposta = new TotalFixedExpensesComparasionResponse()
        {
            PaidValue = consultaGastosFixos.Where(x => x.Paid).Sum(x => x.FixedExpenseValue),
            NotPaidValue = consultaGastosFixos.Where(x => !x.Paid).Sum(x => x.FixedExpenseValue),
        };

        return ResultPattern<TotalFixedExpensesComparasionResponse>.Success(resposta);
    }

    public async Task<ResultPattern<TotalExpensesResponse>> GetTotalDailyExpensesAsync(ExpensesByMothAndYearRequest request)
    {
        var filter = new GetDailyExpensesRequest()
        {
            Year = request.Year,
            Month = request.Month, 
        };

        var dailyExpensesSum = await expensesControlRepository.GetDailyExpensesSum(filter);

        var totalExpnesesResponse = new TotalExpensesResponse() { TotalExpenses = dailyExpensesSum };

        return ResultPattern<TotalExpensesResponse>.Success(totalExpnesesResponse);
    }
    #endregion
}
