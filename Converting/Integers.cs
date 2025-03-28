using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converting {
    public class Integers {
        private static byte[] UintToBytes(UInt32 x) {
            return new byte[]{
                (byte)((x >> 24) & 0xff),
                (byte)((x >> 16) & 0xff),
                (byte)((x >>  8) & 0xff),
                (byte)( x        & 0xff),
            };
        }

        public static byte[] UintsToBytes(UInt32[] rKeys) {
            var bytes = new List<byte>();
            for(int i = 0; i < rKeys.Length; i++) {
                var bytesPart = UintToBytes(rKeys[i]);
                bytes.AddRange(bytesPart);
            }
            return bytes.ToArray();
        }
    }
}
