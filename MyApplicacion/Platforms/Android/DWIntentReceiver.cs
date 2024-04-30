﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionAlmacen.Platforms.Android
{

    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.symbol.datawedge.api.ACTION" })]
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

                WeakReferenceMessenger.Default.Send(bc_type + " " + bc_data);
            }


        }
    }
}

