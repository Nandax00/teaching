using System;
using System.Windows.Media;
using System.Diagnostics;

/// <summary>
/// A ViewModel a Model és a View között elhelyezkedő réteg, feladata, hogy kapcsolatot
/// teremtsen a két réteg között. Használhatja a modell metódusait, őt pedig a nézet használja.
/// Itt definiáljuk a propertyket (attribútumokat és parancsokat), amiket a view-ban bindolunk, valamint az
/// eseménykezelőket, amelyeket a parancsok kiváltanak.
/// 
/// Step x: azt jelöli, hogy milyen sorrendben hívódnak meg az utasítások onnantól kezdve, hogy interakcióba
/// léptünk egy UI elemmel (pl. megnyomtunk egy gombot).
/// </summary>
namespace SampleWPFApp
{
    class ViewModel : ViewModelBase
    {
        #region Fields

        private Color labelColor;
        private String labelContent;
        private Model model = new Model();

        #endregion

        #region Properties
        // Property, amelyet az ablakban lévő labellel kötünk össze, ennek segítségével változtatjuk majd futási időben a label színét.
        // Privát adattagja az azonos típusú labelColor változó.
        public Color LabelColor
        {
            get { return labelColor; }
            set
            {
                labelColor = value;
                OnPropertyChanged("LabelColor"); // Ha ezt nem írjuk ide (vagy nem hívjuk meg a property minden változtatása után), akkor a propertyvel összekötött UI elem nem fog futási időben változni.
            }
        }

        public String LabelContent
        {
            get => labelContent;  // C# 7-től használható ez a jelölés get és set accessornál is.
            set
            {
                labelContent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        //Step 1/a
        // A DelegateCommand egy olyan property, aminek nincs külön privát tagja
        public DelegateCommand ChangeColorCommand { get; private set; }
        public DelegateCommand KeyPressedCommand { get; private set; }
        public DelegateCommand QuitCommand { get; private set; }

        #endregion

        #region Events

        public event EventHandler ChangeColorEvent;
        public event EventHandler QuitEvent;

        #endregion

        #region Constructors
        public ViewModel(Model m)
        {
            model = m;
            //Step 1/b (a konstruktor természetesen csak egyszer hívódik meg, a command definícióját jelölöm itt, hiszen ez az, ami a UI elemmel összeköttetésben áll)
            // Lambdakifejezesséel inicializáljuk a commandot
            ChangeColorCommand = new DelegateCommand(param => 
            {
                OnColorChange();
                Debug.Print("Fire");
            });

            KeyPressedCommand = new DelegateCommand(param => { OnKeyPressed(Convert.ToString(param)); });

            QuitCommand = new DelegateCommand(param => OnQuit());
        }
        #endregion

        #region Public methods
        // Step 5
        public void ChangeColor()
        {
            byte[] rgb = model.RGB();
            LabelColor = Color.FromRgb(rgb[0], rgb[1], rgb[2]);
        }
        #endregion

        #region Private methods
        // Step 2: ezeket a metódusokat kötjük össze közvetlenül a DelegateCommandokkal, itt váltódnak ki az események.
        // Az eseményekhez eseménykezelőt az App.xaml.cs osztályban rendelünk.
        private void OnColorChange()
        {
            if(ChangeColorEvent != null)
            {
                // Step 3
                ChangeColorEvent(this, EventArgs.Empty);
            }
        }

        private void OnKeyPressed(String key)
        {
            switch(key)
            {
                case "Up":
                    LabelContent = "Up arrow was pressed.";
                    break;
                case "Down":
                    LabelContent = "Down arrow was pressed.";
                    break;
                case "Left":
                    LabelContent = "Left arrow was pressed.";
                    break;
                case "Right":
                    LabelContent = "Right arrow was pressed.";
                    break;
            }
        }

        private void OnQuit()
        {
            if(QuitEvent != null)
            {
                QuitEvent(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
