namespace James.Testing.Pdf.AzCognitiveService
{
    public class Line : ILine
    {
        public Line(Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.Line line)
        {
            Text = line.Text;
            TopLeft = new Point { X = line.BoundingBox[0], Y = line.BoundingBox[1] };
            TopRight = new Point { X = line.BoundingBox[2], Y = line.BoundingBox[3] };
            BottomRight = new Point { X = line.BoundingBox[4], Y = line.BoundingBox[5] };
            BottomLeft = new Point { X = line.BoundingBox[6], Y = line.BoundingBox[7] };
        }

        public Point TopLeft { get; private set; }
        public Point TopRight { get; private set; }
        public Point BottomRight { get; private set; }
        public Point BottomLeft { get; private set; }

        public string Text { get; private set; }
    }
}
