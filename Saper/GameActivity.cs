using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;


namespace Saper
{
    [Activity(ParentActivity = typeof(MainActivity), Theme = "@style/AppTheme.NoActionBar")]
    public partial class GameActivity : AppCompatActivity
    {
        public delegate void ClickFunction(Cell.Position pos);
        ISharedPreferences shPref;
        protected override void OnCreate(Bundle savedInstanceState)
        {    
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.gameLayout);
            InitializeComponent();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            shPref = GetSharedPreferences("myData", FileCreationMode.Private);
            var editor = shPref.Edit();
            DataManager.Save(shPref);
            Toast.MakeText((this), "Game was saved", ToastLength.Short).Show();
        }

        public static void cell_Click(Cell.Position pos)
        {
            var cell = Map.Cells[pos.Y, pos.X];
            if (!Game.FirstButtonWasPressed)
            {
                Map.SetBombs(cell.Pos);
                Game.FirstButtonWasPressed = true;
            }
            cell.Open();
        }

        public static void cell_LongClick(Cell.Position pos)
        {
            var cell = Map.Cells[pos.Y, pos.X];
            cell.SetOrRemoveFlag();
        }

        void reset_Click(object sender, EventArgs e)
        {
            Toast.MakeText((this), "Game was reseted", ToastLength.Short).Show();
            ClearMap();
            Game.StartGame(Map.Width, Map.Height);
            InitializeComponent();
        }
    }
}