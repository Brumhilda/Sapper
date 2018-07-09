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
using Android.Graphics;

namespace Saper
{
    public class BombCell : Cell
    {
        public BombCell(int x,int y)
            :base(x,y)
        {
        }

        public override void InitializeButton(Context context)
        {
            base.InitializeButton(context);
            Button.Text = opened ? "X" : isFlag ? "F" : "";
            if (opened)
            {
                Button.SetBackgroundColor(Color.DarkRed);
            }
        }

        public override void Open()
        {
            base.Open();
            if (!isFlag && !opened)
            {
                opened = true;
                Button.Text = "X";
                Button.SetTextColor(Color.Black);
                Button.SetBackgroundColor(Color.DarkRed);
                Button.Enabled = false;
                Game.FinishGame(false);
            }
        }

        public override void SetOrRemoveFlag()
        {
            base.SetOrRemoveFlag();

        }

        public static Position GenerateBombPos(Cell.Position firstPos)
        {
            var random = new Random();
            var bombPosX = random.Next(Map.Width);
            var bombPosY = random.Next(Map.Height);
            while (Map.Cells[bombPosY, bombPosX].opened || Map.Cells[bombPosY, bombPosX] is BombCell || 
                (firstPos.X == bombPosX && firstPos.Y == bombPosY))
            {
                bombPosX = random.Next(Map.Width);
                bombPosY = random.Next(Map.Height);
            }
            Console.WriteLine("============" + bombPosY + "===========" + bombPosX);
            return new Position { X = bombPosX, Y = bombPosY };
        }
    }
}