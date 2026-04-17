using Microsoft.AspNetCore.Mvc;
using CyberStore.Core.Entities;
using CyberStore.Application.Services.Interfaces;
namespace CyberStore.Controllers;
public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IContactService _contactService;
    public HomeController(IProductService productService, IContactService contactService)
    {
        _productService = productService;
        _contactService = contactService;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
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
            await _contactService.SubmitMessageAsync(model);
            TempData["SuccessMessage"] = "Your message has been sent successfully.";
            return RedirectToAction(nameof(Contact));
        }
        return View("Contact", model);
    }
}
