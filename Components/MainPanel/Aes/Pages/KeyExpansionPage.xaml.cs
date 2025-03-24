using AesVisualizer.Components.MainPanel.Aes.Pages.KeyExpansion;
using AesVisualizer.Converting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public partial class KeyExpansionPage : BasePage {
        private TextBlock GetFormattedBlock(string text) {
            return new TextBlock {
                Text = text, FontSize = 20, 
                FontFamily = prefixTextBlock.FontFamily,
                VerticalAlignment = VerticalAlignment.Center,
                
            };
        }

        private void SetLogs(List<string> logs) {
            const int PREFIX_BLOCKS_COUNT = 4+1;
            int rowIndex = PREFIX_BLOCKS_COUNT;
            foreach (var line in logs) {
                var textBlock = GetFormattedBlock(line);
                var rowDefinition = new RowDefinition { Height = new GridLength(40) };
                mainGrid.Children.Add(textBlock);
                mainGrid.RowDefinitions.Add(rowDefinition);
                Grid.SetRow(textBlock, rowIndex++);
            }
        }

        public KeyExpansionPage(byte[] key, int Nr, int pageNum, int pageCount, 
        out UInt32[] expKey): base(pageNum, pageCount){
            PageName = "KeyExpansion";
            InitializeComponent();
            List<string> logs;
            expKey = new KeyExpander().BuildKey(out logs, key, Nr);
            SetLogs(logs);
        }
    }
}
