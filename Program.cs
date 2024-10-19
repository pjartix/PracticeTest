//Scenario 1


using System;
using System.Collections.Generic;
using System.Linq;
class Person
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
}
class PersonImplementation
{
    public string GetName(IList<Person> person)
    {
        string result = "";
        foreach (var p in person)
        {
            result += p.Name + " " + p.Address + " ";
        }
        return result.Trim();

    }
    public double Average(IList<Person> person)
    {
        return person.Average(p => p.Age);

    }
    public int Max(IList<Person> person)
    {
        return person.Max(p => p.Age);
    }
}
class Source
{
    static void Main(string[] args)
    {
        var line1 = System.Console.ReadLine().Trim();
        var num = int.Parse(line1);
        IList<Person> p = new List<Person>();

        for (int i = 0; i < num; i++)
        {
            string name = Console.ReadLine();
            string address = Console.ReadLine();
            int age = Int32.Parse(Console.ReadLine());
            p.Add(new Person { Name = name, Address = address, Age = age });
        }
        PersonImplementation personImp = new PersonImplementation();
        Console.WriteLine(personImp.GetName(p));
        Console.WriteLine(personImp.Average(p));
        Console.WriteLine(personImp.Max(p));
    }
}



//Scenario 2

using System;
using System.Collections.Generic;

interface IBroadbandPlan
{
    int GetBroadbandPlanAmount();
}
class Black : IBroadbandPlan
{
    private readonly bool _isSubscriptionValid;
    private readonly int _discountPercentage;
    private const int PlanAmount = 3000;

    public Black(bool isSubscriptionValid, int discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 50.");
        }
        _isSubscriptionValid = isSubscriptionValid;
        _discountPercentage = discountPercentage;
    }

    public int GetBroadbandPlanAmount()
    {
        if (_isSubscriptionValid)
        {
            return PlanAmount - (PlanAmount * _discountPercentage / 100);
        }
        return PlanAmount;
    }
}

class Gold : IBroadbandPlan
{
    private readonly bool _isSubscriptionValid;
    private readonly int _discountPercentage;
    private const int PlanAmount = 1500;

    public Gold(bool isSubscriptionValid, int discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 30.");
        }
        _isSubscriptionValid = isSubscriptionValid;
        _discountPercentage = discountPercentage;
    }

    public int GetBroadbandPlanAmount()
    {
        if (_isSubscriptionValid)
        {
            return PlanAmount - (PlanAmount * _discountPercentage / 100);
        }
        return PlanAmount;
    }
}

class SubscribePlan
{
    private readonly IList<IBroadbandPlan> _broadbandPlans;

    public SubscribePlan(IList<IBroadbandPlan> broadbandPlans)
    {
        _broadbandPlans = broadbandPlans ?? throw new ArgumentNullException(nameof(broadbandPlans), "Broadband plans list cannot be null.");
    }

    public IList<Tuple<string, int>> GetSubscriptionPlan()
    {
        var subscriptionPlanList = new List<Tuple<string, int>>();

        foreach (var plan in _broadbandPlans)
        {
            string planType = plan.GetType().Name;
            int planAmount = plan.GetBroadbandPlanAmount();
            subscriptionPlanList.Add(new Tuple<string, int>(planType, planAmount));
        }

        return subscriptionPlanList;
    }
}

class Program21
{
    static void Main(string[] args)
    {
        var plans = new List<IBroadbandPlan>
        {
            new Black(true, 50),
            new Black(false, 10),
            new Gold(true, 30),
            new Black(true, 20),
            new Gold(false, 20)
        };

        var subscriptionPlans = new SubscribePlan(plans);
        var result = subscriptionPlans.GetSubscriptionPlan();

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Item1}, {item.Item2}");
        }
    }
}



//Scenario 3

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum CommodityCategory
{
    Furniture,
    Grocery,
    Service
}
class Commodity
{
    public Commodity(CommodityCategory category, string commodityName, int commodityQuantity, double commodityPrice)
    {
        Category = category;
        CommodityName = commodityName;
        CommodityQuantity = commodityQuantity;
        CommodityPrice = commodityPrice;
    }
    public CommodityCategory Category { get; set; }

    public string CommodityName { get; set; }
    public int CommodityQuantity { get; set; }
    public double CommodityPrice { get; set; }
}
class PrepareBill
{
    private readonly IDictionary<CommodityCategory, double> _taxRates;

    public PrepareBill()
    {
        _taxRates = new Dictionary<CommodityCategory, double>();
    }
    public void SetTaxRates(CommodityCategory category, double taxRate)
    {
        if (!_taxRates.ContainsKey(category))
        {
            _taxRates[category] = taxRate;
        }
    }
    public double CalculateBillAmount(IList<Commodity> items)
    {
        double totalAmount = 0;
        foreach (var item in items)
        {
            if (!_taxRates.ContainsKey(item.Category))
            {
                throw new ArgumentException($"Tax rate for {item.Category} is not defined.");
            }
            double taxRate = _taxRates[item.Category];
            double itemTotal = item.CommodityQuantity * item.CommodityPrice;
            double itemTotalWithTax = taxRate + itemTotal;
            totalAmount += itemTotalWithTax;
        }
        return totalAmount;
    }
}
class Source3
{
    static void Main(string[] args)
    {
        var Commodities = new List<Commodity>
        {
            new Commodity(CommodityCategory.Furniture, "Bed", 2, 5000),
            new Commodity(CommodityCategory.Grocery, "Flour", 5, 80),
            new Commodity(CommodityCategory.Service, "Insurance", 8, 8500),
        };
        var prepareBill = new PrepareBill();
        prepareBill.SetTaxRates(CommodityCategory.Furniture, 18);
        prepareBill.SetTaxRates(CommodityCategory.Grocery, 18);
        prepareBill.SetTaxRates(CommodityCategory.Service, 18);

        var billAmount = prepareBill.CalculateBillAmount(Commodities);
        Console.WriteLine($"{billAmount}");
    }
}



//scenario 4

using System;
using System.Collections.Generic;

public delegate bool IsEligibleforScholarship(Student student);
public class Student
{
    public int RollNo { get; set; }
    public string Name { get; set; }
    public int Marks { get; set; }
    public char SportsGrade { get; set; }

    public static string GetEligibleStudents(List<Student> studentsList, IsEligibleforScholarship isEligible)
    {
        List<string> eligibleStudents = new List<string>();

        foreach (Student student in studentsList)
        {
            if (isEligible(student))
            {
                eligibleStudents.Add(student.Name);
            }
        }

        return string.Join(", ", eligibleStudents);
    }
}

public class Program2
{
    public static bool ScholarshipEligibility(Student student)
    {
        return student.Marks > 80 && student.SportsGrade == 'A';
    }

    static void Main()
    {
        List<Student> lstStudents = new List<Student>
        {
            new Student { RollNo = 1, Name = "Raj", Marks = 75, SportsGrade = 'A' },
            new Student { RollNo = 2, Name = "Rahul", Marks = 82, SportsGrade = 'A' },
            new Student { RollNo = 3, Name = "Kiran", Marks = 89, SportsGrade = 'B' },
            new Student { RollNo = 4, Name = "Sunil", Marks = 86, SportsGrade = 'A' }
        };


        IsEligibleforScholarship eligibilityCheck = new IsEligibleforScholarship(ScholarshipEligibility);

        string eligibleStudents = Student.GetEligibleStudents(lstStudents, eligibilityCheck);

        Console.WriteLine(eligibleStudents);
    }
}



//Scenario 5


using System;
using System.Collections.Generic;

interface IBroadbandPlan
{
    int GetBroadbandPlanAmount();
}
class Black : IBroadbandPlan
{
    private readonly bool _isSubscriptionValid;
    private readonly int _discountPercentage;
    private const int PlanAmount = 3000;

    public Black(bool isSubscriptionValid, int discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 50.");
        }
        _isSubscriptionValid = isSubscriptionValid;
        _discountPercentage = discountPercentage;
    }

    public int GetBroadbandPlanAmount()
    {
        if (_isSubscriptionValid)
        {
            return PlanAmount - (PlanAmount * _discountPercentage / 100);
        }
        return PlanAmount;
    }
}

class Gold : IBroadbandPlan
{
    private readonly bool _isSubscriptionValid;
    private readonly int _discountPercentage;
    private const int PlanAmount = 1500;

    public Gold(bool isSubscriptionValid, int discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 30.");
        }
        _isSubscriptionValid = isSubscriptionValid;
        _discountPercentage = discountPercentage;
    }

    public int GetBroadbandPlanAmount()
    {
        if (_isSubscriptionValid)
        {
            return PlanAmount - (PlanAmount * _discountPercentage / 100);
        }
        return PlanAmount;
    }
}

class SubscribePlan
{
    private readonly IList<IBroadbandPlan> _broadbandPlans;

    public SubscribePlan(IList<IBroadbandPlan> broadbandPlans)
    {
        _broadbandPlans = broadbandPlans ?? throw new ArgumentNullException(nameof(broadbandPlans), "Broadband plans list cannot be null.");
    }

    public IList<Tuple<string, int>> GetSubscriptionPlan()
    {
        var subscriptionPlanList = new List<Tuple<string, int>>();

        foreach (var plan in _broadbandPlans)
        {
            string planType = plan.GetType().Name;
            int planAmount = plan.GetBroadbandPlanAmount();
            subscriptionPlanList.Add(new Tuple<string, int>(planType, planAmount));
        }

        return subscriptionPlanList;
    }
}

class Program21
{
    static void Main(string[] args)
    {
        var plans = new List<IBroadbandPlan>
        {
            new Black(true, 50),
            new Black(false, 10),
            new Gold(true, 30),
            new Black(true, 20),
            new Gold(false, 20)
        };

        var subscriptionPlans = new SubscribePlan(plans);
        var result = subscriptionPlans.GetSubscriptionPlan();

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Item1}, {item.Item2}");
        }
    }
}



