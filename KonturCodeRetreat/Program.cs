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
            var dxs = new[] {-1, 0, 1};
            var dys = new[] {-1, 0, 1};
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
            var initialState = new State[][]
            {
                new State[] {0, 0 ,0 ,0},
                new State[] {0, (State) 1 , 0, 0},
                new State[] {0, (State)1, 0, 0},
                new State[] {0, (State)1, 0 ,0},
                new State[] {0, 0 ,0 ,0},
            };

            var filed = new Field(initialState);


            for (int i = 0; i < 10000; i++)
            {
                Visualise(filed.RealFiled);
                Thread.Sleep(1000);
                filed.Tick();
            }
        }

        public static void Visualise(State[][] state)
        {
            Console.Clear();
            foreach (var row in state)
            {
                foreach (var item in row)
                {
                    Console.Write(item == State.Alive? "■" : " ");
                }
                Console.WriteLine();
            }
        }
    }
}
