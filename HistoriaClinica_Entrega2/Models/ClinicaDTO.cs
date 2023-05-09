using HistoriaClinica_Entrega2.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace HistoriaClinica_Entrega2.Models
{
    public class ClinicaDTO
    {
        static Conexion connection = new Conexion();
        SqlConnection sqlConnection = new SqlConnection(connection.conexion);
        

        /*public DataSet buscarTodos()
        {

            String query = "SELECT IdCliente, TipoDocumento, NumeroDocumento, Nombre, Apellidos, Direccion, Celular, FhNacimiento, Email FROM tbClientes";

            sqlConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Clientes");
            sqlConnection.Close();

            return dataSet;

        }*/

        public List<Persona> ObtnerInformacionPacientesBD()
        {
            List<Persona> lista = new List<Persona>();

            using (SqlConnection sqlConnection = new SqlConnection(connection.conexion))
            {
                sqlConnection.Open();

                string query = @"SELECT p.identificacion, p.nombre, p.apellidos, p.fechaNacimiento, 
                        pa.id_Paciente, pa.historiaClinica, pa.cantidadEnfermedades, pa.enfermedadMasRelevante, pa.costoTratamientos,
                        t.id_Trabajador, t.tipoRegimen, t.semanasCotizadas, t.fechaIngreso, t.fechaIngresoEPS, t.Eps, t.tipoAfiliacion
                        FROM TbPersona p
                        JOIN TbPaciente pa ON p.id_Paciente = pa.id_Paciente
                        JOIN TbTrabajador t ON p.id_Trabajador = t.id_Trabajador";
            

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Paciente paciente = new Paciente(
                                reader["historiaClinica"].ToString(),
                                Convert.ToInt32(reader["cantidadEnfermedades"]),
                                reader["enfermedadMasRelevante"].ToString(),
                                Convert.ToDouble(reader["costoTratamientos"]));

                            Trabajador trabajador = new Trabajador(
                                reader["tipoRegimen"].ToString(),
                                Convert.ToInt32(reader["semanasCotizadas"]),
                                Convert.ToDateTime(reader["fechaIngreso"]),
                                Convert.ToDateTime(reader["fechaIngresoEPS"]),
                                reader["Eps"].ToString(),
                                reader["tipoAfiliacion"].ToString());

                            Persona PersonaBD = new Persona(
                                Convert.ToInt32(reader["identificacion"]),
                                reader["nombre"].ToString(),
                                reader["apellidos"].ToString(),
                                Convert.ToDateTime(reader["fechaNacimiento"]),
                                trabajador, paciente );

                            lista.Add(PersonaBD);
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lista;
        }

        public void ingresarPaciente(Persona paciente)
        {

            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();

                // Inicia una transacción
                SqlTransaction transaction = sqlconnection.BeginTransaction();

                try
                {
                    // Inserta en la tabla TbPaciente
                    using (SqlCommand command = new SqlCommand("INSERT INTO TbPaciente (historiaClinica, cantidadEnfermedades, enfermedadMasRelevante, costoTratamientos) VALUES (@historiaClinica, @cantidadEnfermedades, @enfermedadMasRelevante, @costoTratamientos); SELECT SCOPE_IDENTITY()", sqlconnection, transaction))
                    {
                        command.Parameters.AddWithValue("@historiaClinica", paciente.InformacionPaciente.HistoriaClinica);
                        command.Parameters.AddWithValue("@cantidadEnfermedades", paciente.InformacionPaciente.CantidadEnfermedades);
                        command.Parameters.AddWithValue("@enfermedadMasRelevante", paciente.InformacionPaciente.EnfermedadRelevante);
                        command.Parameters.AddWithValue("@costoTratamientos", paciente.InformacionPaciente.CostosTratamientos);

                        // Obtiene el ID recién insertado
                        int pacienteId = Convert.ToInt32(command.ExecuteScalar());

                        // Inserta en la tabla TbTrabajador
                        using (SqlCommand command2 = new SqlCommand("INSERT INTO TbTrabajador (tipoRegimen, semanasCotizadas, fechaIngreso, fechaIngresoEPS, Eps, tipoAfiliacion) VALUES (@tipoRegimen, @semanasCotizadas, @fechaIngreso, @fechaIngresoEPS, @Eps, @tipoAfiliacion); SELECT SCOPE_IDENTITY()", sqlconnection, transaction))
                        {
                            command2.Parameters.AddWithValue("@tipoRegimen", paciente.Trabajador.TipoRegimen);
                            command2.Parameters.AddWithValue("@semanasCotizadas", paciente.Trabajador.SemanasCotizadas);
                            command2.Parameters.AddWithValue("@fechaIngreso", paciente.Trabajador.FechaIngreso);
                            command2.Parameters.AddWithValue("@fechaIngresoEPS", paciente.Trabajador.FechaIngresoEPS);
                            command2.Parameters.AddWithValue("@Eps", paciente.Trabajador.EPS);
                            command2.Parameters.AddWithValue("@tipoAfiliacion", paciente.Trabajador.TipoAfiliacion);

                            // Obtiene el ID recién insertado
                            int trabajadorId = Convert.ToInt32(command2.ExecuteScalar());

                            // Inserta en la tabla TbPersona
                            using (SqlCommand command3 = new SqlCommand("INSERT INTO TbPersona (identificacion, nombre, apellidos, fechaNacimiento, id_Paciente, id_Trabajador) VALUES (@identificacion, @nombre, @apellidos, @fechaNacimiento, @id_Paciente, @id_Trabajador)", sqlconnection, transaction))
                            {
                                command3.Parameters.AddWithValue("@identificacion", paciente.Identificacion);
                                command3.Parameters.AddWithValue("@nombre", paciente.Nombre);
                                command3.Parameters.AddWithValue("@apellidos", paciente.Apellidos);
                                command3.Parameters.AddWithValue("@fechaNacimiento", paciente.FechaNacimiento);
                                command3.Parameters.AddWithValue("@id_Paciente", pacienteId); // Usa el ID del paciente recién insertado
                                command3.Parameters.AddWithValue("@id_Trabajador", trabajadorId); // Usa el ID del trabajador recién insertado

                                // Ejecuta la sentencia
                                command3.ExecuteNonQuery();     
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    // Si hay un error, hace un rollback de la transacción
                    transaction.Rollback();

                    throw ex;
                }

            }
        }

        public void EliminarPaciente(int idPaciente)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlTransaction transaction = sqlconnection.BeginTransaction();

                    SqlCommand command = new SqlCommand("SELECT id_Trabajador FROM TbPersona WHERE identificacion = @identificacion", sqlconnection,transaction);
                    command.Parameters.AddWithValue("@identificacion", idPaciente);

                    int idTrabajadorPersonaBD = Convert.ToInt32(command.ExecuteScalar());

                    SqlCommand command2 = new SqlCommand("SELECT id_Paciente FROM TbPersona WHERE identificacion = @identificacion", sqlconnection,transaction);
                    command2.Parameters.AddWithValue("@identificacion", idPaciente);

                    int idPacientePersonaBD = Convert.ToInt32(command.ExecuteScalar());


                SqlCommand commandPersona = new SqlCommand("DELETE FROM TbPersona WHERE identificacion = @idPersona", sqlconnection, transaction);
                    commandPersona.Parameters.AddWithValue("@idPersona", idPaciente);
                    commandPersona.ExecuteNonQuery();
                // Eliminar registro de TbTrabajador
                    SqlCommand commandTrabajador = new SqlCommand("DELETE FROM TbTrabajador WHERE id_Trabajador = @idTrabajador", sqlconnection, transaction);
                    commandTrabajador.Parameters.AddWithValue("@idTrabajador", idTrabajadorPersonaBD);
                    commandTrabajador.ExecuteNonQuery();

                    // Eliminar registro de TbPaciente
                    SqlCommand commandPaciente = new SqlCommand("DELETE FROM TbPaciente WHERE id_Paciente = @idPaciente", sqlconnection, transaction);
                    commandPaciente.Parameters.AddWithValue("@idPaciente", idPacientePersonaBD);
                    commandPaciente.ExecuteNonQuery();

                    // Eliminar registro de TbPersona
                    

                    // Confirmar la transacción
                    transaction.Commit();
                    sqlconnection.Close();
                
            }
        }
        

        public void cambiarTipoRegimenBD(Persona paciente, string TipoRegimen)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Trabajador FROM TbPersona WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", paciente.Identificacion);

                int idTrabajadorPersonaBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbTrabajador SET tipoRegimen = @nuevoTipoReimgen WHERE id_Trabajador = @id_Trabajador";
                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Trabajador", idTrabajadorPersonaBD),
                        new SqlParameter("@nuevoTipoRegimen", TipoRegimen)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();
                sqlconnection.Close();
            }
        }

        public void cambiarHistoriaClinicaBD(Persona paciente, string nuevaHistoriaClinica)
        {

            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPersona WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", paciente.Identificacion);

                int idPacientePersonaBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbPaciente SET historiaClinica = @nuevaHistoriaClinica WHERE id_Paciente = @id_Paciente";
                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Paciente", idPacientePersonaBD),
                        new SqlParameter("@nuevaHistoriaClinica", nuevaHistoriaClinica)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();
                sqlconnection.Close();
            }
        }

        public void cambiarCostoTratamientosBD(Persona paciente, int nuevoCostoTratamientos)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPersona WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", paciente.Identificacion);

                int idPacientePersonaBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbPaciente SET costoTratamientos = @nuevoCosto WHERE id_Paciente = @id_Paciente";
                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Paciente", idPacientePersonaBD),
                        new SqlParameter("@nuevoCosto", nuevoCostoTratamientos)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();
                sqlconnection.Close();
            }
        }

        public void cambiarEnfermedadRelevante(Persona paciente, string enfermedad)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPersona WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", paciente.Identificacion);

                int idPacientePersonaBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbPaciente SET enfermedadMasRelevante = @nuevaEnfermedadRelevante WHERE id_Paciente = @id_Paciente";
                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Paciente", idPacientePersonaBD),
                        new SqlParameter("@nuevaEnfermedadRelevante", enfermedad)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();
                sqlconnection.Close();
            }
        }

        public void CambioEPSBD(int identificacion, string EPS)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Trabajador FROM TbPersona WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", identificacion);

                int idTrabajadorPersonaBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbTrabajador SET Eps = @nuevaEPS, fechaIngresoEPS = GETDATE() WHERE id_Trabajador = @id_Trabajador";

                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Trabajador", idTrabajadorPersonaBD),
                        new SqlParameter("@nuevaEPS", EPS)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());
                

                updateCommand.ExecuteNonQuery();
                
                sqlconnection.Close();
            }
        }

    }
        

}