using Domain.Contracts.Repositories.AddComment;
using Domain.Contracts.UseCases.Comentario;
using Domain.Entities;
using System;

namespace Application.UseCases.AddComment
{
    public class CommentUseCase: ICommentUseCase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public int AddComment(Comment comment)
        {
            return _commentRepository.AddComment(comment);
        }
    }
}
