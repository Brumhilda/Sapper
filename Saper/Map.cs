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
    public static class Map
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static Cell[,] Cells;
        public static int CellAmount { get; set; } = 0;
        public static int BombAmount { get; set; } = 0;
        private static int openedCells = 0;
        public static int OpenedEmptyCellAmount
        {
            get
            {
                return openedCells;
            }
            set
            {
                openedCells = value;
                if (openedCells == CellAmount - BombAmount)
                    Game.FinishGame(true);
            }
        }

        public static void Initialize(int width, int height)
        {
            Width = width;
            Height = height;
            CellAmount = Width * Height;
            BombAmount = FindBombAmount();
            Cells = new Cell[Height, Width];
            openedCells = 0;
        }

        static int FindBombAmount()
        {
            var rand = new Random();
            return rand.Next(Convert.ToInt32(CellAmount*0.1), Convert.ToInt32(CellAmount *0.2));
        }

        public static void SetBombs(Cell.Position firstPos)
        {
            for(var i = 0; i < BombAmount; i++)
            {
                var pos = BombCell.GenerateBombPos(firstPos);
                var button = Cells[pos.Y, pos.X].Button;
                Cells[pos.Y, pos.X] = new BombCell(pos.X,pos.Y); 
                Cells[pos.Y, pos.X].Button = button;
            }
            foreach (var cell in Cells)
                if (cell is EmptyCell)
                    ((EmptyCell)cell).FindBombCount();
        }
    }
}