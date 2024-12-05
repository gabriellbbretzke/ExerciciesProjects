namespace Solid._02_OCP.Bad;

public enum EPRoductType
{
    Eletronics = 1,
    Health = 2,
    Beauty = 3,
    Fashion = 4
}

public class Discount
{
    public decimal Calculate(EPRoductType type, decimal price)
    {
        if (type == EPRoductType.Eletronics)
            return price * 0.2M;
        
        if (type == EPRoductType.Health)
            return price * 0.2M;
        
        if (type == EPRoductType.Beauty)
            return price * 0.2M;
        
        if (type == EPRoductType.Fashion)
            return price * 0.2M;

        return price;
    }
}
