using MediaHub.Data.MediaModule.Model;
using System;
using System.Collections.Generic;

namespace MediaHub.Test.MediaCommentTest
{
    internal class MediaCommentDataManagerMock : IMediaCommentDataManager
    {
        private List<MediaComment> _comments = new List<MediaComment>();
        private int _mediaId = 0;
        private string _userId = "0";
        private readonly Random rndGenerator = new Random();

        public List<MediaComment> MediaComments
        {
            get { return _comments.FindAll(c => c.MediaId == _mediaId); }
        }

        public void AddComment(string text)
        {
            _comments.Add(new MediaComment()
            {
                Id = rndGenerator.Next(),
                MediaId = _mediaId,
                UserId = _userId,
                Created = DateTime.UtcNow,
                CommentText = text
            });
        }

        public void Load(int mediaId, string userId)
        {
            _mediaId = mediaId;
            _userId = userId;
        }

        public void UpdateComment(int Id, string text)
        {
            var comment = _comments.Find(c => c.Id == Id);
            if (comment!.UserId != _userId)
            {
                throw new InvalidOperationException("You are not allowed to edit this comment");
            }
            comment!.CommentText = text;
            comment!.Created = DateTime.UtcNow;
        }
    }
}
