using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
namespace MyApplicacion.Views;

public partial class UbicacionPage : ContentPage
{
    string referencia;
    public UbicacionPage(string pReferencia)
    {
        InitializeComponent();
        referencia = pReferencia;
        mostrarPallets();
    }
    
    private async void mostrarPallets()
    {
        List<Pallet> pallet = await App.PalletRepo.MostrarAlmacenados(referencia);
        palletList.ItemsSource = pallet;
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///Views.AlmacenPage");

        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:
        bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES REUBICAR EL PALLET {item.Id}?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta)
        {

            if (referencia != null)
            {
                string ubicacion = "k2";
                await App.PalletRepo.AddUbicacion(item.Id,ubicacion);
                string statusMessage = App.PalletRepo.StatusMessage;
                Console.WriteLine(statusMessage);

               
            }

            

            await Shell.Current.GoToAsync("///Views.AlmacenPage");

        }
        
    }

    //TODO: Aqui Tiene que haber un boton en el que se tenga que poner una ubicacion por lo que se accedera a la Base de datos

    private async void descargarDatos(object sender, EventArgs e)
    {
        Peticiones.DownloadDate(DataDownload);
    }
    

}