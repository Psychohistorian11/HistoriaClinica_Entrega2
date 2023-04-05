using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class Persona
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



        public Persona(int identificacion,
            string nombre,
            string apellidos,
            DateTime fechaNacimiento,
            string tipoRegimen,
            int semanasCotizadas,
            DateTime fechaIngreso,
            DateTime fechaIngresoEPS,
            string ePS,
            string historiaClinica,
            int cantidadEnfermedades,
            string enfermedadRelevante,
            string tipoAfiliacion,
            double costosTratamientos)
        {
            this.Identificacion = identificacion;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.FechaNacimiento = fechaNacimiento;
            this.TipoRegimen = tipoRegimen;
            this.SemanasCotizadas = semanasCotizadas;
            this.FechaIngreso = fechaIngreso;
            this.FechaIngresoEPS = fechaIngresoEPS;
            EPS1 = ePS;
            this.HistoriaClinica = historiaClinica;
            this.CantidadEnfermedades = cantidadEnfermedades;
            this.EnfermedadRelevante = enfermedadRelevante;
            this.TipoAfiliacion = tipoAfiliacion;
            this.CostosTratamientos = costosTratamientos;
        }

        public int Identificacion { get => identificacion; set => identificacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string TipoRegimen { get => tipoRegimen; set => tipoRegimen = value; }
        public int SemanasCotizadas { get => semanasCotizadas; set => semanasCotizadas = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public DateTime FechaIngresoEPS { get => fechaIngresoEPS; set => fechaIngresoEPS = value; }
        public string EPS1 { get => EPS; set => EPS = value; }
        public string HistoriaClinica { get => historiaClinica; set => historiaClinica = value; }
        public int CantidadEnfermedades { get => cantidadEnfermedades; set => cantidadEnfermedades = value; }
        public string EnfermedadRelevante { get => enfermedadRelevante; set => enfermedadRelevante = value; }
        public string TipoAfiliacion { get => tipoAfiliacion; set => tipoAfiliacion = value; }
        public double CostosTratamientos { get => costosTratamientos; set => costosTratamientos = value; }



    }
}