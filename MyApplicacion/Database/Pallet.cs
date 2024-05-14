

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

        
       

    }
}
