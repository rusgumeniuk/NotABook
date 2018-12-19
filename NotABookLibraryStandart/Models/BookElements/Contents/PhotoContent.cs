using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Media;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public class PhotoContent : Content
    {
        public byte[] BytesOfPhoto { get; set; }
        public string ImageTitle { get; set; }
        public object Content
        {
            get
            {
                using (var memoryStream = new MemoryStream(BytesOfPhoto))
                {
                    return Image.FromStream(memoryStream);
                }
            }
            set
            {
                if (value is Image image)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        binaryFormatter.Serialize(memoryStream, value);
                        BytesOfPhoto = memoryStream.ToArray();
                    }
                    ImageTitle = image.Tag?.ToString();
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
            return BytesOfPhoto.Equals(content.BytesOfPhoto) && ImageTitle.Equals(content.ImageTitle);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Content.GetHashCode();
        }

        public override bool IsContainsText(string text)
        {
            return ImageTitle?.Contains(text) ?? false || (GenerateString()?.Contains(text) ?? false);
        }
    }
}
