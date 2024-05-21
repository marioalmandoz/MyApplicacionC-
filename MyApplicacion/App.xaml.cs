namespace MyApplicacion
{
    public partial class App : Application
    {
       // public static PalletRepository PalletRepo { get; set; }
        public static DataAccess dataAccess { get; set; }
        public static string CurrentPage { get; set; }
        public App(DataAccess pDataAccess)
        {
            InitializeComponent();

            MainPage = new AppShell();
            dataAccess = pDataAccess;
           // PalletRepo = palletRepository;
        }
    }
}
