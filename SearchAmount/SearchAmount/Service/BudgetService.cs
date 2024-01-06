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
        var diff = 0;
        decimal resultAmount = 0;
        for (DateTime walk = start; walk <= end; walk=walk.AddDays(1))
        {
            //GetBudget(result, startDate).AmountPerDay* 
            resultAmount += GetBudget(result, $"{walk:yyyyMM}").AmountPerDay;

        }
        return resultAmount;
        //      if ((diff = (end - start).Days + 1) > 1 && diff != daysInMonth)
        //{
        //	return GetBudget(result, startDate).AmountPerDay * diff;

        //}

        //if (start.CompareTo(end) == 0) 
        //      {
        //	return GetBudget(result, startDate).AmountPerDay;
        //      }

        //      if (startDate == endDate)
        //      {
        //          return GetBudget(result, startDate).Amount;
        //}

        //      return 0;
    }

    private static Budget GetBudget(List<Budget> result, string startDate)
    {
        return result.Where(i => i.YearMonth.Equals(startDate)).FirstOrDefault() ?? new Budget();
    }
    
}