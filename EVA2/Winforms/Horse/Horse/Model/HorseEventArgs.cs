using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.Model
{
    public class HorseEventArgs : EventArgs
    {
        public Int32 X { get; private set; }

        public Int32 Y { get; private set; }

        public Int32 LastX { get; private set; }

        public Int32 LastY { get; private set; }

        public Int32 BackX { get; private set; }

        public Int32 BackY { get; private set; }

        private Int32 gametime;
        public Int32 GameTime { get { return gametime; } }

        public HorseEventArgs(Int32 time, bool dummy)
        {
            gametime = time;
        }

        public HorseEventArgs(Int32 x, Int32 y, Int32 lx, Int32 ly)
        {
            X = x;
            Y = y;
            LastX = lx;
            LastY = ly;
        }

        public int Score { get; private set; }
        
        public HorseEventArgs(int sc)
        {
            Score = sc;
        }
        
        public HorseEventArgs(int x, int y)
        {
            BackX = x;
            BackY = y;
        } 
    }
}
