using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace KonturCodeRetreat
{
    class Field
    {
        public HashSet<Point> AliveCells { get; private set; }

        public Field(HashSet<Point> initialCells)
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
            var dxs = new int[] {-1, 0, 1};
            var dys = new int[] {-1, 0, 1};
            return dxs.SelectMany(dx => dys, (dx, dy) => new Point(position.X + dx, position.Y + dy))
                .Where(p=>!p.Equals(position));
        }

    }
    [TestFixture]
    class Tests
    {
        private void AssertSetsAreEqual(HashSet<Point> one, HashSet<Point> other)
        {
            Assert.AreEqual(one.OrderBy(x => x.X).ThenBy(x => x.Y),
                other.OrderBy(x => x.X).ThenBy(x => x.Y));
        }
        [Test]
        public void squareStillSquare()
        {
            var cells = new HashSet<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1)
            };

            var field = new Field(cells);
            field.Tick();
            AssertSetsAreEqual(cells, field.AliveCells);
        }

        [Test]
        public void stick()
        {
            var cells = new HashSet<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(0, 2)
            };

            var newCells = new HashSet<Point>
            {
                new Point(-1, 1),
                new Point(0, 1),
                new Point(1, 1)
            };
            var field = new Field(cells);
            field.Tick();
            AssertSetsAreEqual(newCells, field.AliveCells);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var cells = new HashSet<Point>
            {
                new Point(0, 0),
                new Point(2, 0),
                new Point(2, 1),
                new Point(1, 1),
                new Point(1, 2),
            };

            var field = new Field(cells);
            while (true)
            {
                Visualize(field);
                field.Tick();
                Thread.Sleep(1000);
            }

        }

        static void Visualize(Field field)
        {
            Console.Clear();
            var minX = field.AliveCells.Min(p => p.X);
            var minY = field.AliveCells.Min(p => p.Y);

            var maxX = field.AliveCells.Max(p => p.X);
            var maxY = field.AliveCells.Max(p => p.Y);

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    Console.Write(field.AliveCells.Contains(new Point(i, j)) ? '+' : ' ');
                }
                Console.WriteLine();
            }

        }
    }
}
