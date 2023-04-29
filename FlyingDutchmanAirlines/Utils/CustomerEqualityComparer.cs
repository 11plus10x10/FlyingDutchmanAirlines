using System.Security.Cryptography;
using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.Utils;

/// <summary>
/// Class for check congruity between the <typeparamref name="Customer"/> instance in
/// the database and the <typeparamref name="Customer"/> instance ?
/// </summary>
internal class CustomerEqualityComparer : EqualityComparer<Customer>
{
    public override bool Equals(Customer? x, Customer? y)
    {
        if (x is null || y is null) return false;
        return x.CustomerId == y.CustomerId && x.Name == y.Name;
    }

    public override int GetHashCode(Customer obj)
    {
        var randomNumber = RandomNumberGenerator.GetInt32(int.MaxValue / 2);
        return (obj.CustomerId + obj.Name.Length + randomNumber).GetHashCode();
    }
}
