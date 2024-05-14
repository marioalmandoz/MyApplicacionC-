using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;

namespace MyApplicacion.Views;

public partial class ReferenciasPedidoPage : ContentPage
{
    string referencia;
    DateTime fecha;
    public ReferenciasPedidoPage(string referencia)
    {
        InitializeComponent();
        this.referencia = referencia;
        mostrarPallets();
       // WeakReferenceMessenger.Default.Register<String>(this, getReferencia);
        
        
    }

    private async void mostrarPallets()
    {
        List<Pallet> pallet = await App.PalletRepo.MostrarAlmacenados(referencia);
        palletList.ItemsSource = pallet;
    }
    private void getReferencia(object recipient, string message) 
    {
        if (message != null)
        {
            referencia = message;
            mostrarPallets();
        }
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///Views.PedidoPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:
        bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {item.nPiezas} piezas?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta)
        {
            // TODO: Aquí habrá un método que insertará los datos en la base de datos
            //App.PalletRepo.AddNewPallet(datosNecesariosParaCrearLaInstancia)

            if (referencia != null)
            {
                await App.PalletRepo.EliminarPalletPorId(item.Id);
                string statusMessage = App.PalletRepo.StatusMessage;
                Console.WriteLine(statusMessage);
                
                WeakReferenceMessenger.Default.Send(item.referencia);
            }

            //De momento llevamos a la clase ProduccionPage

            await Shell.Current.GoToAsync("///Views.PedidoPage");

        }
        else
        {
            //WeakReferenceMessenger.Default.Send(item.Id.ToString());
            await Shell.Current.Navigation.PushAsync(new CajasPedidoPage(item.Id));
            //await Shell.Current.GoToAsync("///Views.CajasPedidoPage");

        }
    }
}