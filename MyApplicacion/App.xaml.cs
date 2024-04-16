
namespace MyApplicacion
{
    public partial class App : Application
    {
        public static PalletRepository PalletRepo { get; set; }
        public App(PalletRepository palletRepository)
        {
            InitializeComponent();

            MainPage = new AppShell();
            PalletRepo = palletRepository;
        }
    }
}
