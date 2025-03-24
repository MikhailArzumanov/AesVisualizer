using AesVisualizer.Styles;
using System;
using System.Windows.Media;

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public partial class ShiftRowsPage : BasePage {
        private byte[] prevBytes;
        private byte[] nextBytes;


        private void SetData(ref byte[] state) {
            prevBytes = (byte[])state.Clone();
            nextBytes = state = new byte[state.Length];
            for(int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    int k  = 4*i + j;
                    int k_ = 4*((i+j)%4) + j;
                    nextBytes[k] = prevBytes[k_];
                }
            }
            prevStateGrid.SetData(prevBytes);
            nextStateGrid.SetData(nextBytes);
        }

        public ShiftRowsPage(ref byte[] state, int pageNum, int pagesCount) 
        : base(pageNum, pagesCount) {
            PageName = "ShiftRows";
            InitializeComponent();
            prevStateGrid.BackgroundMap = HexGridBackgrounds.SourceColors;
            nextStateGrid.BackgroundMap = HexGridBackgrounds.ShiftedClrs;
            SetData(ref state);
        }

        public override byte[] GetNext() {
            return nextBytes;
        }
    }
}
