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
    public partial class TimesheetForm : Form
    {
        public TimesheetForm()
        {
            InitializeComponent();
        }

        private void TimesheetForm_Load(object sender, EventArgs e)
        {
            tbMonth.Text = Convert.ToString(DateTime.Today.Month);
            tbYear.Text = Convert.ToString(DateTime.Today.Year);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimesheetServiceClient tsClient = new TimesheetServiceClient();
            int year = Convert.ToInt32(tbYear.Text);
            int month = Convert.ToInt32(tbMonth.Text);
            var result = tsClient.GetTimesheetSummary(1, year, month);
            dataGridView1.Rows.Clear();
            foreach (var p in result)
            {
                dataGridView1.Rows.Add(new string[] {
                    Convert.ToString(p.StaffID),
                    p.StaffName,
                    p.SupervisorName,
                    p.AgentInCharge,
                    Convert.ToString(p.WorkingHours) + " Hours",
                    p.Status
                });
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TimesheetServiceClient tsClient = new TimesheetServiceClient();
            int year = Convert.ToInt32(tbYear.Text);
            int month = Convert.ToInt32(tbMonth.Text);
            int personId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
            string name = dataGridView1.Rows[e.RowIndex].Cells["StaffName"].Value.ToString();
            var data = tsClient.GetTimesheetDetail(personId, year, month);
            TimesheetDetailForm form = new TimesheetDetailForm(name, data);
            form.Show();

        }
    }
}
