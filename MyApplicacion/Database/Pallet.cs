using SQLite;


namespace MyApplicacion.Database
{
    [SQLite.Table("pallet")]
    public class Pallet
    {
        [PrimaryKey]
        public int Id { get; set; }
        //Resto de atributos
        public DateTime fecha_hora { get; set; }
        public string referencia { get; set; }
        public string baan {  get; set; }
        public string nPiezas { get; set; }
        public string nCajas { get; set; }
        public string ubicacion { get; set; }
        public bool almacen { get; set; }
        public bool produccion { get; set; }
        public bool rec { get; set; }
        public string incidencia { get; set; }

        // Método para insertar un nuevo pallet en la base de datos
        public void Insert(SQLiteConnection conn)
        {
            string query = "INSERT INTO pallet (Id, fecha_hora, referencia, baan, nPiezas, nCajas, ubicacion, almacen, produccion, rec, incidencia) " +
                           "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            conn.Execute(query, Id, fecha_hora, referencia, baan, nPiezas, nCajas, ubicacion, almacen, produccion, rec, incidencia);
        }

        // Método para actualizar un pallet existente en la base de datos
        public void Update(SQLiteConnection conn)
        {
            string query = "UPDATE pallet " +
                           "SET fecha_hora=?, referencia=?, baan=?, nPiezas=?, nCajas=?, ubicacion=?, almacen=?, produccion=?, rec=?, incidencia=? " +
                           "WHERE Id=?";
            conn.Execute(query, fecha_hora, referencia, baan, nPiezas, nCajas, ubicacion, almacen, produccion, rec, incidencia, Id);
        }

        // Método para eliminar un pallet existente de la base de datos
        public void Delete(SQLiteConnection conn)
        {
            string query = "DELETE FROM pallet WHERE Id=?";
            conn.Execute(query, Id);
        }
    }
}
