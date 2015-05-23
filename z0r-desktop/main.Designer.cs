namespace z0r_desktop
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.notification = new System.Windows.Forms.NotifyIcon(this.components);
            this.options = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.customLink = new System.Windows.Forms.ToolStripMenuItem();
            this.linkHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.soundEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.autoShrinkEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRunEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showTips = new System.Windows.Forms.ToolStripMenuItem();
            this.closeZ0r = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.options.SuspendLayout();
            this.SuspendLayout();
            // 
            // notification
            // 
            this.notification.ContextMenuStrip = this.options;
            this.notification.Icon = ((System.Drawing.Icon)(resources.GetObject("notification.Icon")));
            this.notification.Text = "z0r";
            // 
            // options
            // 
            this.options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customLink,
            this.linkHistory,
            this.toolStripSeparator1,
            this.soundEnabled,
            this.autoShrinkEnabled,
            this.autoRunEnabled,
            this.toolStripSeparator2,
            this.showTips,
            this.closeZ0r});
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(201, 170);
            // 
            // customLink
            // 
            this.customLink.Image = ((System.Drawing.Image)(resources.GetObject("customLink.Image")));
            this.customLink.Name = "customLink";
            this.customLink.Size = new System.Drawing.Size(200, 22);
            this.customLink.Text = "Custom link";
            this.customLink.Click += new System.EventHandler(this.customLink_Click);
            // 
            // linkHistory
            // 
            this.linkHistory.Image = ((System.Drawing.Image)(resources.GetObject("linkHistory.Image")));
            this.linkHistory.Name = "linkHistory";
            this.linkHistory.Size = new System.Drawing.Size(200, 22);
            this.linkHistory.Text = "History";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // soundEnabled
            // 
            this.soundEnabled.Name = "soundEnabled";
            this.soundEnabled.Size = new System.Drawing.Size(200, 22);
            this.soundEnabled.Text = "Play sound";
            this.soundEnabled.ToolTipText = "Enable/disable sound at shrink/expand completed";
            this.soundEnabled.Click += new System.EventHandler(this.toggleSound);
            // 
            // autoShrinkEnabled
            // 
            this.autoShrinkEnabled.Name = "autoShrinkEnabled";
            this.autoShrinkEnabled.Size = new System.Drawing.Size(200, 22);
            this.autoShrinkEnabled.Text = "Auto Shrink/Expand";
            this.autoShrinkEnabled.ToolTipText = "If active, everytime the clipboard is changed and contains a valid URL, it will b" +
    "e automatically shrinked/exapnded.";
            this.autoShrinkEnabled.Click += new System.EventHandler(this.toggleAutoShrink);
            // 
            // autoRunEnabled
            // 
            this.autoRunEnabled.Name = "autoRunEnabled";
            this.autoRunEnabled.Size = new System.Drawing.Size(200, 22);
            this.autoRunEnabled.Text = "Run at Windows startup";
            this.autoRunEnabled.ToolTipText = "Run z0r at Windows startup";
            this.autoRunEnabled.Click += new System.EventHandler(this.toggleAutorun);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
            // 
            // showTips
            // 
            this.showTips.Image = ((System.Drawing.Image)(resources.GetObject("showTips.Image")));
            this.showTips.Name = "showTips";
            this.showTips.Size = new System.Drawing.Size(200, 22);
            this.showTips.Text = "Help";
            this.showTips.Click += new System.EventHandler(this.showTips_Click);
            // 
            // closeZ0r
            // 
            this.closeZ0r.Image = ((System.Drawing.Image)(resources.GetObject("closeZ0r.Image")));
            this.closeZ0r.Name = "closeZ0r";
            this.closeZ0r.Size = new System.Drawing.Size(200, 22);
            this.closeZ0r.Text = "Close";
            this.closeZ0r.Click += new System.EventHandler(this.closeZ0r_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(90, 28);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "main";
            this.Text = "z0r";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.z0rClosing);
            this.Shown += new System.EventHandler(this.z0rShown);
            this.Resize += new System.EventHandler(this.z0rResized);
            this.options.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notification;
        private System.Windows.Forms.ContextMenuStrip options;
        private System.Windows.Forms.ToolStripMenuItem customLink;
        private System.Windows.Forms.ToolStripMenuItem linkHistory;
        private System.Windows.Forms.ToolStripMenuItem closeZ0r;
        private System.Windows.Forms.ToolStripMenuItem showTips;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem soundEnabled;
        private System.Windows.Forms.ToolStripMenuItem autoShrinkEnabled;
        private System.Windows.Forms.ToolStripMenuItem autoRunEnabled;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

