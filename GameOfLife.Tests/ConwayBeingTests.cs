using IvorChalton.GameOfLife.DTO;
using IvorChalton.GameOfLife.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IvorChalton.GameOfLife.Tests
{
    [TestClass]
    public class ConwayBeingTests
    {
        [TestMethod]
        public void Test_Conway_Being_Decisions()
        {
            // Two or three cells surrounding mean the cell stays put
            var conwayBeing = new ConwayBeing(new MockCellAcquirer_with2_alive());
            Cell cell = new Cell(1, 2) { IsAlive = true };
            Assert.IsFalse(conwayBeing.Decide(cell));  // No change expected

            conwayBeing = new ConwayBeing(new MockCellAcquirer_with3_alive());
            cell = new Cell(1, 2) { IsAlive = true };
            Assert.IsFalse(conwayBeing.Decide(cell));  // No change expected

            conwayBeing = new ConwayBeing(new MockCellAcquirer_with1_alive());
            cell = new Cell(1, 2) { IsAlive = true };
            Assert.IsTrue(conwayBeing.Decide(cell));  // Change expected - starvation

            conwayBeing = new ConwayBeing(new MockCellAcquirer_with4_alive());
            cell = new Cell(1, 2) { IsAlive = true };
            Assert.IsTrue(conwayBeing.Decide(cell));  // Change expected - overcrowding

        }

        class MockCellAcquirer_with1_alive : ICellAcquirer
        {
            public List<Cell> Acquire(Point p)
            {
                return new List<Cell>(new Cell[]
                {
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                });
            }
        }


        class MockCellAcquirer_with2_alive : ICellAcquirer
        {
            public List<Cell> Acquire(Point p)
            {
                return new List<Cell>(new Cell[]
                {
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                });
            }
        }

        class MockCellAcquirer_with3_alive : ICellAcquirer
        {
            public List<Cell> Acquire(Point p)
            {
                return new List<Cell>(new Cell[]
                {
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                });
            }
        }
        class MockCellAcquirer_with4_alive : ICellAcquirer
        {
            public List<Cell> Acquire(Point p)
            {
                return new List<Cell>(new Cell[]
                {
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = false },
                    new Cell(1, 2) {IsAlive = true },
                    new Cell(1, 2) {IsAlive = true},
                });
            }
        }
    }
}
