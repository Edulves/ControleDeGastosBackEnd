namespace ExpensesControl.DTOs.Requests.DataConsolidationRequests
{
    public class GetByFullDateOrMothAndYearRequest
    {
        public DateTime BeginningOfPeriod { get; set; }
        public DateTime EndOfPeriod { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
