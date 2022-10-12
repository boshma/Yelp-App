using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpUI
{
    public class User : SQLQueries
    {
        public string userID { get; set; }
        public double avg_stars { get; set; }
        public int coolVotes { get; set; }
        public int funnyVotes { get; set; }
        public int usefulVotes { get; set; }
        public string yelping_since { get; set; }
        public string name { get; set; }
        public int fans { get; set; }
        public int tipCount { get; set; }
        public int totalLikes { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

    }
}
