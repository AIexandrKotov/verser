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
    public partial class UpdatedNotification : Form
    {
        public readonly VerserMain Main;

        public UpdatedNotification(VerserMain main)
        {
            InitializeComponent();
            Main = main;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Main.FullUpdate();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
