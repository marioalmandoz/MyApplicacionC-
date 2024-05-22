using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class ReferenciasPedidoPage : ContentPage
{
    string ubicacion;
    string referencia;
    string scaneo;
    
    public ReferenciasPedidoPage(string referencia)
    {
        InitializeComponent();
        this.referencia = referencia;
        App.CurrentPage = "ReferenciasPedidoPage";
        mostrarPallets();
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
        
    }

    private async void mostrarPallets()
    {
        List<Pallet> pallet = App.dataAccess.MostrarAlmacen(referencia);
        palletList.ItemsSource = pallet;
        //---------------------------------------------------------------
        //List<Pallet> pallet = await App.PalletRepo.MostrarAlmacenados(referencia);
        //palletList.ItemsSource = pallet;
    }
    private async void OnDataReceived(object recipient, string message)
    {
        var parts = message.Split('|');
        if (parts.Length > 1 && parts[0] == "ReferenciasPedidoPage")
        {
            Console.WriteLine(message);
            scaneo = parts[1];
            string pattern = @"([HhJjIi]\d{1,2})";
            Match match = Regex.Match(scaneo, pattern);
            if (match.Success)
            {
                
                ubicacion = match.Groups[1].Value;
                Console.WriteLine("Datos: " + ubicacion);
                ScanResultLabel.Text = "Datos: " + ubicacion;
                int existe = App.dataAccess.getPalletUbica(ubicacion, referencia);
                if(existe > 0)
                {
                    realizarOperacion(existe);
                }
                else
                {
                    await DisplayAlert("Error", $"No hay pallets en la ubicacion: {ubicacion}", "Ok");
                }
                
            }
            else
            {
                Console.WriteLine("No se encontraron coincidencias Referencia.");
            }

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
            if (referencia != null)
            {
                App.dataAccess.EliminarPallet(item.Id);
                //-----------------------------------
                //await App.PalletRepo.EliminarPalletPorId(item.Id);
                //string statusMessage = App.PalletRepo.StatusMessage;
                //Console.WriteLine(statusMessage);
            }
            await Shell.Current.GoToAsync("///Views.PedidoPage");

        }
        else
        {
            await Shell.Current.Navigation.PushAsync(new CajasPedidoPage(item.Id));
            //await Shell.Current.GoToAsync("///Views.CajasPedidoPage");
        }
    }
    private async void realizarOperacion(int pId)
    {
        bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {ubicacion}?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta)
        {
            if (referencia != null)
            {
                App.dataAccess.EliminarPallet(pId);
                //-----------------------------------
                //await App.PalletRepo.EliminarPalletPorId(item.Id);
                //string statusMessage = App.PalletRepo.StatusMessage;
                //Console.WriteLine(statusMessage);
            }
            await Shell.Current.GoToAsync("///Views.PedidoPage");

        }
        else
        {
            await Shell.Current.Navigation.PushAsync(new CajasPedidoPage(pId));
            //await Shell.Current.GoToAsync("///Views.CajasPedidoPage");
        }
    }
}