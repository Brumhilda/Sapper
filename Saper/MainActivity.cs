using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;

using Android.Views;

namespace Saper
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public partial class MainActivity : AppCompatActivity
	{
        ISharedPreferences shPref;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.initLayout);
            //var vm = new PageVM
            InitializeComponent();
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            Toast.MakeText((this), "Game was restored", ToastLength.Short).Show();
            //shPref = GetSharedPreferences("myData",FileCreationMode.Private);
        }

        void startButton_Click(object sender, EventArgs e)
        {
            Game.StartGame(Convert.ToInt32(editText1.Text), Convert.ToInt32(editText2.Text));
            StartActivity(new Intent(this, typeof(GameActivity)));
        }

        void restore_Click(object sender, EventArgs e)
        {
            shPref = GetSharedPreferences("myData", FileCreationMode.Private);
            DataManager.RestoreData(shPref);
            StartActivity(new Intent(this, typeof(GameActivity)));
            Toast.MakeText((this), "Game was restored", ToastLength.Short).Show();
        }

        
	}
}
   

