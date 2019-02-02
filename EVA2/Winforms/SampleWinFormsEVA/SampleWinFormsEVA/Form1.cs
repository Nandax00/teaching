using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SampleWinFormsEVA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddButton();
        }
        private void AddButton()
        {
            Button button = new Button();
            button.Location = new Point(0, 0);
            button.Text = "Exit";
            button.Click += new System.EventHandler(CloseWindow);
            //button.Click += (s, e) => { Close(); };    // Lambda expressions may shorten the traditional way of defining and using a method
            Controls.Add(button);
        }

        Model model = new Model();

        bool isBusy = false;

        #region Synchronous    
        // Use regions to separate code 
        
        private int CountCharactersSync()
        {
            Thread.Sleep(5000);    // Don't use this
            return 15;
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                isBusy = true;
                label1.Text = "Processing...";
                int count = CountCharactersSync();
                int[] color = model.ColorGenerator();
                panel1.BackColor = Color.FromArgb(color[0], color[1], color[2]);
                label1.Text = count.ToString();
                isBusy = false;
            }
        }
        
        #endregion
        /*
        #region Asynchronous
        // Async operations are used for I/O methods or CPU-bound code like expensive calculations
        private async Task<int> CountCharacters()
        {
            int count = 15;
            await Task.Delay(5000);   //use Task.Delay if waiting is necessary
            return count;
        }

        // Event handler for button
        // Add in Design/Properties/Events or use button1.Click += new EventHandler(btn1_Click)
        // Do not add multiple event handlers to UI element at once
        // Only use async void in signature of event handlers
        private async void btn1_Click(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                isBusy = true;
                label1.Text = "Processing...";
                int count = await CountCharacters();   // Async and await go hand in hand, do not use one without the other
                int[] color = model.ColorGenerator();
                panel1.BackColor = Color.FromArgb(color[0], color[1], color[2]);
                label1.Text = "Press button for new color";
                isBusy = false;
            }
        }
        
        #endregion
    */
        private void CloseWindow(Object sender, EventArgs e)
        {
            Close();
        }
    }
}
