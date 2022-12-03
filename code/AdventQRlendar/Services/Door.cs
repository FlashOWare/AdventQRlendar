namespace AdventQRlendar.Services;

public sealed class Door
{
    public Door(int number)
    {
        Number = number;
        Url = String.Empty;
    }

    public Door(int number, string url)
    {
        Number = number;
        Url = url;
    }

    public int Number { get; }
    public string Url { get; set; }

    public bool IsSet()
    {
        return !string.IsNullOrWhiteSpace(Url);
    }
}
