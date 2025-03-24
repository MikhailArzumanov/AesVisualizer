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
    public partial class AddRoundKeyPage : BasePage {
        private byte[] prevState;
        private byte[] keyBytes ;
        private byte[] nextState;
        
        private void Process(ref byte[] state, UInt32[] expKey, int round) {
            prevState = (byte[])state.Clone();
            keyBytes  = new byte[16];
            nextState = state = (byte[])state.Clone();
            for (int i = 0; i < 4; i++) {
                UInt32 keyPart = expKey[4*round + i];
                for (int j = 0; j < 4; j++) {
                    int stCell = 4*i + j; 
                    int keyShift = 24 - 8*j;
                    var keyShifted = keyPart >> keyShift;
                     keyBytes[stCell]  = Convert.ToByte(keyShifted & 0xff);
                    nextState[stCell] ^= keyBytes[stCell];
                }
            }
        }

        private void Display() {
            stateGrid   .SetData(prevState);
            roundKeyGrid.SetData(keyBytes );
            resGrid     .SetData(nextState);
        }

        public AddRoundKeyPage(ref byte[] state, UInt32[] expKey, 
            int round, int pageNum, int pagesCount
        ) : base (pageNum, pagesCount) {
            PageName = "AddRoundKey";
            InitializeComponent();
            Process(ref state, expKey, round);
            Display();
        }
        public override byte[] GetNext() {
            return nextState;
        }
    }
}
