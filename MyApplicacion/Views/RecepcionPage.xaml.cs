using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;

namespace MyApplicacion.Views;

public partial class RecepcionPage : ContentPage
{

    string referencia;
    public RecepcionPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<String>(this, getReferencia);
    }

    private void getReferencia(object recipient, string message)
    {
        Console.WriteLine(message);
        if(message!=null) {
            referencia = message;
            mostrarPallets();
        }
    }
    private async void mostrarPallets()
    {
        List<Pallet> pallet = await App.PalletRepo.MostrarReferencias(referencia);
        palletList.ItemsSource = pallet;
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///Views.AlmacenPage");

        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void BtnIncidecias_Clicked(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///Views.IncidenciasPage");

    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:
        
        bool respuesta = await DisplayAlert("Alerta", $"¿ESTAS SEGURO DE QUE QUIERES VALIDAR EL PALLET DE FECHA: {item.fecha_hora}? ", "VALIDAR", "CANCELAR");
        if (respuesta)
        {
            await App.PalletRepo.tickAlmacen(item.fecha_hora);
            string statusMessage = App.PalletRepo.StatusMessage;
            Console.WriteLine(statusMessage);

            await Shell.Current.GoToAsync("///Views.AlmacenPage");
        }
    }

    //TODO: Aqui habra un metodo donde se realizara la comprobacion
    //de esa recepcion y donde se mostrar una tabla con los datos necesarios
    //y donde se haran modificaciones en la Base de datos

}