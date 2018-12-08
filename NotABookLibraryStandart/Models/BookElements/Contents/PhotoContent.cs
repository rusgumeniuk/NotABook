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
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, value);
                    bytesOfPhoto = memoryStream.ToArray();
                }
            }
        }

        public override object Clone()
        {
            PhotoContent clone = this.MemberwiseClone() as PhotoContent;
            clone.bytesOfPhoto = this.bytesOfPhoto;
            return clone;
        }
    }
}
