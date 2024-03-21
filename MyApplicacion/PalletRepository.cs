using MyApplicacion.Database;
using SQLite;
using System.Data;



namespace MyApplicacion
{
    public class PalletRepository(string path)
    {
        string _dbPath= path;
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;
        private async Task Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            await conn.CreateTableAsync<MyApplicacion.Database.Pallet>();

        }
        public async Task AddNewPallet(string pReferencia, string pCant) 
        {
            int result = 0;
            try
            {
                await Init();
                DateTime dateTime = DateTime.Now;
                if (string.IsNullOrEmpty(pReferencia))
                    throw new Exception("Valid reference required");
                if (string.IsNullOrEmpty(pCant))
                    throw new Exception("Valid reference required");
                MyApplicacion.Database.Pallet pallet = new() { fecha_hora = dateTime, referencia = pReferencia, cant=pCant };
                result = await conn.InsertAsync(pallet);

                StatusMessage = string.Format("{0} record(S) added (Name: {1})", result, pReferencia, pCant);
            }catch (Exception ex)
            {
                
            }
        }

        public async Task<List<MyApplicacion.Database.Pallet>> GetAll()
        {
            try
            {
                await Init();
                return await conn.Table<MyApplicacion.Database.Pallet>().ToListAsync();
            }
            catch (Exception ex) {
                StatusMessage = string.Format("Error para mostar los datos. {0}", ex.Message);
            }
            return new List<MyApplicacion.Database.Pallet>();
        }
        public async Task EliminarPallet()
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
        public async Task<bool> ComprobarReferencia(string pReferencia)
        {
            Boolean existe= false;
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

    }
}
