﻿using MyApplicacion.Database;
using SQLite;



namespace MyApplicacion
{
    public class PalletRepository(string path)
    {
        string _dbPath = path;
        int cant; 
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;
        private async Task Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            await conn.CreateTableAsync<MyApplicacion.Database.Pallet>();

        }
        public async Task<int> GetNumberOfPallets()
        {
            try
            {
                await Init();

                // Realizar la consulta para contar el número de registros en la tabla de pallets
                var count = await conn.Table<Pallet>().CountAsync();

                return count;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                StatusMessage = $"Error al obtener el número de pallets: {ex.Message}";
                return -1; // Valor de error
            }
        }

        public async Task AddNewPallet(string pReferencia, string pUbicacion, String pCant)
        {
            //Numero de pallets en la case de datos
            cant = await GetNumberOfPallets();
            if (cant >= 0)
            {
                Console.WriteLine(cant);


                try
                {
                    await Init();
                    DateTime dateTime = DateTime.Now;
                    

                    if (string.IsNullOrEmpty(pReferencia))
                        throw new Exception("Valid reference required");
                    if (string.IsNullOrEmpty(pUbicacion))
                        throw new Exception("Valid reference required");

                    MyApplicacion.Database.Pallet pallet = new() { Id = cant + 1, fecha_hora = dateTime, referencia = pReferencia, ubicacion = pUbicacion, produccion = true, cant= pCant };
                    await conn.InsertAsync(pallet);

                    StatusMessage = string.Format("{0} record(S) added (Name: {1})", pReferencia, pUbicacion);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task tickAlmacen(DateTime pFecha)
        {
            // Realizar una consulta para verificar si existe algún pallet con la referencia dada
            var palletExistente = await conn.Table<Pallet>().FirstOrDefaultAsync(p => p.fecha_hora == pFecha);
            if (palletExistente != null)
            {
                // Si se encuentra un pallet con la referencia dada, mostrar un mensaje indicando que existe
                StatusMessage = $"La fecha '{pFecha}' existe en la base de datos";
                palletExistente.almacen = true;
                await conn.UpdateAsync(palletExistente);

                // Consultar el pallet actualizado de la base de datos
                var palletActualizado = await conn.Table<Pallet>().FirstOrDefaultAsync(p => p.fecha_hora == pFecha);

                // Verificar si el pallet se ha actualizado correctamente
                if (palletActualizado != null && palletActualizado.almacen)
                {
                    // Si el pallet se ha actualizado correctamente, mostrar un mensaje indicando que se ha introducido el nuevo valor correctamente
                    StatusMessage = $"El valor del almacen se ha introducido correctamente para la propiedad 'PropiedadAModificar' del pallet con fecha '{pFecha}'";
                }
                else
                {
                    // Si no se ha actualizado correctamente, mostrar un mensaje de error
                    StatusMessage = "Error al actualizar el pallet en la base de datos";
                }
            }
            else
            {
                // Si no se encuentra ningún pallet con la fecha dada, Indicar que ese palet no existe en la base de datos
                StatusMessage = $"La Fecha '{pFecha}' no existe en la base de datos";
            }
        }

        public async Task tickRecambio(DateTime pFecha)
        {
            // Realizar una consulta para verificar si existe algún pallet con la referencia dada
            var palletExistente = await conn.Table<Pallet>().FirstOrDefaultAsync(p => p.fecha_hora == pFecha);
            if (palletExistente != null)
            {
                // Si se encuentra un pallet con la referencia dada, mostrar un mensaje indicando que existe
                StatusMessage = $"La fecha '{pFecha}' existe en la base de datos";
                palletExistente.rec = true;
                await conn.UpdateAsync(palletExistente);

                // Consultar el pallet actualizado de la base de datos
                var palletActualizado = await conn.Table<Pallet>().FirstOrDefaultAsync(p => p.fecha_hora == pFecha);

                // Verificar si el pallet se ha actualizado correctamente
                if (palletActualizado != null && palletActualizado.rec)
                {
                    // Si el pallet se ha actualizado correctamente, mostrar un mensaje indicando que se ha introducido el nuevo valor correctamente
                    StatusMessage = $"El valor del recambio se ha introducido correctamente para la propiedad 'PropiedadAModificar' del pallet con fecha '{pFecha}'";
                }
                else
                {
                    // Si no se ha actualizado correctamente, mostrar un mensaje de error
                    StatusMessage = "Error al actualizar el pallet en la base de datos";
                }
            }
            else
            {
                // Si no se encuentra ningún pallet con la fecha dada, Indicar que ese palet no existe en la base de datos
                StatusMessage = $"La Fecha '{pFecha}' no existe en la base de datos";
            }
        }

        
        public async Task AddUbicacion(DateTime pFecha, string pUbicacion)
        {
            // Realizar una consulta para verificar si existe algún pallet con la referencia dada
            var palletExistente = await conn.Table<Pallet>().FirstOrDefaultAsync(p => p.fecha_hora == pFecha);

            if (palletExistente != null)
            {
                // Si se encuentra un pallet con la referencia dada, mostrar un mensaje indicando que existe
                StatusMessage = $"La fecha '{pFecha}' existe en la base de datos";

                palletExistente.ubicacion = pUbicacion;
                await conn.UpdateAsync(palletExistente);

                // Consultar el pallet actualizado de la base de datos
                var palletActualizado = await conn.Table<Pallet>().FirstOrDefaultAsync(p => p.fecha_hora == pFecha);

                // Verificar si el pallet se ha actualizado correctamente
                if (palletActualizado != null && palletActualizado.ubicacion == pUbicacion)
                {
                    // Si el pallet se ha actualizado correctamente, mostrar un mensaje indicando que se ha introducido el nuevo valor correctamente
                    StatusMessage = $"El valor '{pUbicacion}' se ha introducido correctamente para la propiedad 'PropiedadAModificar' del pallet con fecha '{pFecha}'";
                }
                else
                {
                    // Si no se ha actualizado correctamente, mostrar un mensaje de error
                    StatusMessage = "Error al actualizar el pallet en la base de datos";
                }
            }
            else
            {
                // Si no se encuentra ningún pallet con la fecha dada, Indicar que ese palet no existe en la base de datos
                StatusMessage = $"La Fecha '{pFecha}' no existe en la base de datos";
            }
        }

        public async Task<List<MyApplicacion.Database.Pallet>> GetAll()
        {
            try
            {
                await Init();
                return await conn.Table<MyApplicacion.Database.Pallet>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error para mostar los datos. {0}", ex.Message);
            }
            return new List<MyApplicacion.Database.Pallet>();
        }
        public async Task EliminarPallets()
        {
            try
            {
                await Init();
                int id = 1;
                //await conn.DeleteAllAsync<Pallet>();
                // Buscar el pallet por su ID
                var palletAEliminar = await conn.FindAsync<Pallet>(id);

                if (palletAEliminar != null)
                {
                    // Si se encontró un pallet con el ID dado, eliminarlo
                    await conn.DeleteAllAsync<Pallet>();
                    StatusMessage = "Pallet eliminado correctamente";
                }
                else
                {
                    // Si no se encontró ningún pallet con el ID dado, mostrar un mensaje de error
                    StatusMessage = "No se encontró ningún pallet con el ID especificado";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar el pallet: {ex.Message}";
            }
        }
        public async Task EliminarPalletPorId(int pId)
        {
            try
            {
                await Init();

                Console.WriteLine(pId);
                var palletAEliminar = await conn.FindAsync<Pallet>(p => p.Id == pId);
                Console.WriteLine(palletAEliminar);

                if (palletAEliminar!=null)
                {
                    // Si se encontró un pallet con el ID dado, eliminarlo
                    await conn.DeleteAsync(palletAEliminar);
                    StatusMessage = $"Pallet eliminado correctamente";
                    
                }
                else
                {
                    // Si no se encontró ningún pallet con el ID dado, mostrar un mensaje de error
                    StatusMessage = "No se encontró ningún pallet con el Id especificada";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar el pallet: {ex.Message}";
            }
        }
        public async Task<bool> ComprobarReferencia(string pReferencia)
        {
            Boolean existe = false;
            try
            {
                // Inicializar la conexión con la base de datos
                await Init();

                // Verificar si la referencia está vacía
                if (string.IsNullOrEmpty(pReferencia))
                    throw new Exception("La referencia no puede estar vacía");

                // Realizar una consulta para verificar si existe algún pallet con la referencia dada
                var palletExistente = await conn.Table<Pallet>().FirstOrDefaultAsync(p => p.referencia == pReferencia);

                if (palletExistente != null)
                {
                    // Si se encuentra un pallet con la referencia dada, mostrar un mensaje indicando que existe
                    StatusMessage = $"La referencia '{pReferencia}' existe en la base de datos";
                    existe = true;
                }
                else
                {
                    // Si no se encuentra ningún pallet con la referencia dada, mostrar un mensaje indicando que no existe
                    StatusMessage = $"La referencia '{pReferencia}' no existe en la base de datos";
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción y mostrar un mensaje de error
                StatusMessage = $"Error al comprobar la referencia: {ex.Message}";
            }
            return existe;
        }

        public async Task<List<MyApplicacion.Database.Pallet>> MostrarReferencias(string pReferencia)
        {
            try
            {
                // Verificar si la referencia existe en la base de datos
                bool existeReferencia = await ComprobarReferencia(pReferencia);

                if (existeReferencia)
                {
                    // Si la referencia existe, inicializar la conexión con la base de datos
                    await Init();

                    // Realizar una consulta para obtener todos los pallets con la referencia dada
                    var pallets = await conn.Table<Pallet>().Where(p => p.referencia == pReferencia).ToListAsync();

                    // Devolver la lista de pallets encontrados
                    return pallets;
                }
                else
                {
                    // Si la referencia no existe, devolver una lista vacía
                    return new List<Pallet>();
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción y mostrar un mensaje de error
                StatusMessage = $"Error al mostrar las referencias: {ex.Message}";
                return new List<Pallet>();
            }
        }

        public async Task RetirarPiezas(string cant, DateTime fecha)
        {
            try
            {
                await Init();
                int cantidad = int.Parse(cant);

                var pallet = await conn.Table<Pallet>()
                                .Where(p => p.fecha_hora == fecha)
                                .FirstOrDefaultAsync();

                if (pallet != null)
                {
                    // Convertir la cantidad del pallet de string a int
                    int cantidadPallet = int.Parse(pallet.cant);

                    // Restar la cantidad proporcionada a la cantidad del pallet
                    cantidadPallet -= cantidad;

                    // Convertir la nueva cantidad de int a string
                    pallet.cant = cantidadPallet.ToString();

                    // Actualizar el pallet en la base de datos
                    await conn.UpdateAsync(pallet);

                    // Mensaje de éxito
                    StatusMessage = $"Cantidad retirada del pallet con fecha {fecha}: {cantidad}";
                }
                else
                {
                    // Mensaje de error si no se encuentra el pallet
                    StatusMessage = $"No se encontró ningún pallet con la fecha especificada: {fecha}";
                }

            }
            catch (Exception ex)
            {
                // Manejo de errores
                StatusMessage = $"Error al retirar la cantidad del pallet: {ex.Message}";
            }
        }

    }
}
