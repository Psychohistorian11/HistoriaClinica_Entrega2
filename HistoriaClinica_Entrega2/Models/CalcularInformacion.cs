using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class CalcularInformacion 
    {
        List<Persona> listaDePacientes = Clinica.listaDePacientes;

        public List<double> calcularTotalCostosPorEPS()
        {
            List<double> costosEps = new List<double>();
            List<Persona> afiliadosSura = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS == "Sura").ToList();
            List<Persona> afiliadosNuevaEps = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS == "Nueva EPS").ToList();
            List<Persona> afiliadosSaludtotal = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS == "Salud Total").ToList();
            List<Persona> afiliadosSanitas = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS == "Sanitas").ToList();
            List<Persona> afiliadosSavia = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS == "Savia").ToList();

            double costoSura = afiliadosSura.Sum(afiliado => afiliado.InformacionPaciente.CostosTratamientos);
            double costoNuevaEps = afiliadosNuevaEps.Sum(afiliado => afiliado.InformacionPaciente.CostosTratamientos);
            double costoSaludTotal = afiliadosSaludtotal.Sum(afiliado => afiliado.InformacionPaciente.CostosTratamientos);
            double costoSanitas = afiliadosSanitas.Sum(afiliado => afiliado.InformacionPaciente.CostosTratamientos);
            double costoSavia = afiliadosSavia.Sum(afiliado => afiliado.InformacionPaciente.CostosTratamientos);

            costosEps.Add(costoSura);
            costosEps.Add(costoNuevaEps);
            costosEps.Add(costoSaludTotal);
            costosEps.Add(costoSanitas);
            costosEps.Add(costoSavia);
            return costosEps;

        }
        public double calcularPorcentajePacienteSinEnfermedad()
        {
            int cantidadDePacientes = 0;
            int cantidadDePacientes0Enfermedades = 0;
            double porcentajePacientesSinEnfermedades = 0f;
            foreach (Persona paciente in listaDePacientes)
            {
                cantidadDePacientes++;
                if (paciente.InformacionPaciente.CantidadEnfermedades == 0)
                {
                    cantidadDePacientes0Enfermedades++;
                }
            }
            porcentajePacientesSinEnfermedades = (cantidadDePacientes0Enfermedades * 100) / cantidadDePacientes;
            return porcentajePacientesSinEnfermedades;
        }
        public double calcularTotalPacientesCancer()
        {
            string enfermedadBuscada = "cancer";
            int numeroPacientes = 0;

            foreach (Persona Paciente in listaDePacientes)
            {
                if (Paciente.InformacionPaciente.EnfermedadRelevante.ToLower() == enfermedadBuscada.ToLower())
                {
                    numeroPacientes++;
                }
            }

            return numeroPacientes;
        }
        public List<double> calcularPorcentajeCostosPorEPS()
        {
            List<double> costosEps = calcularTotalCostosPorEPS();

            double costoSura = costosEps[0];
            double costoNuevaEps = costosEps[1];
            double costoSaludTotal = costosEps[2];
            double costoSanitas = costosEps[3];
            double costoSavia = costosEps[4];
            double TotalCosto = costosEps.Sum();
            double porcentajeSura = (costoSura / TotalCosto) * 100;
            double porcentajeNuevaEps = (costoNuevaEps / TotalCosto) * 100;
            double porcentajeSaludTotal = (costoSaludTotal / TotalCosto) * 100;
            double porcentajeSanitas = (costoSanitas / TotalCosto) * 100;
            double porcentajeSavia = (costoSavia / TotalCosto) * 100;

            List<double> costosPorcentajes = new List<double> { porcentajeSura, porcentajeNuevaEps, porcentajeSaludTotal, porcentajeSanitas, porcentajeSavia };
            return costosPorcentajes;

        }
        public List<double> calcularPorcentajesPorEdad()
        {
            double cantidadDePacientes = Convert.ToDouble(listaDePacientes.Count);
            double cantidadNiños = 0;
            double cantidadAdolescente = 0;
            double cantidadJovenes = 0;
            double cantidadAdultos = 0;
            double cantidadAdultoMayor = 0;
            double cantidadAnciano = 0;
            List<double> porcentajeDePacientes = new List<double>();

            foreach (Persona paciente in listaDePacientes)
            {


                DateTime fecha = paciente.FechaNacimiento;
                int edad = DateTime.Today.Year - fecha.Year;

                // Se ajusta la edad si aún no ha cumplido años en este año
                if (DateTime.Today < fecha.AddYears(edad))
                {
                    edad--;
                }
                if (edad >= 0 && edad < 12)
                {
                    cantidadNiños++;
                }
                else if (edad >= 12 && edad < 18)
                {
                    cantidadAdolescente++;
                }
                else if (edad >= 18 && edad < 30)
                {
                    cantidadJovenes++;
                }
                else if (edad >= 30 && edad < 55)
                {
                    cantidadAdultos++;
                }
                else if (edad >= 55 && edad < 75)
                {
                    cantidadAdultoMayor++;
                }
                else if (edad >= 75)
                {
                    cantidadAnciano++;
                }


            }
            double cantidadPorcentajeNiños = (cantidadNiños * 100) / cantidadDePacientes;
            double cantidadPorcentajeJovenes = (cantidadJovenes * 100) / cantidadDePacientes;
            double cantidadPorcentajeAdolescente = (cantidadAdolescente * 100) / cantidadDePacientes;
            double cantidadPorcentajeAdultos = (cantidadAdultos * 100) / cantidadDePacientes;
            double cantidadPorcentajeAdultoMayor = (cantidadAdultoMayor * 100) / cantidadDePacientes;
            double cantidadPorcentajeAnciano = (cantidadAnciano * 100) / cantidadDePacientes;

            porcentajeDePacientes.Add(cantidadPorcentajeNiños);
            porcentajeDePacientes.Add(cantidadPorcentajeJovenes);
            porcentajeDePacientes.Add(cantidadPorcentajeAdolescente);
            porcentajeDePacientes.Add(cantidadPorcentajeAdultos);
            porcentajeDePacientes.Add(cantidadPorcentajeAdultoMayor);
            porcentajeDePacientes.Add(cantidadPorcentajeAnciano);

            return porcentajeDePacientes;
        }
        public Persona encontrarMayorCosto()
        {

            List<Persona> listaCostosPaciente = listaDePacientes.Where(paciente1 => paciente1.InformacionPaciente.CostosTratamientos > 0).ToList();
            double mayor_costo = listaCostosPaciente.Max(paciente1 => paciente1.InformacionPaciente.CostosTratamientos);
            List<Persona> paciente = listaCostosPaciente.Where(paciente1 => paciente1.InformacionPaciente.CostosTratamientos == mayor_costo).ToList();
            return paciente[0];


        }
        public List<double> calcularPacientesPorRegimen()
        {
            double porcentajeContributivo;
            double porcentajeSubsidiado;
            List<double> porcentajes = new List<double>();
            List<Persona> afiliadosContributivo = listaDePacientes.Where(afiliado => afiliado.Trabajador.TipoRegimen == "Contributivo").ToList();
            List<Persona> afiliadosSubsidiado = listaDePacientes.Where(afiliado => afiliado.Trabajador.TipoRegimen == "Subsidiado").ToList();

            try
            {
                porcentajeContributivo = (Convert.ToDouble(afiliadosContributivo.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
                porcentajeSubsidiado = (Convert.ToDouble(afiliadosSubsidiado.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
            }
            catch(DivideByZeroException)
            {
                porcentajeContributivo = 0;
                porcentajeSubsidiado = 0;
            }

            porcentajes.Add(porcentajeContributivo);
            porcentajes.Add(porcentajeSubsidiado);
            return porcentajes;
        }

        public List<double> calcularPorcentajePacientesPorTipoAfiliacion()
        {
            double porcentajeCotizante;
            double porcentajeBeneficiario;
            List<Persona> afiliadosCotizantes = listaDePacientes.Where(paciente => paciente.Trabajador.TipoAfiliacion == "Cotizante").ToList();
            List<Persona> afiliadosBeneficiarios = listaDePacientes.Where(paciente => paciente.Trabajador.TipoAfiliacion == "Beneficiario").ToList();
            try
            {
                porcentajeCotizante = (Convert.ToDouble(afiliadosCotizantes.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
                porcentajeBeneficiario = (Convert.ToDouble(afiliadosBeneficiarios.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
            }
            catch (DivideByZeroException)
            {
                porcentajeCotizante = 0;
                porcentajeBeneficiario = 0;
            }

            List<double> porcentajes = new List<double> { porcentajeCotizante, porcentajeBeneficiario };
            return porcentajes;

        }
    }
}