namespace MyApplicacion.Database
{
    [SQLite.Table("pallet")]
    public class Pallet
    {
        public DateTime fecha_hora { get; set; }
        public string referencia { get; set; }
        public string cant { get; set; }
        public string ubicacion { get; set; }
        public bool almacen { get; set; }
        public bool produccion { get; set; }
        public bool rec { get; set; }

    }
}
