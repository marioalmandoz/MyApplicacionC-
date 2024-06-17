using Android.App;
using Android.Content;
using Android.OS;
using ProduccionAlmacen.Platforms.Android;

namespace MyApplicacion;
//WITH CONFIGCHANGES! //[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true)]
public class MainActivity : MauiAppCompatActivity
{

    private BroadcastReceiver myBroadcastReceiver;

    protected override void OnPostCreate(Bundle savedInstanceState)
    {
        base.OnPostCreate(savedInstanceState);
        RegisterReceivers();
        Console.WriteLine("Aqui");
    }


    protected override void OnSaveInstanceState(Bundle outState)
    {
        string currentDatetime = DateTime.Now.ToString();
        outState.PutString("time", currentDatetime);
        base.OnSaveInstanceState(outState);

        Console.WriteLine("Entra en OnSavedInstanceState");
    }

    void RegisterReceivers()
    {
        Console.WriteLine("En RegisterReceiver");

        IntentFilter filter = new IntentFilter();
        filter.AddAction("com.symbol.datawedge.materials");

        myBroadcastReceiver = new DWIntentReceiver();
        RegisterReceiver(myBroadcastReceiver, filter);
        Console.WriteLine("salir de RegisterReceiver");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnregisterReceiver(myBroadcastReceiver);
    }

}
