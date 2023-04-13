using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class Paciente
    {
        string historiaClinica;
        int cantidadEnfermedades;
        string enfermedadRelevante;
        double costosTratamientos;

        public Paciente(string historiaClinica, int cantidadEnfermedades, string enfermedadRelevante, double costosTratamientos)
        {
            this.HistoriaClinica = historiaClinica;
            this.CantidadEnfermedades = cantidadEnfermedades;
            this.EnfermedadRelevante = enfermedadRelevante;
            this.CostosTratamientos = costosTratamientos;
        }

        public string HistoriaClinica { get => historiaClinica; set => historiaClinica = value; }
        public int CantidadEnfermedades { get => cantidadEnfermedades; set => cantidadEnfermedades = value; }
        public string EnfermedadRelevante { get => enfermedadRelevante; set => enfermedadRelevante = value; }
        public double CostosTratamientos { get => costosTratamientos; set => costosTratamientos = value; }
    }
}