using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class NameForm : Form
    {
        public string PlayerName { get; set; }
        
        public NameForm()
        {
            InitializeComponent();
            NameEntry.Text = "Anonymous";
            NameEntry.SelectAll();
            NameEntry.Focus();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            PlayerName = NameEntry.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PlayerName = NameEntry.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void NameEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PlayerName = NameEntry.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
