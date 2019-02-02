using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Roberto mintakódjából: az INotifyPropertyChanged interface-t valósítja meg, gondoskodik róla, hogy
/// a ViewModelben létrehozott propertyk futási időben is változni tudjanak (OnPropertyChanged)
/// </summary>

namespace SampleWPFApp
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Nézetmodell ősosztály példányosítása.
        /// </summary>
        protected ViewModelBase() { }

        /// <summary>
        /// Tulajdonság változásának eseménye.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Tulajdonság változása ellenőrzéssel.
        /// </summary>
        /// <param name="propertyName">Tulajdonság neve.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
