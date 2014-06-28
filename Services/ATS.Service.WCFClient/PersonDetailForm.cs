using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATS.Service.WCFClient.WCFPerson;

namespace ATS.Service.WCFClient
{
    public partial class PersonDetailForm : Form
    {
        Person p;
        public PersonDetailForm(Person p)
        {
            this.p = p;
            InitializeComponent();
        }

        private void PersonDetailForm_Load(object sender, EventArgs e)
        {
            tbName.Text = this.p.PersonName;
            tbAddress.Text = this.p.HomeAddress;
            tbPhone.Text = this.p.Phone;
            tbSupervisor.Text = (p is Staff) ? (p as Staff).SupervisorName : "";
            if(p is Staff){
                var agent = (p as Staff).Agent;
                if(agent != null)
                {
                    tbAgentName.Text = agent.PersonName;
                    tbAgentPhone.Text = agent.Phone;

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
