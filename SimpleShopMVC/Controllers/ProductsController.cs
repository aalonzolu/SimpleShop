using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleShopMVC.Models;
using SimpleShopMVC.Data;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public ProductsController()
    {
        _context = new ApplicationDbContext();
    }
    
    public async Task<IActionResult> Index()
    {
        List<Product> products = await _context.Products.ToListAsync();
        return View(products);
    }
    
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ImageUrl")] Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            Console.WriteLine("Not Valid");
            Console.WriteLine(ModelState.ValidationState);
            return View(product);
        }
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Remove(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        // Remove the product from the context
        _context.Products.Remove(product);
        // Save the changes
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }
}