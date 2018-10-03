using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace DragonRider.Shared.Api.Artemis.Component.Basic
{
    [EntityComponent]
    public class PositionComponent : EntityComponent
    {
        public Vector2 Position { get; set; }
    }
}
