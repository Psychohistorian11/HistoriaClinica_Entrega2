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
        Trabajador trabajador;
        Paciente informacionPaciente;

        public Persona(int identificacion, string nombre, string apellidos, DateTime fechaNacimiento, Trabajador trabajador, Paciente informacionPaciente)
        {
            this.Identificacion = identificacion;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.FechaNacimiento = fechaNacimiento;
            this.Trabajador = trabajador;
            this.InformacionPaciente = informacionPaciente;
        }

        public int Identificacion { get => identificacion; set => identificacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public Trabajador Trabajador { get => trabajador; set => trabajador = value; }
        public Paciente InformacionPaciente { get => informacionPaciente; set => informacionPaciente = value; }
        

    }
}