using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace z0r_desktop {
    public partial class main : Form {
        
        // Classes import
        hotkeys hk = new hotkeys();
        settings sett = new settings();

        // Ding
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        // Reg Expressions
        Regex urlRegex = new Regex("(https?|ftp|file)://[-A-Za-z0-9\\+&@#/%?=~_|!:,.;]*\\.[A-Za-z0-9\\+&@%#/=~_|():?]+");
        Regex z0rRegex = new Regex("http://z0r.it/[^ \n]");

        // Main form
        public main() {
            sett.checkFiles(); // Check if all files are ok, otherwhise create/download them
            hk.GlobalHotkey(Constants.ALT, Keys.Z, this); // Register ALT+Z hotkey
            hk.Register();
            InitializeComponent();
        }

        // When the main form is showed is minimized
        private void z0rShown(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        // When the main form is minimized is hidden and shows a notification
        private void z0rResized(object sender, EventArgs e) {
            try {
                // This will happen only if the main form is minimized
                if (this.WindowState == FormWindowState.Minimized) {
                    this.Visible = false;
                    notification.Visible = true;
                    notification.ShowBalloonTip(1, "z0r", "Running", ToolTipIcon.Info);
                }
            } catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        // Close bot
        private void closeZ0r_Click(object sender, EventArgs e) {
            DialogResult closingResponse; // Response from messagebox
            closingResponse = MessageBox.Show("Are you sure you want to exit z0r?" , "Close z0r", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closingResponse == DialogResult.Yes) Application.Exit();
        }

        // When z0r is closing
        private void z0rClosing(object sender, FormClosingEventArgs e) {
            try { hk.Unregister(); }// Unregister hotkeys
            catch (Exception ex) { MessageBox.Show(ex.Message);}
         }

        // Hotkey Handler
        protected override void WndProc(ref Message m) {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID) shrink(Clipboard.GetText());
            base.WndProc(ref m);   
        }

        // Shrink the clipboard content
        private void shrink(string longLink) {
            string urlToShrink = checkClipboard(longLink);
            if (z0rRegex.IsMatch(removeWWW(urlToShrink))) Clipboard.SetText(getLongLink(urlToShrink));
            else if(!(urlToShrink == "error")) Clipboard.SetText(getShortLink(urlToShrink));
            else notification.ShowBalloonTip(1, "z0r", "Invalid URL", ToolTipIcon.Error);
        }

        // Check clipboard for invalid URL
        public string checkClipboard(string clipContent) {
            if(urlRegex.IsMatch(clipContent)) return clipContent;
             else {
                clipContent = "http://" + clipContent;
                if(urlRegex.IsMatch(clipContent)) return clipContent;
                else return "error";
            }
        }

        // Get short link from z0r
        public string getShortLink(string longLink) {
            notification.ShowBalloonTip(1, "z0r", "Shrinkig...", ToolTipIcon.Info);
            try {
                string URI = "http://z0r.it/yourls-api.php?signature=4e4b657a91&action=shorturl&format=simply&url=" + longLink + "&title=upload_wth_z0r_desktop";
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(sett.soundFile);
                player.Play();
                notification.ShowBalloonTip(1, "z0r", "URL shrinked and pasted in clipboard!", ToolTipIcon.Info);
                return removeWWW(sr.ReadToEnd().Trim());
            } catch {
                notification.ShowBalloonTip(1, "z0r", "Connection error", ToolTipIcon.Error);
                return longLink;
            }
        }

        // Get long link from z0r
        public string getLongLink(string shortLink) { 
            notification.ShowBalloonTip(1, "z0r", "Expanding...", ToolTipIcon.Info);
            try {
                string URI = "http://z0r.it/yourls-api.php?signature=4e4b657a91&action=expand&format=simply&shorturl=" + getLinkID(shortLink) + "&title=check_with_z0r_desktop";
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(sett.soundFile);
                player.Play();
                notification.ShowBalloonTip(1, "z0r", "URL expanded and pasted in clipboard!", ToolTipIcon.Info);
                return sr.ReadToEnd().Trim();
            }
            catch {
                notification.ShowBalloonTip(1, "z0r", "Connection error", ToolTipIcon.Error);
                return shortLink;
            }
        }

        // Get the link ID
        public string getLinkID(string link) {
            char[] splitchar = { '/' };
            return link.Split(splitchar).Last();
        }

        // Remove WWW from the link
        public string removeWWW(string link) {
            char[] splitchar = { '.' };
            return "http://z0r." + link.Split(splitchar).Last();
        }
    }

    // Files and settings
    public class settings {

        // Directories
        string z0rFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/z0r";
        public string soundFile = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/z0r/z0rDing.wav";

        // Check if all files are OK, otherwise download them
        public void checkFiles() {

            if (!Directory.Exists(z0rFolder)) Directory.CreateDirectory(z0rFolder); // Check z0r folder

            // Check audio file
            if (!File.Exists(soundFile)) {
                WebClient wc = new WebClient();
                wc.DownloadFile("http://l33tspace.altervista.org/Ding.wav", soundFile);
            }
        }
    }

    // HotKeys methods
    public class hotkeys {
        // External hotkeys methods
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Fields
        private int modifier;
        private int key;
        private IntPtr hWnd;
        private int id;

        // Constructor
        public void GlobalHotkey(int modifier, Keys key, Form form) {
            this.modifier = modifier;
            this.key = (int)key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
        }

        public override int GetHashCode() {
            return modifier ^ key ^ hWnd.ToInt32();
        }

        // Register hotkeys
        public bool Register() {
            return RegisterHotKey(hWnd, id, modifier, key);
        }

        // Unregister hotkeys
        public bool Unregister() {
            return UnregisterHotKey(hWnd, id);
        }

    }

    // Constants to semplify hotkeys use
    public static class Constants  {
        //modifiers
        public const int NOMOD = 0x0000;
        public const int ALT = 0x0001;
        public const int CTRL = 0x0002;
        public const int SHIFT = 0x0004;
        public const int WIN = 0x0008;

        //windows message id for hotkey
        public const int WM_HOTKEY_MSG_ID = 0x0312;
    }
}

