using SkiaSharp;

namespace PaintByNumbers.Paint;

public class Util
{
    internal static dynamic GetBitmap(FileInfo file)
    {
        if (!File.Exists(file.FullName)) throw new ArgumentException($"File {file.FullName} does not exist.");

        using var reader = new StreamReader(file.FullName);
        var picture = SKPicture.Deserialize(reader.BaseStream);
        var image = SKImage.FromPicture(picture, new SKSizeI(1000,1000));
        var bitmap = SKBitmap.FromImage(image);
        return bitmap;
    }
}