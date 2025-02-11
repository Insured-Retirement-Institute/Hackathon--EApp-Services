namespace eapp_services.Models;

public class AppSubmit
{
    public Guid? Id { get; set; }
    public DataItemPost[]? DataItems { get; set; }
    public DateTime? SubmittedDate { get; set; }
}
