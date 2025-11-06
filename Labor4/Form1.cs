using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace miniprojekt_1
{
    public partial class Form1 : Form
    {

        private List<string> keywordsForFiltering = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack) webBrowser1.GoBack();

        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward) webBrowser1.GoForward();

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUrl.Text))
            {
                webBrowser1.Navigate(txtUrl.Text);
            }

        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbKeywords_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddKeyword_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddKeywordForm())
            {
                addForm.ShowDialog(); // Megvárja, amíg a form bezárul
            }
            RefreshKeywordsComboBox(); // Frissítjük a listát

        }

        private void btnDeleteKeyword_Click(object sender, EventArgs e)
        {
            if (cmbKeywords.SelectedItem != null)
            {
                string selectedKeyword = cmbKeywords.SelectedItem.ToString();
                DatabaseHelper.DeleteKeyword(selectedKeyword);
                RefreshKeywordsComboBox(); // Frissítjük a listát
            }

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            txtUrl.Text = webBrowser1.Url.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseHelper.InitializeDatabase();
            RefreshKeywordsComboBox();
        }

        private void RefreshKeywordsComboBox()
        {
            // 1. Betöltjük a listát az adatbázisból
            List<string> keywordsFromDb = DatabaseHelper.LoadKeywords();

            // 2. Frissítjük a belső listát a szűréshez IS
            // (Ez az a sor, ami a kommentedben hiányzott)
            // Mostantól a 'keywordsForFiltering' lista ugyanazt tartalmazza,
            // mint ami az adatbázisban van.
            keywordsForFiltering = keywordsFromDb;

            // 3. Hozzárendeljük a ComboBox-hoz, hogy a felhasználó is lássa
            // Fontos: A DataSource-t érdemes előbb null-ra állítani,
            // hogy a ComboBox biztosan frissítse a tartalmát.
            cmbKeywords.DataSource = null;
            cmbKeywords.DataSource = keywordsForFiltering;
        }

        // AddKeywordForm.cs-ben

        private async void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString().ToLower();

            // A szűrési logikát kiszervezzük egy aszinkron Task-ba (8. pont)
            bool isBlocked = await Task.Run(() =>
            {
                return keywordsForFiltering.Any(keyword => url.Contains(keyword.ToLower()));
            });

            if (isBlocked)
            {
                // 3. pont: Navigáció megszakítása
                e.Cancel = true;
                MessageBox.Show($"URL blokkolva: Az oldal ({url}) tartalmaz egy tiltott kulcsszót.", "Blokkolva");

                // 8. pont: Írás fájlba a Trace osztályon keresztül
                LogBlockedUrl(url);
            }
        }

        private void LogBlockedUrl(string url)
        {
            // A Trace.WriteLine a beállított listener-be (a .log fájlba) fog írni
            Trace.WriteLine($"[{DateTime.Now}] BLOKKOLVA: {url}");
            // Az autoflush=true miatt a Flush() hívása nem feltétlenül szükséges, 
            // de biztos, ami biztos:
            Trace.Flush();
        }
    }
}
