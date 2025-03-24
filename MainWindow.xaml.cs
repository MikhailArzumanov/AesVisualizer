using AesVisualizer.Components.SideMenu;
using AesVisualizer.Converting;
using AesVisualizer.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace AesVisualizer {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void AtModeSelection(DisplayMode mode) {
            mainPanel.SetMode(mode);
        }
    }
}
