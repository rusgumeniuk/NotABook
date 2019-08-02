using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.Roles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace NotABookViewModels
{
    public class MainWindowViewModel : ViewModelCustomBase
    {
        User user;
        #region Bind fields
        private const string FIND_ALL_NOTES_TEXT = "Find all notes";
        private const string FIND_NOTE_TEXT = "Find note";
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        public Book CurrentBook { get; set; }
        public Note CurrentNote { get; set; }
        public Category SelectedCategory { get; set; }
        public string FindNoteText { get; set; } = FIND_NOTE_TEXT;
        public string FindAllNoteText { get; set; } = FIND_ALL_NOTES_TEXT;
        #endregion

        public MainWindowViewModel(IService service) : base(service)
        {
            user = Service.GetUser(Thread.CurrentPrincipal.Identity.Name);
            Books = new ObservableCollection<Book>(Service.FindBooks(user));
            CurrentBook = Books[0];
            UpdateCurrentBookData();
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
        private RelayCommand findAllNotesCommand;
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
        public ICommand FindAllNotesCommand
        {
            get => findAllNotesCommand ?? (findAllNotesCommand = new RelayCommand(FindAllNotes));
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
                String.IsNullOrWhiteSpace(FindNoteText) || String.IsNullOrWhiteSpace(FIND_NOTE_TEXT) ?
                CurrentBook?.Notes :
                CurrentBook?.FindNotes(FindNoteText, Service.FindLinksNoteCategory(user, CurrentBook))
                );
        }
        public void FindAllNotes()
        {
            UpdateNoteList(
                String.IsNullOrWhiteSpace(FindAllNoteText) || String.IsNullOrWhiteSpace(FIND_ALL_NOTES_TEXT) ?
                CurrentBook?.Notes :
                Service.FindAllNotesByWord(user, FindAllNoteText)
                );
        }
        public void SelectNote()
        {
            Messenger.Default.Send("UpdateNoteFrame");
        }
        public void SelectBook()
        {
            UpdateCurrentBookData();
        }
        public void RemoveNote()
        {
            Service.RemoveNote(user, CurrentNote);
            UpdateDataFromDB();
            UpdateNoteList(CurrentBook?.Notes);
        }
        public void RemoveCategory()
        {
            Service.RemoveCategory(user, SelectedCategory);
            UpdateDataFromDB();
            UpdateNoteList(CurrentBook?.Notes);
        }
        public void RemoveBook()
        {
            Service.RemoveBook(user, CurrentBook);
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
            Books = new ObservableCollection<Book>(Service.FindBooks(user));
            CurrentBook = Books.FirstOrDefault(book => book.Title.Equals(CurrentBook.Title));
            UpdateCurrentBookData();
        }
        private void UpdateCurrentBookData()
        {
            UpdateNoteList(CurrentBook?.Notes);
            FindNoteText = FIND_NOTE_TEXT;
            FindAllNoteText = FIND_ALL_NOTES_TEXT;
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
