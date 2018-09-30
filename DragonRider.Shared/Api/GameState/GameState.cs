using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DragonRider.Shared.Api.GameState
{
    public interface IGameState
    {
        GameState Tag { get; }
        PlayerIndex? PlayerIndexInControl { get; set; }
    }

    public abstract partial class GameState : DrawableGameComponent, IGameState
    {
        #region Fields

        protected readonly IStateManager manager;
        protected ContentManager content;

        protected GameState tag;
        protected PlayerIndex? indexInControl;

        protected readonly List<GameComponent> components;

        #endregion

        #region Properties

        public GameState Tag => tag;

        public PlayerIndex? PlayerIndexInControl
        {
            get => indexInControl;
            set => indexInControl = value;
        }

        public List<GameComponent> Components => components;

        #endregion

        #region Constructors

        public GameState(Microsoft.Xna.Framework.Game game) : base(game)
        {
            manager = (IStateManager) Game.Services.GetService(typeof(IStateManager));
            content = game.Content;
            tag = this;
            components = new List<GameComponent>();
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                if (component.Enabled)
                    component.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var component in components)
                if (component is DrawableGameComponent gameComponent && gameComponent.Visible)
                    gameComponent.Draw(gameTime);
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (manager.CurrentState == tag)
                Show();
            else
                Hide();
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;

            foreach (var component in components)
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

            foreach (var component in components)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent gameComponent)
                    gameComponent.Visible = false;
            }
        }

        #endregion
    }
}
