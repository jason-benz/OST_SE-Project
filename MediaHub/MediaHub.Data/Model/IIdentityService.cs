namespace MediaHub.Data.Model;

public class IIdentityService 
{
   public string? LoginName { get; }
   public bool IsAuthenticated { get; }

}