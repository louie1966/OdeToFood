using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Tests.Features
{
    public class RestaurantRater
    {
        private Restaurant _restaurant;

        public RestaurantRater(Restaurant restaurant)
        {
            
            this._restaurant = restaurant;
        }

        public RatingResult ComputeResult(IRatingAlgorithm algorithm, 
                                          int numberOfReviewsToUse)
        {
            var filteredReviews = _restaurant.Reviews.Take(numberOfReviewsToUse);

            return algorithm.Compute(filteredReviews.ToList());
        }       
    }
}
