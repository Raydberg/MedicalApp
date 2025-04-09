using System.Data;
using Microsoft.Data.SqlClient;  
using MedicalApp.Models;

namespace MedicalApp.Services;

public class StoredProcedureService
{
    private readonly string _connectionString;

    public StoredProcedureService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionSQL");
    }

    public IEnumerable<CitasAnio> ListarCitasAnio(int year)
    {
        List<CitasAnio> lista = new List<CitasAnio>();

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CitasAnio", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@year", year);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new CitasAnio
                {
                    NroCita = dr.GetInt32(0),
                    Fecha = dr.GetDateTime(1),
                    Pago = (float)dr.GetDouble(2),
                    NomPac = dr.GetString(3),
                    NomMed = dr.GetString(4)
                });
            }

            cn.Close();
        }

        return lista;
    }

    public IEnumerable<Medico> ListarMedicos()
    {
        List<Medico> lista = new List<Medico>();

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("ListarMedicos", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Medico
                {
                    Codmed = dr.GetString(0),
                    Nommed = dr.GetString(1),
                    Especialidad = new Especialidad { Nombre = dr.GetString(2) },
                    AnioColegiatura = dr.GetInt32(3),
                    Distrito = new Distrito { Nombre = dr.GetString(4) },
                    Estado = dr.GetInt32(5)
                });
            }

            cn.Close();
        }

        return lista;
    }

    public IEnumerable<Especialidad> ListarEspecialidades()
    {
        List<Especialidad> lista = new List<Especialidad>();

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("ListarEspecialidades", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Especialidad
                {
                    Codesp = dr.GetString(0),
                    Nombre = dr.GetString(1)
                });
            }

            cn.Close();
        }

        return lista;
    }

    public string AgregarMedico(Medico medico)
    {
        string mensaje = "";

        using (SqlConnection cn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("AgregarMedico", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codmed", medico.Codmed);
            cmd.Parameters.AddWithValue("@codesp", medico.Codesp);
            cmd.Parameters.AddWithValue("@nommed", medico.Nommed);
            cmd.Parameters.AddWithValue("@anio_colegiatura", medico.AnioColegiatura);
            cmd.Parameters.AddWithValue("@coddis", medico.Coddis);

            SqlParameter outputParameter = new SqlParameter();
            outputParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(outputParameter);

            cn.Open();
            cmd.ExecuteNonQuery();

            mensaje = outputParameter.Value.ToString();
            cn.Close();
        }

        return mensaje;
    }
}