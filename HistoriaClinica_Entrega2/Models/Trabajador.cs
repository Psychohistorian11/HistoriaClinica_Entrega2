using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class Trabajador
    {
        string tipoRegimen;
        int semanasCotizadas;
        DateTime fechaIngreso;
        DateTime fechaIngresoEPS;
        string Eps;
        string tipoAfiliacion;

        public Trabajador(string tipoRegimen, int semanasCotizadas, DateTime fechaIngreso, DateTime fechaIngresoEPS, string ePS, string tipoAfiliacion)
        {
            this.TipoRegimen = tipoRegimen;
            this.SemanasCotizadas = semanasCotizadas;
            this.FechaIngreso = fechaIngreso;
            this.FechaIngresoEPS = fechaIngresoEPS;
            this.Eps = ePS;
            this.TipoAfiliacion = tipoAfiliacion;
        }

        public string TipoRegimen { get => tipoRegimen; set => tipoRegimen = value; }
        public int SemanasCotizadas { get => semanasCotizadas; set => semanasCotizadas = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public DateTime FechaIngresoEPS { get => fechaIngresoEPS; set => fechaIngresoEPS = value; }
        public string EPS { get => Eps; set => Eps = value; }
        public string TipoAfiliacion { get => tipoAfiliacion; set => tipoAfiliacion = value; }
    }
}