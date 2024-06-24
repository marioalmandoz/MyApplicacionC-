using CommunityToolkit.Maui.Views;
using ProduccionAlmacen.Views;

namespace MyApplicacion.Views;

public partial class CajasPedidoPage : ContentPage
{
    int id;
	public CajasPedidoPage(int pId)
	{
		InitializeComponent();
        id = pId;
    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        // Aquí puedes acceder al valor introducido en el Entry
        try
        {
            int numeroCajas = int.Parse(numeroPiezasEntry.Text);

            numeroPiezasEntry.Text = "";
            

            if (numeroCajas>0)
            {
                // Aquí puedes realizar la validación y cualquier otra lógica necesaria
                // Mostrar una alerta para confirmar si el usuario está seguro
                var popup = new PopUpPage("Confirmación", $"¿Estás seguro de querer retirar {numeroCajas} cajas en total?", 2);
                var respuesta = await this.ShowPopupAsync(popup) as bool?;
              
                // Verificar la respuesta del usuario
                if (respuesta.HasValue && respuesta.Value)
                {
                    
                    App.dataAccess.RetirarCajas(numeroCajas.ToString(),id);
                  

                    await Shell.Current.GoToAsync("///Views.PedidoPage");

                }
            }
        }
        catch (Exception ex)
        {
            var popup = new PopUpPage("Error", $"Introduce el numero correcto de cajas", 1);
            await this.ShowPopupAsync(popup);
            //await DisplayAlert("Error", $"Introduce el numero de piezas", "Ok");
        }
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        string referencia =  App.dataAccess.getReferencia(id);
        if (referencia != null)
        {
            await Shell.Current.Navigation.PushAsync(new ReferenciasPedidoPage(referencia));
        }
    }
}