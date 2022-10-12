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
using System.Text.RegularExpressions;
using System.Xaml;
using Microsoft.Maps.MapControl.WPF;
using System.Device.Location;

namespace YelpUI
{
    public partial class Form1 : Form
    {
        Form2 form2;
        Form3 form3;
        string queryForSorting;
        
        internal int[] checkinsArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public Form1()
        {
            InitializeComponent();
            AddColumnsToGrid();
            AddStates();
            //Set Credentials for map
            mapUserControl1.Map.CredentialsProvider = new ApplicationIdCredentialsProvider("ij9m4kXF9y1dWhdSB32F~kZleupJqeM4xTdJn-65ayg~ApA6rZM_WE53KJnHWs3GGisYmO83tzKnHqWYE9DBDWAC6i3aQjt8m843IXmWZIH4");
            tabBusiness.Enabled = false;
            tabMap.Enabled = false;
        }
        private void AddStates()
        {
            string sqlstr = "SELECT DISTINCT state FROM business ORDER BY state";
            SQLQueries.executeQuery(sqlstr, addState);
        }

        private void lstState_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstCity.Items.Clear();
            lstZipcode.Items.Clear();
            lstCategories.Items.Clear();
            lstFilteringCategories.Items.Clear();
            if (lstState.SelectedIndex > -1)
            {
                string args = "city";
                string tables = "business";
                string condition = "state = '" + lstState.SelectedItem.ToString() + "'" + " ORDER BY city";

                string sqlstr = SQLQueries.CreateBaseSelectQuery(args, tables, condition);
                SQLQueries.executeQuery(sqlstr, addCity);
            }
        }

        private void lstCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstZipcode.Items.Clear();
            lstCategories.Items.Clear();
            if (lstCity.SelectedIndex > -1)
            {
                string args = "postal_code";
                string tables = "business";
                string conditions = "state = " + "'" + lstState.SelectedItem.ToString()
                        + "' AND CITY = '" + lstCity.SelectedItem.ToString() + "' ORDER BY postal_code";

                string sqlstr = SQLQueries.CreateBaseSelectQuery(args, tables, conditions);
                SQLQueries.executeQuery(sqlstr, addZipcode);
            }
        }

        private void lstZipcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstCategories.Items.Clear();
            if (lstZipcode.SelectedIndex > -1)
            {
                string args = "category_name";
                string tables = "BusinessCategories bc, Business b";
                string conditions = "bc.business_id = b.business_id AND b.state = '" +
                                lstState.SelectedItem.ToString() +
                                "' AND b.city = '" +
                                lstCity.SelectedItem.ToString() +
                                "' AND b.postal_code = '" +
                                lstZipcode.SelectedItem.ToString() +
                                "'";

                string sqlstr = SQLQueries.CreateBaseSelectQuery(args, tables, conditions);
                SQLQueries.executeQuery(sqlstr, addCategories);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedIndex > -1)
            {
                if (lstFilteringCategories.FindStringExact(lstCategories.SelectedItem.ToString()) == ListBox.NoMatches)
                    lstFilteringCategories.Items.Add(lstCategories.SelectedItem.ToString());
            }
        }

        private void btnSearchBusiness_Click(object sender, EventArgs e)
        {
            mapUserControl1.Map.Children.Clear();
            StringBuilder query = new StringBuilder();
            dgvSearchResults.Rows.Clear();
            dgvSearchResults.Refresh();
            if (lstState.SelectedIndex < 0 || lstCity.SelectedIndex < 0 || lstZipcode.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a state, city, and zipcode before performing your search.", "Error: Unselected Criteria");
            }
            else
            {
                string args = "b.business_id";
                string tables = "BusinessCategories bc, Business b";
                string conditions = "bc.business_id = b.business_id AND b.state = '" +
                                lstState.SelectedItem.ToString() +
                                "' AND b.city = '" +
                                lstCity.SelectedItem.ToString() +
                                "' AND b.postal_code = '" +
                                lstZipcode.SelectedItem.ToString() + "'";

                string sqlstr = SQLQueries.CreateBaseSelectQuery(args, tables, conditions);
                query.Append(sqlstr);

                if (lstFilteringCategories.Items.Count > 0)
                {
                    query = SQLQueries.AddFilteringCategories(lstFilteringCategories.Items, query);
                }

                if (checkBoxCreditCard.CheckState == CheckState.Checked)
                {
                    query = SQLQueries.AddAttribute(query, "BusinessAcceptsCreditCards", "True");
                }
                if (checkboxReservations.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantsReservations", "True");
                }
                if (checkboxWheelChair.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "WheelchairAccessible", "True");
                }
                if (checkboxOutdoorSeating.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "OutdoorSeating", "True");
                }
                if (checkboxKids.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "GoodForKids", "True");
                }
                if (checkboxGroups.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantGoodForGroups", "True");
                }
                if (checkBoxDelivery.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantsDelivery", "True");
                }
                if (checkBoxTakeOut.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantsTakeOut", "True");
                }
                if (checkBoxWifi.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "WiFi", "free");
                }
                if (checkBoxBikeParking.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "BikeParking", "True");
                }
                if (checkboxprice1.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantsPriceRange2", "1");
                }
                if (checkBoxprice2.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantsPriceRange2", "2");
                }
                if (checkBoxprice3.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantsPriceRange2", "3");
                }
                if (checkBoxprice4.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "RestaurantsPriceRange2", "4");
                }
                if (checkBoxbreakfest.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "breakfast", "True");
                }
                if (checkBoxLunch.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "lunch", "True");
                }
                if (checkBoxBrunch.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "brunch", "True");
                }
                if (checkBoxDinner.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "dinner", "True");
                }
                if (checkBoxDessert.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "dessert", "True");
                }
                if (checkBoxLateNight.Checked == true)
                {
                    query = SQLQueries.AddAttribute(query, "latenight", "True");
                }

                SQLQueries.executeQuery(query.ToString(), preaddGridRow);
                queryForSorting = query.ToString();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstFilteringCategories.SelectedIndex > -1)
            {
                lstFilteringCategories.Items.RemoveAt(lstFilteringCategories.SelectedIndex);
            }
        }

        private void AddColumnsToGrid()
        {
            dgvSearchResults.Columns.Add("clmnBName", "Business Name");
            dgvSearchResults.Columns.Add("clmnAddress", "Address");
            dgvSearchResults.Columns.Add("clmnCity", "City");
            dgvSearchResults.Columns.Add("clmnDistance", "Distance (Miles)");
            dgvSearchResults.Columns.Add("clmnState", "State");
            dgvSearchResults.Columns.Add("clmnStars", "Stars");
            dgvSearchResults.Columns.Add("clmnNumTips", "# of Tips");
            dgvSearchResults.Columns.Add("clmnNumCheckIns", "# of CheckIns");
            dgvSearchResults.Columns.Add("clmnBID", "BusinessID");
            dgvSearchResults.EnableHeadersVisualStyles = false;
            dgvSearchResults.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E6C6FF");
            foreach (DataGridViewColumn column in dgvSearchResults.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dgvSearchResults.Columns["clmnBName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSearchResults.Columns["clmnAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSearchResults.RowHeadersVisible = false;
            dgvSearchResults.Columns["clmnBID"].Visible = false;

            dgvFriendsList.Columns.Add("clmnName", "Name");
            dgvFriendsList.Columns.Add("clmnTotalLikes", "Total Likes");
            dgvFriendsList.Columns.Add("clmnAvgStars", "Average Stars");
            dgvFriendsList.Columns.Add("clmnYelpSince", "Yelping Since");
            dgvFriendsList.EnableHeadersVisualStyles = false;
            dgvFriendsList.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E6C6FF");
            foreach (DataGridViewColumn column in dgvFriendsList.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dgvFriendsList.Columns["clmnName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFriendsList.Columns["clmnAvgStars"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvFriendsList.Columns["clmnTotalLikes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvFriendsList.Columns["clmnYelpSince"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFriendsList.RowHeadersVisible = false;

            dgvLatestTipsOfFriends.Columns.Add("clmnName", "User Name");
            dgvLatestTipsOfFriends.Columns.Add("clmnBusiness", "Business");
            dgvLatestTipsOfFriends.Columns.Add("clmnCity", "City");
            dgvLatestTipsOfFriends.Columns.Add("clmnText", "Text");
            dgvLatestTipsOfFriends.Columns.Add("clmnDate", "Date");
            dgvLatestTipsOfFriends.EnableHeadersVisualStyles = false;
            dgvLatestTipsOfFriends.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E6C6FF");
            foreach (DataGridViewColumn column in dgvLatestTipsOfFriends.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dgvLatestTipsOfFriends.Columns["clmnName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvLatestTipsOfFriends.Columns["clmnBusiness"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLatestTipsOfFriends.Columns["clmnCity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLatestTipsOfFriends.Columns["clmnText"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLatestTipsOfFriends.Columns["clmnDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLatestTipsOfFriends.RowHeadersVisible = false;
        }

        private void btnTip_Click(object sender, EventArgs e)
        {
            string businessID = dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString();
            string strsql = "SELECT DISTINCT date, name, number_of_likes, text_review, Business.business_id, Users.user_id " +
                "FROM Tips, Business, Users " +
                "WHERE Business.business_id = '" + businessID + "' AND Business.business_id = Tips.business_id AND Users.user_id = Tips.user_id;";

            form2 = new Form2(this);
            form2.Show();

            form2.dgvTips.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            form2.dgvTips.Columns.Add("clmnDate", "Date");
            form2.dgvTips.Columns.Add("clmnName", "User Name");
            form2.dgvTips.Columns.Add("clmnNumLikes", "# of Likes");
            form2.dgvTips.Columns.Add("clmnReview", "Tip Content");
            form2.dgvTips.Columns.Add("clmnBID", "Business ID");
            form2.dgvTips.Columns.Add("clmnUID", "User ID");

            form2.dgvTips.EnableHeadersVisualStyles = false;
            form2.dgvTips.Columns["clmnBID"].Visible = false;
            form2.dgvTips.Columns["clmnUID"].Visible = false;
            form2.dgvTips.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFDAF5");
            foreach (DataGridViewColumn column in form2.dgvTips.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            form2.dgvTips.Columns["clmnReview"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            form2.dgvTips.RowHeadersVisible = false;
            SQLQueries.executeQuery(strsql, form2.addTip);

            form2.dgvFriendsTipsBusiness.Columns.Add("clmnName", "User Name");
            form2.dgvFriendsTipsBusiness.Columns.Add("clmnDate", "Date");
            form2.dgvFriendsTipsBusiness.Columns.Add("clmnText", "Text");

            form2.dgvFriendsTipsBusiness.EnableHeadersVisualStyles = false;
            form2.dgvFriendsTipsBusiness.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFDAF5");
            foreach (DataGridViewColumn column in form2.dgvFriendsTipsBusiness.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            form2.dgvFriendsTipsBusiness.Columns["clmnText"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            form2.dgvFriendsTipsBusiness.RowHeadersVisible = false;

            if (lstUsers.SelectedIndex > 0)
            {
                string sqlstr2 = "SELECT name, date, text_review " +
                 "from tips, users, business " +
                    "where users.user_id in ( select friend_id " +
                       "from friends,tips " +
                        "where friends.user_id = '" + lstUsers.SelectedItem.ToString() + "' and friends.friend_id = tips.user_id " +
                           "group by friend_id ) and tips.user_id = users.user_id and tips.business_id = business.business_id and business.business_id = '" + dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString() + "'";
                SQLQueries.executeQuery(sqlstr2, addTipsOfFriendsBusiness);
            }
        }

        private void txtboxCurrentUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstUsers.Items.Clear();
                string usersName = txtboxCurrentUser.Text;
                string strsql = "SELECT user_id" +
                    " FROM users " +
                    "WHERE name = '" + usersName + "'";
                SQLQueries.executeQuery(strsql, addUsers);
                tabBusiness.Enabled = true;
                tabMap.Enabled = true;
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvFriendsList.Rows.Clear();
            dgvLatestTipsOfFriends.Rows.Clear();
            string userID = lstUsers.SelectedItem.ToString();
            string sqlstr = "SELECT average_stars, name, number_of_cool_votes,number_of_funny_votes, number_of_useful_votes, tip_count, fan_count,tip_likes, yelping_since, latitude, longitude " +
                "FROM users " +
                    "WHERE user_id = '" + userID + "'";
            SQLQueries.executeQuery(sqlstr, addUserInfo);

            string sqlstr2 = "SELECT friend_id " +
                "FROM friends " +
                    "WHERE user_id = '" + userID + "'";
            SQLQueries.executeQuery(sqlstr2, addFriends);

            string sqlstr3 = "SELECT name, business_name, city, text_review, date " +
                "from tips, users, business " +
                    "where date in ( select max(date) as mDate " +
                        "from friends,tips " +
                         "where friends.user_id = '" + userID + "' and friends.friend_id = tips.user_id " +
                            "group by friend_id ) and tips.user_id = users.user_id and tips.business_id = business.business_id";
            SQLQueries.executeQuery(sqlstr3, addTipsOfFriends);

            string sqlSelectLocation = "SELECT longitude, latitude FROM Users WHERE user_id = '" + lstUsers.SelectedItem.ToString() + "';";
            SQLQueries.executeQuery(sqlSelectLocation, UpdateLocation);
        }

        private void addState(NpgsqlDataReader R)
        {
            lstState.Items.Add(R.GetString(0));
        }

        private void addCity(NpgsqlDataReader R)
        {
            lstCity.Items.Add(R.GetString(0));
        }

        private void addZipcode(NpgsqlDataReader R)
        {
            lstZipcode.Items.Add(R.GetString(0));
        }

        private void addCategories(NpgsqlDataReader R)
        {
            lstCategories.Items.Add(R.GetString(0));
        }

        private void addUsers(NpgsqlDataReader R)
        {
            lstUsers.Items.Add(R.GetString(0));
        }

        private void addUserInfo(NpgsqlDataReader R)
        {
            User user = new User();

            user.avg_stars = R.GetDouble(0);
            user.name = R.GetString(1);
            user.coolVotes = R.GetInt32(2);
            user.funnyVotes = R.GetInt32(3);
            user.usefulVotes = R.GetInt32(4);
            user.tipCount = R.GetInt32(5);
            user.fans = R.GetInt32(6);
            user.totalLikes = R.GetInt32(7);
            user.yelping_since = R.GetString(8);
            user.latitude = R.GetDouble(9);
            user.longitude = R.GetDouble(10);

            txtboxUserStars.Text = user.avg_stars.ToString();
            txtboxUserName.Text = user.name;
            txtBoxCoolVotes.Text = user.coolVotes.ToString();
            txtBoxFunnyVotes.Text = user.funnyVotes.ToString();
            txtBoxUsefulVotes.Text = user.usefulVotes.ToString();
            txtBoxTipCount.Text = user.tipCount.ToString();
            txtFans.Text = user.fans.ToString();
            txtBoxTipLikes.Text = user.totalLikes.ToString();
            txtBoxYelpingSince.Text = user.yelping_since;
            txtLatitude.Text = user.latitude.ToString();
            txtLongitude.Text = user.longitude.ToString();
        }

        private void addFriends(NpgsqlDataReader R)
        {
            string userid = R.GetString(0);
            string args = "name, tip_likes, average_stars, yelping_since";
            string tables = "users";
            string conditions = "user_id = '" + userid + "'";
            string sqlstr = SQLQueries.CreateBaseSelectQuery(args, tables, conditions);
            SQLQueries.executeQuery(sqlstr, fillFriendsList);
        }

        private void fillFriendsList(NpgsqlDataReader R)
        {
            var index = dgvFriendsList.Rows.Add();
            dgvFriendsList.Rows[index].Cells["clmnName"].Value = R.GetString(0);
            dgvFriendsList.Rows[index].Cells["clmnTotalLikes"].Value = R.GetInt32(1);
            dgvFriendsList.Rows[index].Cells["clmnAvgStars"].Value = R.GetDouble(2);
            dgvFriendsList.Rows[index].Cells["clmnYelpSince"].Value = DateTime.Parse(R.GetString(3)).ToString("M/d/yyyy h:mm:ss tt");
        }

        private void addTipsOfFriendsBusiness(NpgsqlDataReader R)
        {
            var index = form2.dgvFriendsTipsBusiness.Rows.Add();
            form2.dgvFriendsTipsBusiness.Rows[index].Cells["clmnName"].Value = R.GetString(0);
            form2.dgvFriendsTipsBusiness.Rows[index].Cells["clmnDate"].Value = R.GetDateTime(1);
            form2.dgvFriendsTipsBusiness.Rows[index].Cells["clmnText"].Value = R.GetString(2);
        }

        private void addTipsOfFriends(NpgsqlDataReader R)
        {
            var index = dgvLatestTipsOfFriends.Rows.Add();
            dgvLatestTipsOfFriends.Rows[index].Cells["clmnName"].Value = R.GetString(0);
            dgvLatestTipsOfFriends.Rows[index].Cells["clmnBusiness"].Value = R.GetString(1);
            dgvLatestTipsOfFriends.Rows[index].Cells["clmnCity"].Value = R.GetString(2);
            dgvLatestTipsOfFriends.Rows[index].Cells["clmnText"].Value = R.GetString(3);
            dgvLatestTipsOfFriends.Rows[index].Cells["clmnDate"].Value = R.GetDateTime(4);
        }

        private void preaddGridRow(NpgsqlDataReader R)
        {
            string businessid = R.GetString(0);
            string args = "business_name, address, city, state, total_number_of_tips, total_number_of_checkins, b.business_id, longitude, latitude, stars";
            string tables = "business b";
            string conditions = "b.business_id = '" + businessid + "'";
            string sqlstr = SQLQueries.CreateBaseSelectQuery(args, tables, conditions);
            SQLQueries.executeQuery(sqlstr, addGridRow);
        }

        private void addGridRow(NpgsqlDataReader R)
        {
            Business business = new Business()
            {
                Name = R.GetString(0),
                Address = R.GetString(1),
                City = R.GetString(2),
                State = R.GetString(3),
                NumTips = R.GetInt32(4),
                NumCheckIns = R.GetInt32(5),
                BusinessID = R.GetString(6),
                Longitude = R.GetDouble(7),
                Latitude = R.GetDouble(8),
                Stars = R.GetDouble(9)
            };

            var startCoord = new GeoCoordinate(Convert.ToDouble(txtLatitude.Text), Convert.ToDouble(txtLongitude.Text));
            var endCoord = new GeoCoordinate(business.Latitude, business.Longitude);


            double distance = startCoord.GetDistanceTo(endCoord);
            //Convert to miles
            double miles = distance / 1609;

            double milesrounded = Math.Round(miles, 2);

            var index = dgvSearchResults.Rows.Add();
            dgvSearchResults.Rows[index].Cells["clmnBName"].Value = business.Name;
            dgvSearchResults.Rows[index].Cells["clmnAddress"].Value = business.Address;
            dgvSearchResults.Rows[index].Cells["clmnCity"].Value = business.City;
            dgvSearchResults.Rows[index].Cells["clmnDistance"].Value = milesrounded;
            dgvSearchResults.Rows[index].Cells["clmnState"].Value = business.State;
            dgvSearchResults.Rows[index].Cells["clmnNumTips"].Value = business.NumTips;
            dgvSearchResults.Rows[index].Cells["clmnNumCheckIns"].Value = business.NumCheckIns;
            dgvSearchResults.Rows[index].Cells["clmnBID"].Value = business.BusinessID;
            dgvSearchResults.Rows[index].Cells["clmnStars"].Value = business.Stars;
            Pushpin pin = new Pushpin();
            pin.Location = new Location(R.GetDouble(8), R.GetDouble(7));
            mapUserControl1.Map.Children.Add(pin);
            mapUserControl1.Map.SetView(center: pin.Location, zoomLevel: 18);

        }

        private void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            double test = 0;
            if (txtLongitude.Text != "" && txtLatitude.Text != ""
                && double.TryParse(txtLongitude.Text, out test) && double.TryParse(txtLatitude.Text, out test))
            {
                string sqlUpdateLocation = "UPDATE Users " +
                    "SET longitude = " + txtLongitude.Text + ", latitude = " + txtLatitude.Text +
                    "WHERE user_id = '" + lstUsers.SelectedItem.ToString() + "';";
                SQLQueries.executeQuery(sqlUpdateLocation, null);

                string sqlSelectLocation = "SELECT longitude, latitude FROM Users WHERE user_id = '" + lstUsers.SelectedItem.ToString() + "';";
                SQLQueries.executeQuery(sqlSelectLocation, UpdateLocation);
            }
        }

        private void UpdateLocation(NpgsqlDataReader R)
        {
            txtLongitude.Text = R.GetDouble(0).ToString();
            txtLatitude.Text = R.GetDouble(1).ToString();
        }


        internal void addToChartArray(NpgsqlDataReader R)
        {
            checkinsArray[R.GetInt32(1) - 1] = R.GetInt32(0);
        }

        private string convertIntToMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
            }
            return "error, number not between 1 - 12";
        }

        private void btnShowCheckins_Click(object sender, EventArgs e)
        {
            form3 = new Form3(this);
            form3.Show();

            string sqlstr = "select count(month) as numCheckins, month from checkins where business_id = '" +
               dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString() + "' group by month order by month";

            for (int i = 0; i < 12; i++)
            {
                checkinsArray[i] = 0;
            }
            SQLQueries.executeQuery(sqlstr, addToChartArray);

            populateChart();
        }

        internal void populateChart()
        {
            for (int i = 0; i < 12; i++)
            {
                form3.chartCheckIns.Series["Number Of Check ins"].Points.AddXY(convertIntToMonth(i + 1), checkinsArray[i]);
            }
        }

        private void dgvSearchResults_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtSelectedBusinessName.Clear();
            txtSelectedBusinessAddress.Clear();
            if (dgvSearchResults.CurrentRow.Index > -1)
            {
                txtSelectedBusinessName.Text = dgvSearchResults.CurrentRow.Cells["clmnBName"].Value.ToString();
                txtSelectedBusinessAddress.Text = dgvSearchResults.CurrentRow.Cells["clmnAddress"].Value.ToString();
                string today = DateTime.Now.DayOfWeek.ToString();
                string sqlBusinessHours = "SELECT opening_time, closing_time FROM BusinessHours WHERE business_id = '"
                    + dgvSearchResults.CurrentRow.Cells["clmnBID"].Value.ToString()
                    + "' AND day = '" + today + "';";
                SQLQueries.executeQuery(sqlBusinessHours, SetBusinessHours);
            }
        }

        private void SetBusinessHours(NpgsqlDataReader R)
        {
            txtSelectedBusinessHours.Clear();
            string openingTime = DateTime.Parse(R.GetString(0)).ToShortTimeString();
            string closingTime = DateTime.Parse(R.GetString(1)).ToShortTimeString();
            txtSelectedBusinessHours.Text = "Today (" + DateTime.Now.DayOfWeek.ToString() + "):\tOpens: " + openingTime + "\tCloses: " + closingTime;
        }

    }
}

