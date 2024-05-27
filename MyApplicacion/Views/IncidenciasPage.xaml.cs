using CommunityToolkit.Mvvm.Messaging;
using Plugin.Maui.Audio;

namespace MyApplicacion.Views;

public partial class IncidenciasPage : ContentPage
{
    int id;
    public IncidenciasPage(int pId)
    {
        InitializeComponent();
        CargarElementos();
        id = pId;
       // WeakReferenceMessenger.Default.Register<String>(this, getElemento);
    }

    private void CargarElementos()
    {
        List<string> elementos = new List<string> { "PALLETS Y CAJAS ROTAS","REF EN PALLET QUE NO CORRESPONDE", "REF MEZCLADA EN MISMO PALLET", "PALLET INCOMPLETOS", "PALLET CON MAS KLT´S O CAJAS QUE EN LA INSTRUCCION DE TRABAJO","CANTIDAD INCORRECTA", "VARIOS" };
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
        if(incidencia=="3"|| incidencia=="6")
        {
            int row = App.dataAccess.addIncidencias(id, ddlIncidencias.SelectedItem.ToString());
            if (row > 0)
            {
                AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("aprobacion_sound.wav")).Play();
                await DisplayAlert("Confirmación", $"Se ha Recepcionado el pallet", "Ok");
                await Shell.Current.GoToAsync("///Views.AlmacenPage");
            }
            else
            {
                AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
                await DisplayAlert("Error", $"No se ha podido recepcionar el pallet", "Ok");
            }
        }else if(incidencia=="5")
        {
            cajasFrame.IsVisible = true;
            if(cajasEntry.Text!=null)
            {
                App.dataAccess.EditarCajas(cajasEntry.Text.ToString(), id);
                await DisplayAlert("Confirmación", $"Se ha Editado el pallet", "Ok");
                int row = App.dataAccess.addIncidencias(id, ddlIncidencias.SelectedItem.ToString());
                if (row > 0)
                {
                    AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("aprobacion_sound.wav")).Play();
                    await DisplayAlert("Confirmación", $"Se ha Recepcionado el pallet", "Ok");
                    await Shell.Current.GoToAsync("///Views.AlmacenPage");
                }
                else
                {
                    AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
                    await DisplayAlert("Error", $"No se ha podido recepcionar el pallet", "Ok");
                }
               
            }
            

        }
        else
        {
            int row = App.dataAccess.EliminarPallet(id);
            if(row > 0)
            {
                await DisplayAlert("Confirmación", $"Se ha Eliminado el pallet", "Ok");
                await Shell.Current.GoToAsync("///Views.AlmacenPage");
            }
            else
            {
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