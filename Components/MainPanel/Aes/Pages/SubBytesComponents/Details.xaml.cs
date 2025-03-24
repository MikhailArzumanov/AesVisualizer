using AesVisualizer.Converting;
using AesVisualizer.Math;
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

namespace AesVisualizer.Components.MainPanel.Aes.Pages.SubBytesComponents {
    public partial class Details : UserControl {
        
        private byte getBit(byte x, int i) {
            return Convert.ToByte((x >> (i%8)) & 0x1);
        }

        private byte SetRevX(byte x) {
            byte revX = Convert.ToByte(Galois.Inverse(x, 0x11b));
            var revDescFrmt = "Первым этапом идёт поиск обратного в поле галуа GF(2⁸) \r\n" +
                "по модулю g(x) = x⁸ + x⁴ + x³ + x + 1. Для 0x{0} представлено как 0x{1}.";
            revDescription.Text = String.Format(revDescFrmt, Hex.ByteToHex(x), Hex.ByteToHex(revX));
            return revX;
        }

        private byte SetX_(byte revX) {
            numBits.SetData(revX);
            byte x_ = 0;
            for (int i = 7; i >= 0; i--) {
                x_ <<= 1;
                var xorRes = getBit(revX, i) ^ getBit(revX, i + 4);
                xorRes ^= getBit(revX, i + 5) ^ getBit(revX, i + 6);
                xorRes ^= getBit(revX, i + 7);
                x_ ^= Convert.ToByte(xorRes);
            }
            tmpResBits.SetData(x_);
            return x_;
        }

        private void setBitRepresentation(byte x) {
            var formatStr = "Далее проводится матричное умножение " +
                "битового представления числа 0x{0} = 0b{1}";
            var hex  =  Hex.ByteToHex(x);
            var bits = Bits.ByteToBits(x);
            bitRepresentationBlock.Text = String.Format(formatStr, hex, bits);
        }

        public void SetDetails(byte x) {
            var revX = SetRevX(x);
            setBitRepresentation(revX);
            var x_ = SetX_(revX);
            var xRes = Convert.ToByte(x_ ^ 0x63);
            var resFormat = "0x{0} xor 0x{1} = 0x{2}";
            Func<byte, string> toHex = (byte b) => Hex.ByteToHex(b);
            resultingDetail.Text = String.Format(
                resFormat, toHex(x_), toHex(0x63), toHex(xRes)
            );
        }


        private byte[] mltplrMatrixData = new byte[] {
            0b10001111,0b11000111,0b11100011,0b11110001,
            0b11111000,0b01111100,0b00111110,0b00011111,
        };

        public Details() {
            InitializeComponent();
            multiplierMatrix.SetData(mltplrMatrixData);
        }
    }
}
