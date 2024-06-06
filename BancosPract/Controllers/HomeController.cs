using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BancosPract.Models;
using BancosPract.Entities;

namespace BancosPract.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;

        _context = context;
    
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BancoAdd()
    {
       Bancos banco= new Bancos();
        banco.Id = new Guid();
        banco.Name = "Cassandra";
        banco.Address ="Golfo de Mexico #160. colmitras norte";
        banco.NoCuenta=024123;
        banco.Cash=1230;
        this._context.Banco.Add(banco);
        this._context.SaveChanges();
 
        return View();
    } 

    public IActionResult BancoSave()
    {
        return Redirect("Privacy");
    }

    public IActionResult Privacy()
    {
    
         List<BancosModel> list = new List<BancosModel>();
         list = _context.Banco.Select(b=> new BancosModel()
         {
            Id = b.Id,
             Name = b.Name,
             NoCuenta =b.NoCuenta,
             Address = b.Address,
             Cash = b.Cash,

         }).ToList();
        return View(list);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
