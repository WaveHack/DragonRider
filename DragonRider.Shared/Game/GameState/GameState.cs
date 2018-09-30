namespace DragonRider.Shared.Game.GameState
{
    public abstract class GameState : Api.GameState.GameState
    {
        #region Fields

        private Game _game;

        #endregion

        #region Properties

        protected Game GameRef => _game;

        #endregion

        #region Constructors

        public GameState(Microsoft.Xna.Framework.Game game) : base(game)
        {
            _game = (Game) game;
        }

        #endregion
    }
}
