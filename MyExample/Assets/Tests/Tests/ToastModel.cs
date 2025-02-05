internal class ToastModel
{
    internal ToastModel() { }

    internal ToastModel(string text, float duration, ToastPosition position) : base()
    {
        Text = text;
        Duration = duration;
        Position = position;
    }

    internal string Text { get; set; } = string.Empty;
    internal float Duration { get; set; } = 3f; // Default 3 seconds
    internal ToastPosition Position { get; set; } = ToastPosition.BottomCenter; // Default position
}
public enum ToastPosition
{
    TopCenter = 1,
    MiddleCenter = 4,
    BottomCenter = 7
}