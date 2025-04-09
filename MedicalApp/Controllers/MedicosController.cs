using Microsoft.AspNetCore.Mvc;
using MedicalApp.Models;
using MedicalApp.Services;

namespace MedicalApp.Controllers;

public class MedicosController : Controller
{
    private readonly StoredProcedureService _spService;
    
    public MedicosController(StoredProcedureService spService)
    {
        _spService = spService;
    }
    
    public IActionResult Index()
    {
        var medicos = _spService.ListarMedicos();
        return View(medicos);
    }
    
    public IActionResult ListadoMedicos()
    {
        var medicos = _spService.ListarMedicos();
        return PartialView("_ListadoMedicos", medicos);
    }
    
    public IActionResult Agregar()
    {
        ViewBag.Especialidades = _spService.ListarEspecialidades();
        
        return View();
    }
    
    [HttpPost]
    public IActionResult Agregar(Medico medico)
    {
        string mensaje = _spService.AgregarMedico(medico);
        
        ViewBag.Mensaje = mensaje;
        ViewBag.Especialidades = _spService.ListarEspecialidades();
        
        return View();
    }
}