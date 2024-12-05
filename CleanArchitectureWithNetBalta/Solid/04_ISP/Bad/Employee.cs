namespace Solid._04_ISP.Bad;

public interface IEmployee
{
    string Name { get; set; }
    void CalculateSalary();
    void CalculateBenefits();
}

public class FulTimeEmployee : IEmployee
{
    public string Name { get; set ; }

    public void CalculateBenefits()
    {
        Console.WriteLine("Contract Salary");
    }

    public void CalculateSalary()
    {
        Console.WriteLine("Contract Benefits");
    }
}

public class ContractEmployee : IEmployee
{
    public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void CalculateBenefits()
    {
        Console.WriteLine("Contract Salary");
    }

    public void CalculateSalary()
    {
        throw new NotImplementedException();
    }
}