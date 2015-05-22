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
            this.closeZ0r = new System.Windows.Forms.ToolStripMenuItem();
            this.settings = new System.Windows.Forms.ToolStripMenuItem();
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
            this.settings,
            this.closeZ0r});
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(153, 114);
            // 
            // customLink
            // 
            this.customLink.Name = "customLink";
            this.customLink.Size = new System.Drawing.Size(152, 22);
            this.customLink.Text = "Custom link";
            // 
            // linkHistory
            // 
            this.linkHistory.Name = "linkHistory";
            this.linkHistory.Size = new System.Drawing.Size(152, 22);
            this.linkHistory.Text = "History";
            // 
            // closeZ0r
            // 
            this.closeZ0r.Name = "closeZ0r";
            this.closeZ0r.Size = new System.Drawing.Size(152, 22);
            this.closeZ0r.Text = "Close";
            this.closeZ0r.Click += new System.EventHandler(this.closeZ0r_Click);
            // 
            // settings
            // 
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(152, 22);
            this.settings.Text = "Settings";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(124, 33);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "main";
            this.Text = "Form1";
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
        private System.Windows.Forms.ToolStripMenuItem settings;
    }
}

