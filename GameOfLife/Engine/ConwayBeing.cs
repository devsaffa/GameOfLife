using IvorChalton.GameOfLife.DTO;
using System.Linq;

namespace IvorChalton.GameOfLife.Engine
{
    /// <summary>
    /// The Conway being decides whether a cell lives or dies, dependent on its surrounding cells
    /// </summary>
    class ConwayBeing : IOmnipotentBeing
    {
        readonly ICellAcquirer _acquirer;

        public ConwayBeing(ICellAcquirer acquirer)
        {
            _acquirer = acquirer;
        }

        public bool Decide(Cell cell)
        {
            var surroundingAlive = _acquirer.Acquire(cell.Location).Count(c => c.IsAlive);

            if ((!cell.IsAlive && surroundingAlive == 3) // Dead cells come alive if exactly 3 joining cells are alive
                || (cell.IsAlive && !(surroundingAlive == 2 || surroundingAlive == 3))) // Living cells stay alive only if 2 or 3 joining cells are alive
                return true;

            return false;
        }
    }
}
