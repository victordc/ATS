using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATS.Service.WCFClient.LeaveClient;

namespace ATS.Service.WCFClient
{
    public partial class LeaveDetailForm : Form
    {
        private Leave[] data;
        private String name;

        public LeaveDetailForm(String name, Leave[] data)
        {
            this.data = data;
            this.name = name;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LeaveDetailForm_Load(object sender, EventArgs e)
        {
            label2.Text = name;
            dataGridView1.DataSource = data;
        }
    }
}
