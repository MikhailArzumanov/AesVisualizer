using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math {
    using word = UInt32;
    public class Galois {
        private const word FIRST_WORD_BIT = 0x1u << 31;
        public static word Mod(word x, word modulus) {
            while((modulus & FIRST_WORD_BIT) == 0) {
                modulus <<= 1;
            }
            while ((modulus & 0x1u) == 0) { 
                modulus >>= 1;
                word xorRes = x ^ modulus;
                if (xorRes < x) {
                    x = xorRes;
                }
            }
            return x;
        }
        public static word Div(word x, word y) {
            word res = 0x0u; word bit = 0x1u;
            while ((y & FIRST_WORD_BIT) == 0) {
                y <<= 1; bit <<= 1;
            }
            while ((y & 0x1u) == 0) { 
                y >>= 1; bit >>= 1;
                word xorRes = x ^ y;
                if (xorRes < x) {
                    x = xorRes;
                    res ^= bit;
                }
            }
            return res;
        }

        public static word Multiply(word x, word y, word modulus) {
            word res = 0x0u;
            while (y > 0) {
                if ((y & 0x1u) == 0x1u) {
                    res ^= x;
                }
                y >>= 1;
                x <<= 1;
            }
            return Mod(res, modulus);
        }

        public static word Inverse(word x, word modulus) {
            if (x == 0u) return 0u;
            for(word y = 1; y < 2*modulus; y++) {
                word multiplied = Multiply(x, y, modulus);
                if (multiplied == 1u) {
                    return y;
                }
            }
            return 0u;
        }
    }
}
