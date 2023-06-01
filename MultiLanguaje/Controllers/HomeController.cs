using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MultiLanguaje.Models;

namespace MultiLanguaje.Controllers;

public class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        ViewData["TextHello"] = _localizer["TextHello"];
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult SetCulture(string culture, string urlToReturn)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(5) }
        );

        return LocalRedirect(urlToReturn);
    }
}
