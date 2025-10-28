namespace ApiCQRS.Domian.Models;

public class Order
{
    public int Id { get; set; }
    public  required string FirstName { get; set; }
    public  required string LastName { get; set; }
    public required string Status { get; set; }
    public  required DateTime CreateAt { get; set; }
    public  required decimal  TotalCost { get; set; }
}
