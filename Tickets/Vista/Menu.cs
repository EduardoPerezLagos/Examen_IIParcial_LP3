using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void NuevoTicketsToolStripButton_Click(object sender, EventArgs e)
        {
            TicketsForm ticketsform = new TicketsForm();
            ticketsform.MdiParent = this;
            ticketsform.Show();
        }

        private void backStageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
