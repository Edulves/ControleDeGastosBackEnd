namespace ExpensesControl.DTOs.Requests.DataConsolidationRequests
{
    public class GetByFullDateOrMothAndYearRequest
    {
        public DateOnly BeginningOfPeriod { get; set; }
        public DateOnly EndOfPeriod { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
