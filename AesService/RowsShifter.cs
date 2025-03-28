using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesService {
    public class RowsShifter {
        public static void Process(ref byte[] nextBytes, ref byte[] prevBytes) {
            for(int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    int k  = 4*i + j;
                    int k_ = 4*((i+j)%4) + j;
                    nextBytes[k] = prevBytes[k_];
                }
            }
        }
    }
}
