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
using System.Security.Principal;
using Microsoft.Win32;

namespace z0r_desktop {

    // Main 
    public partial class main : Form {
        
        // Classes import
        hotkeys hk = new hotkeys();

        // Registry key
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        // Ding
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        // Reg Expressions
        Regex urlRegex = new Regex("(https?|ftp|file)://[-A-Za-z0-9\\+&@#/%?=~_|!:,.;]*\\.[A-Za-z0-9\\+&@%#/=~_|():?]+");
        Regex z0rRegex = new Regex("http://z0r.it/[^ \n]");

        // Settings
        public bool soundIsActive;
        public bool autoShrinkIsActive;

        // Directories
        string z0rFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/z0r";
        public string soundFile = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/z0r/z0rDing.wav";
        public string settingsFile = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/z0r/settings.ini";
        public string historyFile = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/z0r/history.txt"; 

        // Last shrinked URL, to avoid multiple shrink
        string lastLink;

        // Main form
        public main() {
            checkFiles(); // Check if all files are ok, otherwhise create/download them
            hk.GlobalHotkey(Constants.ALT, Keys.Z, this); // Register ALT+Z hotkey
            hk.Register();
            InitializeComponent();
            loadSettings();
            AddClipboardFormatListener(this.Handle); // Clipboard handler
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

        // sys message Handler
        protected override void WndProc(ref Message m) {
            // Hotkey press event
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID) shrink(Clipboard.GetText()); 

            base.WndProc(ref m);

            // Clipboard change event
            if (m.Msg == WM_CLIPBOARDUPDATE) {
                    // Only if the last link is different from actual link, or loops
                if (Clipboard.GetText() != lastLink) autoShrink(Clipboard.GetText());       
            }
        }

        // Shrink the clipboard content
        private void shrink(string longLink) {
            string urlToShrink = checkClipboard(longLink);
            if (z0rRegex.IsMatch(removeWWW(urlToShrink))) {
                    lastLink = getLongLink(urlToShrink);
                    Clipboard.SetText(lastLink);
            }
            else if (!(urlToShrink == "error"))
            {
                lastLink = getShortLink(urlToShrink);
                Clipboard.SetText(lastLink);
                registerHistory(longLink, lastLink, DateTime.Now.ToString());
            }
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
                playSound(); 
                notification.ShowBalloonTip(1, "z0r", "URL shrinked and pasted in clipboard!", ToolTipIcon.Info);
                return removeWWW(sr.ReadToEnd().Trim());
            } catch {
                notification.ShowBalloonTip(1, "z0r", "Connection error", ToolTipIcon.Error);
                return longLink;
            }
        }

        // Get custom short link from z0r
        public string getCustomShortLink(string longLink, string customLink) {
            try {
                string URI = "http://z0r.it/yourls-api.php?signature=4e4b657a91&action=shorturl&title=uploaded_with_z0r_desktop&keyword=" + customLink + "&format=simply&url=" + longLink;
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                playSound();
                notification.ShowBalloonTip(1, "z0r", "Custom URL shrinked and pasted in clipboard!", ToolTipIcon.Info);
                return sr.ReadToEnd().Trim();
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
                playSound();
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

        // Automatic shrinking, if active
        public void autoShrink(string longLink){
            if (autoShrinkEnabled.Checked) {
                string urlToShrink = checkClipboard(longLink);
                if (z0rRegex.IsMatch(removeWWW(urlToShrink))){
                    // nothing
                } else if (!(urlToShrink == "error")) {
                    lastLink = getShortLink(urlToShrink);
                    Clipboard.SetText(lastLink);
                }
                else notification.ShowBalloonTip(1, "z0r", "Invalid URL", ToolTipIcon.Error);
            }
        }

        // Function to play sound
        public void playSound() {
            if (soundEnabled.Checked) {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundFile);
                player.Play();
            }   
        }

        // Places the given window in the system-maintained clipboard format listener list.
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        // Removes the given window from the system-maintained clipboard format listener list.
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        // Sent when the contents of the clipboard have changed.
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        // Custom link button click
        private void customLink_Click(object sender, EventArgs e){
            string customInput = Microsoft.VisualBasic.Interaction.InputBox("Insert the text of your custom link. The link created will be like: http://z0r.it/your-text", "Custom link", "");
            string customText = customInput.Replace(" ", "-");
            string longLink = Clipboard.GetText();
            lastLink = getCustomShortLink(longLink, customText);
            Clipboard.SetText(lastLink);
            registerHistory(longLink, lastLink, DateTime.Now.ToString());
        }

        // Shows help dialog
        private void showTips_Click(object sender, EventArgs e){
            string helpText = "SHRINK\nCopy the link you want to shrink in the clipboard and press ATL+Z\n\nEXPAND\nSame thing as Shrink, but if you have a z0r link in clipboard it will be extended.\n\nAUTOSHRINK\nIf enabled, everytime you copy a link in clipbord it will automatcally shrinked\n\nInforge.net";
            MessageBox.Show(helpText, "Z0r Info", MessageBoxButtons.OK);
        }

        // Check if all files are OK, otherwise download them
        public void checkFiles()
        {
            // Check z0r folder
            if (!Directory.Exists(z0rFolder)) Directory.CreateDirectory(z0rFolder);

            // Check audio file
            if (!File.Exists(soundFile)){
                WebClient wc = new WebClient();
                wc.DownloadFile("http://l33tspace.altervista.org/Ding.wav", soundFile);
            }
        }

        // Load and update settings
        public void loadSettings()
        {
            if (!File.Exists(settingsFile))
            {
                // Default setting file
                File.Create(settingsFile).Dispose();
                StreamWriter writeText = new StreamWriter(settingsFile);
                writeText.WriteLine("True");
                writeText.WriteLine("False");
                writeText.WriteLine("False");
                writeText.Close();
            }

            StreamReader readText = new StreamReader(settingsFile);
            soundEnabled.Checked = Convert.ToBoolean(readText.ReadLine());
            autoShrinkEnabled.Checked = Convert.ToBoolean(readText.ReadLine());
            autoRunEnabled.Checked = Convert.ToBoolean(readText.ReadLine());
            readText.Close();
        }

        // Save settings avery time settings are changed
        public void saveSettings() {
            if (!File.Exists(settingsFile)) {
                File.Create(settingsFile).Dispose();
            } else {
                File.Delete(settingsFile);
                File.Create(settingsFile).Dispose();
            }

            StreamWriter writeText = new StreamWriter(settingsFile);
            writeText.WriteLine(soundEnabled.Checked.ToString());
            writeText.WriteLine(autoShrinkEnabled.Checked.ToString());
            writeText.WriteLine(autoRunEnabled.Checked.ToString());
            writeText.Close();


        }

        // Check if user is admin
        public bool isAdmin() {
            //bool value to hold our return value
            bool response;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                response = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                response = false;
            }
            catch (Exception ex)
            {
                response = false;
            }
            return response;
        }

        // Toggle sound
        private void toggleSound(object sender, EventArgs e)
        {
            if (soundEnabled.Checked) soundEnabled.Checked = false;
            else soundEnabled.Checked = true;
            saveSettings();
        }

        // Toggle auto shrink/expand
        private void toggleAutoShrink(object sender, EventArgs e)
        {
            if (autoShrinkEnabled.Checked) autoShrinkEnabled.Checked = false;
            else autoShrinkEnabled.Checked = true;
            saveSettings();
        }

        // Toggle autorun at Windows Startup
        private void toggleAutorun(object sender, EventArgs e) {
            if (isAdmin()){
                if (autoRunEnabled.Checked){
                    autoRunEnabled.Checked = false;
                    if (rkApp.GetValue("z0r") != null)  rkApp.DeleteValue("z0r", false);
                } else {
                    autoRunEnabled.Checked = true;
                    if (rkApp.GetValue("z0r") == null)  rkApp.SetValue("z0r", Application.ExecutablePath.ToString());
                }
                saveSettings();
            }
            else
            {
                MessageBox.Show("To change this option you must run z0r as Administrator", "Admin privileges requested");
            }
        }

        // Avoid the menu closing after clicking an item
        private void menuClosing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
        }

        // Detect if the program is in the startup and refresh the control
        private void menuOpened(object sender, EventArgs e)
        {
            if (rkApp.GetValue("z0r") != null) {
                autoRunEnabled.Checked = true;
            } else {
                autoRunEnabled.Checked = false;
            }
        }

        // Write history
        public void registerHistory(string longLink, string shortLink, string time) {
            // Check file before write
            if (!File.Exists(historyFile)) File.Create(historyFile).Dispose();

            StreamWriter writeText = new StreamWriter(historyFile, true);
            writeText.WriteLine(time + " - " + shortLink + " (" + longLink + ")");
            writeText.Close();
        }

        // Open history file
        private void linkHistory_Click(object sender, EventArgs e)
        {
            history historyWindow = new history();
            historyWindow.Show();
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

