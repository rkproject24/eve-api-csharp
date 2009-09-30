using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVE_API;

namespace Test_GUI
{
    public partial class Main : Form
    {
        ServerStatus status;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void btnUpdateServerStatus_Click(object sender, EventArgs e)
        {
            status = EVEAPI.GetServerStatus();
            if (status.result.serverOpen == "True")
                lblServerOnline.Text = "Server Online";
            else
                lblServerOnline.Text = "Server Offline";

            lblServerPlayers.Text = status.result.onlinePlayers + " players";
        }
    }
}
