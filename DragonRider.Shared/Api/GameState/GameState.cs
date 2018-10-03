using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DragonRider.Shared.Api.GameState
{
    public abstract partial class GameState : DrawableGameComponent
    {
        #region Properties

        public GameState Tag { get; }
        public PlayerIndex? PlayerIndexInControl { get; set; }

        public List<GameComponent> Components { get; }

        protected IStateManager Manager { get; }
        protected ContentManager Content { get; }

        #endregion

        #region Constructors

        public GameState(Microsoft.Xna.Framework.Game game) : base(game)
        {
            Tag = this;
            Components = new List<GameComponent>();
            Manager = (IStateManager) Game.Services.GetService(typeof(IStateManager));
            Content = game.Content;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            foreach (var component in Components)
                if (component.Enabled)
                    component.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var component in Components)
                if (component is DrawableGameComponent gameComponent && gameComponent.Visible)
                    gameComponent.Draw(gameTime);
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (Manager.CurrentState == Tag)
                Show();
            else
                Hide();
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;

            foreach (var component in Components)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent gameComponent)
                    gameComponent.Visible = true;
            }
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;

            foreach (var component in Components)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent gameComponent)
                    gameComponent.Visible = false;
            }
        }

        #endregion
    }
}
