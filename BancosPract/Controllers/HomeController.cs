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
     [HttpGet]
    public IActionResult BancoAdd()
    {
        return View();
    }


    [HttpPost]  
    public IActionResult BancoAdd(BancosModel model)
    {

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        Bancos banco= new Bancos();
        banco.Id = new Guid();
        banco.Name = model.Name;
        banco.Address = model.Address;
        banco.NoCuenta=model.NoCuenta;
        banco.Cash=model.Cash;
        this._context.Banco.Add(banco);
        this._context.SaveChanges();
 
        return RedirectToAction("Privacy","Home");
    } 
    [HttpGet]
      public IActionResult BancoEdit(Guid Id)
      {
        Bancos bancoActualizar = this._context.Banco
        .Where(b => b.Id == Id)
        .First();

          //si no lo encuentra
        if(bancoActualizar == null)
        {
            return RedirectToAction("Privacy","Home");
        }
         //asignar modelo a base
        BancosModel model = new BancosModel();
        model.Id=bancoActualizar.Id;
        model.Name=bancoActualizar.Name;
        model.Address=bancoActualizar.Address;
        model.NoCuenta=bancoActualizar.NoCuenta;
        model.Cash=bancoActualizar.Cash;
        return View(model);
      }



    [HttpPost]
     public IActionResult BancoEdit(BancosModel bancosM)
     {
        Bancos BancosEntity = this._context.Banco
        .Where(b => b.Id == bancosM.Id).First();

        if(BancosEntity == null)
        {
            return RedirectToAction("bancosM");
        }
        BancosEntity.Name=bancosM.Name;
        BancosEntity.Address=bancosM.Address;
        BancosEntity.NoCuenta=bancosM.NoCuenta;
        BancosEntity.Cash=bancosM.Cash;

        this._context.Banco.Update(BancosEntity);
        this._context.SaveChanges();
       
       return RedirectToAction("Privacy","Home");

     }

    public IActionResult BancoSave()
    {
        return View();
    }
    [HttpGet]
     public IActionResult BancoDelete(Guid Id)
     {
        Bancos bancoActualizar = this._context.Banco
        .Where(b => b.Id == Id)
        .First();

          //si no lo encuentra
        if(bancoActualizar == null)
        {
            return RedirectToAction("Privacy","Home");
        }
         //asignar modelo a base
        BancosModel model = new BancosModel();
        model.Id=bancoActualizar.Id;
        model.Name=bancoActualizar.Name;
        model.Address=bancoActualizar.Address;
        model.NoCuenta=bancoActualizar.NoCuenta;
        model.Cash=bancoActualizar.Cash;
        return View(model);

        
     }
     [HttpPost]
     public IActionResult BancoDelete( BancosModel bancosModel)
     {
        bool exits = this._context.Banco.Any(b => b.Id==bancosModel.Id);
        if(!exits)
        {
            return View (bancosModel);
        }
    
        Bancos BancosEntity = this._context.Banco
        .Where(b => b.Id == bancosModel.Id).First();
        BancosEntity.Name=bancosModel.Name;
        BancosEntity.Address=bancosModel.Address;
        BancosEntity.NoCuenta=bancosModel.NoCuenta;
        BancosEntity.Cash=bancosModel.Cash;
        
        this._context.Banco.Remove(BancosEntity);
        this._context.SaveChanges();

        return RedirectToAction("Privacy","Home");

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
