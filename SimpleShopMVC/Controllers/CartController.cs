using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleShopMVC.Models;
using SimpleShopMVC.Data;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context = new();

    public async Task<IActionResult> Index()
    {
        List<CartItem> cartItems = await _context.CartItems.Include(x => x.Product).ToListAsync();
        return View(cartItems);
    }
    
    public IActionResult AddToCart(int id)
    {
        Product product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        CartItem cartItem = new CartItem
        {
            Product = product,
            Quantity = 1
        };
        
        CartItem existingCartItem = _context.CartItems.FirstOrDefault(ci => ci.Product.Id == id);
        if (existingCartItem != null)
        {
            existingCartItem.Quantity++;
            _context.Update(existingCartItem);
        }
        else
        {
            _context.CartItems.Add(cartItem);
        }
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult RemoveFromCart(int id)
    {
        CartItem cartItem = _context.CartItems.Find(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        _context.CartItems.Remove(cartItem);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}