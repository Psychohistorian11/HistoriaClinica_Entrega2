﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{

    public class Clinica
    {
        public static Clinica instanciaUnica;
        public CalcularInformacion calcularInfo = new CalcularInformacion();
        public CambiarInformacion cambiarInfo = new CambiarInformacion();
        public VerificarInformacion verificarInfo = new VerificarInformacion();

        /*public Clinica()
        {
            
        }*/

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

        /*public bool verificarExistenciaDeIdentidad(int id)
        {
            foreach(Persona persona in listaDePacientes)
            {
                if (persona.Identificacion == id)
                {
                    return true;
                }
                
            }
            return false;
        }

        public bool verificar3mesesEnEPS(int id)
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
        
        public Persona CambioEPS(int identificacion, string EPS)
        {

            foreach (Persona paciente in listaDePacientes)
            {
                if (paciente.Identificacion == identificacion)
                {
                    paciente.Trabajador.EPS1 = EPS;
                    return paciente; 
                }

            }
            return null;
        }

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
        }*/

        public void ingresarPaciente(Persona nuevoPaciente)
        {
            ListaDePacientes.Add(nuevoPaciente);

        }

        /*public double calcularPorcentajePacienteSinEnfermedad()
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

        public List<double> calcularTotalCostosPorEPS()
        {
            List<double> costosEps = new List<double>();
            List<Persona> afiliadosSura = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS1 == "Sura").ToList();
            List<Persona> afiliadosNuevaEps = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS1 == "Nueva EPS").ToList();
            List<Persona> afiliadosSaludtotal = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS1 == "Salud Total").ToList();
            List<Persona> afiliadosSanitas = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS1 == "Sanitas").ToList();
            List<Persona> afiliadosSavia = listaDePacientes.Where(afiliado => afiliado.Trabajador.EPS1 == "Savia").ToList();

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

        public double calcularTotalPacientesCancer()
        {
            string enfermedadBuscada = "cancer";
            int numeroPacientes = 0;

            foreach(Persona Paciente in listaDePacientes)
            {
                if(Paciente.InformacionPaciente.EnfermedadRelevante.ToLower() == enfermedadBuscada.ToLower())
                {
                    numeroPacientes++;
                }
            }

            return numeroPacientes;
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

            List<double> porcentajes = new List<double>();
            List<Persona> afiliadosContributivo = listaDePacientes.Where(afiliado => afiliado.Trabajador.TipoRegimen == "Contributivo").ToList();
            List<Persona> afiliadosSubsidiado = listaDePacientes.Where(afiliado => afiliado.Trabajador.TipoRegimen == "Subsidiado").ToList();

            double porcentajeContributivo = (Convert.ToDouble(afiliadosContributivo.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
            double porcentajeSubsidiado = (Convert.ToDouble(afiliadosSubsidiado.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
            porcentajes.Add(porcentajeContributivo);
            porcentajes.Add(porcentajeSubsidiado);
            return porcentajes;
        }
        public List<double> calcularPorcentajePacientesPorTipoAfiliacion()
        {
            List<Persona> afiliadosCotizantes = listaDePacientes.Where(paciente => paciente.Trabajador.TipoAfiliacion == "Cotizante").ToList();
            List<Persona> afiliadosBeneficiarios = listaDePacientes.Where(paciente => paciente.Trabajador.TipoAfiliacion == "Beneficiario").ToList();
            double porcentajeCotizantes = (Convert.ToDouble(afiliadosCotizantes.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
            double porcentajeBeneficiarios = (Convert.ToDouble(afiliadosBeneficiarios.Count) / Convert.ToDouble(listaDePacientes.Count)) * 100;
            List<double> porcentajes = new List<double> { porcentajeCotizantes, porcentajeBeneficiarios };
            return porcentajes;


        }*/
    }
}