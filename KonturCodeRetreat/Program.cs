using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KonturCodeRetreat
{
    class GameHandler
    {
        public static int[][] MakeTurn(int[][] field)
        {
            return Enumerable.Range(0, field.Length)
                .Select(_ => Enumerable.Range(0, field[0].Length)
                    .Select(__ => 0).ToArray())
                .ToArray();
        }

        public static IEnumerable<Point> GetNeighbours(int x, int y, int[][] field)
        {
            var dxs = new int[] {-1, 0, 1};
            var dys = new int[] {-1, 0, 1};

            foreach (var dx in dxs)
            {
                foreach (var dy in dys)
                {
                    if (x + dx >= 0 && x + dx < field.Length && y + dy >= 0 && y + dy < field[0].Length && !(dx == 0 && dy == 0))
                        yield return new Point(x + dx, y + dy);
                }
            }
        } 
    }
    class Program
    {
    
        static void Main(string[] args)
        {
        }
    }
}
