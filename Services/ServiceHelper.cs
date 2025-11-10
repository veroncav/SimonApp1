namespace SimonApp1;

public static class ServiceHelper
{
    public static IServiceProvider Services { get; set; } = default!;
    public static T Get<T>() where T : class => (T)Services.GetService(typeof(T))!;
}
