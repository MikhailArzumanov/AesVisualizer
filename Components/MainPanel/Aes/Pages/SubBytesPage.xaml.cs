using AesVisualizer.Components.MainPanel.Aes.Pages.KeyExpansion;
using System;

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public partial class SubBytesPage : BasePage {
        private byte[] prevBytes;
        private byte[] nextBytes;

        private void Process(ref byte[] state) {
            prevBytes = (byte[])state.Clone();
            nextBytes = state = (byte[])state.Clone();

            for (int i = 0; i < 16; i++) {
                nextBytes[i] = Consts.SBox[nextBytes[i]];
            }

            mainPart.SetData(prevBytes, nextBytes);
            detailsPart.SetDetails(prevBytes[0]);
        }

        public SubBytesPage(ref byte[] state, int pageNum, int pagesCount) 
        : base(pageNum, pagesCount) {
            PageName = "SubBytes";
            InitializeComponent();
            Process(ref state);
        }

        public override byte[] GetNext() {
            return nextBytes;
        }
    }
}
