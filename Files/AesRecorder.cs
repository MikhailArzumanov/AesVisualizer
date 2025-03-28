using AesVisualizer.Components.MainPanel.Aes.Pages;
using Converting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using frmtr = AesVisualizer.Files.AesRecordFormatter;

namespace AesVisualizer.Files {
    public class AesRecorder {
        private byte[]   srcData ;
        private IPage[]  pages   ;
        private UInt32[] rKeys   ;
        private int      nr      ;

        private void AddPrefix(ref List<string> record, byte[] key) {
            var keySlice = key.Take(16).ToArray();
            var firstLines = frmtr.formLines(srcData, null, null, null, keySlice);
            record.AddRange(firstLines);
        }

        private void AddMainCycle(ref List<string> record, byte[] key) {
            for (int i = 1; i < nr; i++) {
                int pagesSkipped = 1 + 4 * (i-1);
                var keySlice = key.Skip(16 * i).Take(16).ToArray();
                var rLines = frmtr.formLines(
                    pages[pagesSkipped + 0].GetNext(),
                    pages[pagesSkipped + 1].GetNext(),
                    pages[pagesSkipped + 2].GetNext(),
                    pages[pagesSkipped + 3].GetNext(),
                    keySlice
                );
                record.AddRange(rLines);
            }
        }

        private void AddSuffix(ref List<string> record, byte[] key) {
            int pagesSkipped = 1 + 4*(nr-1);
            var keySlice = key.Skip(16*nr).Take(16).ToArray();
            var suffixLines = frmtr.formLines(
                pages[pagesSkipped + 0].GetNext(),
                pages[pagesSkipped + 1].GetNext(),
                pages[pagesSkipped + 2].GetNext(),
                null, keySlice
            );
            record.AddRange(suffixLines);
            var resData = pages[pagesSkipped + 3].GetNext();
            var finalLines = frmtr.formLines(resData, null, null, null, null);
            record.AddRange(finalLines);
        }

        public void BuildRecords(string fileName) {
            var records = new List<string>();
            var key = Integers.UintsToBytes(rKeys);
            AddPrefix   (ref records, key);
            AddMainCycle(ref records, key);
            AddSuffix   (ref records, key);
            var data = String.Join("\n", records);
            File.WriteAllText(fileName, data);
        }

        public AesRecorder(byte[] srcData, UInt32[] rKeys, IPage[] pages, int nr) {
            this.srcData = srcData; 
            this.rKeys   = rKeys  ;
            this.pages   = pages  ; 
            this.nr      = nr     ;
        }

    }
}
