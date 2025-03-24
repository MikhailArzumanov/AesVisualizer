using AesVisualizer.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public partial class MixColumnsPage : BasePage {
        private byte[] prevBytes;
        private byte[] nextBytes;

        private static byte[] mixingMatrix = new byte[] {
            0x02, 0x03, 0x01, 0x01,
            0x01, 0x02, 0x03, 0x01,
            0x01, 0x01, 0x02, 0x03,
            0x03, 0x01, 0x01, 0x02, 
        };

        private void MixColumn(byte[] col, int colShift) {
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

        private void DisplayResults() {
            mainPart.SetData(prevBytes, mixingMatrix, nextBytes);

            var srcFirstCol = prevBytes.Take(4).ToArray();
            var mixedFirstCol = nextBytes.Take(4).ToArray();
            detailsPart.SetData(srcFirstCol, mixingMatrix, mixedFirstCol);
        }

        private void Process(ref byte[] state) {
            prevBytes = (byte[])state.Clone();
            nextBytes = state = (byte[])state.Clone();
            for(int i = 0; i < 4; i++) {
                MixColumn(state, 4*i);
            }
        }

        public MixColumnsPage(ref byte[] state, int pageNum, int pagesCount)
        : base(pageNum, pagesCount) {
            PageName = "MixColumns";
            InitializeComponent();
            Process(ref state);
            DisplayResults();
        }

        public override byte[] GetNext() {
            return nextBytes;
        }
    }
}
