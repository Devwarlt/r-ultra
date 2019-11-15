using Ultraviolet.Graphics.Graphics2D.Text;

namespace org.loesoft.rotmg.ultra.core.texts
{
    public struct TextDisplayStyle
    {
        private readonly string name;
        private readonly TextStyle style;

        public TextDisplayStyle(string name, TextStyle style)
        {
            this.name = name;
            this.style = style;
        }

        public string GetName() => name;

        public TextStyle GetStyle() => style;
    }
}
