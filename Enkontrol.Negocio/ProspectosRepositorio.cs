using Dapper;
using Enkontrol.Modelos.Prospectos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Enkontrol.Negocio
{
    public class ProspectosRepositorio
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public List<SelectListItem> ConsultaListaGeneros()
        {
            var res = new List<SelectListItem>();
            res.Add(new SelectListItem { Text="-- Genero --", Value = "0"});
            try
            {
                using (var conexion = new SqlConnection(_connection))
                {
                    var generos = conexion.Query<Generos>("SPSelGenero", commandType: CommandType.StoredProcedure);

                    foreach (var item in generos)
                    {
                        res.Add(new SelectListItem { Text = item.cDescripcion, Value = item.Id.ToString()});
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return res;
        }

        public List<SelectListItem> ConsultaListaEstadosCiviles()
        {
            var res = new List<SelectListItem>();
            res.Add(new SelectListItem { Text = "-- Estado Civil --", Value = "0" });
            try
            {
                using (var conexion = new SqlConnection(_connection))
                {
                    var generos = conexion.Query<Generos>("SPSelEstadoCivil", commandType: CommandType.StoredProcedure);

                    foreach (var item in generos)
                    {
                        res.Add(new SelectListItem { Text = item.cDescripcion, Value = item.Id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return res;
        }

        public List<Prospecto> ConsultasListaProspectos(string filtro, string genero, string estadocivil)
        {
            var res = new List<Prospecto>();

            try
            {
                using (var conexion = new SqlConnection(_connection))
                {
                    res = conexion.Query<Prospecto>("SPSelProspectos", new { pcFiltro = string.IsNullOrEmpty(filtro) ? null : filtro, piGenero = genero == "0" ? null : genero, piEstadoCivil = estadocivil == "0" ? null : estadocivil }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {

            }
            return res;
        }

        public bool InsertarNuevoProspecto(Prospecto pros)
        {
            bool insert = false;
            try
            {
                using (var conexion = new SqlConnection(_connection))
                {
                    var res = conexion.Query<int>("SPInsProspecto", new { pcNombre = pros.cNombre, pcApellidoPaterno = pros.cApellidoPaterno, pcApellidoMaterno = pros.cApellidoMaterno, pdFechaNacimiento = pros.dtFechaNacimiento, pcTelefono = pros.cTelefonoMovil, pcEmail = pros.cEmail, piIdGenero = Convert.ToInt32(pros.Genero), piEstadoCivil = Convert.ToInt32(pros.EstadoCivil) }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (res == 1)
                        insert = true;
                }
            }
            catch(Exception ex)
            {
                
            }
            return insert;
        }

        public Prospecto ConsultaProspectoId(string Id)
        {
            var prospecto = new Prospecto();
            try
            {
                using (var conexion = new SqlConnection(_connection))
                {
                    prospecto = conexion.Query<Prospecto>("SPSelIDProspectos", new { piID = Convert.ToInt32(Id) }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {

            }
            return prospecto;
        }

        public int EliminarProspectoId(string Id)
        {
            int eli = 0;
            try
            {
                using (var conexion = new SqlConnection(_connection))
                {
                    eli = conexion.Query<int>("SPDelProspectos", new { piIdProspecto = Convert.ToInt32(Id) }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return eli;
        }

        public bool ActualizarProspectoId(Prospecto model)
        {
            bool mod = false;
            try
            {
                using (var conexion = new SqlConnection(_connection))
                {
                    var res = conexion.Query<int>("SPUpdProspectos", new { piIdProspecto = model.Id, pcNombre = model.cNombre, pcApellidoPaterno = model.cApellidoPaterno, pcApellidoMaterno = model.cApellidoMaterno, pdFechaNacimiento = model.dtFechaNacimiento, pcTelefono = model.cTelefonoMovil, pcEmail = model.cEmail, piIdGenero = Convert.ToInt32(model.Genero), piEstadoCivil = Convert.ToInt32(model.EstadoCivil)}, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (res == 1)
                        mod = true;
                }
            }
            catch (Exception ex)
            {

            }
            return mod;
        }
    }
}
