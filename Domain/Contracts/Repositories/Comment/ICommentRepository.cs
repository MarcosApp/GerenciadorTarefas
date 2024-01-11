using Domain.Entities;

namespace Domain.Contracts.Repositories.AddComment
{
    public interface ICommentRepository
    {
        int AddComment(Comment comment);
    }
}
