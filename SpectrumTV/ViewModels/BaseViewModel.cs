using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpectrumTV.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;

            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

            return true;
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public virtual void Dispose()
        {
            // Clear event subscribers
            PropertyChanged = null;
        }
    }
}
