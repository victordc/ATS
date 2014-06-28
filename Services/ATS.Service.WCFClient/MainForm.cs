using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATS.Service.WCFClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffForm form = new StaffForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LeaveForm form = new LeaveForm();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TimesheetForm form = new TimesheetForm();
            form.Show();
        }
    }
}
