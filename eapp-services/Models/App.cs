namespace eapp_services.Models;

public class App
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CallbackUrl { get; set; }
    public Stage[] Stages { get; set; }
}
