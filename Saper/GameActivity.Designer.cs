using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Graphics;
using Android.Widget;

namespace Saper
{
    public partial class GameActivity
    {
        TextView textView1;
        TableLayout tableLayout1;
        Button buttonReset;

        void InitializeComponent()
        {
            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            tableLayout1 = FindViewById<TableLayout>(Resource.Id.tableLayout1);
            buttonReset = FindViewById<Button>(Resource.Id.button2);
            Game.Result = textView1;
            CreateGameMap();
            foreach (var cell in Map.Cells)
            {
                cell.Button.LongClick += (s, args) => cell_LongClick(cell.Pos);
                cell.Button.Click += (s, args) => cell_Click(cell.Pos);                
            }
            buttonReset.Click += new EventHandler(reset_Click);
        }

        void CreateGameMap()
        {
            for(var i =0; i < Map.Height;i++)
            {
                var tableRow = new TableRow(this); 
                for(var j = 0; j < Map.Width; j++)
                {
                    if (Map.Cells[i, j] == null)
                        Map.Cells[i, j] = new EmptyCell(j, i);
                    Map.Cells[i, j].InitializeButton(this);
                    tableRow.AddView(Map.Cells[i, j].Button);
                    tableRow.AddView(Map.Cells[i, j].Separator);
                }
                tableLayout1.AddView(tableRow);
            }
        }

        void ClearMap()
        {
            tableLayout1.RemoveAllViews();
            textView1.SetBackgroundColor(Color.Gray);
        }
    }
}