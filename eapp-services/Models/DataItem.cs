namespace eapp_services.Models;

public class DataItem
{
    public Guid? DataItemId { get; set; }
    public int Order { get; set; }
    public string DisplayLabel { get; set; }
    public string DataType { get; set; }
    public DataOption[] DataOptions { get; set; }
    public Guid? ParentDataItemId { get; set; }
    public string? ParentDataItemRequiredOption { get; set; }
}
