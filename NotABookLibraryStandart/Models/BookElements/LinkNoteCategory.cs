﻿using System;

namespace NotABookLibraryStandart.Models.BookElements
{
    public class LinkNoteCategory : Entity
    {
        public LinkNoteCategory() : base() { }
        public LinkNoteCategory(Note note, Category category) : base()
        {
            Note = note ?? throw new ArgumentNullException(nameof(note));
            Category = category ?? throw new ArgumentNullException(nameof(category));
        }
        public static LinkNoteCategory CreateConnection(Note note, Category category)
        {
            return new LinkNoteCategory(note, category);
        }

        public Note Note { get; set; }
        public Category Category { get; set; }
    }
}
