﻿namespace MediaHub.Data.MessagingModule.Model;

public interface IChatDataManager
{
    public void InsertMessage(Message message);
    public List<Message> GetMessagesBetweenTwoUsers(string userId1, string userId2);
}