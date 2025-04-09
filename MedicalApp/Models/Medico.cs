namespace MedicalApp.Models;

public class Medico
{
    public string Codmed { get; set; }
    public string Codesp { get; set; }
    public string Nommed { get; set; }
    public int AnioColegiatura { get; set; }
    public string Coddis { get; set; }
    public int Estado { get; set; }

    public Especialidad Especialidad { get; set; }
    public Distrito Distrito { get; set; }
    public ICollection<Cita> Citas { get; set; }
}