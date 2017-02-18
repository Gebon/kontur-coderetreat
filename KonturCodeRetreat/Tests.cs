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
        public void singleCitizenShouldNntLive()
        {
            var field = new int[][]
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
    }
}
