namespace MedicalApp.Models;

public class Cita
{
    public int Id { get; set; }
    public int NroCita { get; set; }
    public DateTime Fecha { get; set; }
    public float Pago { get; set; }
    public int Codpac { get; set; }
    public string Codmed { get; set; }
    public int Estado { get; set; }

    public Paciente Paciente { get; set; }
    public Medico Medico { get; set; }
}