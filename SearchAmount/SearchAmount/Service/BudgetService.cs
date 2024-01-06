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
        var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
        int diff = 0;
        decimal resultAmount = 0;
        for (DateTime walk = start; walk <= end; walk=walk.AddDays(1))
        {
            //GetStartAmount(result, startDate).AmountPerDay* 
            resultAmount +=GetStartAmount(result, $"{walk:yyyyMM}").AmountPerDay;

        }
        return resultAmount;
        //      if ((diff = (end - start).Days + 1) > 1 && diff != daysInMonth)
        //{
        //	return GetStartAmount(result, startDate).AmountPerDay * diff;

        //}

        //if (start.CompareTo(end) == 0) 
        //      {
        //	return GetStartAmount(result, startDate).AmountPerDay;
        //      }

        //      if (startDate == endDate)
        //      {
        //          return GetStartAmount(result, startDate).Amount;
        //}

        //      return 0;
    }

    private static Budget GetStartAmount(List<Budget> result, string startDate)
    {
        return result.Where(i => i.YearMonth.Equals(startDate)).FirstOrDefault();
    }

    private static Budget GetEndAmount(List<Budget> result, string endDate)
    {
        return result.Where(i => i.YearMonth.Equals(endDate)).FirstOrDefault();
    }
}