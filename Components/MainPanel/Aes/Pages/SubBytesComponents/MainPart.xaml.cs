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
    public partial class MainPart : UserControl {
        
        public void SetData(byte[] state, byte[] result) {
            prevStateGrid.SetData(state);
            nextStateGrid.SetData(result);
        }
        
        public MainPart() {
            InitializeComponent();
        }
    }
}
