using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
 

    private readonly LastFmService _lastFmService;

    public HomeController()
    {
        _lastFmService = new LastFmService();
    }

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Index(string artistName)
    {
        var results = await _lastFmService.SearchArtistsAsync(artistName);
        return View(results);
    }
}
