using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class CambiarInformacion
    {
        List<Persona> listaDePacientes = Clinica.listaDePacientes;
        ClinicaDTO clinicaDTO = new ClinicaDTO();  

        public List<Persona> ListaDePacientes { get => listaDePacientes; set => listaDePacientes = value; }

        public void cambiarTipoRegimen(Persona paciente, string tipoRegimen)
        {
            paciente.Trabajador.TipoRegimen = tipoRegimen;
            clinicaDTO.cambiarTipoRegimenBD(paciente, tipoRegimen);
        }

        public void cambiarHistoriaClinica(Persona paciente, string historia)
        {
            paciente.InformacionPaciente.HistoriaClinica = historia;
            clinicaDTO.cambiarHistoriaClinicaBD(paciente, historia);
        }

        public void cambiarCostoTratamientos(Persona paciente, int nuevoCosto)
        {
            paciente.InformacionPaciente.CostosTratamientos = nuevoCosto;
            clinicaDTO.cambiarCostoTratamientosBD(paciente, nuevoCosto);
        }

        public void cambiarEnfermedadRelevante(Persona paciente, string enfermedad)
        {
            paciente.InformacionPaciente.EnfermedadRelevante = enfermedad;
            clinicaDTO.cambiarEnfermedadRelevante(paciente, enfermedad);
        }
        public void CambioEPS(int identificacion, string EPS)
        {
            clinicaDTO.CambioEPSBD(identificacion, EPS);
            foreach (Persona paciente in ListaDePacientes)
            {
                if (paciente.Identificacion == identificacion)
                {
                    paciente.Trabajador.EPS = EPS;
                    paciente.Trabajador.FechaIngresoEPS = DateTime.Now;
                    
                }
            }
        }
    }
}