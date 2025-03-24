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
    public partial class BitGrid : UserControl {
        public BitGrid() {
            InitializeComponent();
        }
        public Border GetByteCell(byte x, int j) {
            var bit = (x >> (7-j)) & 0b1;
            var bitText = Convert.ToChar('0'+bit).ToString();
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

        public void SetData(byte[] data) {
            if(data.Length != 8) {
                throw new ArgumentException();
            }

            theGrid.Children.Clear();
            for (int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    var cell = GetByteCell(data[i], j);
                    theGrid.Children.Add(cell);
                    Grid.SetColumn(cell, i);
                    Grid.SetRow   (cell, j);
                }
            }
        }
    }
}
