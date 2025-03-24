using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AesVisualizer.Components.MainPanel.Aes.Pages;

namespace AesVisualizer.Components.MainPanel.Aes {
    public class PagesBuilder {
        private byte[]   state ;
        private BasePage[]  pages ;
        
        private readonly int roundsCount;
        private readonly int pagesAmount;

        private int pageIndex ;

        public void PrefixActions(byte[] keyBytes, out UInt32[] expKey) {
            pages[pageIndex] = new KeyExpansionPage(
                keyBytes, roundsCount, pageNum: 1, pagesAmount, out expKey
            );
            pages[++pageIndex] = new AddRoundKeyPage(
                ref state, expKey, round: 0, pageIndex + 1, pagesAmount
            );
        }

        private void MainCycle(UInt32[] expKey) {
            for (int round = 1; round < roundsCount; round++) {
                pages[++pageIndex] = new SubBytesPage(ref state, pageIndex + 1, pagesAmount);
                pages[++pageIndex] = new ShiftRowsPage(ref state, pageIndex + 1, pagesAmount);
                pages[++pageIndex] = new MixColumnsPage(ref state, pageIndex + 1, pagesAmount);
                pages[++pageIndex] = new AddRoundKeyPage(
                    ref state, expKey, round, pageIndex + 1, pagesAmount
                );
            }
        }

        private void SuffixActions(UInt32[] expKey) {
            pages[++pageIndex] = new    SubBytesPage(ref state, pageIndex+1, pagesAmount);
            pages[++pageIndex] = new   ShiftRowsPage(ref state, pageIndex+1, pagesAmount);
            pages[++pageIndex] = new AddRoundKeyPage(
                ref state, expKey, round: roundsCount, pageIndex+1, pagesAmount
            );
        }

        public BasePage[] BuildPages(byte[] keyBytes, out UInt32[] expKey) {
            pageIndex = 0;
            PrefixActions(keyBytes, out expKey);
            MainCycle    (expKey);
            SuffixActions(expKey);
            return pages;
        }

        public PagesBuilder(byte[] srcState, int roundsCount) {
            this.roundsCount = roundsCount;
            pagesAmount = 2 + 4*(roundsCount - 1) + 3;
            state = (byte[])srcState.Clone();
            pages = new BasePage[pagesAmount];
        }
    }
}