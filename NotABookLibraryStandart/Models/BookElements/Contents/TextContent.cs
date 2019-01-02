using System;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public class TextContent : Content
    {
        public string Text { get; set; }
        public object Content
        {
            get => Text;
            set
            {
                Text = value?.ToString();
            }
        }

        public override bool IsEmptyContent()
        {
            return String.IsNullOrWhiteSpace(Text);
        }

        public override string GetTitleFromContent()
        {
            if (String.IsNullOrEmpty(Text))
                return null;

            string title = Text.Clone() as string;

            if (Text.Contains(" "))
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            TextContent cont = obj as TextContent;
            return Text.Equals(cont.Text);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Content?.GetHashCode() ?? new Random().GetHashCode();
        }

        public override bool IsContainsText(string text)
        {
            return Text.ToUpperInvariant().Contains(text.ToUpperInvariant());
        }
    }
}
