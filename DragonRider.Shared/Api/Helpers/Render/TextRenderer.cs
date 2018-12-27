using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Api.Helpers.Render
{
    public class TextRenderer
    {
        #region Properties

        public SpriteBatch SpriteBatch { get; set; }
        public SpriteFont SpriteFont { get; set; }

        #endregion

        #region Methods

        public void DrawShadowed(string text, Vector2 position, Color color, float scale = 1)
        {
            // Shadow
            SpriteBatch.DrawString(SpriteFont, text, position + new Vector2(1, 1), Color.Gray, 0, Vector2.Zero, scale,
                SpriteEffects.None, 0);

            // Text
            SpriteBatch.DrawString(SpriteFont, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawStroked(string text, Vector2 position, Color color, float scale = 1)
        {
            // Stroke
            SpriteBatch.DrawString(SpriteFont, text, position + new Vector2(0, -1), Color.Black, 0f, Vector2.Zero,
                scale, SpriteEffects.None, 0);
            SpriteBatch.DrawString(SpriteFont, text, position + new Vector2(0, 1), Color.Black, 0f, Vector2.Zero,
                scale, SpriteEffects.None, 0);
            SpriteBatch.DrawString(SpriteFont, text, position + new Vector2(-1, 0), Color.Black, 0f, Vector2.Zero,
                scale, SpriteEffects.None, 0);
            SpriteBatch.DrawString(SpriteFont, text, position + new Vector2(1, -0), Color.Black, 0f, Vector2.Zero,
                scale, SpriteEffects.None, 0);

            // Text
            SpriteBatch.DrawString(SpriteFont, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        #endregion
    }
}
