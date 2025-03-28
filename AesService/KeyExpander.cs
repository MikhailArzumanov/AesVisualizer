using Converting;
using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesService {
    public class KeyExpander {

        private UInt32[] ExpKey { get; set; } = null;
        private int Nk { get; set; }
        private Func<uint, string> toHex = (uint x) => Hex.WordToHex(x);

        private void BuildKeyPrefix(ref List<string> procRec, byte[] key) {
            for (int i = 0; i < Nk; i++) {
                ExpKey[i] = 0;
                for (int j = 0; j < 4; j++) {
                    ExpKey[i] <<= 8;
                    ExpKey[i] |= key[4 * i + j];
                }
                string line = $"w{i} = {toHex(ExpKey[i])}";
                procRec.Add(line);
            }
        }

        private UInt32 SubstituteWord(UInt32 x) {
            UInt32 res = 
                (Convert.ToUInt32(Consts.SBox[(x >> 24) & 0xff]) << 24) |
                (Convert.ToUInt32(Consts.SBox[(x >> 16) & 0xff]) << 16) |
                (Convert.ToUInt32(Consts.SBox[(x >>  8) & 0xff]) <<  8) |
                (Convert.ToUInt32(Consts.SBox[(x      ) & 0xff])      );
            return res;
        }
        private UInt32 RotateWord(UInt32 x) {
            UInt32 tmp = x >> 24;
            UInt32 res = (x << 8) ^ tmp;
            return res;
        }


        private void ProcSubRot(ref List<string> buildingRecord, int sdi, int i) {
            UInt32 rotated = RotateWord(ExpKey[i - 1]);
            UInt32 substituted = SubstituteWord(rotated);
            UInt32 tmp = substituted ^ Consts.Rcon[i / Nk];
            ExpKey[i] = ExpKey[i - Nk] ^ tmp;
            buildingRecord.Add($"\tr{sdi} = RotWord(w{i - 1}) = {toHex(rotated)}");
            buildingRecord.Add($"\ts{sdi} = SubWord(r{sdi}) = {toHex(substituted)}");
            buildingRecord.Add($"\tRcon[{i / Nk}] = {toHex(Consts.Rcon[i / Nk])}");
            buildingRecord.Add($"\tt{sdi} = s{sdi} xor Rcon[{i / Nk}] = {toHex(tmp)}");
            buildingRecord.Add($"w{i} = w{i - Nk} xor t{sdi} = " +
                $"{toHex(ExpKey[i - Nk])} xor {toHex(tmp)} = {toHex(ExpKey[i])}"
            );
        }

        private void ProcSubstitution(ref List<string> buildingRecord, int sdi, int i) {
            UInt32 substituted = SubstituteWord(ExpKey[i - 1]);
            ExpKey[i] = ExpKey[i - Nk] ^ substituted;
            buildingRecord.Add($"\tt{sdi} = SubWord(w{i - 1}) = {toHex(substituted)}");
            buildingRecord.Add($"w{i} = w{i - Nk} xor t{sdi++} = " +
                $"{toHex(ExpKey[i - Nk])} xor {toHex(substituted)} = {toHex(ExpKey[i])}"
            );
        }

        private void ProcRegularRound(ref List<string> buildingRecord, int i) {
            ExpKey[i] = ExpKey[i - Nk] ^ ExpKey[i - 1];
            buildingRecord.Add($"w{i} = w{i - Nk} xor w{i - 1} = " +
                $"{toHex(ExpKey[i - Nk])} xor {toHex(ExpKey[i - 1])} " +
                $"= {toHex(ExpKey[i])}"
            );
        }

        private void ProcIteration(ref List<string> records, ref int sdi, int wrdIndex) {
            if (wrdIndex % Nk == 0) {
                ProcSubRot(ref records, sdi, wrdIndex);
                sdi++;
            }
            else if (Nk > 6 && wrdIndex % Nk == 4) {
                ProcSubstitution(ref records, sdi, wrdIndex);
                sdi++;
            } else {
                ProcRegularRound(ref records, wrdIndex);
            }
        }

        public UInt32[] BuildKey(out List<string> buildingRecord, byte[] key, int Nr) {
            buildingRecord = new List<string>();
            ExpKey = new UInt32[4*(Nr+1)]; Nk = key.Length / 4;

            BuildKeyPrefix(ref buildingRecord, key);
            int sdi = 1;
            for (int i = Nk; i < 4*(Nr+1); i++) {
                ProcIteration(ref buildingRecord, ref sdi, i);
            }
            return ExpKey;
        }
    }
}
