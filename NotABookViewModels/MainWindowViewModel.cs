using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using Microsoft.Win32;

using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NotABookViewModels
{
    public class MainWindowViewModel : ViewModelCustomBase
    {
        #region Bind fields
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Category> NoteCategories { get; set; }
        public Book CurrentBook { get; set; }
        public Note CurrentNote { get; set; } = new Note("Cool", new List<Content>() { new TextContent() { Content = "Hello here" } });
        public Category SelectedCategory { get; set; }
        public string FindNoteText { get; set; } = "Find note";
        public string CountOfFinding { get; set; }

        public byte NotePanelVisibility { get; set; } = 2;
        public UIElement SelectedContent { get; set; }
        public ObservableCollection<UIElement> Controls { get; set; } = new ObservableCollection<UIElement>();
        public bool IsStackPanelContainsTextBox
        {
            get => Controls.FirstOrDefault(control => control is TextBox) != null;
        }
        #endregion

        public MainWindowViewModel(IService service) : base(service)
        {
            Books = Service.FindBooks() as ObservableCollection<Book>;
            CurrentBook = Books.Count > 0 ? Books[1] : null;
            Notes = CurrentBook?.Notes;
            Categories = Service.FindCategories() as ObservableCollection<Category>;
        }

        #region Commands
        #region fields and prop
        private RelayCommand addEditNoteCommand;
        private RelayCommand addEditCategoryCommand;
        private RelayCommand addEditBookCommand;
        private RelayCommand findNoteCommand;
        private RelayCommand selectNoteCommand;
        private RelayCommand selectBookCommand;
        private RelayCommand removeNoteCommand;
        private RelayCommand removeCategoryCommand;
        private RelayCommand removeBookCommand;
        private RelayCommand closingCommand;
        private RelayCommand attachCommand;
        private RelayCommand removeContentCommand;
        private RelayCommand faqCommand;
        private RelayCommand aboutCommand;
        private RelayCommand addCategoryToNoteCommand;
        private RelayCommand removeCategoryFromNoteCommand;
        private RelayCommand lostFocusCommand;

        public ICommand AddEditNoteCommand
        {
            get => addEditNoteCommand ?? (addEditNoteCommand = new RelayCommand(AddEditNote));
        }
        public ICommand AddEditCategoryCommand
        {
            get => addEditCategoryCommand ?? (addEditCategoryCommand = new RelayCommand(AddEditCategory));
        }
        public ICommand AddEditBookCommand
        {
            get => addEditBookCommand ?? (addEditBookCommand = new RelayCommand(AddEditBook));
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
        public ICommand ClosingCommand
        {
            get => closingCommand ?? (closingCommand = new RelayCommand(SaveNoteDataAndHide));
        }
        public ICommand AttachCommand
        {
            get => attachCommand ?? (attachCommand = new RelayCommand(AttachImage));
        }
        public ICommand RemoveContentCommand
        {
            get => removeContentCommand ?? (removeContentCommand = new RelayCommand(RemoveContent));
        }
        public ICommand FaqCommand
        {
            get => faqCommand ?? (faqCommand = new RelayCommand(ShowFAQ));
        }
        public ICommand AboutCommand
        {
            get => aboutCommand ?? (aboutCommand = new RelayCommand(ShowAbout));
        }
        public ICommand AddCategoryToNoteCommand
        {
            get => addCategoryToNoteCommand ?? (addCategoryToNoteCommand = new RelayCommand(AddCategoryToNote));
        }
        public ICommand RemoveCategoryFromNoteCommand
        {
            get => removeCategoryFromNoteCommand ?? (removeCategoryFromNoteCommand = new RelayCommand(RemoveCategoryFromNote));
        }
        public ICommand LostFocusCommand
        {
            get => lostFocusCommand ?? (lostFocusCommand = new RelayCommand(LostFocus));
        }
        #endregion

        #region Command methods
        public void AddEditNote()
        {
            Messenger.Default.Send("AddEditNote");
        }
        public void AddEditCategory()
        {
            Messenger.Default.Send("AddEditCategory");
        }
        public void AddEditBook()
        {
            Messenger.Default.Send("AddEditBook");
        }
        public void FindNote()
        {
            SaveNoteDataAndHide();
            if (String.IsNullOrWhiteSpace(FindNoteText))
                Notes = CurrentBook.Notes;
            else
            {
                IList<Note> result = CurrentBook?.FindNotes(FindNoteText, Service.FindLinksNoteCategory());
                CountOfFinding = (result?.Count ?? 0).ToString() + " ";
                Notes = result as ObservableCollection<Note>;
            }
        }
        public void SelectNote()
        {
            SaveNoteDataAndHide();
            SaveNoteDataAndHide();
            InputContentsToStackPanel();
            AddTextBoxIfNoContent();
            NotePanelVisibility = 0;

        }
        public void SelectBook()
        {
            UpdateBookData();
        }
        public void RemoveNote()
        {
            Service.RemoveNote(CurrentNote);
            UpdateDataFromDB();
            UpdateBookData();
        }
        public void RemoveCategory()
        {
            Service.RemoveCategory(SelectedCategory);
            UpdateDataFromDB();
            UpdateBookData();
        }
        public void RemoveBook()
        {
            Service.RemoveBook(CurrentBook);
            UpdateDataFromDB();
            UpdateBookData();
        }

        public void SaveNoteDataAndHide()
        {
            CurrentNote = null;
            Controls.Clear();
            NotePanelVisibility = 2;
        }
        private void AttachImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var extension = System.IO.Path.GetExtension(dialog.FileName);

                if (PhotoContent.IsImageExtension(extension))
                {
                    Image myImage = new Image();
                    byte[] imageByte = File.ReadAllBytes(dialog.FileName);
                    myImage.Source = PhotoContent.BytesToImage(imageByte);
                    string name = System.IO.Path.GetFileName(dialog.FileName);

                    Controls.Add(myImage);

                    (CurrentNote).NoteContents
                        .Add(new PhotoContent()
                        { Content = PhotoContent.ImageToBytes(myImage), ImageTitle = name }
                        );
                    Controls.Add(new TextBox() { BorderBrush = System.Windows.Media.Brushes.White, MinLines = 5 });
                }
                else
                {
                    Messenger.Default.Send("WrongFile");
                }
            }
        }
        private void RemoveContent()
        {
            var lol = SelectedContent is TextBox ? (SelectedContent as TextBox).Text : (SelectedContent as Image).Source.ToString();
            var result = MessageBox.Show("Delete content " + lol + "?", "Remove content", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Controls.Remove(SelectedContent);
                AddTextBoxIfNoContent();
            }
        }
        public void ShowFAQ()
        {
            throw new NotImplementedException();
        }
        public void ShowAbout()
        {
            Messenger.Default.Send("AboutPage");
        }

        public void AddCategoryToNote()
        {
            if (NoteCategories.Contains(SelectedCategory))
                Messenger.Default.Send("NoteAlreadyMarked");
            else
            {
                Service.AddLinkNoteCategory(new LinkNoteCategory(CurrentNote, SelectedCategory));
                NoteCategories = Service.FindLinksNoteCategory(CurrentNote).Select(link => link.Category) as ObservableCollection<Category>;
                Service.SaveChanges();
            }
        }
        public void RemoveCategoryFromNote()
        {
            Service
               .RemoveLinkNoteCategory(
                   Service.FindLinksNoteCategory()
                       .FirstOrDefault(link => link.Note.Equals(CurrentNote) && link.Category.Equals(SelectedCategory))
               );
            NoteCategories = Service.FindLinksNoteCategory(CurrentNote).Select(link => link.Category) as ObservableCollection<Category>;
            Service.SaveChanges();
        }
        public void LostFocus()
        {
            if (Notes.Count < 1)
            {
                FindNoteText = "Find note";
                UpdateBookData();
                SaveNoteDataAndHide();
            }
        }
        #endregion
        #endregion

        private void UpdateDataFromDB()
        {
            Books = Service.FindBooks() as ObservableCollection<Book>;
            Categories = Service.FindCategories() as ObservableCollection<Category>;
            Notes = CurrentBook.Notes;
        }
        private void UpdateBookData()
        {
            UpdateDataFromDB();
            SaveNoteDataAndHide();
            CountOfFinding = CurrentBook?.Notes.Count.ToString() + " ";
        }

        private void InputContentsToStackPanel()
        {
            if ((CurrentNote).IsHasNotContent)
                AddTextBoxIfNoContent();
            else
            {
                foreach (var content in (CurrentNote).NoteContents)
                {
                    Controls.Add(content is TextContent ?
                    new TextBox() { Text = (content as TextContent).Content as string, BorderBrush = System.Windows.Media.Brushes.White, MinLines = 5 }
                    : (UIElement)new System.Windows.Controls.Image() { Source = PhotoContent.BytesToImage((content as PhotoContent).BytesOfPhoto) });
                }
            }
        }
        private void AddTextBoxIfNoContent()
        {
            if ((CurrentNote).IsHasNotContent && !IsStackPanelContainsTextBox)
            {
                Controls.Add(new TextBox() { BorderBrush = System.Windows.Media.Brushes.White, MinLines = 5 });
            }
        }
    }
}
