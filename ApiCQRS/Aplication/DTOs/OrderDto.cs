namespace ApiCQRS.Aplication.DTOs;

public record OrderDto
(
    int Id,
    string FirstName,
    string LastName,
    string Status,
    DateTime CreateAt,
    decimal TotalCost
);
