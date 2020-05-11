namespace James.Testing.Pdf
{
    public interface ILine
    {
        string Text { get; }
        Point BottomLeft { get; }
        Point BottomRight { get; }
        Point TopLeft { get; }
        Point TopRight { get; }
    }
}