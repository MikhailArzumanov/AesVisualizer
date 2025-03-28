using Constants;


namespace AesService { 
    public class BytesSubstitutor {
        public static void Process(ref byte[] state) {
            for (int i = 0; i < 16; i++) {
                state[i] = Consts.SBox[state[i]];
            }
        }
    }
}