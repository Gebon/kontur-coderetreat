using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KonturCodeRetreat
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void singleCitizenShouldNotLive()
        {
            var field = new[]
            {
                new[] {0}
            };

            field = GameHandler.MakeTurn(field);

            Assert.AreEqual(0, field[0][0]);
        }

        [Test]
        public void singleCitizenShouldDie()
        {
            var field = new[]
            {
                new[] {1}
            };

            field = GameHandler.MakeTurn(field);

            Assert.AreEqual(0, field[0][0]);
        }

        [Test]
        public void CitizenWithLessThanTwoNeighboursShouldDie()
        {
            var field = new[]
            {
                new[] {1, 0},
                new[] {0, 1},
            };

            field = GameHandler.MakeTurn(field);

            Assert.AreEqual(0, field[1][1]);
        }

        [Test]
        public void CitizenWithTwoNeighboursShouldtChangeState()
        {
            var field = new[]
            {
                new[] {1, 1, 1}
            };

            field = GameHandler.MakeTurn(field);

            Assert.AreEqual(1, field[0][1]);
        }

        [Test]
        public void ZeroNeighbours()
        {
            var field = new[]
            {
                new[] {0}
            };

            var neigbours = GameHandler.GetNeighbours(0, 0, field);

            Assert.AreEqual(0, neigbours.Count());
        }

        [Test]
        public void ThreeNeigbours()
        {
            var field = new[]
            {
                new[] {0, 0},
                new[] {0, 0}
            };

            var neigbours = GameHandler.GetNeighbours(0, 0, field);

            Assert.AreEqual(3, neigbours.Count());
        }

        [Test]
        public void EightNeibours()
        {
            var fie
        }
    }
}
