using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATS.Service.WCFClient.WCFPerson;

namespace ATS.Service.WCFClient
{
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int companyId = Convert.ToInt32(tbCompany.Text);
            PersonServiceClient psClient = new PersonServiceClient();
            var result = psClient.GetSupervisedStaffs(companyId);
            dataGridView1.Rows.Clear();
            foreach (Person p in result)
            {

                dataGridView1.Rows.Add(new string[] {
                    Convert.ToString(p.PersonId),
                    p.PersonName,
                    p.Phone,
                    p is Staff ? (p as Staff).SupervisorName : "",
                    p is Staff ? (p as Staff).Agent.PersonName : ""
                });
                
                
            }
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //fetech row items
            
            int personId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
            PersonServiceClient psClient = new PersonServiceClient();
            Person p = psClient.GetPerson(personId);
            PersonDetailForm form = new PersonDetailForm(p);
            form.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int companyId = Convert.ToInt32(tbCompany.Text);
            try
            {
                PersonServiceClient psClient = new PersonServiceClient();
                var result = psClient.GetSupervisedStaffs(companyId);
                MessageBox.Show("No Error");
            }
            catch (FaultException<ATSFault> fe)
            {
                MessageBox.Show("Error" + Environment.NewLine 
                    + "CODE:    " + fe.Detail.ErrorCode + Environment.NewLine
                    + "MESSAGE: " + fe.Detail.ErrorMessage);
            }
        }
    }
}
