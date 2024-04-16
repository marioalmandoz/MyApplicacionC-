

namespace MyApplicacion
{
    public partial class MainPage : ContentPage
    {
        public object NavigationService { get; private set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Produccion_click(object sender, EventArgs e)
        {
            // Acciones que quieres realizar cuando se hace clic en el botón 1
            // Por ejemplo, mostrar un mensaje en la consola
            System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón 1.2");
            await Shell.Current.GoToAsync("///Views.ProduccionPage");
            System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón 1.3");

        }


        private async void Almacen_click(object sender, EventArgs e)
        {
            // Acciones que quieres realizar cuando se hace clic en el botón 2
            // Por ejemplo, mostrar un mensaje en la consola
            System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón 2!");
            await Shell.Current.GoToAsync("///Views.AlmacenPage");
        }
        private async void Db_click(object sender, EventArgs e)
        {
            // Acciones que quieres realizar cuando se hace clic en el botón 2
            // Por ejemplo, mostrar un mensaje en la consola
            await Shell.Current.GoToAsync("///Views.DatabasePage");
            System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón 2!");

        }

    }

}
