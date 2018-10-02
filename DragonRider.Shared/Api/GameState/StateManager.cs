using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Api.GameState
{
    public interface IStateManager
    {
        event EventHandler StateChanged;

        GameState CurrentState { get; }

        void PushState(GameState state, PlayerIndex? index);
        void ChangeState(GameState state, PlayerIndex? index);
        void PopState();
        bool ContainsState(GameState state);
    }

    public class StateManager : GameComponent, IStateManager
    {
        #region Constants

        private const int DrawOrderStart = 5000;
        private const int DrawOrderIncrement = 50;

        #endregion

        #region Fields

        private readonly Stack<GameState> _gameStates = new Stack<GameState>();
        private int _drawOrder;

        #endregion

        #region Properties

        public GameState CurrentState => _gameStates.Peek();

        #endregion

        #region Constructors

        public StateManager(Microsoft.Xna.Framework.Game game) : base(game)
        {
            Game.Services.AddService(typeof(IStateManager), this);
        }

        #endregion

        #region Events

        public event EventHandler StateChanged;

        #endregion

        #region Methods

        public void PushState(GameState state, PlayerIndex? index)
        {
            Debug.WriteLine("StateManager.PushState(state: " + state + ", index: " + index + ")");

            _drawOrder += DrawOrderIncrement;
            AddState(state, index);
            OnStateChanged();
        }

        public void ChangeState(GameState state, PlayerIndex? index)
        {
            Debug.WriteLine("StateManager.ChangeState(state: " + state + ", index: " + index + ")");

            while (_gameStates.Count > 0)
                RemoveState();

            _drawOrder = DrawOrderStart;
            state.DrawOrder = _drawOrder;

            PushState(state, index);
        }

        public void PopState()
        {
            Debug.WriteLine("StateManager.PopState()");

            if (_gameStates.Count == 0)
                return;

            RemoveState();
            _drawOrder -= DrawOrderIncrement;
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return _gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            Debug.WriteLine("StateManager.OnStateChanged()");

            StateChanged?.Invoke(this, null);
        }

        private void AddState(GameState state, PlayerIndex? index)
        {
            Debug.WriteLine("StateManager.AddState(state: " + state + ", index: " + index + ")");

            _gameStates.Push(state);
            state.PlayerIndexInControl = index;
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        private void RemoveState()
        {
            var state = _gameStates.Peek();

            Debug.WriteLine("StateManager.RemoveState(state: " + state + ")");

            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        #endregion
    }
}
