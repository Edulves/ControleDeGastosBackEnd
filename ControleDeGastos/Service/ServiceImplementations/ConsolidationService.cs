using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.DataConsolidationRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.DTOs.Responses.DataConsolidationResponses;
using ExpensesControl.Repositories.InterfaceRepositories;
using ExpensesControl.Service.ServiceInterfaces;

namespace ExpensesControl.Service.ServiceImplementations;

public class ConsolidationService(IExpensesControlRepository expensesControlRepository) : IConsolidationService
{
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

        if (result.Count <= 0)
            return ResultPattern<DailyExpensesPerCategoryResult>.Failure("No expense found with the current filter");

        var groupedResult = result.GroupBy(x => x.TransactionCategoryId);

        var GastosPorCategoria = groupedResult.Select(x => new GetDailyExpensesByCategoryReponse()
        {
            CategoryName = x.FirstOrDefault()?.TransactionCategory?.Name ?? "No category",
            ExpenseValue = x.Sum(x => x.Amount),
        }).OrderByDescending(x => x.ExpenseValue).ToList();

        var dailyExpensesResult = new DailyExpensesPerCategoryResult();
        dailyExpensesResult.DailyExpensesByCategoryList.AddRange(GastosPorCategoria);
        dailyExpensesResult.Total = dailyExpensesResult.DailyExpensesByCategoryList.Sum(x => x.ExpenseValue);

        return ResultPattern<DailyExpensesPerCategoryResult>.Success(dailyExpensesResult);
    }

    public async Task<ResultPattern<DailyExpensesConsolidationResult>> GetExpensesSumPerDayAsync(ExpensesByMothAndYearRequest request)
    {
        var filter = new GetDailyExpensesRequest()
        {
            Year = request.Year,
            Month = request.Month
        };

        var result = await expensesControlRepository.GetListDailyExpenses(filter);

        if (result.Count <= 0)
            return ResultPattern<DailyExpensesConsolidationResult>.Failure("No daily expense found");

        var groupedResult = result.GroupBy(x => x.CreatedAt.Date);

        var expensesByCategory = groupedResult.Select(x => new GetDailyExpensesByDayResponse()
        {
            InputDate = x.Key,
            ExpenseValuePerDay = x.Sum(x => x.Amount)
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
            PaidValue = consultaGastosFixos.Where(x => x.IsPaid).Sum(x => x.Amount),
            NotPaidValue = consultaGastosFixos.Where(x => !x.IsPaid).Sum(x => x.Amount),
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
}
