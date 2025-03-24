using AesVisualizer.Components.MainPanel.Aes.Pages;
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

namespace AesVisualizer.Components.MainPanel.Aes.ControlPanel {
    public delegate void PageChanging(int step);
    public delegate void NkChanging(int Nk);
    public partial class AesControlPanel : UserControl {
        public AesControlPanel() {
            InitializeComponent();
        }

        private const string BEGIN_ACTION_TEXT = "Осуществить алгоритм";
        private const string RESET_ACTION_TEXT = "Сбросить состояние";

        private bool wasTurnedOn               = false;
        public  bool IsBusy      { get; set; } = false;

        public event Action       AtBegining    ;
        public event PageChanging AtPageChange  ;
        public event NkChanging   AtNkSelection ;
        public event Action       AtReset       ;

        public event Action AtFirstPageOpening ;
        public event Action AtLastPageOpening  ;


        private void SetControlsEnabled(bool value, params Control[] disablingControls) {
            foreach (var item in disablingControls) {
                item.IsEnabled = value;
            }
        }

        private void setBtnsEnabled(IPage page) {
            bool next, prev;
            if (page == null) {
                next = prev = false;
            } else {
                next = page.PageNumber == page.PagesCount ? false : true;
                prev = page.PageNumber == 1               ? false : true;
            }
            SetControlsEnabled(next, nextPageBtn,  lastPageBtn);
            SetControlsEnabled(prev, prevPageBtn, firstPageBtn);
        }

        private async void HidePageLabel() {
            pageLabel.BeginAnimation(OpacityProperty, Animations.fadeAnimation);
            await Task.Delay(Animations.ANIMATION_DURATION);
            pageLabel.Content = String.Empty;
        }

        private void ShowPageLabel(IPage page) {
            pageLabel.Content = $"{page.PageName} ({page.PageNumber}/{page.PagesCount})";
            if (wasTurnedOn) return;
            pageLabel.BeginAnimation(OpacityProperty, Animations.appearAnimation);
        }

        public void RenderComponent(IPage page) {
            setBtnsEnabled(page);
            if (page == null) {
                if (!wasTurnedOn) return;
                wasTurnedOn = false;
                ActionBtn.Content = BEGIN_ACTION_TEXT;
                HidePageLabel();
            } else {
                ActionBtn.Content = RESET_ACTION_TEXT;
                ShowPageLabel(page);
                wasTurnedOn = true;
            }
        }

        private void NkSelected(object sender, SelectionChangedEventArgs e) {
            var nkArray = new int[3] { 128, 192, 256 };
            var selectedIndex = keySelector.SelectedIndex;
            var nk = nkArray[selectedIndex];
            AtNkSelection(nk);
        }

        private void openPrev(object sender, RoutedEventArgs e) {
            AtPageChange(-1);
        }
        private void openNext(object sender, RoutedEventArgs e) {
            AtPageChange(1);
        }

        private void AtActionBtnClicked(object sender, RoutedEventArgs e) {
            if (IsBusy) return;
            if (wasTurnedOn) {
                AtReset();
            } else {
                AtBegining();
            }
        }

        private void openFirstPage(object sender, RoutedEventArgs e) {
            AtFirstPageOpening();
        }
        private void openLastPage(object sender, RoutedEventArgs e) {
            AtLastPageOpening();
        }
    }
}
