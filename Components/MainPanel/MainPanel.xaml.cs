using AesVisualizer.Components.SideMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AesVisualizer.Components.MainPanel {
    public partial class MainPanel : UserControl {


        private IPanel[] panels;

        private bool isBusy = false;
        private DisplayMode currentMode  = DisplayMode.INVALID;
        private IPanel      currentPanel = null;

        private void InitPanels() {
            panels = new IPanel[] {
                InfoPanel, AesPanel, 
            };
        }

        public MainPanel() {
            InitializeComponent();
            InitPanels();
        }

        private void StartOpacityAnimation(IPanel panel, AnimationTimeline animation) {
            panel.BeginAnimation(UserControl.OpacityProperty, animation);
        }

        private async Task FadeOutIfDisplayed() {
            if (currentPanel != null) {
                StartOpacityAnimation(currentPanel, Animations.fadeAnimation);
                await Task.Delay(Animations.ANIMATION_DURATION);
            }
        }

        private void FadeInNewPanel(int index) {
            currentPanel = panels[index];
            currentPanel.Reset();
            StartOpacityAnimation(currentPanel, Animations.appearAnimation);
        }

        public async void SetMode(DisplayMode mode) {
            if (currentMode == mode || isBusy) return;
            isBusy = true;
            int index = (int)mode;
            await FadeOutIfDisplayed();
            FadeInNewPanel(index);
            isBusy = false;
        }
    }
}
