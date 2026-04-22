using Microsoft.AspNetCore.Mvc;
using CyberStore.Core.Entities;
using CyberStore.Application.Services.Interfaces;
using CyberStore.Application.Common;

namespace CyberStore.Controllers;

public class HomeController(IProductService productService, IContactService contactService)
    : Controller
{
    public async Task<IActionResult> Index(string search = "", int categoryId = 0, string sortBy = "name", int page = 1)
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

        // Apply sorting
        products = sortBy switch
        {
            "price_asc" => products.OrderBy(p => p.Price),
            "price_desc" => products.OrderByDescending(p => p.Price),
            "newest" => products.OrderByDescending(p => p.Id),
            "name" => products.OrderBy(p => p.Name),
            _ => products.OrderBy(p => p.Name)
        };

        // Apply pagination
        const int pageSize = 12;
        var pagedResults = products.Paginate(page, pageSize);

        ViewData["SearchTerm"] = search;
        ViewData["SelectedCategory"] = categoryId;
        ViewData["SortBy"] = sortBy;
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = pagedResults.TotalPages;
        ViewData["TotalCount"] = pagedResults.TotalCount;

        return View(pagedResults.Items);
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
