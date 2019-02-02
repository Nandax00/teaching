using System;

/// <summary>
/// Az alkalmazás üzleti logikája. Csak a perzisztenciának adhat át közvetlenül adatokat.
/// Innen veszi a nézetmodell az utasításokat.
/// </summary>

namespace SampleWPFApp
{
    class Model
    {
        Random random = new Random();

        //Step 6
        public byte [] RGB()
        {
            byte[] rgb = new byte[3];
            for (int i = 0; i < 3; i++)
            {
                rgb[i] = Convert.ToByte(random.Next(0, 255));
            }

            return rgb;
        }
    }
}
