using Ultraviolet;
using Ultraviolet.Windows.Forms;

namespace org.loesoft.rotmg.ultra.core.gui.screen
{
    public partial class TestForm : UltravioletForm
    {
        private UltravioletContext context;

        public TestForm(UltravioletContext context)
        {
            this.context = context;

            OnInitializing();

            InitializeComponent();
        }

        protected override void OnInitializing()
        {
            OnCreatingUltravioletContext();
        }

        protected override UltravioletContext OnCreatingUltravioletContext() => context;
    }
}
