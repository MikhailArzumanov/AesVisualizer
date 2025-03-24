using AesVisualizer.Converting;
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
    public partial class HexVector : UserControl {
        public HexVector() {
            InitializeComponent();
        }

        public Border GetByteCell(byte x) {
            var hexText = Hex.ByteToHex(x);
            var textBlock = new TextBlock {
                FontFamily = new FontFamily("Consolas"),
                Text = hexText, FontSize = 20,
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

        public void SetData(byte[] data) {
            if(data.Length != 4) {
                throw new ArgumentException();
            }
            theGrid.Children.Clear();
            for (int i = 0; i < 4; i++) {
                var cell = GetByteCell(data[i]);
                theGrid.Children.Add(cell);
                Grid.SetRow(cell, i);
            }
        }
    }
}
