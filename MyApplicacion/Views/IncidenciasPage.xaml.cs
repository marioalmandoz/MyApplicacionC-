using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;
using ProduccionAlmacen.Views;

namespace MyApplicacion.Views;

public partial class IncidenciasPage : ContentPage
{
    int id;
    int cantidad;
    public IncidenciasPage(int pId, int pCantidad)
    {
        InitializeComponent();
        CargarElementos();
        id = pId;
        cantidad = pCantidad;
       // WeakReferenceMessenger.Default.Register<String>(this, getElemento);
    }

    private void CargarElementos()
    {
        List<string> elementos = new List<string> { "PALLETS Y CAJAS ROTAS","REF EN PALLET QUE NO CORRESPONDE", "REF MEZCLADA EN MISMO PALLET", "PALLET CON MAS KLT´S O CAJAS QUE EN LA INSTRUCCION DE TRABAJO", "PALLET INCOMPLETOS", "CANTIDAD INCORRECTA", "VARIOS" };
        foreach (var elemento in elementos)
        {
            ddlIncidencias.Items.Add(elemento);
        }
    }


    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        //string referencia = await App.PalletRepo.getReferencia(id);
        //if (referencia != null)
        //{
        //    await Shell.Current.Navigation.PushAsync(new RecepcionPage());
        //}
        await Shell.Current.GoToAsync("///Views.RecepcionPage");


        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
    
    private async void BtnValidar_Clicked(object sender, EventArgs e)
    {
        Console.WriteLine(id);
        string incidencia = ddlIncidencias.SelectedIndex.ToString();
        Console.WriteLine(incidencia);
        if(incidencia=="4"|| incidencia=="6")
        {
            int row = App.dataAccess.addIncidencias(id, ddlIncidencias.SelectedItem.ToString());
            if (row > 0)
            {
                AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("aprobacion_sound.wav")).Play();
                var popup = new PopUpPage("Confirmación", $"Se ha Recepcionado el pallet", 1);
                //Aqui hay que hacer la subida de datos
                //Peticiones.SubirBaan(item.baan, item.nPiezas.ToString()); 
                //añadir codigo para gestionar la respuesta
                await this.ShowPopupAsync(popup);
                
                //await DisplayAlert("Confirmación", $"Se ha Recepcionado el pallet", "Ok");
                await Shell.Current.GoToAsync("///Views.AlmacenPage");
            }
            else
            {
                AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
                var popup = new PopUpPage("Error", $"No se ha podido recepcionar el pallet", 1);
                await this.ShowPopupAsync(popup);
               // await DisplayAlert("Error", $"No se ha podido recepcionar el pallet", "Ok");
            }
        }else if(incidencia=="5")
        {
            cajasFrame.IsVisible = true;
            if(cajasEntry.Text!=null)
            {
                string total = (int.Parse(cajasEntry.Text) * cantidad).ToString();
                App.dataAccess.EditarCajas(cajasEntry.Text.ToString(), id,total);
                var popup = new PopUpPage("Confirmación", $"Se ha Editado el pallet", 1);
                await this.ShowPopupAsync(popup);
                // await DisplayAlert("Confirmación", $"Se ha Editado el pallet", "Ok");
                int row = App.dataAccess.addIncidencias(id, ddlIncidencias.SelectedItem.ToString());
                if (row > 0)
                {
                    AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("aprobacion_sound.wav")).Play();
                    popup = new PopUpPage("Confirmación", $"Se ha Recepcionado el pallet", 1);
                    await this.ShowPopupAsync(popup);
                    //Aqui hay que hacer la subida de datos
                    //Peticiones.SubirBaan(item.baan, item.nPiezas.ToString());
                    //añadir codigo para gestionar la respuesta
                    await Shell.Current.GoToAsync("///Views.AlmacenPage");
                }
                else
                {
                    AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
                    popup = new PopUpPage("Error", $"No se ha podido recepcionar el pallet", 1);
                    await this.ShowPopupAsync(popup);
                    //await DisplayAlert("Error", $"No se ha podido recepcionar el pallet", "Ok");
                }
               
            }
            

        }
        else
        {
            int row = App.dataAccess.EliminarPallet(id);
            if(row > 0)
            {
                var popup = new PopUpPage("Confirmación", $"Se ha Eliminado el pallet", 1);
                await this.ShowPopupAsync(popup);
               // await DisplayAlert("Confirmación", $"Se ha Eliminado el pallet", "Ok");
                await Shell.Current.GoToAsync("///Views.AlmacenPage");
            }
            else
            {
                var popup = new PopUpPage("Error", $"No se ha podido Eliminar el pallet", 1);
                await this.ShowPopupAsync(popup);
                await DisplayAlert("Error", $"No se ha podido Eliminar el pallet", "Ok");
            }
        }

        
        
        // await App.PalletRepo.addIncidencia(id,ddlIncidencias.SelectedItem.ToString());


        //Aqui se Volvera a la pagina de recepcionarPallet
        
    }

    private async void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        //Aqui se Volvera a la pagina de recepcionarPallet
        await Shell.Current.GoToAsync("///Views.RecepcionPage");
    }
}