using ApiCQRS.Domian.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCQRS.Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
}
