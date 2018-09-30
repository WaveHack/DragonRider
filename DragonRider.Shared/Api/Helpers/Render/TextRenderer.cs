using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Api.Helpers.Render
{
    public class TextRenderer
    {
        #region Fields

        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        #endregion

        #region Properties

        public SpriteBatch SpriteBatch
        {
            get => _spriteBatch;
            set => _spriteBatch = value;
        }

        public SpriteFont SpriteFont
        {
            get => _spriteFont;
            set => _spriteFont = value;
        }

        #endregion

        #region Methods

        public void DrawShadowed(string text, Vector2 position, Color color, float scale = 1)
        {
            // Shadow
            _spriteBatch.DrawString(_spriteFont, text, position + new Vector2(1, 1), Color.Gray, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

            // Text
            _spriteBatch.DrawString(_spriteFont, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawStroked(string text, Vector2 position, Color color, float scale = 1)
        {
            // Stroke
            _spriteBatch.DrawString(_spriteFont, text, position + new Vector2(0, -1), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_spriteFont, text, position + new Vector2(0, 1), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_spriteFont, text, position + new Vector2(-1, 0), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_spriteFont, text, position + new Vector2(1, -0), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);

            // Text
            _spriteBatch.DrawString(_spriteFont, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        #endregion
    }
}
