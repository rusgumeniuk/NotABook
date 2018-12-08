using NotABookLibraryStandart.Models.BookElements;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public class TextContent : Content
    {
        private string content;
        public object Content
        {
            get => content;
            set
            {
                content = value?.ToString();
            }
        }

        public override object Clone()
        {
            TextContent clone = this.MemberwiseClone() as TextContent;
            clone.Content = this.content.Clone() as byte[];
            return clone;
        }

        public override bool IsEmptyContent()
        {
            return String.IsNullOrWhiteSpace(content);
        }

        public override string GetTitleFromContent()
        {
            if (String.IsNullOrEmpty(content))
                return null;

            string title = content.Clone() as string;

            if (content.Contains(" "))
            {
                StringBuilder sb = new StringBuilder();
                for (byte countOfWords = 0; countOfWords < 4 && sb.Length < 30; ++countOfWords)
                {
                    string word = GetNextWord(ref title);
                    if (word != null)
                    {
                        sb.Append(word + " ");
                    }
                    else
                        break;
                }

                return (sb.Length < 30 ? sb.ToString() : sb.ToString().Substring(0, 29));

            }
            else
                return GetNextWord(ref title);
        }

       

        private string GetNextWord(ref string str)
        {
            if (String.IsNullOrWhiteSpace(str)) return null;

            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;

            for (; i < str.Length && str[i] != ' ' && stringBuilder.Length < 30; ++i)
            {
                stringBuilder.Append(str[i]);
            }

            str = str.Substring(str.Length > i + 1 ? i + 1 : i);

            return stringBuilder.ToString();
        }
    }
}
