using NotABookLibraryStandart.Models.BookElements.Contents;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotABookTests.ModelTests
{
    public class ContentTests
    {
        private readonly TextContent textContent;
        private readonly PhotoContent photoContent;
        public ContentTests()
        {
            textContent = new TextContent();
            photoContent = new PhotoContent();
        }

        [Fact]
        public void CreatePhotoContent_WhenSetWrongByteArray_ReturnsException()
        {
            PhotoContent content;
            Action action = () => content = new PhotoContent() { Content = "tt" };
            Assert.ThrowsAny<Exception>(action);

        }
    }
}
