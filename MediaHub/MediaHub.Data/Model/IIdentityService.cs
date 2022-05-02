namespace MediaHub.Data.Model;

public interface IIdentityService 
{
   public string? UserId { get; }
   public string? LoginName { get; }
   public bool IsAuthenticated { get; }
}