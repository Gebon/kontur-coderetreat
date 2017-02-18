using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonturCodeRetreat
{
    class Program
    {
        public HashSet<Point> AliveCells { get; private set; }
        private IList<HashSet<Point>> previousStates = new List<HashSet<Point>>(); 
        static void Main(string[] args)
        {
            var field = new Program();
            field.Tick();
        }

        public void Tick()
        {
            previousStates.Add(AliveCells);
            var cellsForProcessing = new HashSet<Point>(AliveCells.SelectMany(p => GetNeighbours(p)).Concat(AliveCells));
            AliveCells =  new HashSet<Point>(cellsForProcessing.Where(p => ShouldBeAlive(p)));
        }

        private bool ShouldBeAlive(Point point)
        {
            var neighboursCount = GetNeighbours(point).Count(p => AliveCells.Contains(p));
            return neighboursCount == 3 || (neighboursCount == 2 && AliveCells.Contains(point));
        }

        IEnumerable<Point> GetNeighbours(Point position)
        {
            var dxs = new[] { -1, 0, 1 };
            var dys = new[] { -1, 0, 1 };
            return dxs.SelectMany(dx => dys, (dx, dy) => new Point(position.X + dx, position.Y + dy))
                    .Where(p => !p.Equals(position));
        }
    }
}
