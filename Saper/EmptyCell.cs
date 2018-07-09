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
    public class EmptyCell : Cell
    {
        private int bombCount = 0;
        public int BombCount
        {
            get { return bombCount; }
            set
            {
                if (value >= 0 && value < 9)
                    bombCount = value;
                else
                    throw new ArgumentException();
            }
        }

        public EmptyCell(int x, int y)
            :base(x,y)
        {
        }

        public override void InitializeButton(Context context)
        {
            base.InitializeButton(context);
            Button.Text = opened ? BombCount == 0 ? "" : BombCount.ToString()  : isFlag ? "F" : "";
            SetButton();
        }

        void SetButton()
        {
            Button.SetTextColor(
                !opened ? Color.Black :
                BombCount == 1 ? Color.Blue :
                BombCount == 2 ? Color.DarkGreen :
                BombCount == 3 ? Color.Red :
                BombCount == 4 ? Color.DarkBlue : Color.Black
                );
            Button.SetBackgroundColor(
                opened ? Color.DimGray :
                Color.Gray);
        }

        public override void Open()
        {
            base.Open();
            if (!isFlag && !opened)
            {
                opened = true;
                Map.OpenedEmptyCellAmount++;
                if (BombCount == 0)
                {
                    for(var i=Pos.X-1; i <= Pos.X+1; i++ )
                        for(var j=Pos.Y-1; j <= Pos.Y + 1; j++)
                            if(ExistInMap(new Position { X = i, Y = j}) && !Map.Cells[j,i].opened)
                                Map.Cells[j, i].Open();
                }
                else
                    Button.Text = BombCount.ToString();
            }
            SetButton();
            Button.Enabled = false;
            Button.Pressed = true;
        }

        public void FindBombCount()
        {
            for (var x = Pos.X - 1; x <= Pos.X + 1; x++)
                for (var y = Pos.Y - 1; y <= Pos.Y + 1; y++)
                    if (ExistInMap(new Position { X = x, Y = y }) && Map.Cells[y, x] is BombCell)
                        BombCount++;
        }
    }
}