using Microsoft.AspNetCore.Mvc;
using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Controllers;

public class HomeController : Controller
{
    private readonly ChatContext _context;

    public HomeController(ChatContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var messages = _context.Messages
            .OrderByDescending(m => m.Timestamp)
            .Take(50)
            .OrderBy(m => m.Timestamp)
            .ToList();
        return View(messages);
    }
}