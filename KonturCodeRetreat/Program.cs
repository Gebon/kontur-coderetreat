using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace KonturCodeRetreat
{
    class Field
    {
        public HashSet<Point> AliveCells { get; private set; }

        public Field(IEnumerable<Point> initialCells)
        {
            AliveCells = new HashSet<Point>(initialCells);
        }
        public void Tick()
        {
            var allCellsWithNeighbours = new HashSet<Point>(AliveCells.SelectMany(GetNeighbours).Concat(AliveCells));
            AliveCells = new HashSet<Point>(allCellsWithNeighbours.Where(ShouldBeAlive));
        }

        private bool ShouldBeAlive(Point point)
        {
            var neighboursCount = GetNeighbours(point).Count(p => AliveCells.Contains(p));
            return neighboursCount == 3 || (neighboursCount == 2 && AliveCells.Contains(point));
        }

        public IEnumerable<Point> GetNeighbours(Point position)
        {
            var dxs = new[] {-1, 0, 1};
            var dys = new[] {-1, 0, 1};
            return dxs.SelectMany(dx => dys, (dx, dy) => new Point(position.X + dx, position.Y + dy))
                .Where(p=>!p.Equals(position));
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var glidersFactory = new HashSet<Point>
            {
                new Point(5, 1),
                new Point(6, 1),
                new Point(5, 2),
                new Point(6, 2),
                new Point(5, 11),
                new Point(6, 11),
                new Point(7, 11),
                new Point(4, 12),
                new Point(8, 12),
                new Point(3, 13),
                new Point(9, 13),
                new Point(3, 14),
                new Point(9, 14),
                new Point(6, 15),
                new Point(4, 16),
                new Point(8, 16),
                new Point(5, 17),
                new Point(6, 17),
                new Point(7, 17),
                new Point(6, 18),
                new Point(3, 21),
                new Point(4, 21),
                new Point(5, 21),
                new Point(3, 22),
                new Point(4, 22),
                new Point(5, 22),
                new Point(2, 23),
                new Point(6, 23),
                new Point(1, 25),
                new Point(2, 25),
                new Point(6, 25),
                new Point(7, 25),
                new Point(4, 35),
                new Point(5, 35),
                new Point(4, 36),
                new Point(5, 36)
            };
            var field = new Field(glidersFactory);
            Console.CursorVisible = false;
            while (true)
            {
                Visualize(field);
                field.Tick();
                Thread.Sleep(100);
            }

        }

        private static int StartX;
        private static int StartY;

        static void Visualize(Field field)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        StartY--;
                        break;
                    case ConsoleKey.RightArrow:
                        StartY++;
                        break;
                    case ConsoleKey.UpArrow:
                        StartX--;
                        break;
                    case ConsoleKey.DownArrow:
                        StartX++;
                        break;
                }
            }

            var sb = new StringBuilder();
            for (int i = StartX; i < StartX + Console.WindowHeight - 1; i++)
            {
                for (int j = StartY; j < StartY + Console.WindowWidth - 1; j++)
                {
                    sb.Append(field.AliveCells.Contains(new Point(i, j)) ? '+' : ' ');
                }
                sb.AppendLine();
            }
            Console.Clear();
            Console.Write(sb.ToString());

        }
    }
}
