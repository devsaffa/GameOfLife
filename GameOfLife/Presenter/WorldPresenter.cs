using IvorChalton.GameOfLife.Engine;
using IvorChalton.GameOfLife.View;

namespace IvorChalton.GameOfLife.Presenter
{
    /// <summary>
    /// Presenter for a World
    /// </summary>
    class WorldPresenter
    {
        readonly IWorldView _view;
        readonly World _world;

        public WorldPresenter(IWorldView view, World world)
        {
            _view = view;
            _world = world;

            _world.CellsUpdated += (o, e) => _view.Update(e.Cells);

            _view.OnSeedWorld += (o, e) => _world.Seed(e.NumCells);
            _view.OnGrowOlderOrdered += (o, e) => _world.GrowOlder();
        }
    }
}
