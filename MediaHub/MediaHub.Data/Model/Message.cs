using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.Model;

[Table("Message")]
public class Message
{
    [Key]
    [Column(Order=1)]
    public UserProfile Sender { get; set; }
    [Key]
    [Column(Order=2)]  
    public UserProfile? Receiver { get; set; }
    [Key]
    [Column(Order=3)]
    public DateTime TimeSent { get; set; }
    public DateTime TimeReceived { get; set; }
    public string Content { get; set; }
}