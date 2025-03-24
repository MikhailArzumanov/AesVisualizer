using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public class BasePage : UserControl, IPage {
        public string PageName   { get; set; }
        public int    PageNumber { get; set; }
        public int    PagesCount { get; set; }

        protected BasePage(int pageNum, int pagesCount) {
            PageNumber = pageNum; PagesCount = pagesCount;
        }

        public async void Hide() {
            BeginAnimation(UserControl.OpacityProperty, Animations.fadeAnimation);
            await Task.Delay(Animations.ANIMATION_DURATION);
            Visibility = Visibility.Collapsed;
        }
        public void Show() {
            Visibility = Visibility.Visible;
            BeginAnimation(UserControl.OpacityProperty, Animations.appearAnimation);
        }

        public virtual byte[] GetNext() => null;
    }
}
