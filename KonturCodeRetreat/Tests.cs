using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace KonturCodeRetreat
{
    [TestFixture]
    internal class Tests
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
}