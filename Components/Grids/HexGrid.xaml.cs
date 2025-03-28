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

namespace AesVisualizer.Components.Grids {
    public partial class HexGrid : UserControl {
        public HexGrid() {
            InitializeComponent();
        }

        public Brush[,] BackgroundMap { get; set; } = new Brush[4, 4] {
            { Brushes.White, Brushes.White, Brushes.White, Brushes.White},
            { Brushes.White, Brushes.White, Brushes.White, Brushes.White},
            { Brushes.White, Brushes.White, Brushes.White, Brushes.White},
            { Brushes.White, Brushes.White, Brushes.White, Brushes.White}
        };

        public Border GetByteCell(byte x, int i, int j) {
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
                Background  = BackgroundMap[j,i],
                Child = textBlock,
            };
            return border;
        }

        public void SetData(byte[] data) {
            if(data.Length != 16) {
                throw new ArgumentException();
            }
            theGrid.Children.Clear();
            for (int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    var cell = GetByteCell(data[4*i + j], i, j);
                    theGrid.Children.Add(cell);
                    Grid.SetColumn(cell, i);
                    Grid.SetRow   (cell, j);
                }
            }
        }
    }
}
