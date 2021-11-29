using Codea.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Codea.View_Models
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        private string content;
        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewDocument { get; private set; }

        public DocumentViewModel()
        {
            NewDocument = new NewDocumentCommand { vm = this };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
