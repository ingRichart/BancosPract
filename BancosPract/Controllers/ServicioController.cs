using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BancosPract.Models;
using BancosPract.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BancosPract.Controllers;

public class ServicioController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public ServicioController(ILogger<ServicioController> logger, ApplicationDbContext context)
    {
        //_logger = logger;

        _context = context;
    
    }

      public ActionResult ServicioList()
      {
        List<ServiciosModel> list = new List<ServiciosModel>();
        list = _context.Servicios.Select(b=> new ServiciosModel()
         {
           Id=b.Id,
           NombreS=b.NombreS,
           Tipo=b.Tipo,
           NoCuentaS=b.NoCuentaS,
           Costo=b.Costo,


         }).ToList();
        return View(list);
      }
     [HttpGet]
      public IActionResult ServicioAdd()
    {
        return View();
    }
     [HttpPost]
     public IActionResult ServicioAdd(ServiciosModel model)
    {

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        Servicio servicio= new Servicio();
        servicio.Id = new Guid();
        servicio.NombreS=model.NombreS;
        servicio.Tipo=model.Tipo;
        servicio.NoCuentaS=model.NoCuentaS;
        servicio.Costo=model.Costo;
        this._context.Servicios.Add(servicio);
        this._context.SaveChanges();
 
        return RedirectToAction("ServicioList","Servicio");
    } 
    [HttpGet]
      public IActionResult ServicioEdit(Guid Id)
      {
        Servicio? ActServicio = this._context.Servicios
        .Where(b => b.Id == Id)
        .FirstOrDefault();

          //si no lo encuentra
        if(ActServicio == null)
        {
            return RedirectToAction("ServicioList","Servicio");
        }
         //asignar modelo a base
        
        ServiciosModel model = new ServiciosModel();
        model.Id=ActServicio.Id;
        model.NombreS=ActServicio.NombreS;
        model.Tipo=ActServicio.Tipo;
        model.NoCuentaS=ActServicio.NoCuentaS;
        model.Costo=ActServicio.Costo;

        
        return View(model);
      }



    [HttpPost]
     public IActionResult ServicioEdit(ServiciosModel serviciosModel)
     {
        Servicio ServicioEntity = this._context.Servicios
        .Where(b => b.Id == serviciosModel.Id).First();

        if(serviciosModel == null)
        {
            return RedirectToAction("serviciosModel");
        }
        ServicioEntity.NombreS=serviciosModel.NombreS;
        ServicioEntity.Tipo=serviciosModel.Tipo;
        ServicioEntity.NoCuentaS=serviciosModel.NoCuentaS;
        ServicioEntity.Costo=serviciosModel.Costo;

        this._context.Servicios.Update(ServicioEntity);
        this._context.SaveChanges();
       
       return RedirectToAction("ServicioList","Servicio");

     }
     [HttpGet]
     public IActionResult ServicioDelete(Guid Id)
     {
        Servicio servicioAct = this._context.Servicios
        .Where(b => b.Id == Id)
        .First();

          //si no lo encuentra
        if(servicioAct == null)
        {
            return RedirectToAction("ServicioList","Servicio");
        }
         //asignar modelo a base
         ServiciosModel model = new ServiciosModel();
        model.Id=servicioAct.Id;
        model.Tipo=servicioAct.Tipo;
        model.NoCuentaS=servicioAct.NoCuentaS;
        model.Costo=servicioAct.Costo;
        model.NombreS=servicioAct.NombreS;
        return View(model);

        
     }
     [HttpPost]
     public IActionResult ServicioDelete( ServiciosModel serviciosModel)
     {
        bool exits = this._context.Servicios.Any(b => b.Id==serviciosModel.Id);
        if(!exits)
        {
            return View (serviciosModel);
        }
        Servicio ServiciosEntity = this._context.Servicios
        .Where(b => b.Id == serviciosModel.Id).First();
        ServiciosEntity.NombreS=serviciosModel.NombreS;
        ServiciosEntity.Costo=serviciosModel.Costo;
        ServiciosEntity.Tipo=serviciosModel.Tipo;
        ServiciosEntity.NoCuentaS=serviciosModel.NoCuentaS;
        
        this._context.Servicios.Remove(ServiciosEntity);
        this._context.SaveChanges();

        return RedirectToAction("ServicioList","Servicio");

     }


}