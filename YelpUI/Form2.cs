using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace YelpUI
{
    public partial class Form2 : Form
    {
        Form1 form1;

        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        private void txtTip_MouseClick(object sender, MouseEventArgs e)
        {
            txtTip.Text = "";
        }

        public void addTip(NpgsqlDataReader R)
        {
            Tip tip = new Tip()
            {
                Date = R.GetDateTime(0),
                UserName = R.GetString(1),
                NumLikes = R.GetInt32(2),
                TextReview = R.GetString(3),
                BusinessID = R.GetString(4),
                UserID = R.GetString(5)
            };
            var index = dgvTips.Rows.Add();
            dgvTips.Rows[index].Cells["clmnDate"].Value = tip.Date.ToString();
            dgvTips.Rows[index].Cells["clmnName"].Value = tip.UserName;
            dgvTips.Rows[index].Cells["clmnNumLikes"].Value = tip.NumLikes.ToString();
            dgvTips.Rows[index].Cells["clmnReview"].Value = tip.TextReview;
            dgvTips.Rows[index].Cells["clmnBID"].Value = tip.BusinessID;
            dgvTips.Rows[index].Cells["clmnUID"].Value = tip.UserID;
        }

        private void btnLeaveTip_Click(object sender, EventArgs e)
        {
            dgvTips.Rows.Clear();

            string sqlstr = "INSERT INTO Tips(date, business_id, user_id, number_of_likes, text_review) " +
                "VALUES ('" + DateTime.Now.ToString() + "','" + form1.dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString() 
                + "', '" + form1.lstUsers.SelectedItem.ToString() + "', 0, '"
                 + txtTip.Text + "');";

            SQLQueries.executeQuery(sqlstr, null);
            string strsql = "SELECT DISTINCT date, name, number_of_likes, text_review, Business.business_id, Users.user_id " +
                "FROM Tips, Business, Users " +
                "WHERE Business.business_id = '" + form1.dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString()
                + "' AND Business.business_id = Tips.business_id AND Users.user_id = Tips.user_id;";
            SQLQueries.executeQuery(strsql, addTip);
        }

        private void btnLikeTip_Click(object sender, EventArgs e)
        {
            if (dgvTips.CurrentRow.Index > -1)
            {
                string sqlUpdateTip = "UPDATE Tips " +
                    "SET number_of_likes = number_of_likes + 1 " +
                    "WHERE date = '" + dgvTips.CurrentRow.Cells["clmnDate"].Value.ToString()
                    + "' AND business_id = '" + dgvTips.CurrentRow.Cells["clmnBID"].Value.ToString()
                    + "' AND user_id = '" + dgvTips.CurrentRow.Cells["clmnUID"].Value.ToString() + "';";
                SQLQueries.executeQuery(sqlUpdateTip, null);

                string strSelectUpdatedTip = "SELECT DISTINCT date, name, number_of_likes, text_review, Business.business_id, Users.user_id " +
                "FROM Tips, Business, Users " +
                "WHERE date = '" + dgvTips.CurrentRow.Cells["clmnDate"].Value.ToString()
                    + "' AND Business.business_id = '" + dgvTips.CurrentRow.Cells["clmnBID"].Value.ToString()
                    + "' AND Users.user_id = '" + dgvTips.CurrentRow.Cells["clmnUID"].Value.ToString() + "';";
                SQLQueries.executeQuery(strSelectUpdatedTip, UpdateTip);

            }
        }

        private void UpdateTip(NpgsqlDataReader R)
        {
            dgvTips.CurrentRow.Cells["clmnNumLikes"].Value = R.GetInt32(2).ToString();
            dgvTips.Refresh();
        }

    }
}
