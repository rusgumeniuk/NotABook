﻿using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NotABookViewModels
{
    public class MainWindowViewModel : ViewModelCustomBase
    {
        #region Bind fields
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        public Book CurrentBook { get; set; }
        public Note CurrentNote { get; set; }
        public Category SelectedCategory { get; set; }
        public string FindNoteText { get; set; } = "Find note";
        #endregion

        public MainWindowViewModel(IService service) : base(service)
        {
            Books = new ObservableCollection<Book>(Service.FindBooks());
            CurrentBook = Books.FirstOrDefault(book => book.Title.Equals("Рецепти"));
            UpdateNoteList(CurrentBook?.Notes);
        }

        #region Commands
        #region fields and prop
        private RelayCommand createNoteCommand;
        private RelayCommand createCategoryCommand;
        private RelayCommand createBookCommand;
        private RelayCommand editNoteCommand;
        private RelayCommand editCategoryCommand;
        private RelayCommand editBookCommand;
        private RelayCommand findNoteCommand;
        private RelayCommand selectNoteCommand;
        private RelayCommand selectBookCommand;
        private RelayCommand removeNoteCommand;
        private RelayCommand removeCategoryCommand;
        private RelayCommand removeBookCommand;
        private RelayCommand faqCommand;
        private RelayCommand aboutCommand;
        private RelayCommand lostFocusCommand;

        public ICommand CreateNoteCommand
        {
            get => createNoteCommand ?? (createNoteCommand = new RelayCommand(CreateNote));
        }
        public ICommand CreateCategoryCommand
        {
            get => createCategoryCommand ?? (createCategoryCommand = new RelayCommand(CreateCategory));
        }
        public ICommand CreateBookCommand
        {
            get => createBookCommand ?? (createBookCommand = new RelayCommand(CreateBook));
        }
        public ICommand EditNoteCommand
        {
            get => editNoteCommand ?? (editNoteCommand = new RelayCommand(EditNote));
        }
        public ICommand EditCategoryCommand
        {
            get => editCategoryCommand ?? (editCategoryCommand = new RelayCommand(EditCategory));
        }
        public ICommand EditBookCommand
        {
            get => editBookCommand ?? (editBookCommand = new RelayCommand(EditBook));
        }
        public ICommand FindNoteCommand
        {
            get => findNoteCommand ?? (findNoteCommand = new RelayCommand(FindNote));
        }
        public ICommand SelectNoteCommand
        {
            get => selectNoteCommand ?? (selectNoteCommand = new RelayCommand(SelectNote));
        }
        public ICommand SelectBookCommand
        {
            get => selectBookCommand ?? (selectBookCommand = new RelayCommand(SelectBook));
        }
        public ICommand RemoveNoteCommand
        {
            get => removeNoteCommand ?? (removeNoteCommand = new RelayCommand(RemoveNote));
        }
        public ICommand RemoveCategoryCommand
        {
            get => removeCategoryCommand ?? (removeCategoryCommand = new RelayCommand(RemoveCategory));
        }
        public ICommand RemoveBookCommand
        {
            get => removeBookCommand ?? (removeBookCommand = new RelayCommand(RemoveBook));
        }
        public ICommand FaqCommand
        {
            get => faqCommand ?? (faqCommand = new RelayCommand(ShowFAQ));
        }
        public ICommand AboutCommand
        {
            get => aboutCommand ?? (aboutCommand = new RelayCommand(ShowAbout));
        }
        public ICommand LostFocusCommand
        {
            get => lostFocusCommand ?? (lostFocusCommand = new RelayCommand(LostFocus));
        }
        #endregion

        #region Command methods
        public void CreateNote()
        {
            Messenger.Default.Send("CreateNote");
        }
        public void CreateCategory()
        {
            Messenger.Default.Send("CreateCategory");
        }
        public void CreateBook()
        {
            Messenger.Default.Send("CreateBook");
        }
        public void EditNote()
        {
            Messenger.Default.Send("EditNote");
        }
        public void EditCategory()
        {
            Messenger.Default.Send("EditCategory");
        }
        public void EditBook()
        {
            Messenger.Default.Send("EditBook");
        }
        public void FindNote()
        {
            UpdateNoteList(
                String.IsNullOrWhiteSpace(FindNoteText) ?
                CurrentBook?.Notes :
                CurrentBook?.FindNotes(FindNoteText, Service.FindLinksNoteCategory())
                );
        }
        public void SelectNote()
        {
            Messenger.Default.Send("UpdateNoteFrame");
        }
        public void SelectBook()
        {
            UpdateNoteList(CurrentBook?.Notes);
        }
        public void RemoveNote()
        {
            Service.RemoveNote(CurrentNote);
            UpdateDataFromDB();
            UpdateNoteList(CurrentBook?.Notes);
        }
        public void RemoveCategory()
        {
            Service.RemoveCategory(SelectedCategory);
            UpdateDataFromDB();
            UpdateNoteList(CurrentBook?.Notes);
        }
        public void RemoveBook()
        {
            Service.RemoveBook(CurrentBook);
            UpdateDataFromDB();
            UpdateNoteList(CurrentBook?.Notes);
        }
        public void ShowFAQ()
        {
            throw new NotImplementedException();
        }
        public void ShowAbout()
        {
            Messenger.Default.Send("AboutPage");
        }
        public void LostFocus()
        {
            if (Notes.Count < 1)
            {
                FindNoteText = "Find note";
                UpdateNoteList(CurrentBook?.Notes);

            }
        }
        #endregion
        #endregion

        private void UpdateDataFromDB()
        {
            Books = new ObservableCollection<Book>(Service.FindBooks());
            CurrentBook = Books.FirstOrDefault(book => book.Title.Equals(CurrentBook.Title));
            UpdateNoteList(CurrentBook?.Notes);
        }
        private void UpdateNoteList(IList<Note> notes)
        {
            Notes.Clear();
            AddRange(notes);
        }

        private void AddRange(IList<Note> items)
        {
            if (items == null || items.Count < 1) return;
            foreach (var item in items)
            {
                Notes.Add(item);
            }
        }
    }
}