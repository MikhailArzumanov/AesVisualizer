using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace AesVisualizer {
    public class Animations {
        private const double VISIBLE_OPACITY    = 1.0;
        private const double HIDDEN_OPACITY     = 0.0;
        public const int     ANIMATION_DURATION = 500;


        public static DoubleAnimation fadeAnimation = new DoubleAnimation {
            From = VISIBLE_OPACITY,
            To = HIDDEN_OPACITY,
            Duration = TimeSpan.FromMilliseconds(ANIMATION_DURATION)
        };

        public static DoubleAnimation appearAnimation = new DoubleAnimation {
            From = HIDDEN_OPACITY,
            To = VISIBLE_OPACITY,
            Duration = TimeSpan.FromMilliseconds(ANIMATION_DURATION)
        };
    }
}
