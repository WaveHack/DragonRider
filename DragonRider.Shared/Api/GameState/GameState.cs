using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Api.GameState
{
    public abstract partial class GameState : DrawableGameComponent
    {
        #region Properties

        public PlayerIndex? PlayerIndexInControl { get; set; }
        public GameState Tag { get; }
        public List<GameComponent> Components { get; }

        protected ContentManager Content { get; }
        protected new Game Game { get; }
        protected IStateManager Manager { get; }
        protected SpriteBatch SpriteBatch { get; private set; }

        #endregion

        #region Constructors

        public GameState(Game game) : base(game)
        {
            PlayerIndexInControl = PlayerIndex.One;
            Tag = this;
            Components = new List<GameComponent>();
            Game = game;
            Content = game.Content;
            Manager = (IStateManager) Game.Services.GetService(typeof(IStateManager));
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();

            SpriteBatch = Game.SpriteBatch;
        }

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
