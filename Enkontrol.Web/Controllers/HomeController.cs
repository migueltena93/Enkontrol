using Enkontrol.Negocio;
using Enkontrol.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enkontrol.Modelos.Prospectos;
using Prospecto = Enkontrol.Web.Models.Prospecto;

namespace Enkontrol.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly List<SelectListItem> _Genero;
        private readonly List<SelectListItem> _EstadoCivil;
        private readonly ProspectosRepositorio _prosnegocio;

        public HomeController()
        {
            _Genero = new List<SelectListItem>();
            _EstadoCivil = new List<SelectListItem>();
            _prosnegocio = new ProspectosRepositorio();
        }
        public ActionResult Index()
        {
            ViewBag.Genero = _prosnegocio.ConsultaListaGeneros();
            ViewBag.EstadoCivil = _prosnegocio.ConsultaListaEstadosCiviles();

            return View();
        }

        public ActionResult _GridView(string filtro, string gen, string estadocivil)
        {
            var lstprospectos = new List<Prospecto>();
            try
            {
                var res = _prosnegocio.ConsultasListaProspectos(filtro, gen, estadocivil);

                lstprospectos = res.Select(x => new Prospecto
                {
                    Id = x.Id,
                    cNombre = x.cNombre,
                    cApellidoPaterno = x.cApellidoPaterno,
                    cApellidoMaterno = x.cApellidoMaterno,
                    dtFechaNacimiento = x.dtFechaNacimiento,
                    cTelefonoMovil = x.cTelefonoMovil,
                    cEmail = x.cEmail,
                    Genero = x.Genero,
                    EstadoCivil = x.EstadoCivil
                }).ToList();
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = $"No se pudierón obtener los prospectos registrados en la BD." }, JsonRequestBehavior.AllowGet);
            }
            return PartialView(lstprospectos);
        }

        [HttpPost]
        public ActionResult NuevoProspecto(string jsonnuevoprospecto)
        {
            var mensaje = "La información del prospecto se ingreso correctamente a la BD.";

            try
            {
                var modelnuevoprospecto = JsonConvert.DeserializeObject<Enkontrol.Modelos.Prospectos.Prospecto>(jsonnuevoprospecto);
                if (modelnuevoprospecto != null)
                {
                    bool res = _prosnegocio.InsertarNuevoProspecto(modelnuevoprospecto);

                    if (!res)
                        return Json(new { Success = false, Message = "No se pudo registrar el nuevo prospecto en la BD" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarProspectoId(string id)
        {
            var mensaje = "No se pudo consultar la información del usuario de la BD.";
            var pros = new Enkontrol.Modelos.Prospectos.Prospecto();
            try
            {
                pros = _prosnegocio.ConsultaProspectoId(id);

                if (pros.Id == 0)
                    return Json(new { Success = false, Message = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message = mensaje, Info = pros }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarProspectoId(string id)
        {
            var mensaje = "No se pudo eliminar la información del usuario de la BD.";
            int res = 0;
            try
            {
                res = _prosnegocio.EliminarProspectoId(id);

                if (res == 0)
                    return Json(new { Success = false, Message = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message = "Prospecto eliminado exitosamente de la BD" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActualizarProspectoId(string jsonactualizar)
        {
            var mensaje = "No se pudo actualizar la información del prspecto en la BD.";
            bool res;
            try
            {
                var modelnuevoprospecto = JsonConvert.DeserializeObject<Enkontrol.Modelos.Prospectos.Prospecto>(jsonactualizar);
                if (modelnuevoprospecto != null)
                {
                    res = _prosnegocio.ActualizarProspectoId(modelnuevoprospecto);

                    if (!res)
                        return Json(new { Success = false, Message = "No se pudo actualizar la información del prospecto en la BD" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message = "Prospecto actualizado exitosamente de la BD" }, JsonRequestBehavior.AllowGet);
        }
    }
}