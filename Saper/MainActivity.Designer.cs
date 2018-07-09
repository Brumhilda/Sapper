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

namespace Saper
{
   partial class MainActivity
    {
        TextView textView2;
        TextView textView3;
        TextView textView4;
        EditText editText1;
        EditText editText2;
        Button button1;
        Button buttonRestore;

        void InitializeComponent()
        {
            textView2 = FindViewById<TextView>(Resource.Id.textView2);
            textView3 = FindViewById<TextView>(Resource.Id.textView3);
            textView4 = FindViewById<TextView>(Resource.Id.textView4);
            editText1 = FindViewById<EditText>(Resource.Id.editText1);
            editText2 = FindViewById<EditText>(Resource.Id.editText2);
            button1 = FindViewById<Button>(Resource.Id.button1);
            buttonRestore = FindViewById<Button>(Resource.Id.button3);

            button1.Click += new EventHandler(startButton_Click);
            buttonRestore.Click += new EventHandler(restore_Click);
        }
    }
}