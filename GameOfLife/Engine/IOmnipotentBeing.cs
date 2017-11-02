using IvorChalton.GameOfLife.DTO;

namespace IvorChalton.GameOfLife.Engine
{
    /// <summary>
    /// A being responsible for determining whether a cell lives or dies
    /// </summary>
    interface IOmnipotentBeing
    {
        /// <summary>
        /// Decide whether this cell should live or die given its surrounding cells
        /// </summary>
        /// <returns>True if the cell should CHANGE state</returns>
        bool Decide(Cell cell);
    }
}
