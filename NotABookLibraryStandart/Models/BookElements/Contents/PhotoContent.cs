using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public class PhotoContent : Content
    {
        private byte[] bytesOfPhoto;
        private string imageTitle;
        public object Content
        {
            get
            {
                using (var memoryStream = new MemoryStream(bytesOfPhoto))
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
                        bytesOfPhoto = memoryStream.ToArray();
                    }
                    imageTitle = image.Tag?.ToString();
                }

            }
        }

        public override object Clone()
        {
            PhotoContent clone = this.MemberwiseClone() as PhotoContent;
            clone.bytesOfPhoto = this.bytesOfPhoto;
            return clone;
        }

        public override bool IsEmptyContent()
        {
            return bytesOfPhoto.Length == 0 && String.IsNullOrWhiteSpace(imageTitle);
        }

        public override string GetTitleFromContent()
        {
            if (!String.IsNullOrWhiteSpace(imageTitle))
                return imageTitle;
            if (bytesOfPhoto == null || bytesOfPhoto.Length == 0)
                return null;
            return GenerateString();
        }

        public string GenerateString()
        {
            string title = String.Empty;
            for (byte i = 0; i < bytesOfPhoto.Length && i < 15; ++i)
            {
                title += (char)bytesOfPhoto[i];
            }
            return title;
        }
    }
}
