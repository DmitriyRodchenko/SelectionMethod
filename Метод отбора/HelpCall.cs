using System.Windows.Forms;

namespace Методотбора
{
    public partial class HelpCall : Form
    {
        public HelpCall()
        {
            InitializeComponent();
        }

        private void HelpCall_Load(object sender, System.EventArgs e)
        {
            buttonOK.Select();
        }
    }
}
