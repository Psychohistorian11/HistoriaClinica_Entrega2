using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{

    public class Clinica
    {
        public static Clinica instanciaUnica;
        public  CalcularInformacion calcularInfo = new CalcularInformacion();
        public  CambiarInformacion cambiarInfo = new CambiarInformacion();
        public  VerificarInformacion verificarInfo = new VerificarInformacion();


        public static Clinica ObtenerInstancia()
        {
            if (instanciaUnica == null)
            {
                instanciaUnica = new Clinica();
            }

            return instanciaUnica;
        }
        public static List<Persona> listaDePacientes = new List<Persona>();
        
        public  List<Persona> ListaDePacientes { get => listaDePacientes; set => listaDePacientes = value; }
        public CalcularInformacion CalcularInfo { get => calcularInfo; set => calcularInfo = value; }
        public CambiarInformacion CambiarInfo { get => cambiarInfo; set => cambiarInfo = value; }
        public VerificarInformacion VerificarInfo { get => verificarInfo; set => verificarInfo = value; }

        public Persona obtenerPacientePorId(int id)
        {


            foreach (Persona persona in listaDePacientes)
            {
                if (persona.Identificacion == id)
                {
                    return persona;
                }
            }

            return null;
            
        }

        public void ingresarPaciente(Persona nuevoPaciente)
        {
            ListaDePacientes.Add(nuevoPaciente);

        }

 
    }
}