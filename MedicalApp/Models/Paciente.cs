﻿namespace MedicalApp.Models;

public class Paciente
{
    public int Codpac { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Direccion { get; set; }
    public string Coddis { get; set; }
    public string Telefono { get; set; }
    public int Estado { get; set; }

    public Distrito Distrito { get; set; }
    public ICollection<Cita> Citas { get; set; }
}