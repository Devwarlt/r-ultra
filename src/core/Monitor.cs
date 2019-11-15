using org.loesoft.rotmg.ultra.core.entities;
using org.loesoft.rotmg.ultra.core.texts;
using Ultraviolet;
using Ultraviolet.Graphics.Graphics2D;

namespace org.loesoft.rotmg.ultra.core
{
    public sealed class Monitor
    {
        private const string pattern = "TEXT";
        //"|b|Monitor|b|\n" +
        //"\t\tThis is the game monitor.";

        private readonly TextDisplay text;

        public Monitor() => text = new TextDisplay(pattern, Color.Red, TextAlignment.MiddleCenter);

        public void Draw(SpriteBatch batch) => text.Draw(batch);

        public void SetPosition(Entity entity) => text.SetPosition(entity.GetPosition());
    }
}
