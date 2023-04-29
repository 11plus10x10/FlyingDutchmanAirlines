using FlyingDutchmanAirlines.Utils;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public partial class Customer
{
    private static readonly CustomerEqualityComparer Comparer = new();

    public Customer(string name)
    {
        Name = name;
    }

    public static bool operator ==(Customer x, Customer y) => Comparer.Equals(x, y);
    public static bool operator !=(Customer x, Customer y) => !(x == y);
}
