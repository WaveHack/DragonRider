using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Api.DataTypes
{
    public struct ColoredText
    {
        public string Text { get; }
        public Color Color { get; }

        public ColoredText(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
}
