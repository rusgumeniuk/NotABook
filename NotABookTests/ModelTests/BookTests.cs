using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;

using System;

using Xunit;

namespace NotABookTests.ModelTests
{
    public class BookTests
    {
        private readonly Book firstBook;
        private readonly Book secondBook;
        private Note note;
        public BookTests()
        {
            firstBook = new Book("Current book");
            secondBook = new Book("Second book");
            note = new Note("some title");
        }

        [Fact]
        public void FindNotes_WhenTextIsNull_ReturnsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => firstBook.FindNotes(null, null));
        }
        [Fact]
        public void FindNotes_WhenFirstNoteContains_ReturnsTrue()
        {
            string text = "Test";
            firstBook.Notes.Add(new Note(text));
            Assert.True(firstBook.IsContainsText(text));
        }
        [Fact]
        public void FindNotes_WhenContentOfNoteContains_ReturnsTrue()
        {
            string text = "Test";
            note.Title = text;
            note.AddContent(new TextContent() { Content = text });
            firstBook.Notes.Add(note);

            Assert.True(firstBook.IsContainsText(text));
        }

        [Fact]
        public void ChangeBook_WhenNoteIsNull_ReturnsArgumentException()
        {
            note = null;
            Assert.Throws<ArgumentException>(() => firstBook.ChangeBook(note, secondBook));
        }
        [Fact]
        public void ChangeBook_WhenNewBookIsCurrentBook_ReturnsArgumentException()
        {
            firstBook.Notes.Add(note);
            Assert.Throws<ArgumentException>(() => firstBook.ChangeBook(note, firstBook));
        }
        [Fact]
        public void ChangeBook_WhenCurrentBookDoesntContainsNote_ReturnsArgumentException()
        {
            Note note = new Note(String.Empty);
            Assert.Throws<ArgumentException>(() => firstBook.ChangeBook(note, secondBook));
        }
        [Fact]
        public void ChangeBook_WhenCorrectVariable_ReturnsTrue()
        {
            Note note = new Note("test note");
            Book newBook = new Book("Second book");
            firstBook.Notes.Add(note);

            Assert.True(firstBook.ChangeBook(note, newBook));
            Assert.Collection(newBook.Notes, nt => nt.Equals(note));
            Assert.Empty(firstBook.Notes);
        }
    }
}
