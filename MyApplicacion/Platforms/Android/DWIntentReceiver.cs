using Android.App;
using Android.Content;
using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion;


namespace ProduccionAlmacen.Platforms.Android
{

    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.symbol.datawedge.materials" })]
    public class DWIntentReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Console.WriteLine("Entra en OnReceive");
            //System.Console.WriteLine("Here is DW on MAUI");
            if (intent.Extras != null)
            {
                
                String bc_type = intent.Extras.GetString("com.symbol.datawedge.label_type");
                String bc_data = intent.Extras.GetString("com.symbol.datawedge.data_string");
                Console.WriteLine(bc_data);

                string pageIdentifier = App.CurrentPage;
                WeakReferenceMessenger.Default.Send($"{pageIdentifier}|{bc_type} {bc_data}");
            }


        }
    }
}

