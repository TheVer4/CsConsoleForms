using System;
using System.Drawing;
using System.Text;

namespace ConsoleForms {
    public sealed class Table : Component, IControllable {
        private TextBox[,] tableData;
        private Point[,] coords;
        private BorderType type;
        private int selectedX = 0, selectedY = 0;
        
        public Table(string[,] tableData, BorderType type = BorderType.thin) : base("") {
            TextBox[,] result = new TextBox[tableData.GetLength(0),tableData.GetLength(1)];
            for(int x = 0; x < tableData.GetLength(0); x++)
            for (int y = 0; y < tableData.GetLength(1); y++)
                result[x,y] = new TextBox(tableData[x,y]);
            this.tableData = result;
            init();
            CreateBorders(type);
        }
        public Table(TextBox[,] tableData, BorderType type = BorderType.thin) : base("") {
            this.tableData = tableData;
            init();
            CreateBorders(type);
        }

        private void init() {
            this.coords = GetCellsCoords();
        }

        private void CreateBorders(BorderType type) {
            switch (type) {
                case BorderType.thin:
                    this.Text = CreateBorders("┐┌┬┤├┼─│└┴┘");
                    break;
                case BorderType.thick:
                    this.Text = CreateBorders("╗╔╦╣╠╬═║╚╩╝");
                    break;
                case BorderType.thickHorisontal:
                    this.Text = CreateBorders("╕╒╤╡╞╪═│╘╧╛");
                    break;
                case BorderType.thickVertical:
                    this.Text = CreateBorders("╖╓╥╢╟╫─║╙╨╜");
                    break;
            }
        }
        public void PrintValues(bool withSelection = false) {
            WalkThroughAllTable((x, y) => {
                int X = coords[x, y].X + this.Location.X;
                int Y = coords[x, y].Y + this.Location.Y;
                tableData[x, y].Location = new Point(X, Y);
                if (withSelection && x == selectedX && y == selectedY) {
                    Console.BackgroundColor = Properties.selectedBackground;
                    Console.ForegroundColor = Properties.selectedForeground;
                    tableData[x, y].Draw();
                    Console.BackgroundColor = Properties.defaultBackground;
                    Console.ForegroundColor = Properties.defaultForeground;
                }
                else 
                    tableData[x, y].Draw();
            });
        }
        private Point[,] GetCellsCoords() {
            FindoutTableScales(out int[] rowH, out int[] colW);
            int xMax = tableData.GetLength(0), 
                yMax = tableData.GetLength(1);
            Point[,] coords = new Point[xMax,yMax];
            int ySum = 0, 
                xSum = 0;
            WalkThroughAllTable((y, x) => {
                coords[y, x] = new Point(1 + Location.Y + xSum, 1 + Location.X + ySum);
                if (x + 1 < yMax)
                    xSum += 1 + colW[x];
                else {
                    xSum = 0;
                    if (y + 1 < xMax)
                        ySum += 1 + rowH[y];
                    else
                        ySum = 0;
                }
            });
            return coords;
        }
        private void FindoutTableScales(out int[] rowHeights, out int[] columnWidths) {
            int[] widths = new int[tableData.GetLength(1)];
            WalkThroughAllTable((x, y) => {
                int cellWidth = tableData[x, y].GetWidth();
                if (widths[y] < cellWidth)
                    widths[y] = cellWidth;
            });
            
            int[] heights = new int[tableData.GetLength(0)];
            WalkThroughAllTable((x, y) => {
                int cellHeight = tableData[x, y].GetHeight();
                if (heights[x] < cellHeight)
                    heights[x] = cellHeight;
            });
            columnWidths = widths;
            rowHeights = heights;
        }
        private void WalkThroughAllTable(Action<int, int> action) {
            for(int x = 0; x < tableData.GetLength(0); x++)
            for (int y = 0; y < tableData.GetLength(1); y++)
                action(x, y);
        }
        private string CreateBorders(string borderSprites) {
            string defaultBorders = "┐┌┬┤├┼─│└┴┘";
            if (borderSprites.Length != defaultBorders.Length)
                borderSprites = defaultBorders;
            StringBuilder rowBuilder = new StringBuilder();
            StringBuilder colBuilder = new StringBuilder();
            StringBuilder dataBuilder = new StringBuilder();
            FindoutTableScales(out int[] rowH, out int[] colW);
            int xMax = tableData.GetLength(0),
                yMax = tableData.GetLength(1);
            for (int x = 0; x < xMax; x++) {
                colBuilder.Clear();
                dataBuilder.Clear();
                for (int y = 0; y < yMax; y++) {
                    char leftCorner;
                    char rightCorner;
                    if (x == 0) {
                        rightCorner = borderSprites[0];
                        leftCorner = (y == 0) ? borderSprites[1] : borderSprites[2];
                    }
                    else {
                        rightCorner = borderSprites[3];
                        leftCorner = (y == 0) ? borderSprites[4] : borderSprites[5];
                    }
                    char horisontal = borderSprites[6], vertical = borderSprites[7];
                    colBuilder.Append(leftCorner + new String(horisontal, colW[y]));
                    dataBuilder.Append(vertical + new String(' ', colW[y]));
                    if (y + 1 == yMax) {
                        colBuilder.Append(rightCorner.ToString());
                        dataBuilder.Append(vertical.ToString());
                    }
                }
                rowBuilder.AppendLine(colBuilder.ToString());
                for(int k = 0; k < rowH[x]; k++)
                    rowBuilder.AppendLine(dataBuilder.ToString());
            }
            colBuilder.Clear();
            for (int y = 0; y < yMax; y++) {
                char leftCorner;
                if (y == 0)
                    leftCorner = borderSprites[8];
                else
                    leftCorner = borderSprites[9];
                char horisontal = borderSprites[6], right = borderSprites[10];
                colBuilder.Append(leftCorner + new String(horisontal, colW[y]));
                if (y + 1 == yMax)
                    colBuilder.Append(right.ToString());
            }
            rowBuilder.Append(colBuilder.ToString());
            return rowBuilder.ToString();
        }
        public enum BorderType {
            thin,
            thick,
            thickHorisontal,
            thickVertical
        }


        public void OnUpPressed() {
            int max = tableData.GetLength(0) - 1;
            if (selectedX > 0)
                selectedX--;
            else
                selectedX = max;
            PrintValues(true);
        }

        public void OnDownPressed() {
            int max = tableData.GetLength(0) - 1;
            if (selectedX < max)
                selectedX++;
            else
                selectedX = 0;
            PrintValues(true);
        }

        public void OnLeftPressed() {
            int max = tableData.GetLength(1) - 1;
            if (selectedY > 0)
                selectedY--;
            else
                selectedY = max;
            PrintValues(true);
        }

        public void OnRightPressed() {
            int max = tableData.GetLength(1) - 1;
            if (selectedY < max)
                selectedY++;
            else
                selectedY = 0;
            PrintValues(true);
        }

        public void OnEnterPressed() {
            ConsoleKeyInfo cki;
            do {
                cki = Console.ReadKey(false);
                switch (cki.Key) {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        this.OnLeftPressed();
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.D:
                        this.OnRightPressed();
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        this.OnUpPressed();
                        break;
                    case ConsoleKey.DownArrow: 
                    case ConsoleKey.S:
                        this.OnDownPressed();
                        break;
                    default:
                        continue;
                }
            } while (cki.Key != ConsoleKey.Escape);
        }

        public void OnEscapePressed() {
            
        }

        public override void Draw() {
            base.Draw();
            this.PrintValues();
        }
    }
}