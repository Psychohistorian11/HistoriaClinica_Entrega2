using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class CambiarInformacion
    {
        List<Persona> listaDePacientes = Clinica.listaDePacientes;

        public void cambiarTipoAfiliacion(Persona paciente, string tipoAfiliacion)
        {
            paciente.Trabajador.TipoAfiliacion = tipoAfiliacion;
        }

        public void cambiarHistoriaClinica(Persona paciente, string historia)
        {
            paciente.InformacionPaciente.HistoriaClinica = historia;
        }

        public void cambiarCostoTratamientos(Persona paciente, int nuevoCosto)
        {
            paciente.InformacionPaciente.CostosTratamientos = nuevoCosto;
        }

        public void cambiarEnfermedadRelevante(Persona paciente, string enfermedad)
        {
            paciente.InformacionPaciente.EnfermedadRelevante = enfermedad;
        }
        public Persona CambioEPS(int identificacion, string EPS)
        {

            foreach (Persona paciente in listaDePacientes)
            {
                if (paciente.Identificacion == identificacion)
                {
                    paciente.Trabajador.EPS = EPS;
                    return paciente;
                }

            }
            return null;
        }
    }
}