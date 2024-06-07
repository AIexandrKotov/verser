using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace verser_gui
{
    public partial class SuperForm : Form
    {
        public SuperForm()
        {
            InitializeComponent();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            if (appendsMenu.Visible)
                appendsMenu.Hide();
            else
            {
                appendsMenu.Show(btnSender, new Point(btnSender.Width - appendsMenu.Width, btnSender.Height));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            if (configurationsListMenu.Visible)
                configurationsListMenu.Hide();
            else
            {
                configurationsListMenu.Show(btnSender, new Point(btnSender.Width - configurationsListMenu.Width, btnSender.Height));
            }
        }
    }
}
