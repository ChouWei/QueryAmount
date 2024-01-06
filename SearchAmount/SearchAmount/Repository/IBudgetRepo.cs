namespace SearchAmount.Repository;

public interface IBudgetRepo
{
    List<Budget> GetAll();
}

public class Budget
{
    public int Amount { get; set; }

    public string YearMonth { get; set; }

    public int AmountPerDay => string.IsNullOrEmpty(YearMonth) ? 0 : Amount / DateTime.DaysInMonth(int.Parse(YearMonth[..4]), int.Parse(YearMonth[4..]));
}