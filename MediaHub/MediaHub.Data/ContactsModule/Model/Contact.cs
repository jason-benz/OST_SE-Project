using MediaHub.Data.ProfileModule.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.ContactsModule.Model;

[Table("Contact")]
public class Contact
{
    [Key] 
    public int Id { get; set; }
    
    [Column("UserId", TypeName = "NVARCHAR(450)")]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserProfile UserProfile { get; set; }

    [Column("ContactId", TypeName = "NVARCHAR(450)")]
    public string ContactId { get; set; }

    [ForeignKey(nameof(ContactId))]
    public UserProfile ContactUserProfile { get; set; }

    [Column("OpenRequest")]
    public bool OpenRequest { get; set; }
    
    [Column("IsBlocked")]
    public bool IsBlocked { get; set; }
    

#pragma warning disable CS8618
    /// <summary>
    /// Empty ctor needed for EF
    /// </summary>
    protected Contact() { }
#pragma warning restore CS8618
    
    public Contact(string id, string contactId)
    {
        UserId = id;
        ContactId = contactId;
    }

}