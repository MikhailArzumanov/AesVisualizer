using AesVisualizer.Components.MainPanel.Aes.Pages;
using AesVisualizer.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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

namespace AesVisualizer.Components.MainPanel.Aes {
    public partial class AesPanel : UserControl, IPanel {
        public AesPanel() {
            InitializeComponent();
        }

        private int nk          = 128;
        private Dictionary<int, int> roundsCounts =
            new Dictionary<int, int> { 
                { 128, 10 }, { 192, 12 }, { 256, 14 }
            };

        private bool isBusy = false;

        private BasePage[] pages       = null ;
        private IPage      currentPage = null ;
        private int        pageIndex   = 0    ;

        public async void Reset(){
            if (currentPage == null || isBusy) return;
            currentPage.Hide(); currentPage = null;
            pages = null; pageIndex = 0;
            controlPanel.IsBusy = isBusy = true;
            controlPanel.RenderComponent(null);
            await Task.Delay(Animations.ANIMATION_DURATION);
            mainGrid.Children.Clear();
            controlPanel.IsBusy = isBusy = false;
        }

        private void LogPipeline(byte[] srcData, UInt32[] expKey, int roundsCount) {
            var records = new AesRecorder(srcData, expKey, pages, roundsCount);
            var recordTstamp = DateTime.UtcNow.ToString("s").Replace(':', '_');
            var recordFileName = $"ENCRYPTION-RECORD-{recordTstamp}";
            records.BuildRecords(fileName: recordFileName);
        }

        private void AddPagesToGrid() { 
            foreach (var page in pages) {
                if (page == null) break;
                page.Opacity = 0; 
                page.Visibility = Visibility.Collapsed;
                mainGrid.Children.Add(page);
            }
        }

        private void PrebuildPages(byte[] srcState, byte[] keyBytes) {
            int roundsCount = roundsCounts[nk];
            
            UInt32[] expKey;
            var pageBuilder = new PagesBuilder(srcState, roundsCount);
            pageBuilder.BuildPages(keyBytes, out expKey);
            LogPipeline(srcState, expKey, roundsCount);
            AddPagesToGrid();
        }

        private bool ValidateBytes(byte[] bytes, int desiredCount, string msg) {
            if (bytes == null || bytes.Length != desiredCount) {
                MessageBox.Show(msg);
                return false;
            }
            return true;
        }

        private void Begin() {
            if (isBusy) { return; }
            var keyStr = keyField.Text; var dataStr = dataField.Text;

            var keyBytes = Converting.Hex.HexToBytes(keyStr);
            var KEY_IS_NOT_VALID = 
                $"Ключ должен составлять шестнадцатиричное представление из {nk / 4} символов.";
            bool isKeyValid = ValidateBytes(keyBytes, nk/8, KEY_IS_NOT_VALID);


            var dataBytes = Converting.Hex.HexToBytes(dataStr);
            const string DATA_IS_NOT_VALID =
                    "Данные должны составлять шестнадцатиричное представление из 32 символов.";
            bool isDataValid = ValidateBytes(dataBytes, 16, DATA_IS_NOT_VALID);

            if(!isKeyValid || !isDataValid) {
                return;
            }

            PrebuildPages(dataBytes, keyBytes);
            SwitchPage(-pageIndex);
        }

        private void SelectNk(int nk) {
            this.nk = nk;
        }

        private void SwitchPage(int step) {
            pageIndex += step;
            if(currentPage != null) currentPage.Hide();
            currentPage = (IPage) pages[pageIndex];
            currentPage.Show();
            controlPanel.RenderComponent(currentPage);
        }

        private void openFirstPage() {
            SwitchPage(-pageIndex);
        }
        private void openLastPage() {
            int lastIndex = pages.Length-1;
            SwitchPage(lastIndex - pageIndex);
        }
    }
}
