using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace DragonRider.Shared.Api.Artemis.Component.Graphics
{
    [EntityComponent]
    public class TextureComponent : EntityComponent
    {
        public Texture2D Texture2D { get; set; }
    }
}
