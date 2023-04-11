using HistoriaClinica_Entrega2.Models;
using System.Transactions;

namespace HistoriaClinica_Entrega2_Test
{
    [TestClass]
    public class UnitTest1
    {

        public Persona crearPaciente()
        {
            int identificacion = 100;
            string nombre = "Cristian Jhoan";
            string apellidos = "Franco Raigosa";
            DateTime fechaNacimiento = new DateTime(2004, 07, 19);
            string tipoRegimen = "Contributivo";
            int semanasCotizadas = 10;
            DateTime fechaIngreso = new DateTime(2004, 07, 19);
            DateTime fechaIngresoEPS = new DateTime(2004, 07, 19);
            string EPS = "Sura";
            string historiaClinica = "El paciente recientemente ha tenido sintomas de...";
            int cantidadEnfermedades = 1;
            string enfermedadRelevante = "cancer";
            string tipoAfiliacion = "Cotizante";
            double costosTratamientos = 1000000;

            Persona paciente = new Persona(identificacion, nombre, apellidos, fechaNacimiento, tipoRegimen, semanasCotizadas, fechaIngreso, fechaIngresoEPS
                                , EPS, historiaClinica, cantidadEnfermedades, enfermedadRelevante, tipoAfiliacion, costosTratamientos);

            return paciente;
        }
        [TestMethod()]
        public void obtenerPacientePorIdTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            int expected = 100;


            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);
            Persona personaEncontrada = target.obtenerPacientePorId(paciente.Identificacion);
            int actual = personaEncontrada.Identificacion;

            //Assert
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod()]
        public void verificarExistenciaDeIdentidadTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            bool expected = true;

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);

            bool actual = target.verificarExistenciaDeIdentidad(paciente.Identificacion);

            //Assert
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod()]
        public void verificar3mesesEnEPSTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            bool expected = true;
            bool expected_N2 = false;

            //Act
            Persona paciente = crearPaciente();
            Persona paciente_N2 = crearPaciente();
            paciente_N2.Identificacion = 200;
            paciente_N2.FechaIngresoEPS = DateTime.Now.Date;
            
            target.ingresarPaciente(paciente);
            target.ingresarPaciente(paciente_N2);

            bool actual = target.verificar3mesesEnEPS(paciente.Identificacion);
            bool actual_N2 = target.verificar3mesesEnEPS(paciente_N2.Identificacion);

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected_N2, actual_N2);

            
        }

        [TestMethod()]
        public void CambioEPSTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            string expected = "Nueva EPS";

            //Act
            string EPS_a_cambiar = "Nueva EPS";
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);

            Persona actual = target.CambioEPS(paciente.Identificacion, EPS_a_cambiar);

            //Assert
            Assert.AreEqual(expected, actual.EPS1);

        }

        [TestMethod()]
        public void cambiarTipoAfiliacionTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            string expected = "Beneficiario";

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);

            target.cambiarTipoAfiliacion(paciente, expected);

            //Assert
            Assert.AreEqual(expected, paciente.TipoAfiliacion);

        }

        [TestMethod()]
        public void cambiarHistoriaClinicaTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            string expected = "Nueva Historia Clinica";

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);

            target.cambiarHistoriaClinica(paciente, expected);

            //Assert
            Assert.AreEqual(expected, paciente.HistoriaClinica);
        }

        [TestMethod()]
        public void cambiarCostoTratamientosTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            int expected = 2000000;

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);

            target.cambiarCostoTratamientos(paciente, expected);

            //Assert
            Assert.AreEqual(expected, paciente.CostosTratamientos);
        }

        [TestMethod()]
        public void cambiarEnfermedadRelevanteTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            string expected = "Sida";

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);

            target.cambiarEnfermedadRelevante(paciente, expected);

            //Assert
            Assert.AreEqual(expected, paciente.EnfermedadRelevante);
        }

        [TestMethod()]
        public void ingresarPacienteTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = HistoriaClinica_Entrega2.Models.Clinica.ObtenerInstancia();
            bool expected = true;
            int id = 100;

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);

            bool actual = target.verificarExistenciaDeIdentidad(id);

            //Assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod()]
        public void calcularPorcentajePacienteSinEnfermedadTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target1 = new HistoriaClinica_Entrega2.Models.Clinica();
            double expected = 50;

            //Act
            Persona paciente = crearPaciente();
            target1.ingresarPaciente(paciente);
            Persona paciente_N2 = crearPaciente();
            paciente_N2.CantidadEnfermedades = 0;
            target1.ingresarPaciente(paciente_N2);

            double actual = target1.calcularPorcentajePacienteSinEnfermedad();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void calcularTotalCostosPorEPSTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = new HistoriaClinica_Entrega2.Models.Clinica();
            double expected = 20;

            //Act
            Persona paciente = crearPaciente();
            Persona paciente_N2 = crearPaciente();
            Persona paciente_N3 = crearPaciente();
            Persona paciente_N4 = crearPaciente();
            Persona paciente_N5 = crearPaciente();

            paciente_N2.EPS1 = "Nueva EPS";
            paciente_N3.EPS1 = "Salud Total";
            paciente_N4.EPS1 = "Sanitas";
            paciente_N5.EPS1 = "Savia";

            target.ingresarPaciente(paciente);
            target.ingresarPaciente(paciente_N2);
            target.ingresarPaciente(paciente_N3);
            target.ingresarPaciente(paciente_N4);
            target.ingresarPaciente(paciente_N5);


            List<double> lista = target.calcularPorcentajeCostosPorEPS();

            //Assert
            Assert.AreEqual(expected, Math.Round(lista[0], 0));
            Assert.AreEqual(expected, Math.Round(lista[1], 0));
            Assert.AreEqual(expected, Math.Round(lista[2], 0));
            Assert.AreEqual(expected, Math.Round(lista[3], 0));
            Assert.AreEqual(expected, Math.Round(lista[4], 0));


        }
        [TestMethod()]
        public void calcularPorcentajeCostosPorEPSTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = new HistoriaClinica_Entrega2.Models.Clinica();
            double expectedSura = 1200000;
            double expectedNuevaEPS = 2000000;
            double expectedSaludTotal = 500000;
            double expectedSanitas = 3000000;
            double expectedSavia = 1200000;

            //Act
            Persona paciente_N1Sura = crearPaciente();
            Persona paciente_N2Sura = crearPaciente();
            Persona paciente_N3NuevaEPS = crearPaciente();
            Persona paciente_N4SaludTotal = crearPaciente();
            Persona paciente_N5SaludTotal = crearPaciente();
            Persona paciente_N6Sanitas = crearPaciente();
            Persona paciente_N7Sanitas = crearPaciente();
            Persona paciente_N8Savia = crearPaciente();
            Persona paciente_N9Savia = crearPaciente();

            paciente_N1Sura.EPS1 = "Sura";
            paciente_N2Sura.EPS1 = "Sura";
            paciente_N3NuevaEPS.EPS1 = "Nueva EPS";
            paciente_N4SaludTotal.EPS1 = "Salud Total";
            paciente_N5SaludTotal.EPS1 = "Salud Total";
            paciente_N6Sanitas.EPS1 = "Sanitas";
            paciente_N7Sanitas.EPS1 = "Sanitas";
            paciente_N8Savia.EPS1 = "Savia";
            paciente_N9Savia.EPS1 = "Savia";

            paciente_N1Sura.CostosTratamientos = 1000000;
            paciente_N2Sura.CostosTratamientos = 200000;
            paciente_N3NuevaEPS.CostosTratamientos = 2000000;
            paciente_N4SaludTotal.CostosTratamientos = 250000;
            paciente_N5SaludTotal.CostosTratamientos = 250000;
            paciente_N6Sanitas.CostosTratamientos = 1500000;
            paciente_N7Sanitas.CostosTratamientos = 1500000;
            paciente_N8Savia.CostosTratamientos = 600000;
            paciente_N9Savia.CostosTratamientos = 600000;

            target.ingresarPaciente(paciente_N1Sura);
            target.ingresarPaciente(paciente_N2Sura);
            target.ingresarPaciente(paciente_N3NuevaEPS);
            target.ingresarPaciente(paciente_N4SaludTotal);
            target.ingresarPaciente(paciente_N5SaludTotal);
            target.ingresarPaciente(paciente_N6Sanitas);
            target.ingresarPaciente(paciente_N7Sanitas);
            target.ingresarPaciente(paciente_N8Savia);
            target.ingresarPaciente(paciente_N9Savia);


            List<double> lista = target.calcularTotalCostosPorEPS();

            //Assert
            Assert.AreEqual(expectedSura, Math.Round(lista[0], 0));
            Assert.AreEqual(expectedNuevaEPS, Math.Round(lista[1], 0));
            Assert.AreEqual(expectedSaludTotal, Math.Round(lista[2], 0));
            Assert.AreEqual(expectedSanitas, Math.Round(lista[3], 0));
            Assert.AreEqual(expectedSavia, Math.Round(lista[4], 0));


        }

        [TestMethod()]
        public void calcularTotalPacientesCancerTest()
        {

            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target1 = new HistoriaClinica_Entrega2.Models.Clinica();
            int expected = 1;

            //Act
            Persona paciente = crearPaciente();
            target1.ingresarPaciente(paciente);
            Persona paciente_N2 = crearPaciente();
            paciente_N2.EnfermedadRelevante = "Sida";
            target1.ingresarPaciente(paciente_N2);

            double actual = target1.calcularTotalPacientesCancer();

            //Assert
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod()]
        public void calcularPorcentajesPorEdadTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = new HistoriaClinica_Entrega2.Models.Clinica();
            int expected = 20;

            //Act
            Persona paciente_N1 = crearPaciente();
            Persona paciente_N2 = crearPaciente();
            Persona paciente_N3 = crearPaciente();
            Persona paciente_N4 = crearPaciente();
            Persona paciente_N5 = crearPaciente();

            paciente_N1.FechaNacimiento = new DateTime(1980, 07, 19);
            paciente_N2.FechaNacimiento = new DateTime(2017, 06, 06);
            paciente_N3.FechaNacimiento = new DateTime(2005, 04, 07);
            paciente_N4.FechaNacimiento = new DateTime(1960, 12, 06);
            paciente_N5.FechaNacimiento = new DateTime(1914, 11, 11);
 

            target.ingresarPaciente(paciente_N1);
            target.ingresarPaciente(paciente_N2);
            target.ingresarPaciente(paciente_N3);
            target.ingresarPaciente(paciente_N4);
            target.ingresarPaciente(paciente_N5);


            List<double> actual = target.calcularPorcentajesPorEdad();

            //Assert
            Assert.AreEqual(expected, Math.Round(actual[0], 0));
            Assert.AreEqual(expected, Math.Round(actual[1], 0));
            Assert.AreEqual(expected, Math.Round(actual[3], 0));
            Assert.AreEqual(expected, Math.Round(actual[4], 0));
            Assert.AreEqual(expected, Math.Round(actual[5], 0));

        }

        [TestMethod()]
        public void encontrarMayorCosto()
        {

            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = new HistoriaClinica_Entrega2.Models.Clinica();
            int expected = 1000000;

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);
            Persona paciente_N2 = crearPaciente();
            paciente_N2.CostosTratamientos = 800000;
            target.ingresarPaciente(paciente_N2);

            Persona actual = target.encontrarMayorCosto();

            //Assert
            Assert.AreEqual(expected, actual.CostosTratamientos);



        }

        [TestMethod()]
        public void calcularPacientesPorRegimenTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = new HistoriaClinica_Entrega2.Models.Clinica();
            int expected = 50;

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);
            Persona paciente_N2 = crearPaciente();
            paciente_N2.TipoRegimen = "Subsidiado";
            target.ingresarPaciente(paciente_N2);

            List<double> actual = target.calcularPacientesPorRegimen();

            //Assert
            Assert.AreEqual(expected, actual[0]);
            Assert.AreEqual(expected, actual[1]);
            
        }

        [TestMethod()]
        public void calcularPorcentajePacientesPorTipoAfiliacionTest()
        {
            //Arrange
            HistoriaClinica_Entrega2.Models.Clinica target = new HistoriaClinica_Entrega2.Models.Clinica();
            int expected = 50;

            //Act
            Persona paciente = crearPaciente();
            target.ingresarPaciente(paciente);
            Persona paciente_N2 = crearPaciente();
            paciente_N2.TipoAfiliacion = "Beneficiario";
            target.ingresarPaciente(paciente_N2);

            List<double> actual = target.calcularPorcentajePacientesPorTipoAfiliacion();

            //Assert
            Assert.AreEqual(expected, actual[0]);
            Assert.AreEqual(expected, actual[1]);

        }


    }
}