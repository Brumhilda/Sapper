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
using Android.Support.Design.Widget;
using Android.Support.V7.App;

namespace Saper
{
    public static class DataManager
    {
        static Dictionary<string, int> Data = new Dictionary<string, int>();
        private static string width = "width";
        private static string height = "height";
        private static string notOpenBombs = "notOpenBombs";
        private static string bombAmount = "bombAmount";
        private static string openedCells = "openedCells";
        private static string firstOpened = "firstOpened";
        private static string bomb = "bomb";
        private static string open = "open";
        private static string flag = "flag";
        private static string flagCount = "flagCount";

        public static void Initialize()
        {
            Data[width] = Map.Width;
            Data[height] = Map.Height;
            Data[notOpenBombs] = Game.NotOpenBombs;
            Data[bombAmount] = Map.BombAmount;
            Data[openedCells] = Map.OpenedEmptyCellAmount;
            Data[firstOpened] = Game.FirstButtonWasPressed ? 1 : 0;
            if (Map.Cells != null)
                AddPositions();
        } 

        private static void AddPositions()
        {
            int bCounter = 0,
                oCounter=0,
                fCounter = 0;
            foreach (var cell in Map.Cells)
            {
                if (cell is BombCell)
                {
                    AddInData(bomb, bCounter,cell);
                    bCounter++;
                }
                if (cell.opened)
                {
                    AddInData(open,oCounter,cell);
                    oCounter++;
                }
                else if (cell.isFlag)
                {
                    AddInData(flag, fCounter, cell);
                    fCounter++;
                }
            }
            Data[flagCount] = fCounter;
        }

        private static void AddInData(string param, int i, Cell cell)
        {
            Data[param + "X" + i] = cell.Pos.X;
            Data[param + "Y" + i] = cell.Pos.Y;
        }

        public static void Save(ISharedPreferences iShPref)
        {
            Initialize();
            var editor = iShPref.Edit();
            foreach (var pair in Data)
                editor.PutInt(pair.Key, pair.Value);      
            editor.Commit();
        }

        public static void RestoreData(ISharedPreferences iShPref)
        {
            Initialize();
            var keys = Data.Keys.ToList();
            foreach (var key in keys)
            {
                Data[key] = iShPref.GetInt(key, -1);
                if (Data[key] == -1)
                    throw new Exception("Error of getting saved data");
            }
            GetData(iShPref, Data[bombAmount],bomb);
            GetData(iShPref, Data[openedCells],open);
            Data[flagCount] = iShPref.GetInt(flagCount, -1);
            GetData(iShPref, Data[flagCount], flag);
            RestoreMap();
        }

        private static void GetData(ISharedPreferences iShPref, int amount, string param)
        {
            for (var i = 0; i < amount; i++)
            {
                Data[param + "X" + i] = iShPref.GetInt(param + "X" + i, -1);
                Data[param + "Y" + i] = iShPref.GetInt(param + "Y" + i, -1);
            }
        }

        private static void RestoreMap()
        {
            Game.StartGame(Data[width], Data[height]);            
            Game.FirstButtonWasPressed = Data[firstOpened] == 1;
            Game.NotOpenBombs = Data[notOpenBombs];            
            CreateMapMaket();
            OpenCells();
            SetFlags();
        }

        private static void CreateMapMaket()
        {
            Map.BombAmount = Data[bombAmount];
            Map.OpenedEmptyCellAmount = Data[openedCells];
            for (var i = 0; i < Map.Width; i++)
                for (var j = 0; j < Map.Height; j++)
                    Map.Cells[j, i] = new EmptyCell(i, j);
            for (var i = 0; i < Data[bombAmount]; i++)
                Map.Cells[Data[bomb + "Y" + i], Data[bomb + "X" + i]] = new BombCell(Data[bomb + "X" + i], Data[bomb + "Y" + i]);
            foreach (var cell in Map.Cells)
                if (cell is EmptyCell)
                    ((EmptyCell)cell).FindBombCount();
        }

        private static void OpenCells()
        {
            for (var i = 0; i < Data[openedCells]; i++)
                Map.Cells[Data[open + "Y" + i], Data[open + "X" + i]].opened = true;
        }

        private static void SetFlags()
        {
            for (var i = 0; i < Data[flagCount]; i++)
                Map.Cells[Data[flag + "Y" + i], Data[flag + "X" + i]].isFlag = true;
        }
    }
}