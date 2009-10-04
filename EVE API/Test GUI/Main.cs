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
        Characters characterList;

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

            if (status.serverOpen)
                lblServerOnline.Text = "Server Online";
            else
                lblServerOnline.Text = "Server Offline";

            lblServerPlayers.Text = status.onlinePlayers + " players";
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            characterList = EVEAPI.GetAccountCharacters(Convert.ToInt32(txtUserID.Text), txtUserKey.Text);

            foreach (Characters.Character list in characterList.CharacterList)
            {
                cboCharacterList.Items.Add(list.Name);
            }
            cboCharacterList.Enabled = true;
        }
    }
}
