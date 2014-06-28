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
    public partial class LeaveForm : Form
    {
        public LeaveForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void LeaveForm_Load(object sender, EventArgs e)
        {
            tbMonth.Text = Convert.ToString(DateTime.Today.Month);
            tbYear.Text = Convert.ToString(DateTime.Today.Year);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LeaveServiceClient lsClient = new LeaveServiceClient();
            int companyId = Convert.ToInt32(tbCompany.Text);
            int year = Convert.ToInt32(tbYear.Text);
            int month = Convert.ToInt32(tbMonth.Text);
            var result = lsClient.GetLeaveSummary(companyId, year, month);
            dataGridView1.Rows.Clear();
            foreach (LeaveSummary p in result)
            {
                dataGridView1.Rows.Add(new string[] {
                    Convert.ToString(p.StaffID),
                    p.StaffName,
                    Convert.ToString(p.ApprovedDuration) + " Days",
                    Convert.ToString(p.PendingDuration) + " Days",
                    Convert.ToString(p.RejectDuration) + " Days",
                    Convert.ToString(p.TotalDuration) + " Days",
                });
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LeaveServiceClient lsClient = new LeaveServiceClient();
            int year = Convert.ToInt32(tbYear.Text);
            int month = Convert.ToInt32(tbMonth.Text);
            int personId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
            string name = dataGridView1.Rows[e.RowIndex].Cells["StaffName"].Value.ToString();
            var data = lsClient.GetLeaveDetails(personId, year, month);
            LeaveDetailForm form = new LeaveDetailForm(name, data);
            form.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMonth_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
