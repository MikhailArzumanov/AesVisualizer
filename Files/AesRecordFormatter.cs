using Converting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesVisualizer.Files {
    internal class AesRecordFormatter {
        private static T[] roundArray<T>(T[] array) {
            if (array == null) return null;
            T[] copy = (T[])array.Clone();
            for(int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    int k  = 4*i + j;
                    int k_ = 4*j + i;
                    copy[k] = array[k_];
                }
            }
            return copy;
        }

        private static string toHex(byte[] array) {
            if (array == null) return String.Empty;
            else return Hex.BytesToHex(array, divisor: " ");
        }

        private static string formLine(
            byte[] begin, byte[] sb, byte[] shfrw, byte[] mxc, byte[] key
        ) {
            const string format = "{0}\t{1}\t{2}\t{3}\t{4}";
            var data = new string[] {
                toHex(begin), toHex(sb   ),
                toHex(shfrw), toHex(mxc  ),
                toHex(key  ),
            };
            return String.Format(format, data);
        }
        public static string[] formLines(byte[] begin, 
        byte[] sb, byte[] shfrw, byte[] mxc, byte[] key) {
            var lines = new List<string>();
            begin = roundArray(begin); sb  = roundArray(sb );
            shfrw = roundArray(shfrw); mxc = roundArray(mxc);
            key = roundArray(key);
            for (int i = 0; i < 4; i++) {
                var line = formLine(
                    begin .Skip(4*i) .Take(4) .ToArray(),
                    sb   ?.Skip(4*i)?.Take(4)?.ToArray(),
                    shfrw?.Skip(4*i)?.Take(4)?.ToArray(),
                    mxc  ?.Skip(4*i)?.Take(4)?.ToArray(),
                    key  ?.Skip(4*i)?.Take(4)?.ToArray()
                );
                lines.Add(line);
            }
            return lines.ToArray();
        }
    }
}
