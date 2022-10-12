using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YelpUI
{
    public partial class Form3 : Form
    {
        Form1 form1;
        public Form3(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int seconds = DateTime.Now.Second;
            string time = hour.ToString() + ":" + minute.ToString() + ":" + seconds.ToString();    

            string sqlInsert = "INSERT INTO checkins(Business_id, year, month, day, time ) VALUES ('" + form1.dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString() +
                "','" + year + "','" + month + "','" + day + "','" + time + "')";

            SQLQueries.executeQuery(sqlInsert, null);

            string sqlCheckins = "UPDATE business " +
                "SET total_number_of_checkins = total_number_of_checkins + 1 " +
                 " where business_id = '" + form1.dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString() + "'" ;
            SQLQueries.executeQuery(sqlCheckins, null);

            chartCheckIns.Series["Number Of Check ins"].Points.Clear();
            
            //repopulate the chart
            string sqlstr = "select count(month) as numCheckins, month from checkins where business_id = '" +
               form1.dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString() + "' group by month order by month";

            for (int i = 0; i < 12; i++)
            {
                form1.checkinsArray[i] = 0;
            }
            SQLQueries.executeQuery(sqlstr, form1.addToChartArray);

            form1.populateChart();
        }
    }
}
