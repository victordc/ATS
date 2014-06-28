using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATS.Service.WCFClient.TimesheetClient;

namespace ATS.Service.WCFClient
{
    public partial class TimesheetDetailForm : Form
    {
        private String name;
        private TimeSheet[] data;

        public TimesheetDetailForm(string name, TimeSheet[] data)
        {
            this.name = name;
            this.data = data;
            InitializeComponent();
        }

        private void TimesheetDetailForm_Load(object sender, EventArgs e)
        {
            label2.Text = name;
            dataGridView1.DataSource = data;
        }


    }
}
