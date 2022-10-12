
namespace YelpUI
{
    partial class Form2
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvTips = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvFriendsTipsBusiness = new System.Windows.Forms.DataGridView();
            this.txtTip = new System.Windows.Forms.TextBox();
            this.btnLeaveTip = new System.Windows.Forms.Button();
            this.npgsqlDataAdapter1 = new Npgsql.NpgsqlDataAdapter();
            this.btnLikeTip = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFriendsTipsBusiness)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvTips);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1011, 552);
            this.splitContainer1.SplitterDistance = 294;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvTips
            // 
            this.dgvTips.AllowUserToAddRows = false;
            this.dgvTips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTips.Location = new System.Drawing.Point(3, 0);
            this.dgvTips.Name = "dgvTips";
            this.dgvTips.RowHeadersWidth = 51;
            this.dgvTips.RowTemplate.Height = 24;
            this.dgvTips.Size = new System.Drawing.Size(1008, 293);
            this.dgvTips.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.txtTip);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnLikeTip);
            this.splitContainer2.Panel2.Controls.Add(this.btnLeaveTip);
            this.splitContainer2.Size = new System.Drawing.Size(1011, 254);
            this.splitContainer2.SplitterDistance = 843;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvFriendsTipsBusiness);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(843, 170);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Friends who reviewed this business";
            // 
            // dgvFriendsTipsBusiness
            // 
            this.dgvFriendsTipsBusiness.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFriendsTipsBusiness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFriendsTipsBusiness.Location = new System.Drawing.Point(3, 17);
            this.dgvFriendsTipsBusiness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvFriendsTipsBusiness.Name = "dgvFriendsTipsBusiness";
            this.dgvFriendsTipsBusiness.RowHeadersWidth = 62;
            this.dgvFriendsTipsBusiness.RowTemplate.Height = 28;
            this.dgvFriendsTipsBusiness.Size = new System.Drawing.Size(837, 151);
            this.dgvFriendsTipsBusiness.TabIndex = 0;
            // 
            // txtTip
            // 
            this.txtTip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtTip.Location = new System.Drawing.Point(0, 173);
            this.txtTip.Multiline = true;
            this.txtTip.Name = "txtTip";
            this.txtTip.Size = new System.Drawing.Size(843, 81);
            this.txtTip.TabIndex = 0;
            this.txtTip.Text = "Leave your tip here...";
            this.txtTip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtTip_MouseClick);
            // 
            // btnLeaveTip
            // 
            this.btnLeaveTip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLeaveTip.Location = new System.Drawing.Point(0, 190);
            this.btnLeaveTip.Name = "btnLeaveTip";
            this.btnLeaveTip.Size = new System.Drawing.Size(164, 64);
            this.btnLeaveTip.TabIndex = 1;
            this.btnLeaveTip.Text = "Leave Tip";
            this.btnLeaveTip.UseVisualStyleBackColor = true;
            this.btnLeaveTip.Click += new System.EventHandler(this.btnLeaveTip_Click);
            // 
            // npgsqlDataAdapter1
            // 
            this.npgsqlDataAdapter1.DeleteCommand = null;
            this.npgsqlDataAdapter1.InsertCommand = null;
            this.npgsqlDataAdapter1.SelectCommand = null;
            this.npgsqlDataAdapter1.UpdateCommand = null;
            // 
            // btnLikeTip
            // 
            this.btnLikeTip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLikeTip.Location = new System.Drawing.Point(0, 146);
            this.btnLikeTip.Name = "btnLikeTip";
            this.btnLikeTip.Size = new System.Drawing.Size(164, 44);
            this.btnLikeTip.TabIndex = 2;
            this.btnLikeTip.Text = "Like Tip";
            this.btnLikeTip.UseVisualStyleBackColor = true;
            this.btnLikeTip.Click += new System.EventHandler(this.btnLikeTip_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 552);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTips)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFriendsTipsBusiness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.DataGridView dgvTips;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtTip;
        private System.Windows.Forms.GroupBox groupBox1;
        private Npgsql.NpgsqlDataAdapter npgsqlDataAdapter1;
        internal System.Windows.Forms.DataGridView dgvFriendsTipsBusiness;
        private System.Windows.Forms.Button btnLeaveTip;
        private System.Windows.Forms.Button btnLikeTip;
    }
}