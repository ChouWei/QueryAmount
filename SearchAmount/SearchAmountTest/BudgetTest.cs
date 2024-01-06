using NSubstitute;
using SearchAmount.Repository;
using SearchAmount.Service;

namespace SearchAmountTest;

public class BudgetTest
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

	[Test]
	public void ByDaySearch()
	{
		var start = new DateTime(2024, 1, 1);
		var end = new DateTime(2024, 1, 5);
		var expect = 500m;
		_budgetRepo.GetAll().Returns(new List<Budget>()
		{
			new Budget()
			{
				YearMonth = "202401",
				Amount = 3100
			}
		});
		var act = _budgetService.Query(start, end);
		Assert.That(act, Is.EqualTo(expect));
	}

	[Test]
	public void CrossMonthByDaySearch()
	{
		var start = new DateTime(2024, 3, 30);
		var end = new DateTime(2024, 5, 3);
		var expect = 920m;
		_budgetRepo.GetAll().Returns(new List<Budget>()
		{
			new Budget()
			{
				YearMonth = "202403",
				Amount = 310
			},
			new Budget()
			{
				YearMonth = "202404",
				Amount = 600
			},
			new Budget()
			{
				YearMonth = "202405",
				Amount = 3100
			}
		});
		var act = _budgetService.Query(start, end);
		Assert.That(act, Is.EqualTo(expect));
	}

	[Test]
	public void InvalidDate()
	{
		var start = new DateTime(2024, 10, 1);
		var end = new DateTime(2024, 5, 5);
		var expect = 0m;
		_budgetRepo.GetAll().Returns(new List<Budget>()
		{
			new Budget()
			{
				YearMonth = "202401",
				Amount = 3100
			}
		});
		var act = _budgetService.Query(start, end);
		Assert.That(act, Is.EqualTo(expect));
	}
	
	[Test]
	public void SearchWithoutData()
	{
		var start = new DateTime(2024, 5, 1);
		var end = new DateTime(2024, 5, 5);
		var expect = 0m;
		_budgetRepo.GetAll().Returns(new List<Budget>()
		{
			new Budget()
			{
				YearMonth = "202401",
				Amount = 3100
			}
		});
		var act = _budgetService.Query(start, end);
		Assert.That(act, Is.EqualTo(expect));
	}
}