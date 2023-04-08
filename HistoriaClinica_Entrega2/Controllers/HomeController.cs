using HistoriaClinica_Entrega2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace HistoriaClinica_Entrega2.Controllers
{
    
    public class HomeController : Controller
    {

        Clinica clinica = Clinica.ObtenerInstancia();

        public ActionResult MenuPrincipal()
        {
            return View();
        }
        public ActionResult registro()
        {   
            return View();
        }

        public ActionResult estadisticas()
        {
            if(clinica.ListaDePacientes.Count > 0)
            {
                return View(clinica);
            }
            else
            {
                return RedirectToAction("seNecesitaRegistro");
            }
            
        }

        public ActionResult seNecesitaRegistro() 
        {
            return View();
        }

        public ActionResult verificarParaEPSchange()
        {
            int id;
            string EPS;
            id = Convert.ToInt32(Request.Form["idEPSchange"]);
            EPS = Convert.ToString(Request.Form["varEPSchange"]);

            if (clinica.verificarExistenciaDeIdentidad(id) == false)
            {
                TempData["Notificacion"] = "El paciente no se encuentra en el sistema";
            }
            else if (clinica.verificar3mesesEnEPS(id) == false)
            {
                TempData["Notificacion"] = "El paciente no lleva 3 o más meses en el sistema";
            }
            else
            {
                clinica.CambioEPS(id, EPS);
                string idMomentaneo = Convert.ToString(id);
                TempData["idEPSchange"] = idMomentaneo;
                TempData.Keep("idEPSchange");
                return RedirectToAction("mostrarCambioEPS");
            }

            return RedirectToAction("cambioEPS");
            

        }
        public ActionResult cambioEPS()
        {

           ViewBag.Notificacion = TempData["Notificacion"];
            return View();
        }

        public ActionResult mostrarCambioEPS()
        {
            string dato = (string)TempData["idEPSchange"];
            TempData.Keep("idEPSchange");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = clinica.obtnerPacientePorId(datoEntero);
            return View(paciente);
        }

        public ActionResult mostrarRegistro() 
        {
            int identificacion;
            string nombre;
            string apellidos;
            DateTime fechaNacimiento;
            string tipoRegimen;
            int semanasCotizadas;
            DateTime fechaIngreso;
            DateTime fechaIngresoEPS;
            string EPS;
            string historiaClinica;
            int cantidadEnfermedades;
            string enfermedadRelevante;
            string tipoAfiliacion;
            double costosTratamientos;

            identificacion = Convert.ToInt32(Request.Form["id"]);
            nombre = Convert.ToString(Request.Form["nombre"]);
            apellidos = Convert.ToString(Request.Form["apellidos"]);
            fechaNacimiento = Convert.ToDateTime(Request.Form["fhNacimiento"]);
            tipoRegimen = Convert.ToString(Request.Form["tipoRegimen"]);
            semanasCotizadas = Convert.ToInt32(Request.Form["semanasCotizadas"]);
            fechaIngreso = Convert.ToDateTime(Request.Form["fechaIngreso"]);
            fechaIngresoEPS = Convert.ToDateTime(Request.Form["fechaIngresoEPS"]);
            EPS = Convert.ToString(Request.Form["EPS"]);
            historiaClinica = Convert.ToString(Request.Form["historiaClinica"]);
            cantidadEnfermedades = Convert.ToInt32(Request.Form["cantidadEnfermedades"]);
            enfermedadRelevante = Convert.ToString(Request.Form["enfermedadRelevante"]);
            tipoAfiliacion = Convert.ToString(Request.Form["tipoAfiliacion"]);
            costosTratamientos = Convert.ToDouble(Request.Form["costosTratamientos"]);


            Persona persona = new Persona(identificacion, nombre, apellidos, fechaNacimiento,tipoRegimen,semanasCotizadas,
                                           fechaIngreso,fechaIngresoEPS,EPS,historiaClinica,cantidadEnfermedades, enfermedadRelevante,
                                           tipoAfiliacion,costosTratamientos);

            clinica.ingresarPaciente(persona);
            return View(persona);
        }
        public ActionResult verificarParaRegimeChange()
        {
            int id;
            string tipoAfiliacion;
            id = Convert.ToInt32(Request.Form["idRegimeChange"]);
            tipoAfiliacion = Convert.ToString(Request.Form["varTipoRegimenChange"]);

            if (clinica.verificarExistenciaDeIdentidad(id) == false)
            {
                TempData["NotificacionRegimeChange"] = "El paciente no se encuentra en el sistema";
            }
            else
            {
                Persona persona = clinica.obtnerPacientePorId(id);
                clinica.cambiarTipoAfiliacion(persona, tipoAfiliacion);
                string idMomentaneo = Convert.ToString(id);
                TempData["idRegimeChange"] = idMomentaneo;
                TempData.Keep("idRegimeChange");
                return RedirectToAction("mostrarCambioRegimen");

            }
            return RedirectToAction("cambioRegimen");
        }

        public ActionResult cambioRegimen()
        {
            ViewBag.Notificacion = TempData["NotificacionRegimeChange"];
            return View();
        }
        public ActionResult mostrarCambioRegimen()
        {
            string dato = (string)TempData["idRegimeChange"];
            TempData.Keep("idRegimeChange");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = clinica.obtnerPacientePorId(datoEntero);
            return View(paciente);
        }

        public ActionResult actualizarHistoria()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarHistoria"];
            return View();
        }

        public ActionResult verificarParaActualizarHistoria()
        {
            int id;
            id = Convert.ToInt32(Request.Form["idHistoria"]);

            if (clinica.verificarExistenciaDeIdentidad(id) == false)
            {
                TempData["NotificacionActualizarHistoria"] = "El paciente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(id);
                TempData["idMomentaneoHistoria"] = idMomentaneo;
                TempData.Keep("idMomentaneoHistoria");
                return RedirectToAction("mostrarActualizarHistoria");

            }
            return RedirectToAction("actualizarHistoria");
        }

        public ActionResult mostrarActualizarHistoria()
        {
            string dato = (string)TempData["idMomentaneoHistoria"];
            TempData.Keep("idMomentaneoHistoria");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = clinica.obtnerPacientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambioHistoriaClinicaRealizado"];
            return View(paciente);
        }

        public ActionResult cambioHistoriaClinica()
        {
            string dato = (string)TempData["idMomentaneoHistoria"];
            int datoEntero = Convert.ToInt32(dato);
            string historia = Convert.ToString(Request.Form["nuevaHistoriaClinica"]);
            Persona persona = clinica.obtnerPacientePorId(datoEntero);
            clinica.cambiarHistoriaClinica(persona, historia);

            TempData["cambioHistoriaClinicaRealizado"] = "La historia clinica se ha modificado con éxito";
            return RedirectToAction("mostrarActualizarHistoria");

        }
        /**********************/
        public ActionResult actualizarCosto()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarCosto"];
            return View();
        }

        public ActionResult verificarParaActualizarCosto()
        {
            int id;
            id = Convert.ToInt32(Request.Form["idCosto"]);

            if (clinica.verificarExistenciaDeIdentidad(id) == false)
            {
                TempData["NotificacionActualizarCosto"] = "El paciente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(id);
                TempData["idMomentaneoCosto"] = idMomentaneo;
                TempData.Keep("idMomentaneoCosto");
                return RedirectToAction("mostrarActualizarCosto");

            }
            return RedirectToAction("actualizarCosto");
        }

        public ActionResult mostrarActualizarCosto()
        {
            string dato = (string)TempData["idMomentaneoCosto"];
            TempData.Keep("idMomentaneoCosto");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = clinica.obtnerPacientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambioCostoTratamientosRealizado"];
            return View(paciente);
        }

        public ActionResult cambioCosto()
        {
            string dato = (string)TempData["idMomentaneoCosto"];
            int datoEntero = Convert.ToInt32(dato);
            int costo = Convert.ToInt32(Request.Form["nuevoCosto"]);
            Persona persona = clinica.obtnerPacientePorId(datoEntero);
            clinica.cambiarCostoTratamientos(persona, costo);

            TempData["cambioCostoTratamientosRealizado"] = "El costo de los tratamientos del paciente se ha actualizado con éxito";
            return RedirectToAction("mostrarActualizarCosto");
        }
        /********************/

        public ActionResult actualizarEnfermedad()
        {
            ViewBag.Notificacion = TempData["NotificacionActualizarEnfermedad"];
            return View();
        }

        public ActionResult verificarParaActualizarEnfermedad()
        {
            int id;
            id = Convert.ToInt32(Request.Form["idEnfermedad"]);

            if (clinica.verificarExistenciaDeIdentidad(id) == false)
            {
                TempData["NotificacionActualizarEnfermedad"] = "El paciente no se encuentra en el sistema";
            }
            else
            {
                string idMomentaneo = Convert.ToString(id);
                TempData["idMomentaneoEnfermedad"] = idMomentaneo;
                TempData.Keep("idMomentaneoEnfermedad");
                return RedirectToAction("mostrarActualizarEnfermedad");

            }
            return RedirectToAction("actualizarEnfermedad");
        }

        public ActionResult mostrarActualizarEnfermedad()
        {
            string dato = (string)TempData["idMomentaneoEnfermedad"];
            TempData.Keep("idMomentaneoEnfermedad");
            int datoEntero = Convert.ToInt32(dato);
            Persona paciente = clinica.obtnerPacientePorId(datoEntero);
            ViewBag.Notificacion = TempData["cambioEnfermedadRealizado"];
            return View(paciente);
        }


        public ActionResult cambiarEnfermedad()
        {
            string dato = (string)TempData["idMomentaneoEnfermedad"];
            int datoEntero = Convert.ToInt32(dato);
            string enfermedad = Convert.ToString(Request.Form["nuevaEnfermedad"]);
            Persona persona = clinica.obtnerPacientePorId(datoEntero);
            clinica.cambiarEnfermedadRelevante(persona, enfermedad);

            TempData["cambioEnfermedadRealizado"] = "La enfermedad más relevante del paciente se ha actualizado con éxito";
            return RedirectToAction("mostrarActualizarEnfermedad");
        }



    }
}