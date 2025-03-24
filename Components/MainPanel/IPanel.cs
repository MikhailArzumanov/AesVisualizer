using System.Windows;
using System.Windows.Media.Animation;

namespace AesVisualizer.Components.MainPanel {
    public interface IPanel {
        void Reset();
        void BeginAnimation(DependencyProperty dp, AnimationTimeline animation);
    }
}
