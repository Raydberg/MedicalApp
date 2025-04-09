using Microsoft.AspNetCore.Mvc;
using MedicalApp.Models;
using MedicalApp.Services;

namespace MedicalApp.Controllers;

public class CitasController : Controller
{
    private readonly StoredProcedureService _spService;
    
    public CitasController(StoredProcedureService spService)
    {
        _spService = spService;
    }
    
    public IActionResult CitasPorAnio(int? year, int? page)
    {
        int currentYear = DateTime.Now.Year;
        int anio = year ?? currentYear;
        int pagina = page ?? 1;
        int filasPorPagina = 10;
        
        var citas = _spService.ListarCitasAnio(anio);
        
        // Paginar los resultados
        var citasPaginadas = citas.Skip((pagina - 1) * filasPorPagina)
            .Take(filasPorPagina);
        
        ViewBag.TotalRegistros = citas.Count();
        ViewBag.TotalPaginas = (int)Math.Ceiling((double)ViewBag.TotalRegistros / filasPorPagina);
        ViewBag.PaginaActual = pagina;
        ViewBag.Anio = anio;
        
        return View(citasPaginadas);
    }
}