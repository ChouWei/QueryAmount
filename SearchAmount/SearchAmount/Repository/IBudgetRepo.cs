namespace SearchAmount.Repository;

public interface IBudgetRepo
{
    List<Budget> GetAll();
}

public class Budget
{
     public int Amount { get; set; }
     
     public string YearMonth { get; set; }
}