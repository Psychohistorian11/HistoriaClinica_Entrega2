using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class VerificarInformacion 
    {
        List<Persona> listaDePacientes = Clinica.listaDePacientes; 

        public  bool verificar3mesesEnEPS(int id)
        {
            foreach (Persona paciente in listaDePacientes)
            {
                if (paciente.Identificacion == id)
                {
                    if (DateTime.Now.Date >= paciente.Trabajador.FechaIngresoEPS.Date.AddMonths(3))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return false;
        }
        public  bool verificarExistenciaDeIdentidad(int id)
        {
            foreach (Persona persona in listaDePacientes)
            {
                if (persona.Identificacion == id)
                {
                    return true;
                }

            }
            return false;
        }

        public bool verificarSiLaEPSesLaMisma(int id, string EPSactual)
        {
            foreach(Persona paciente in listaDePacientes)
            {
                if(paciente.Identificacion == id && paciente.Trabajador.EPS == EPSactual)
                {  
                      return true;
                }
                return false;
            }
            return false;

        }

        public bool verificarSiRegimenEsElMismo(int id, string tipoRegimen)
        {
            foreach(Persona paciente in listaDePacientes)
            {
                if(paciente.Identificacion == id && paciente.Trabajador.TipoRegimen == tipoRegimen)
                {
                    return true;
                }

                return false;
            }
            return false; 
        }
    }
}