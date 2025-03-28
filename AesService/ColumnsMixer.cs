using Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesService{
    public class ColumnsMixer {
        private static void MixColumn(byte[] col, int colShift) {
            byte[] _1  = new byte[4],
                   _2  = new byte[4],
                   _3 = new byte[4];
            for(int j = 0; j < 4; j++) {
                _1[j] = col[colShift+j];
                _2[j] = (byte)Galois.Mod(2u*col[colShift+j], modulus: 0x11b);
                _3[j] = (byte)(_1[j] ^ _2[j]);
            }
            col[colShift + 0] = Convert.ToByte(_2[0] ^ _3[1] ^ _1[2] ^ _1[3]);
            col[colShift + 1] = Convert.ToByte(_1[0] ^ _2[1] ^ _3[2] ^ _1[3]);
            col[colShift + 2] = Convert.ToByte(_1[0] ^ _1[1] ^ _2[2] ^ _3[3]);
            col[colShift + 3] = Convert.ToByte(_3[0] ^ _1[1] ^ _1[2] ^ _2[3]);
        }

        public static void Process(ref byte[] state) {
            for (int i = 0; i < 4; i++) {
                MixColumn(state, 4 * i);
            }
        }
    }
}
