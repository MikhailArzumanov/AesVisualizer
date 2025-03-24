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

namespace AesVisualizer.Components.SideMenu {
    public enum DisplayMode {
        INVALID = -1,
        FIRST   =  0,
        INFO    =  0,
        AES     =  1,
        COUNT
    }
    public delegate void ModeSelectionEvent(DisplayMode mode);
    public partial class SideMenu : UserControl {
        public SideMenu() {
            InitializeComponent();
        }

        public event ModeSelectionEvent AtModeSelected;

        private void AtModeSelection(DisplayMode mode){
            AtModeSelected(mode);
        }
    }
}