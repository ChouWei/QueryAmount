using NSubstitute;
using SearchAmount.Repository;
using SearchAmount.Service;

namespace SearchAmountTest;

public class Tests
{
    IBudgetRepo _budgetRepo;
    private BudgetService _budgetService;

    [SetUp]
    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public void OneFullMonthSearch()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 31);
        var expect = 310m;
        _budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            }
        });
        var act = _budgetService.Query(start, end);
        Assert.That(act, Is.EqualTo(expect));
    }

    [Test]
    public void OneDaySearch()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 1);
        var expect = 20m;
        _budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 620
            }
        });
        var act = _budgetService.Query(start, end);
        Assert.That(act, Is.EqualTo(expect));
    }
}