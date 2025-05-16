using System.Collections.Generic;
using TimeWarpGames.Dal;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Bll
{
    public static class ReviewBll
    {
        // Haal alle reviews op voor een bepaald product
        public static List<Review> ReadByProductId(int productId)
        {
            return ReviewDal.ReadByProductId(productId);
        }

        // Maak een nieuwe review aan
        public static bool Create(string userName, string comment, int rating, int productId)
        {
            var review = new Review
            {
                UserName = userName,
                Comment = comment,
                Rating = rating,
                ProductId = productId
            };

            return ReviewDal.Create(review);
        }
    }
}