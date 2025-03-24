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

namespace AesVisualizer.Components.SideMenu.ModeSelector {
    public partial class ModeSelector : UserControl {
        
        private void InitList() {
            var labels = new Dictionary<DisplayMode, string>((int)DisplayMode.COUNT){
                { DisplayMode.INFO , "Информация о приложении" },
                { DisplayMode.AES  , "Визуализатор AES"        },
            };
            
            for(DisplayMode mode = DisplayMode.FIRST; mode < DisplayMode.COUNT; mode++) {
                var label = labels[mode];
                var listBoxItem = new ListBoxItem { 
                    Content = label 
                };
                selector.Items.Add(listBoxItem);
            }
        }
        
        public ModeSelector() {
            InitializeComponent();
            InitList();
        }

        private void ModeSelected(object sender, SelectionChangedEventArgs e) {
            var selectedIndex = selector.SelectedIndex;
            AtModeSelected((DisplayMode) selectedIndex);
        }

        public event ModeSelectionEvent AtModeSelected;
    }
}
