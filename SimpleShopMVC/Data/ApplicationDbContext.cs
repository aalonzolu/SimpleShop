namespace SimpleShopMVC.Data;

using Microsoft.EntityFrameworkCore;
using SimpleShopMVC.Models;



public class ApplicationDbContext : DbContext
{ 
    public DbSet<Product>? Products { get; set; } 
    public DbSet<CartItem>? CartItems { get; set; }
    
    private readonly string _connectionString = "server=" + Environment.GetEnvironmentVariable("MYSQL_HOST") + ";"
                                               + "user=" + Environment.GetEnvironmentVariable("MYSQL_USER") + ";"
                                               + "password=" + Environment.GetEnvironmentVariable("MYSQL_PASSWORD") + ";"
                                               + "database=" + Environment.GetEnvironmentVariable("MYSQL_DATABASE") + ";";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine("MYSQL:");
        System.Console.WriteLine(_connectionString);
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }
}