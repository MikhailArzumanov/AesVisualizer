using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AesVisualizer.Styles {
    public class HexGridBackgrounds {
        private static Color frstColColor = Color.FromRgb(248, 248, 255);
        private static Color scndColColor = Color.FromRgb(230, 230, 255);
        private static Color thrdColColor = Color.FromRgb(220, 220, 255);
        private static Color frthColColor = Color.FromRgb(210, 210, 255);

        private static Brush frstColBrsh = new SolidColorBrush(frstColColor);
        private static Brush scndColBrsh = new SolidColorBrush(scndColColor);
        private static Brush thrdColBrsh = new SolidColorBrush(thrdColColor);
        private static Brush frthColBrsh = new SolidColorBrush(frthColColor);
        
        public static readonly Brush[,] SourceColors = new Brush[,] {
            { frstColBrsh, scndColBrsh, thrdColBrsh, frthColBrsh },
            { frstColBrsh, scndColBrsh, thrdColBrsh, frthColBrsh },
            { frstColBrsh, scndColBrsh, thrdColBrsh, frthColBrsh },
            { frstColBrsh, scndColBrsh, thrdColBrsh, frthColBrsh },
        };
        public static readonly Brush[,] ShiftedClrs = new Brush[,] {
            { frstColBrsh, scndColBrsh, thrdColBrsh, frthColBrsh },
            { scndColBrsh, thrdColBrsh, frthColBrsh, frstColBrsh },
            { thrdColBrsh, frthColBrsh, frstColBrsh, scndColBrsh },
            { frthColBrsh, frstColBrsh, scndColBrsh, thrdColBrsh },
        };
    }
}
