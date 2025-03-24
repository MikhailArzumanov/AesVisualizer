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

namespace AesVisualizer.Components.Grids {
    public partial class BitVector : UserControl {
        public BitVector() {
            InitializeComponent();
        }

        private Border GetCell(bool bit) {
            var bitText = bit ? "1" : "0";
            var textBlock = new TextBlock {
                FontFamily = new FontFamily("Consolas"),
                Text = bitText, FontSize = 20,
                VerticalAlignment   =   VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            var border = new Border {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black,
                Background  = Brushes.White,
                Child = textBlock,
            };
            return border;
        }

        public void SetData(byte x) {
            for(int i = 0; i < 8; i++) {
                var cell = GetCell((x & 0x1) == 1);
                theGrid.Children.Add(cell);
                Grid.SetRow(cell, i);
                x >>= 1;
            }
        }
    }
}
