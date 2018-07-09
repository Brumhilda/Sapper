using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Saper
{
    public static class Game
    {
        public static bool FirstButtonWasPressed;
        static TextView result;
        private static int notOpenBombs;

        public static TextView Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
                result.Text = NotOpenBombs.ToString();
            }
        }

        public static int NotOpenBombs
        {
            get
            {
                return notOpenBombs;
            }
            set
            {
                notOpenBombs = value;
                if(result != null)
                    result.Text = Convert.ToString(notOpenBombs);
            }
        }

        public static void StartGame(int width, int height)
        {
            FirstButtonWasPressed = false;
            Map.Initialize(width, height);
            NotOpenBombs = Map.BombAmount;
        }

        public static void FinishGame(bool win)
        {
            foreach(var cell in Map.Cells)
            {
                cell.Block();
                if (cell is BombCell)
                {
                    if (cell.isFlag)
                        cell.SetOrRemoveFlag();
                    cell.Open();
                }
                if (win)
                    OutputWin();
                else
                    OutputLoss();
            }
        }

        public static void OutputWin()
        {
            result.SetBackgroundColor(Color.DarkGreen);
            result.Text = "Поздравляем! Вы выиграли!";
        }

        public static void OutputLoss()
        {
            result.SetBackgroundColor(Color.Red);
            result.Text = "Игра окончена. Вы проиграли..";
        }
    }
}