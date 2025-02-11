namespace eapp_services.Models;

public class Stage
{
    public int Order { get; set; }
    public string Title { get; set; }
    public DataItem[] DataItems { get; set; }
}
