using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Api.Scene
{
    public abstract class Scene : IDisposable
    {
        protected Game Game { get; }
        protected SceneManager Manager { get; }
        protected SpriteBatch SpriteBatch { get; private set; }

        protected Scene(Game game)
        {
            Game = game;
            Manager = Game.Services.GetService<SceneManager>();
        }

        public virtual void Initialize()
        {
            Debug.WriteLine("[API] Scene.Initialize()");

            SpriteBatch = Game.Services.GetService<SpriteBatch>();
        }

        protected virtual void LoadContent()
        {
        }

        public virtual void Update(float delta)
        {
        }

        public virtual void Draw()
        {
        }

        public void Dispose()
        {
            Debug.WriteLine("[API] Scene.Dispose()");

            UnloadContent();
        }

        protected virtual void UnloadContent()
        {
        }
    }
}
