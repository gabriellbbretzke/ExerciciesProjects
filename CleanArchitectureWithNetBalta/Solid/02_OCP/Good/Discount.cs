namespace Solid._02_OCP.Good;

public enum EPRoductType
{
    Eletronics = 1,
    Health = 2,
    Beauty = 3,
    Fashion = 4
}

public abstract class Discount
{
    public abstract Decimal Calculate(decimal price);
}

public class EletronicsDiscount : Discount
{
    public override decimal Calculate(decimal price) => price * 0.05m;
}

public class FashionDiscount : Discount
{
    public override decimal Calculate(decimal price) => price * 0.05m;
}

public class BeautyDiscount : Discount
{
    public override decimal Calculate(decimal price) => price * 0.05m;
}

public class HealthDiscount : Discount
{
    public override decimal Calculate(decimal price) => price * 0.05m;
}
