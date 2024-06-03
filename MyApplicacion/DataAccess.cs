using MyApplicacion.Database;
using SQLite;

namespace MyApplicacion
{
    public class DataAccess
    {
        readonly SQLiteConnection _database;
        public DataAccess(String dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Pallet>();
        }

        public void AgregarPallet(string pRef, string pUbica)
        {
            try
            {
                string query = "INSERT INTO Pallet (referencia, ubicacion, fecha_hora) VALUES (?,?,?)";
                DateTime now = DateTime.Now;
                int rowsAffected = _database.Execute(query, pRef, pUbica, now);

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Elemento agregado correctamente a la base de datos.");
                }
                else
                {
                    Console.WriteLine("No se pudo agregar el elemento a la base de datos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int AnadirPallet(string pReferencia, string pBaan, string pCajas, String pPiezas, int nPiezasCaja) 
        {
            try
            {
                string query = "INSERT INTO Pallet (referencia, baan, nCajas, nPiezas, nPiezasCaja, fecha_hora, produccion) VALUES (?, ?, ?, ?, ?, ?, ?)";
                DateTime now = DateTime.Now;
                bool produccion = true;
                int rowsAffected = _database.Execute(query, pReferencia, pBaan, pCajas, pPiezas, nPiezasCaja, now, produccion);

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Elemento agregado correctamente a la base de datos.");
                    return rowsAffected;
                }
                else
                {
                    Console.WriteLine("No se pudo agregar el elemento a la base de datos.");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Pallet> MostrarDatos()
        {
            try
            {
                var query = "SELECT * FROM Pallet;";
                var result = _database.Query<Pallet>(query); // Ejecutar la consulta y obtener los resultados
                foreach (var pallet in result)
                {
                    Console.WriteLine($"Id: {pallet.Id}, Referencia: {pallet.referencia}, almacen: {pallet.almacen}"); // Muestra cada objeto Pallet
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Pallet> MostrarProducido(string pReferencia)
        {
            try
            {
                string query = "SELECT * FROM Pallet WHERE almacen IS NULL AND referencia = ?;";
                bool almacen = false; // Queremos filtrar donde almacen es falso
                var result = _database.Query<Pallet>(query, pReferencia);
                foreach (var pallet in result)
                {
                    Console.WriteLine($"Id: {pallet.Id}, Referencia: {pallet.referencia}, almacen: {pallet.almacen}"); // Muestra cada objeto Pallet
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar los pallets producidos: " + ex.Message);
            }
        }
        public List<Pallet> MostrarAlmacen(string pbaan)
        {
            try
            {
                string query = "SELECT * FROM Pallet WHERE almacen = 1 AND baan = ?;";
                bool almacen = true; // Queremos filtrar donde almacen es falso
                var result = _database.Query<Pallet>(query, pbaan);
                foreach (var pallet in result)
                {
                    Console.WriteLine($"Id: {pallet.Id}, Referencia: {pallet.referencia}, almacen: {pallet.almacen}"); // Muestra cada objeto Pallet
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar los pallets Almacen: " + ex.Message);
            }
        }
        public string getReferencia(int pId)
        {
            try
            {
                // Consulta SQL para obtener la referencia del pallet con el Id proporcionado
                string query = "SELECT referencia FROM Pallet WHERE Id = ?";

                // Ejecutar la consulta y obtener la referencia
                string referencia = _database.ExecuteScalar<string>(query, pId);

                return referencia;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la referencia: {ex.Message}");
            }
        }
        public bool ComprobarBaan(string pBaan)
        {
            try
            {
                // Consulta SQL para contar los pallets con la referencia proporcionada
                string query = "SELECT COUNT(*) FROM Pallet WHERE baan = ?";

                // Ejecutar la consulta y obtener el conteo
                int count = _database.ExecuteScalar<int>(query, pBaan);

                // Si el conteo es mayor que cero, significa que hay pallets con esa referencia
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al comprobar la referencia: {ex.Message}");
            }
        }
        public bool ComprobarReferencia(string pReferencia)
        {
            try
            {
                // Consulta SQL para contar los pallets con la referencia proporcionada
                string query = "SELECT COUNT(*) FROM Pallet WHERE referencia = ?";

                // Ejecutar la consulta y obtener el conteo
                int count = _database.ExecuteScalar<int>(query, pReferencia);

                // Si el conteo es mayor que cero, significa que hay pallets con esa referencia
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al comprobar la referencia: {ex.Message}");
            }
        }
        public void ModificarUbicacion(int pId, string pUbicacion)
        {
            try
            {
                // Consulta SQL para actualizar el campo ubicacion
                string query = "UPDATE Pallet SET ubicacion = ? WHERE Id = ?";

                // Ejecutar la consulta con los parámetros proporcionados
                int rowsAffected = _database.Execute(query, pUbicacion, pId);

                // Verificar si se afectaron filas y mostrar un mensaje apropiado
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Ubicación actualizada correctamente para el palet con ID: {pId}");
                }
                else
                {
                    Console.WriteLine("No se pudo actualizar la ubicación del palet. Verifique el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la ubicación del palet: {ex.Message}");
            }
        }
        public void tickAlmacen(int pId)
        {
            try
            {
                string query = "UPDATE Pallet SET almacen = ? WHERE Id = ?";
                bool almacen = true; // Queremos establecer almacen a verdadero
                int rowsAffected = _database.Execute(query, almacen, pId);

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"El atributo 'almacen' del pallet con ID {pId} se ha actualizado correctamente a true.");
                }
                else
                {
                    Console.WriteLine($"No se pudo actualizar el atributo 'almacen' del pallet con ID {pId}.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el atributo 'almacen': " + ex.Message);
            }
        }
        public int EliminarPallet(int pId)
        {
            try
            {
                var query = "DELETE FROM Pallet WHERE Id = ?";
                var affectedRows = _database.Execute(query, pId); // Ejecutar la consulta para eliminar el pallet con el Id proporcionado
                return affectedRows; // Devuelve el número de filas afectadas
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int addIncidencias(int pId, string pIncidencia)
        {
            try
            {
                string query = "UPDATE Pallet SET incidencia = ?, almacen = ? WHERE Id = ?";
                bool almacen = true;
                int rowsAffected = _database.Execute(query, pIncidencia,almacen, pId);

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Incidencia agregada correctamente al palet con ID: " + pId);
                    return rowsAffected;
                }
                else
                {
                    Console.WriteLine("No se pudo agregar la incidencia al palet con ID: " + pId);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la incidencia al palet: " + ex.Message);
            }
        
        }
        public int RetirarCajas(string pNCajas, int pId)
        {
            try
            {
                // Validar que pNCajas sea un string no vacío
                if (string.IsNullOrWhiteSpace(pNCajas))
                {
                    throw new ArgumentException("El número de cajas proporcionado no puede estar vacío.");
                }

                // Convertir el número de cajas proporcionado a un entero
                int newNCajas = int.Parse(pNCajas);

                // Consulta SQL para obtener los valores actuales de nCajas y nPiezasCaja para el pallet con el pId proporcionado
                string selectQuery = "SELECT nCajas, nPiezasCaja FROM Pallet WHERE Id = ?";
                var palletData = _database.Query<Pallet>(selectQuery, pId).FirstOrDefault();

                if (palletData == null)
                {
                    throw new Exception("No se encontró el pallet con el ID proporcionado.");
                }

                int currentNCajas = int.Parse(palletData.nCajas);
                int nPiezasCaja = palletData.nPiezasCaja;

                if (nPiezasCaja <= 0)
                {
                    throw new Exception("El valor de nPiezasCaja debe ser mayor que 0.");
                }

                // Calcular el nuevo valor de nCajas restando las cajas nuevas de las actuales
                int updatedNCajas = currentNCajas - newNCajas;
                if (updatedNCajas < 0)
                {
                    updatedNCajas = 0;  // Asegurarse de que el número de cajas no sea negativo
                }

                // Calcular el nuevo valor de nPiezas basado en el número actualizado de cajas
                int updatedNPiezas = updatedNCajas * nPiezasCaja;

                // Consulta SQL para actualizar el campo nCajas y nPiezas
                string updateQuery = "UPDATE Pallet SET nCajas = ?, nPiezas = ? WHERE Id = ?";

                // Ejecutar la consulta con los parámetros proporcionados
                int rowsAffected = _database.Execute(updateQuery, updatedNCajas, updatedNPiezas, pId);

                // Verificar si se afectaron filas y mostrar un mensaje apropiado
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Número de cajas actualizado a {updatedNCajas} y número de piezas actualizado a {updatedNPiezas} para el pallet con ID: {pId}");
                }
                else
                {
                    Console.WriteLine("No se pudo actualizar el número de cajas del pallet. Verifique el ID proporcionado.");
                }

                return rowsAffected; // Devolver el número de filas afectadas
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el número de cajas del pallet: {ex.Message}");
            }
        }
        public int EditarPallet(Pallet item)
        {
            try
            {
                // Consulta SQL para actualizar todos los campos de un pallet específico por su Id
                string query = @"
            UPDATE Pallet
            SET fecha_hora = ?,
                referencia = ?,
                baan = ?,
                nPiezas = ?,
                nCajas = ?,
                ubicacion = ?,
                almacen = ?,
                produccion = ?,
                rec = ?,
                incidencia = ?
            WHERE Id = ?";

                // Ejecutar la consulta con los parámetros proporcionados
                int rowsAffected = _database.Execute(
                    query,
                    item.fecha_hora,
                    item.referencia,
                    item.baan,
                    item.nPiezas,
                    item.nCajas,
                    item.ubicacion,
                    item.almacen,
                    item.produccion,
                    item.rec,
                    item.incidencia,
                    item.Id
                );

                // Verificar si se afectaron filas y mostrar un mensaje apropiado
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Pallet con ID: {item.Id} actualizado correctamente.");
                    return rowsAffected;
                }
                else
                {
                    Console.WriteLine($"No se pudo actualizar el pallet con ID: {item.Id}. Verifique el ID proporcionado.");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el pallet: {ex.Message}");
            }
        }
        public int getPalletUbica(string pUbicacion, string pBaan)
        {
            try
            {
                // Consulta SQL para obtener el ID del pallet basado en la ubicación y la referencia
                string query = "SELECT Id FROM Pallet WHERE ubicacion = ? AND baan = ? LIMIT 1";

                // Ejecutar la consulta con los parámetros proporcionados
                int palletId = _database.ExecuteScalar<int>(query, pUbicacion, pBaan);

                // Devolver el ID del pallet encontrado
                return palletId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el ID del pallet: {ex.Message}");
            }
        }
        public int EditarCajas(string pNCajas, int pId, string pCantidad)
        {
            try
            {
                // Validar que pNCajas sea un string no vacío
                if (string.IsNullOrWhiteSpace(pNCajas))
                {
                    throw new ArgumentException("El número de cajas proporcionado no puede estar vacío.");
                }

                // Consulta SQL para actualizar el campo nCajas
                string query = "UPDATE Pallet SET nCajas = ?, nPiezas = ? WHERE Id = ?";

                // Ejecutar la consulta con los parámetros proporcionados
                int rowsAffected = _database.Execute(query, pNCajas,pCantidad, pId);

                // Verificar si se afectaron filas y mostrar un mensaje apropiado
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Número de cajas actualizado a {pNCajas} para el pallet con ID: {pId}");
                }
                else
                {
                    Console.WriteLine("No se pudo actualizar el número de cajas del pallet. Verifique el ID proporcionado.");
                }

                return rowsAffected; // Devolver el número de filas afectadas
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el número de cajas del pallet: {ex.Message}");
            }
        }
    }
}
