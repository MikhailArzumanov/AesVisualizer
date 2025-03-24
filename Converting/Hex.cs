using System;
using System.Text;

namespace AesVisualizer.Converting {
    internal class Hex {
        private static byte HexDigitToByte(char digit) {
            if ('0' <= digit && digit <= '9') {
                return (byte)(digit - '0');
            }
            if ('a' <= digit && digit <= 'f') {
                return (byte)(digit - 'a' + 10);
            }
            if ('A' <= digit && digit <= 'F') {
                return (byte)(digit - 'A' + 10);
            }
            return 255;
        }

        public static byte[] HexToBytes(string hexStr) {
            int l = hexStr.Length;
            int bl = l / 2;
            var result = new byte[bl];
            for (int i = 0; i < bl; i++) {
                result[i] = 0;
                byte lh = HexDigitToByte(hexStr[2 * i]),
                     rh = HexDigitToByte(hexStr[2 * i + 1]);
                if(lh == 255 || rh == 255) {
                    return null;
                }
                result[i] ^= (byte)(lh << 4);
                result[i] ^= rh;
            }
            return result;
        }

        public static char ByteToHexDigit(byte x) {
            byte digit = (byte)(x & 0xf);
            if(digit > 9) {
                return Convert.ToChar('a' + digit-10);
            } else {
                return Convert.ToChar('0' + digit   );
            }
        }

        public static string ByteToHex(byte x) {
            char h1 = ByteToHexDigit((byte)(x >>   4));
            char h2 = ByteToHexDigit((byte)(x &  0xf));
            return $"{h1}{h2}";
        }

        public static string BytesToHex(byte[] bytes, string divisor) {
            string firstHexByte = ByteToHex(bytes[0]);
            var res = new StringBuilder(firstHexByte);
            for(int i = 1; i < bytes.Length; i++) {
                string hexByte = ByteToHex(bytes[i]);
                res.Append(divisor);
                res.Append(hexByte);
            }
            return res.ToString();
        }

        public static string WordToHex(UInt32 x, string divisor = " ") {
            byte[] xBytes = new byte[4];
            for(int i = 3; i >= 0; i--) {
                xBytes[i] = (byte)x;
                x >>= 8;
            }
            return BytesToHex(xBytes, divisor);
        }
    }
}
