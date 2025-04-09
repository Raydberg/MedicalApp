namespace MedicalApp.Models;

public class Distrito
{
    public string Coddis { get; set; }
    public string Nombre { get; set; }

    public ICollection<Medico> Medicos { get; set; }
    public ICollection<Paciente> Pacientes { get; set; }
}