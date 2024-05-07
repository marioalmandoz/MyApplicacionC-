using Microsoft.EntityFrameworkCore;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MyApplicacion.Database
{
    [SQLite.Table("pallet")]
    public class Pallet
    {
        [Key]
        public string fecha_hora { get; set; }
        public string referencia { get; set; }
        public string cant { get; set; }
        public string ubicacion { get; set; }
        public bool almacen { get; set; }
        public bool produccion { get; set; }
        public bool rec { get; set; }

        
       

    }
}
