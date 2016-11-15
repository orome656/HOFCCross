using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using Android.Content.Res;
using Newtonsoft.Json;
using System.IO;

namespace HOFCCross.Droid
{
    [Application]
    public class HOFCApplication: Application
    {
        public static Context AppContext;

        public HOFCApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
                
        }

        public override void OnCreate()
        {
            base.OnCreate();

            AppContext = this.ApplicationContext;
        }
    }
}