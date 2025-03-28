using AesService;
using System;

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public partial class SubBytesPage : BasePage {
        private byte[] prevBytes;
        private byte[] nextBytes;

        private void DisplayResults() {
            mainPart.SetData(prevBytes, nextBytes);
            detailsPart.SetDetails(prevBytes[0]);
        }

        

        public SubBytesPage(ref byte[] state, int pageNum, int pagesCount) 
        : base(pageNum, pagesCount) {
            PageName = "SubBytes";
            prevBytes = (byte[])state.Clone();
            nextBytes = state = (byte[])state.Clone();
            InitializeComponent();
            BytesSubstitutor.Process(ref state);
            DisplayResults();
        }

        public override byte[] GetNext() {
            return nextBytes;
        }
    }
}
