using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converting {
    public class Bits {
        public static string ByteToBits(byte x) {
            char[] bits = new char[8];
            for(int i = 7; i >= 0; i--) {
                byte bit = (byte)(x & 0x1);
                bits[i] = Convert.ToChar('0'+bit);
                x >>= 1;
            }
            return new String(bits);
        }
    }
}
