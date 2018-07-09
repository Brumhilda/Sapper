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
    public class Cell
    {
        public struct Position
        {
            public int X;
            public int Y;
        }
        public readonly Position Pos;
        public Button Button { get; set; }
        public bool isFlag = false;
        public bool opened = false;
        public readonly static int MinWidth = 60;
        public readonly static int MinHeight = 60;
        public TextView Separator;
        const int separatorWidth = 5;
        public int ButtonHeight { get; set; }
        public int ButtonWidth { get; set; }

        public Cell(int x, int y)
        {
            Pos.X = x;
            Pos.Y = y;
        }

        public virtual void Open()
        {

        }

        public virtual void SetOrRemoveFlag()
        {
            if (!opened)
            {
                isFlag = !isFlag;
                Button.Text= isFlag ? "F" : "";
                Game.NotOpenBombs = isFlag ? Game.NotOpenBombs-1:Game.NotOpenBombs+1;
            }
        }

        public static int GetCountPixels(int widthOrHeightOfLayout, int columnOrRowCount, int distanceBetween)
        {
            return (widthOrHeightOfLayout - (distanceBetween * (columnOrRowCount - 1)))/ columnOrRowCount;
        }

        public virtual void InitializeButton(Context context)
        {
            Button = new Button(context);
            Button.SetPadding(10, 10, 10, 10);
            Button.SetMinimumWidth(MinWidth);
            Button.SetMinimumHeight(MinHeight);
            ButtonHeight = GetCountPixels(600, Map.Height, separatorWidth);
            ButtonWidth = GetCountPixels(600, Map.Width, separatorWidth);
            Button.SetWidth(ButtonWidth);
            Button.SetHeight(ButtonHeight);
            Button.SetBackgroundColor(Color.Gray);
            Block();
            CreateSeparator(context);
        }

        void CreateSeparator(Context context)
        {
            Separator = new TextView(context);
            Separator.SetWidth(separatorWidth);
            Separator.SetHeight(ButtonHeight);
            Separator.SetBackgroundColor(Color.White);
        }

        public static bool ExistInMap(Position pos)
        {
            return pos.X >= 0 && pos.X < Map.Width && pos.Y >= 0 && pos.Y < Map.Height;
        }

        public void Block()
        {
            Button.Enabled = !opened;
            Button.Pressed = opened;
        }
    }
}