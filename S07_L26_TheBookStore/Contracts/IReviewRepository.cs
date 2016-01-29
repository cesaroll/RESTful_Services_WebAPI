using System.Linq;
using TheBookStore.Models;

namespace TheBookStore.Contracts
{
    public interface IReviewRepository
    {
        IQueryable<Review> AllProp { get; }
        IQueryable<Review> All(int bookId);
        Review AddReview(Review book);
        Review RemoveReview(int id);
    }
}