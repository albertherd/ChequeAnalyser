using ChequeAnalyserLib.Base;
using ChequeAnalyserLib.Interfaces;
using ChequeAnalyserLib.Models;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Color = SixLabors.ImageSharp.Color;

namespace ChequeAnalyserLib
{
    public class ImageManager
    {
        private FontCollection _fontCollection;
        private FontFamily _textFontFamily;
        private Font _textFont;

        public ImageManager()
        {
            InitializeFonts();
        }

        public void InitializeFonts()
        {
            _fontCollection = new FontCollection();
            _textFontFamily = _fontCollection.Install("C:\\Windows\\Fonts\\arial.ttf");
            _textFont = _textFontFamily.CreateFont(12);
        }

        public byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (Image image = Image.Load(imageFilePath))
            {
                ResizeImage(image);
                SaveImageAsJpegToStream(memoryStream, image);
                return memoryStream.ToArray();
            }
        }

        public void RenderChequeTemplateBoundingBoxes(AnalysedImage analysedImage)
        {
            FileInfo fi = new FileInfo(analysedImage.QueuedImage.FilePath);
            using (FileStream fs = File.Create(GetNewFileName(fi, "template")))
            using (Image image = Image.Load(fi.FullName))
            {
                ResizeImage(image);
                DrawBoundingBoxes(image,
                    analysedImage.ChequeTemplate.AccountNumber,
                    analysedImage.ChequeTemplate.Amount,
                    analysedImage.ChequeTemplate.Date,
                    analysedImage.ChequeTemplate.PayDescriptionLineOne,
                    analysedImage.ChequeTemplate.PayDescriptionLineTwo
                    );
                SaveImageAsJpegToStream(fs, image);
            }
        }

        public void RenderAnalysedChequeBoundingBoxes(AnalysedImage analysedImage)
        {
            List<Line> lines =
                analysedImage.Result.RecognitionResults.Select(result => result.Lines.Select(line => line).ToList()).FirstOrDefault();

            FileInfo fi = new FileInfo(analysedImage.QueuedImage.FilePath);
            using (FileStream fs = File.Create(GetNewFileName(fi, "analysed")))
            using (Image image = Image.Load(fi.FullName))
            {
                ResizeImage(image);
                lines.ForEach(line => DrawBoundingBox(image, line));
                SaveImageAsJpegToStream(fs, image);
            }
        }

        private string GetNewFileName(FileInfo fi, string purpose)
        {
            return $"{System.IO.Path.GetFileNameWithoutExtension(fi.Name)}_{purpose}{fi.Extension}";
        }

        private void ResizeImage(Image image)
        {
            const int maxWidth = 1280;

            if (image.Width > maxWidth)
            {
                float downsizeRatio = image.Width / maxWidth;
                image.Mutate(ctx => ctx.Resize((int)(image.Width / downsizeRatio), (int)(image.Height / downsizeRatio)));
            }
        }

        private void DrawBoundingBoxes(Image image, params AABBBoundingBoxItemWithText[] boundingBoxesWithText)
        {
            foreach(AABBBoundingBoxItemWithText boundingBoxWithText in boundingBoxesWithText)
            {
                DrawBoundingBox(image, boundingBoxWithText);
            }
        }

        private void DrawBoundingBox(Image image, AABBBoundingBoxItemWithText boundingBoxWithText)
        {
            const int boundingBoxThickness = 5;
            Random rand = new Random();
            IPen pen = Pens.Solid(Color.FromRgb((byte)rand.Next(byte.MinValue, byte.MaxValue), (byte)rand.Next(byte.MinValue, byte.MaxValue), (byte)rand.Next(byte.MinValue, byte.MaxValue)), boundingBoxThickness);
            IPath boundingBoxPath = boundingBoxWithText.BoundingBox.AsRectangularPolygon();

            image.Mutate(context =>
            {
                context.Draw(pen, boundingBoxPath);
                context.DrawText(boundingBoxWithText.Text, _textFont, Color.Black, new PointF(boundingBoxWithText.BoundingBox.Min.X, boundingBoxWithText.BoundingBox.Min.Y - (boundingBoxThickness * 3)));
            });
        }

        private void SaveImageAsJpegToStream(Stream stream, Image image)
        {
            image.Save(stream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
        }
    }
}
