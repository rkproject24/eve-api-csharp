namespace Test_GUI
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtUserKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboCharacterList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblServerOnline = new System.Windows.Forms.Label();
            this.lblServerPlayers = new System.Windows.Forms.Label();
            this.btnUpdateServerStatus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 512);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(87, 512);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 20);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "label2";
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(543, 19);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(75, 23);
            this.btnCommit.TabIndex = 2;
            this.btnCommit.Text = "Commit";
            this.btnCommit.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "User ID:";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(77, 18);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(100, 20);
            this.txtUserID.TabIndex = 4;
            // 
            // txtUserKey
            // 
            this.txtUserKey.Location = new System.Drawing.Point(221, 19);
            this.txtUserKey.Name = "txtUserKey";
            this.txtUserKey.Size = new System.Drawing.Size(316, 20);
            this.txtUserKey.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(183, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Characters:";
            // 
            // cboCharacterList
            // 
            this.cboCharacterList.FormattingEnabled = true;
            this.cboCharacterList.Location = new System.Drawing.Point(94, 58);
            this.cboCharacterList.Name = "cboCharacterList";
            this.cboCharacterList.Size = new System.Drawing.Size(121, 21);
            this.cboCharacterList.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(355, 512);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Server Status:";
            // 
            // lblServerOnline
            // 
            this.lblServerOnline.AutoSize = true;
            this.lblServerOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerOnline.Location = new System.Drawing.Point(459, 482);
            this.lblServerOnline.Name = "lblServerOnline";
            this.lblServerOnline.Size = new System.Drawing.Size(98, 17);
            this.lblServerOnline.TabIndex = 10;
            this.lblServerOnline.Text = "Server Status:";
            // 
            // lblServerPlayers
            // 
            this.lblServerPlayers.AutoSize = true;
            this.lblServerPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerPlayers.Location = new System.Drawing.Point(459, 512);
            this.lblServerPlayers.Name = "lblServerPlayers";
            this.lblServerPlayers.Size = new System.Drawing.Size(98, 17);
            this.lblServerPlayers.TabIndex = 12;
            this.lblServerPlayers.Text = "Server Status:";
            // 
            // btnUpdateServerStatus
            // 
            this.btnUpdateServerStatus.Location = new System.Drawing.Point(318, 482);
            this.btnUpdateServerStatus.Name = "btnUpdateServerStatus";
            this.btnUpdateServerStatus.Size = new System.Drawing.Size(135, 23);
            this.btnUpdateServerStatus.TabIndex = 13;
            this.btnUpdateServerStatus.Text = "Update Server Status";
            this.btnUpdateServerStatus.UseVisualStyleBackColor = true;
            this.btnUpdateServerStatus.Click += new System.EventHandler(this.btnUpdateServerStatus_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 555);
            this.Controls.Add(this.btnUpdateServerStatus);
            this.Controls.Add(this.lblServerPlayers);
            this.Controls.Add(this.lblServerOnline);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboCharacterList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserKey);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCommit);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtUserKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboCharacterList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblServerOnline;
        private System.Windows.Forms.Label lblServerPlayers;
        private System.Windows.Forms.Button btnUpdateServerStatus;

    }
}

