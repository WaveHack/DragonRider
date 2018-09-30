using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Api.DataTypes.Text
{
    public struct ColoredText
    {
        public readonly string Text;
        public readonly Color Color;

        public ColoredText(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
}
