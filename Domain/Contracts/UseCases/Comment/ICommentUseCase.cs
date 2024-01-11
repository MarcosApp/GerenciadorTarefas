using Domain.Entities;

namespace Domain.Contracts.UseCases.Comentario
{
    public interface ICommentUseCase
    {
        int AddComment(Comment comment);
    }
}
