using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Data;
using WRS2big_Web.Model;
using System.Text;

namespace WRS2big_Web.Admin
{
    public partial class Reviews : System.Web.UI.Page
    {
        //Initialize the FirebaseClient with the database URL and secret key.
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LIBhNTkVW1ksKJsiuR2RHCKB8xlllL98S0sBVTSS",
            BasePath = "https://big-system-64b55-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient twoBigDB;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Connection to the database 
            twoBigDB = new FireSharp.FirebaseClient(config);

            if (!IsPostBack)
            {
                string idno = (string)Session["idno"];

                // Retrieve all reviews from the STOREREVIEW table
                FirebaseResponse response = twoBigDB.Get("STOREREVIEW/");
                Dictionary<string, StoreReview> reviews = response.ResultAs<Dictionary<string, StoreReview>>();
                var filteredList = reviews.Values.Where(d => d.adminID.ToString() == idno)
                    .OrderByDescending(review => review.reviewedDate); // Order the reviews by the reviewedDate in descending order


                StringBuilder reviewItems = new StringBuilder();

                // Iterate over the reviews and build the review items
                foreach (var review in filteredList)
                {
                    reviewItems.AppendFormat(@"
            <div class='tab-pane'>
                <div class='review-item'>
                    <div class='review-desc'>{0}</div>
                    <div class='review-info'>
                        <span><strong>{1}</strong></span><br />
                        <span>{2}</span>
                    </div>
                    <div class='review-rating'>
                        {3}
                    </div>
                </div>
                <hr />
            </div>",
                           Server.HtmlEncode(review.feedback),
                    $"{review.customerFirstName} {review.customerLastName}", // Combine the first name and last name
                    review.reviewedDate.ToString("MMMM dd, yyyy"),
                    BuildRatingStars(review.ratings));
                }

                lblReview.Text = reviewItems.ToString();

                // Calculate the average rating
                double averageRating = filteredList.Average(review => review.ratings);
                lblAverageRating.Text = "Average Rating: " + averageRating.ToString("0.##");
                lblStarIcon.Text =  BuildRatingStars((int)averageRating); // star icon to the average rating
            }
        }
        // Helper method to build the rating stars
        private string BuildRatingStars(int rating)
        {
            StringBuilder stars = new StringBuilder();
            for (int i = 1; i <= 5; i++)
            {
                if (i <= rating)
                    stars.Append("<span class='star-icon' style='color: #FFD700;'>&#9733;</span>"); // Filled star icon with color
                else
                    stars.Append("<span class='star-icon-empty' style='color: #C0C0C0;'>&#9733;</span>"); // Empty star icon with color
            }
            return stars.ToString();
        }
    }
}