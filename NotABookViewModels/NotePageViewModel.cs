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
    public class NotePageViewModel : ViewModelCustomBase
    {
        public string Title { get; set; }
        public ObservableCollection<Category> NoteCategories { get; set; }
        public DateTime DateOfCreating { get; set; }
        public DateTime DateOfLastChanging { get; set; }
        public ObservableCollection<Category> AllCategories { get; set; }
        public Book Book { get; set; }

        private readonly Note CurrentNote;
        private readonly bool isCreating = true;

        public Category SelectedCategory { get; set; }
        public UIElement SelectedContent { get; set; }

        public ObservableCollection<UIElement> Controls { get; set; } = new ObservableCollection<UIElement>();
        public bool IsStackPanelContainsTextBox
        {
            get => Controls.FirstOrDefault(control => control is TextBox) != null;
        }
        public NotePageViewModel(IService service, Book currentBook, Note note = null) : base(service)
        {
            Book = currentBook;
            AllCategories = new ObservableCollection<Category>(Service.FindCategories());
            isCreating = note == null;

            CurrentNote = note ?? new Note(String.Empty);
            Title = note?.Title ?? String.Empty;
            NoteCategories = new ObservableCollection<Category>(Service.FindCategoriesByNote(CurrentNote));
            DateOfCreating = CurrentNote.DateOfCreating;
            DateOfLastChanging = CurrentNote.DateOfLastChanging;
            InputContentsToStackPanel();
        }

        private void InputContentsToStackPanel()
        {
            if (CurrentNote.IsHasNotContent)
                AddTextBoxIfNoContent();
            else
            {
                foreach (var content in CurrentNote.NoteContents)
                {
                    Controls.Add(content is TextContent ?
                    new TextBox() { Text = (content as TextContent).Content as string, BorderBrush = System.Windows.Media.Brushes.White, MinLines = 5 }
                    : (UIElement)new Image() { Source = PhotoContent.BytesToImage((content as PhotoContent).BytesOfPhoto) });
                }
            }
        }

        private void AddTextBoxIfNoContent()
        {
            if (CurrentNote.IsHasNotContent && !IsStackPanelContainsTextBox)
            {
                Controls.Add(new TextBox() { BorderBrush = System.Windows.Media.Brushes.White, MinLines = 5 });
            }
        }



        public void SaveNoteData()
        {
            IList<Content> contents = GetContentFromChildren();
            IList<Content> addContents = new List<Content>();
            bool isHasDifferences = false;
            foreach (var newContent in contents)
            {
                bool exist = false;
                foreach (var existingContent in CurrentNote.NoteContents)
                {
                    if (newContent.Equals(existingContent))
                    {
                        exist = true;
                        addContents.Add(existingContent);
                        break;
                    }
                }
                if (exist)
                    continue;
                else
                {
                    isHasDifferences = true;
                    addContents.Add(newContent);
                }
            }
            if (isHasDifferences || !contents.Count.Equals(CurrentNote.NoteContents))
            {
                foreach (var content in CurrentNote.NoteContents)
                {
                    Service.RemoveContent(content);
                }
                CurrentNote.NoteContents.Clear();
                CurrentNote.NoteContents = addContents;
                Service.SaveChanges();
            }
        }

        private IList<Content> GetContentFromChildren()
        {
            IList<Content> contents = new List<Content>();
            //AddTextBoxIfNoContent();
            foreach (var control in Controls)
            {
                Content cont = null;
                if (control is Image)
                {
                    cont = new PhotoContent() { Content = PhotoContent.ImageToBytes(control as Image), ImageTitle = (control as Image).Source.GetHashCode().ToString() };
                }
                else if (control is TextBox)
                {
                    cont = new TextContent() { Content = (control as TextBox).Text };
                }
                contents.Add(cont);
            }
            return contents;
        }

        private RelayCommand closingCommand;
        private RelayCommand attachCommand;
        private RelayCommand removeContentCommand;
        private RelayCommand createCategoryCommand;
        private RelayCommand addCategoryCommand;
        private RelayCommand removeCategoryCommand;

        public ICommand ClosingCommand
        {
            get => closingCommand ?? (closingCommand = new RelayCommand(SaveDataAndClose));
        }
        public ICommand AttachCommand
        {
            get => attachCommand ?? (attachCommand = new RelayCommand(AttachImage));
        }
        public ICommand RemoveContentCommand
        {
            get => removeContentCommand ?? (removeContentCommand = new RelayCommand(RemoveContent));
        }
        public ICommand CreateCategoryCommand
        {
            get => createCategoryCommand ?? (createCategoryCommand = new RelayCommand(CreateCategory));
        }
        public ICommand AddCategoryCommand
        {
            get => addCategoryCommand ?? (addCategoryCommand = new RelayCommand(AddCategory));
        }
        public ICommand RemoveCategoryCommand
        {
            get => removeCategoryCommand ?? (removeCategoryCommand = new RelayCommand(RemoveCategory));
        }

        private void SaveDataAndClose()
        {
            SaveNoteData();
            if (!String.IsNullOrWhiteSpace(Title) || !CurrentNote.IsHasNotContent)
            {
                if (String.IsNullOrWhiteSpace(Title))
                {
                    CurrentNote.Title = CurrentNote.TitleFromContent;
                }
                if (isCreating)
                    Book.Notes.Add(CurrentNote);
                Service.SaveChanges();
            }
            Messenger.Default.Send("NoteEditFinish");
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

                    CurrentNote.NoteContents
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
        private void CreateCategory()
        {
            Messenger.Default.Send("NewCategory");
        }
        private void AddCategory()
        {
            if (NoteCategories.Contains(SelectedCategory))
                Messenger.Default.Send("NoteAlreadyMarked");
            //MessageBox.Show("Note already marked by this category!");
            else
            {
                Service.AddLinkNoteCategory(new LinkNoteCategory(CurrentNote, SelectedCategory));
                NoteCategories.Add(SelectedCategory);
                Service.SaveChanges();
            }
        }
        private void RemoveCategory()
        {
            Service
                .RemoveLinkNoteCategory(
                    Service.FindLinksNoteCategory()
                        .FirstOrDefault(link => link.Note.Equals(CurrentNote) && link.Category.Equals(SelectedCategory))
                );
            NoteCategories.Remove(SelectedCategory);
            Service.SaveChanges();
        }
    }
}
