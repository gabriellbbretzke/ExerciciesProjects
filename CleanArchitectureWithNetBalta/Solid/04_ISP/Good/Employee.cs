namespace Solid._04_ISP.Good;

public interface ISalaaryCalculator
{
    void CalculateSalary();
}

public interface IBenefitsCalculator
{
    void CalculateBenefits();
}

public class FullTimeEmployee : ISalaaryCalculator, IBenefitsCalculator
{
    public void CalculateBenefits()
    {
        Console.WriteLine("Contract Benefits");
    }

    public void CalculateSalary()
    {
        Console.WriteLine("Contract Salary");
    }
}

public class ContractEmployee : ISalaaryCalculator
{
    public void CalculateSalary()
    {
        Console.WriteLine("Contract Salary");
    }
}

