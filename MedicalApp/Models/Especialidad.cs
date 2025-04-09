namespace MedicalApp.Models;

public class Especialidad
{
    public string Codesp { get; set; }
    public string Nombre { get; set; }

    public ICollection<Medico> Medicos { get; set; }
}