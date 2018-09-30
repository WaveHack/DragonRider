using DragonRider.Shared.Api.GameState;
using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Game.GameState
{
    public interface ITestState : IGameState
    {
    }

    public class TestState : Api.GameState.GameState, ITestState
    {
        #region Fields

        //

        #endregion

        #region Constructors

        public TestState(Microsoft.Xna.Framework.Game game) : base(game)
        {
            game.Services.AddService(typeof(ITestState), this);
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        #endregion
    }
}
