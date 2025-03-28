using AesService;
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

        

        private void DisplayResults() {
            mainPart.SetData(prevBytes, mixingMatrix, nextBytes);

            var srcFirstCol = prevBytes.Take(4).ToArray();
            var mixedFirstCol = nextBytes.Take(4).ToArray();
            detailsPart.SetData(srcFirstCol, mixingMatrix, mixedFirstCol);
        }

        

        public MixColumnsPage(ref byte[] state, int pageNum, int pagesCount)
        : base(pageNum, pagesCount) {
            PageName = "MixColumns";
            prevBytes = (byte[])state.Clone();
            nextBytes = state = (byte[])state.Clone();
            InitializeComponent();
            ColumnsMixer.Process(ref state);
            DisplayResults();
        }

        public override byte[] GetNext() {
            return nextBytes;
        }
    }
}
