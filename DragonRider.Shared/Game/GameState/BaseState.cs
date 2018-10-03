namespace DragonRider.Shared.Game.GameState
{
    public abstract class BaseState : Api.GameState.GameState
    {
        protected new Game Game { get; }

        public BaseState(Game game) : base(game)
        {
            Game = game;
        }
    }
}
