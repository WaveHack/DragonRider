using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Api.GameState
{
    public interface IStateManager
    {
        GameState CurrentState { get; }

        event EventHandler StateChanged;

        void PushState(GameState state, PlayerIndex? index);
        void ChangeState(GameState state, PlayerIndex? index);
        void PopState();
        bool ContainsState(GameState state);
    }

    public class StateManager : GameComponent, IStateManager
    {
        #region Fields

        protected Microsoft.Xna.Framework.Game game;

        private readonly Stack<GameState> gameStates = new Stack<GameState>();

        private const int startDrawOrder = 5000;
        private const int drawOrderInc = 50;
        private int drawOrder;

        #endregion

        #region Event Handlers

        public event EventHandler StateChanged;

        #endregion

        #region Properties

        public GameState CurrentState => gameStates.Peek();

        #endregion

        #region Constructors

        public StateManager(Microsoft.Xna.Framework.Game game) : base(game)
        {
            this.game = game;
            this.game.Services.AddService(typeof(IStateManager), this);
        }

        #endregion

        #region Methods

        public void PushState(GameState state, PlayerIndex? index)
        {
            Debug.WriteLine("StateManager.PushState(state: " + state + ", index: " + index + ")");

            drawOrder += drawOrderInc;
            AddState(state, index);
            OnStateChanged();
        }

        public void ChangeState(GameState state, PlayerIndex? index)
        {
            Debug.WriteLine("StateManager.ChangeState(state: " + state + ", index: " + index + ")");

            while (gameStates.Count > 0)
                RemoveState();

            drawOrder = startDrawOrder;
            state.DrawOrder = drawOrder;

            PushState(state, index);
        }

        public void PopState()
        {
            Debug.WriteLine("StateManager.PopState()");

            if (gameStates.Count == 0)
                return;

            RemoveState();
            drawOrder -= drawOrderInc;
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            Debug.WriteLine("StateManager.OnStateChanged()");

            StateChanged?.Invoke(this, null);
        }

        private void AddState(GameState state, PlayerIndex? index)
        {
            Debug.WriteLine("StateManager.AddState(state: " + state + ", index: " + index + ")");

            gameStates.Push(state);
            state.PlayerIndexInControl = index;
            game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        private void RemoveState()
        {
            var state = gameStates.Peek();

            Debug.WriteLine("StateManager.RemoveState(state: " + state + ")");

            StateChanged -= state.StateChanged;
            game.Components.Remove(state);
            gameStates.Pop();
        }

        #endregion
    }
}
