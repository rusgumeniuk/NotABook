using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;

using System;
using System.Windows.Input;

namespace NotABookViewModels
{
    public class AddEditBookElementViewModel : ViewModelCustomBase
    {
        public string Title { get; set; }

        private readonly bool isCreating;
        private readonly BookElement BookElement;
        public readonly string WindowTitle;
        public AddEditBookElementViewModel(IService service, BookElement bookElement) : base(service)
        {
            BookElement = bookElement;
            isCreating = bookElement.Title == null;
            WindowTitle = !isCreating ? ("Editing '" + bookElement.Title + "'") : ("Adding new " + bookElement?.GetType().Name.ToLowerInvariant());
            Title = bookElement?.Title ?? String.Empty;
        }

        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;

        public ICommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand(Save));
        }
        public ICommand CancelCommand
        {
            get => cancelCommand ?? (cancelCommand = new RelayCommand(Cancel));
        }

        private void Cancel()
        {
            Messenger.Default.Send("CancelBookElement");
        }

        private void Save()
        {
            if (IsValidElement())
            {
                if (!isCreating)
                {
                    BookElement.Title = Title;
                }
                else
                {
                    if (BookElement is Category)
                    {
                        Service.AddCategory(new Category(Title));
                    }
                    else if (BookElement is Book)
                    {
                        Service.AddBook(new Book(Title));
                    }
                }
                Service.SaveChanges();
                Messenger.Default.Send("BookElemChanged");
            }
            else Messenger.Default.Send("NotValidTitle");
        }
        private bool IsValidElement()
        {
            return !String.IsNullOrWhiteSpace(Title);
        }
    }
}
