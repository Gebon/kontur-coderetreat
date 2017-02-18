using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KonturCodeRetreat
{
    public class Field
    {
        public Field(State[][] state)
        {
            RealFiled = state;
        }
        public State[][] RealFiled { get; set; }
        public IEnumerable<Point> GetNeighbours(Point current)
        {
            var dxs = new[] { -1, 0, 1 };
            var dys = new[] { -1, 0, 1 };
            return dxs.SelectMany(dx => dys, (dx, dy) => new Point(current.X + dx, current.Y + dy)).Where(p => IsInField(p) && !p.Equals(current));
        }

        private bool IsInField(Point position)
        {
            return IsInRange(position.X, 0, RealFiled.Length)
                   && IsInRange(position.Y, 0, RealFiled[0].Length);
        }

        private bool IsInRange(int num, int start, int end)
        {
            return num >= start && num < end;
        }

        public void Tick()
        {
            var newState = Enumerable.Range(0, RealFiled.Length)
                .Select(_ => Enumerable.Range(0, RealFiled[0].Length)
                    .Select(__ => State.Dead).ToArray())
                .ToArray();

            for (int i = 0; i < RealFiled.Length; i++)
            {
                for (int j = 0; j < RealFiled[0].Length; j++)
                {
                    newState[i][j] = DetermineNextState(new Point(i, j));
                }
            }
            RealFiled = newState;
        }

        private State DetermineNextState(Point point)
        {
            var neighbourStates = GetNeighbours(point)
                .Select(p => this[p]);

            var aliveNeighboursCount = neighbourStates.Count(s => s == State.Alive);
            if (aliveNeighboursCount == 3)
            {
                return State.Alive;
            }

            if (aliveNeighboursCount == 2)
            {
                return this[point];
            }

            return State.Dead;
        }

        public State this[Point position]
        {
            get { return RealFiled[position.X][position.Y]; }
            set { RealFiled[position.X][position.Y] = value; }
        }

        public State this[int x, int y]
        {
            get { return this[new Point(x, y)]; }
            set { this[new Point(x, y)] = value; }
        }
    }

    public enum State
    {
        Dead = 0,
        Alive = 1
    }

    class Program
    {
        static void Main(string[] args)
        {
            var initialState = Enumerable.Range(0, 20)
                .Select(_ => Enumerable.Range(0, 40)
                    .Select(__ => State.Dead)
                    .ToArray())
                .ToArray();

            var field = new Field(initialState);

            var alivePoints = new HashSet<Point>
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
            foreach (var alivePoint in alivePoints)
            {
                field[alivePoint] = State.Alive;
            }

            for (int i = 0; i < 10000; i++)
            {
                Visualize(field.RealFiled);
                Thread.Sleep(100);
                field.Tick();
            }
        }

        public static void Visualize(State[][] state)
        {
            Console.Clear();
            foreach (var row in state)
            {
                var sb = new StringBuilder();
                foreach (var item in row)
                {
                    sb.Append(item == State.Alive ? "■" : " ");
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
