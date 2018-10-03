using MonoGame.Extended.Entities;

namespace DragonRider.Shared.Api.Artemis.System
{
    public abstract class ServiceSystem : EntitySystem
    {
        #region Constructors

        protected ServiceSystem()
        {
            Game.Services.AddService(this);
        }

        #endregion
    }
}
