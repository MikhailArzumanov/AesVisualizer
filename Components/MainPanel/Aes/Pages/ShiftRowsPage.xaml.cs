using AesService;
using AesVisualizer.Styles;
using System;
using System.Windows.Media;

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public partial class ShiftRowsPage : BasePage {
        private byte[] prevBytes;
        private byte[] nextBytes;

        private void DisplayResults() {
            prevStateGrid.BackgroundMap = HexGridBackgrounds.SourceColors;
            nextStateGrid.BackgroundMap = HexGridBackgrounds.ShiftedClrs;
            prevStateGrid.SetData(prevBytes);
            nextStateGrid.SetData(nextBytes);
        }

        public ShiftRowsPage(ref byte[] state, int pageNum, int pagesCount) 
        : base(pageNum, pagesCount) {
            PageName = "ShiftRows";
            prevBytes = (byte[])state.Clone();
            nextBytes = state = new byte[state.Length];
            InitializeComponent();
            RowsShifter.Process(ref nextBytes, ref prevBytes);
            DisplayResults();
        }

        public override byte[] GetNext() {
            return nextBytes;
        }
    }
}
