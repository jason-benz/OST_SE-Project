using MediaHub.Data.MediaModule.Model;
using System;
using System.Collections.Generic;

namespace MediaHub.Test.MediaCommentTest
{
    internal class MediaCommentDataManagerMock : IMediaCommentDataManager
    {
        private List<MediaComment> _comments = new List<MediaComment>();
        private readonly Random rndGenerator = new Random();

        public List<MediaComment> LoadComments(int mediaId)
        {
            return _comments.FindAll(c => c.MediaId == mediaId);
        }

        public void AddComment(int mediaId, string userId, string text)
        {
            _comments.Add(new MediaComment()
            {
                Id = rndGenerator.Next(),
                MediaId = mediaId,
                UserId = userId,
                Created = DateTime.UtcNow,
                CommentText = text
            });
        }

        public void UpdateComment(int Id, string userId, string text)
        {
            var comment = _comments.Find(c => c.Id == Id);
            if (comment!.UserId != userId)
            {
                throw new InvalidOperationException("You are not allowed to edit this comment");
            }
            comment!.CommentText = text;
            comment!.Created = DateTime.UtcNow;
        }

        public void DeleteComment(int Id, string userId)
        {
            var comment = _comments.Find(c => c.Id == Id);
            if (comment!.UserId != userId)
            {
                throw new InvalidOperationException("You are not allowed to edit this comment");
            }
            _comments.Remove(comment);
        }
    }
}
