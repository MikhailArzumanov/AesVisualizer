using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;

namespace AesVisualizer.Components.MainPanel.Aes.Pages {
    public interface IPage {
        string PageName   { get; set; }
        int    PageNumber { get; set; }
        int    PagesCount { get; set; }

        void Hide();
        void Show();
        byte[] GetNext();
    }
    
}
