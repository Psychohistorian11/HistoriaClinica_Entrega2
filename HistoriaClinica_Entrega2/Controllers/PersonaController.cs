using HistoriaClinica_Entrega2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace HistoriaClinica_Entrega2.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Persona() 
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
            apellidos = Request.Form["apellidos"].ToString();
            fechaNacimiento = Convert.ToDateTime(Request.Form["fhNacimiento"]);
            tipoRegimen = Request.Form["tipoRegimen"].ToString();
            semanasCotizadas = Convert.ToInt32(Request.Form["semanasCotizadas"]);
            fechaIngreso = DateTime.Parse(Request.Form["fechaIngreso"]);
            fechaIngresoEPS = DateTime.Parse(Request.Form["fechaIngresoEPS"]);
            EPS = Request.Form["EPS"].ToString();
            historiaClinica = Request.Form["historiaClinica"].ToString();
            cantidadEnfermedades = Convert.ToInt32(Request.Form["cantidadEnfermedades"]);
            enfermedadRelevante = Convert.ToString(Request.Form["enfermedadRelevante"]);
            tipoAfiliacion = Convert.ToString(Request.Form["tipoAfiliacion"]);
            costosTratamientos = Convert.ToDouble(Request.Form["costosTratamientos"]);


            Persona persona = new Persona(identificacion, nombre, apellidos, fechaNacimiento,tipoRegimen,semanasCotizadas,
                                           fechaIngreso,fechaIngresoEPS,EPS,historiaClinica,cantidadEnfermedades, enfermedadRelevante,
                                           tipoAfiliacion,costosTratamientos);
            return View(persona);
        }
    }
}