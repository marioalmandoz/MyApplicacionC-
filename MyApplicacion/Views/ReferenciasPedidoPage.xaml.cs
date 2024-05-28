using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using ProduccionAlmacen.Views;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class ReferenciasPedidoPage : ContentPage
{
    string ubicacion;
    string baan;
    string scaneo;
    
    public ReferenciasPedidoPage(string pBaan)
    {
        InitializeComponent();
        this.baan = pBaan;
        App.CurrentPage = "ReferenciasPedidoPage";
        mostrarPallets();
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
        
    }

    private async void mostrarPallets()
    {
        List<Pallet> pallet = App.dataAccess.MostrarAlmacen(baan);
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
                int existe = App.dataAccess.getPalletUbica(ubicacion, baan);
                if(existe > 0)
                {
                    realizarOperacion(existe);
                }
                else
                {
                    var popup = new PopUpPage("Error", $"No hay pallets en la ubicacion: {ubicacion}", 1);
                    await this.ShowPopupAsync(popup);
                    //await DisplayAlert("Error", $"No hay pallets en la ubicacion: {ubicacion}", "Ok");
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
        var popup = new PopUpPage("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {item.nPiezas} piezas?", 2);
        var respuesta = await this.ShowPopupAsync(popup) as bool?;
        //bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {item.nPiezas} piezas?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta.HasValue&&respuesta.Value)
        {
            if (baan != null)
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
        var popup = new PopUpPage("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {ubicacion}?", 2);
        var respuesta = await this.ShowPopupAsync(popup) as bool?;
        //bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {ubicacion}?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta.HasValue && respuesta.Value)
        {
            if (baan != null)
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