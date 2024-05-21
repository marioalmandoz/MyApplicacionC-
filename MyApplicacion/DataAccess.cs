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
        public int AnadirPallet(string pReferencia, string pBaan, string pCajas, String pPiezas) 
        {
            try
            {
                string query = "INSERT INTO Pallet (referencia, baan, nCajas, nPiezas, fecha_hora, produccion) VALUES (?, ?, ?, ?, ?, ?)";
                DateTime now = DateTime.Now;
                bool produccion = true;
                int rowsAffected = _database.Execute(query, pReferencia, pBaan, pCajas, pPiezas, now, produccion);

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
        public List<Pallet> MostrarAlmacen(string pReferencia)
        {
            try
            {
                string query = "SELECT * FROM Pallet WHERE almacen = 1 AND referencia = ?;";
                bool almacen = true; // Queremos filtrar donde almacen es falso
                var result = _database.Query<Pallet>(query, pReferencia);
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
        public void RetirarPiezas(int nPiezas, int pId)
        {
            try
            {
                // Consulta SQL para actualizar el campo nPiezas
                string query = "UPDATE Pallet SET nPiezas = nPiezas - ? WHERE Id = ?";
                // Consulta SQL para obtener el valor actual de nPiezas después de la actualización
                string selectQuery = "SELECT nPiezas FROM Pallet WHERE Id = ?";

                // Ejecutar la consulta con los parámetros proporcionados
                int rowsAffected = _database.Execute(query, nPiezas, pId);

                // Verificar si se afectaron filas y mostrar un mensaje apropiado
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"{nPiezas} piezas retiradas correctamente del palet con ID: {pId}");

                    // Obtener el valor actualizado de nPiezas
                    int updatedNPiezas = _database.ExecuteScalar<int>(selectQuery, pId);

                    // Si nPiezas es 0 o menor, eliminar el pallet
                    if (updatedNPiezas <= 0)
                    {
                        int rowsDeleted = EliminarPallet(pId);
                        if (rowsDeleted > 0)
                        {
                            Console.WriteLine($"El pallet con ID: {pId} ha sido eliminado porque nPiezas es {updatedNPiezas}.");
                        }
                        else
                        {
                            Console.WriteLine($"No se pudo eliminar el pallet con ID: {pId}. Verifique el ID proporcionado.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo retirar piezas del palet. Verifique el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al retirar piezas del palet: {ex.Message}");
            }
        }
        public void EditarPallet(Pallet item)
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
                }
                else
                {
                    Console.WriteLine($"No se pudo actualizar el pallet con ID: {item.Id}. Verifique el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el pallet: {ex.Message}");
            }
        }
        public int getPalletUbica(string pUbicacion, string pReferencia)
        {
            try
            {
                // Consulta SQL para obtener el ID del pallet basado en la ubicación y la referencia
                string query = "SELECT Id FROM Pallet WHERE ubicacion = ? AND referencia = ?";

                // Ejecutar la consulta con los parámetros proporcionados
                int palletId = _database.ExecuteScalar<int>(query, pUbicacion, pReferencia);

                // Devolver el ID del pallet encontrado
                return palletId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el ID del pallet: {ex.Message}");
            }
        }
    }
}
