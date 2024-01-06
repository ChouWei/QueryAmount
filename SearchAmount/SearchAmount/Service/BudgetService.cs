using SearchAmount.Repository;

namespace SearchAmount.Service;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        var result = _budgetRepo.GetAll();
        var startDate = start.ToString("yyyyMM");
        var endDate = end.ToString("yyyyMM");

        if (start.CompareTo(end).Equals(0))
        {
            var oneMonthAmount = result.Where(i => i.YearMonth.Equals(startDate)).Select(i => i.Amount)
                .FirstOrDefault();
            var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
            return oneMonthAmount / daysInMonth;
        }

        if (startDate.Equals(endDate))
        {
            return result.Where(i => i.YearMonth.Equals(startDate)).Select(i => i.Amount)
                .FirstOrDefault();
        }

        return 0;
    }
}