using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokshenDesktop.ViewModel
{
    public class CustomMessageBoxViewModel : ViewModelBase
    {
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public CustomMessageBoxViewModel()
        {

        }
    }
}
