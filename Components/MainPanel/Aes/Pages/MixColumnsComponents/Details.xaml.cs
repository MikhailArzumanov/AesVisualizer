using Converting;
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

namespace AesVisualizer.Components.MainPanel.Aes.Pages.MixColumnsComponents {
    public partial class Details : UserControl {

        private void FillExpGrid(byte[] col) {
            var formatStrs = new string[] {
                "2*{0} xor 3*{1} xor {2} xor {3} mod g(x)",
                "{0} xor 2*{1} xor 3*{2} xor {3} mod g(x)",
                "{0} xor {1} xor 2*{2} xor 3*{3} mod g(x)",
                "3*{0} xor {1} xor {2} xor 2*{3} mod g(x)",
            };
            for(int i = 0; i < 4; i++) {
                var format = formatStrs[i];
                string[] colS = col.Select(x => Hex.ByteToHex(x)).ToArray();
                var resStr = String.Format(format, colS[0], colS[1], colS[2], colS[3]);
                var tBlock = new TextBlock {
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(12), FontSize = 16,
                    Text = resStr
                };
                var border = new Border {
                    BorderBrush     = Brushes.Black   ,
                    BorderThickness = new Thickness(1),
                    Child = tBlock,
                };
                exprsnsGrid.Children.Add(border);
                Grid.SetRow(border, i);
            }
        }

        public void SetData(byte[] prevCol, byte[] mixingMatrix, byte[] resCol) {
            columnGrid.SetData(prevCol);
            mixingGrid.SetData(mixingMatrix);
            mixedGrid.SetData(resCol);
            FillExpGrid(prevCol);
        }
        
        public Details() {
            InitializeComponent();
        }
    }
}
