using Microsoft.AspNetCore.Mvc;
using CyberStore.Core.Entities;
using CyberStore.Application.Services.Interfaces;
namespace CyberStore.Controllers;
public class HomeController(IProductService productService, IContactService contactService)
    : Controller
{
    public async Task<IActionResult> Index(string search = "", int categoryId = 0)
    {
        IEnumerable<Product> products;

        if (!string.IsNullOrWhiteSpace(search))
        {
            products = await productService.SearchProductsAsync(search);
        }
        else if (categoryId > 0)
        {
            products = await productService.GetProductsByCategoryAsync(categoryId);
        }
        else
        {
            products = await productService.GetAllProductsAsync();
        }

        ViewData["SearchTerm"] = search;
        ViewData["SelectedCategory"] = categoryId;
        return View(products);
    }
    public IActionResult Contact()
    {
        return View(new ContactMessage());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitContact(ContactMessage model)
    {
        if (ModelState.IsValid)
        {
            await contactService.SubmitMessageAsync(model);
            TempData["SuccessMessage"] = "Your message has been sent successfully.";
            return RedirectToAction(nameof(Contact));
        }
        return View("Contact", model);
    }
}
