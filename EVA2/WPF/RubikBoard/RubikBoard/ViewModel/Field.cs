using System;

namespace RubikBoard.ViewModel
{
    public class Field : ViewModelBase
    {
        private Int32 color;

        public Int32 Color
        {
            get { return color; }
            set
            {
                if(color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Vízszintes koordináta lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 X { get; set; }

        /// <summary>
        /// Függőleges koordináta lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 Y { get; set; }

        /// <summary>
        /// Sorszám lekérdezése.
        /// </summary>
        public Int32 Number { get; set; }

        /// <summary>
        /// Lépés parancs lekérdezése, vagy beállítása.
        /// </summary>
        public DelegateCommand StepCommand { get; set; }
    }
}
