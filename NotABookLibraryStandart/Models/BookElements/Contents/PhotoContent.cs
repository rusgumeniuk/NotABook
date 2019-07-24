using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public class PhotoContent : Content
    {
        [IgnoreDataMember]
        public byte[] BytesOfPhoto { get; set; }
        public string ImageTitle { get; set; }
        public object Content
        {
            get
            {
                using (var memoryStream = new MemoryStream(BytesOfPhoto))
                {
                    return System.Drawing.Image.FromStream(memoryStream);
                }
            }
            set
            {
                if (value is Image)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        binaryFormatter.Serialize(memoryStream, value);
                        BytesOfPhoto = memoryStream.ToArray();
                    }
                    ImageTitle = $"Image{DateTime.Now.Millisecond}";
                }
                else if (value is byte[])
                {
                    BytesOfPhoto = (byte[])value;
                }
            }
        }

        public override bool IsEmptyContent()
        {
            return BytesOfPhoto.Length == 0 && String.IsNullOrWhiteSpace(ImageTitle);
        }

        public override string GetTitleFromContent()
        {
            if (!String.IsNullOrWhiteSpace(ImageTitle))
                return ImageTitle;
            if (BytesOfPhoto == null || BytesOfPhoto.Length == 0)
                return null;
            return GenerateString();
        }

        public string GenerateString()
        {
            string title = String.Empty;
            for (byte i = 0; i < BytesOfPhoto.Length && i < 15; ++i)
            {
                title += (char)BytesOfPhoto[i];
            }
            return title;
        }

        public override bool Equals(object obj)
        {
            if (obj as PhotoContent == null)
                return false;
            PhotoContent content = obj as PhotoContent;
            return BytesOfPhoto.SequenceEqual(content.BytesOfPhoto) || ImageTitle.Equals(content.ImageTitle);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Content.GetHashCode();
        }

        public override bool IsContainsText(string text)
        {
            return ImageTitle?.ToUpperInvariant().Contains(text.ToUpperInvariant()) ?? false || (GenerateString()?.ToUpperInvariant().Contains(text.ToUpperInvariant()) ?? false);
        }

        public static bool IsImageExtension(string extension)
        {
            return extension.Equals(".jpg") || extension.Equals(".png");
        }

        public static BitmapImage BytesToImage(byte[] bytes)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public static byte[] ImageToBytes(Image image)
        {
            if (image.Source is BitmapSource bitmapSource)
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    return stream.ToArray();
                }
            }
            return null;
        }
    }
}
