using SQLite;
namespace MyApplicacion.Database
{
    [SQLite.Table("pallet")]
    public class Pallet
    {
        [SQLite.PrimaryKey, AutoIncrement] public int Id { get; set; }
        public DateTime fecha_hora { get; set; }
        public string referencia { get; set; }
        public string cant { get; set; }
    }
}
