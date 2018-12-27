using System;
using System.Diagnostics;

namespace DragonRider.Shared.Api.System
{
    public abstract class System : IDisposable
    {
        protected Game Game { get; }

        protected System(Game game)
        {
            Debug.WriteLine("[API] Register service: " + GetType().Name);

            Game = game;
            Game.Services.AddService(this);
        }

        public void Dispose()
        {
            Debug.WriteLine("[API] Remove service: " + GetType().Name);

            Game.Services.RemoveService(GetType());
        }
    }
}
