namespace LudoNarrareSimpleInterfacePrototype
{
    partial class LNSimpleInterface
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
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.WorldDataTab = new System.Windows.Forms.TabPage();
            this.WorldDataOutBox = new System.Windows.Forms.RichTextBox();
            this.InterfaceTab = new System.Windows.Forms.TabPage();
            this.InterfaceSplit = new System.Windows.Forms.SplitContainer();
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.OpenWorldDataDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxVerb = new System.Windows.Forms.ComboBox();
            this.inputPromptLabel = new System.Windows.Forms.Label();
            this.buttonInput = new System.Windows.Forms.Button();
            this.TopMenu.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.WorldDataTab.SuspendLayout();
            this.InterfaceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InterfaceSplit)).BeginInit();
            this.InterfaceSplit.Panel1.SuspendLayout();
            this.InterfaceSplit.Panel2.SuspendLayout();
            this.InterfaceSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenu
            // 
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(876, 24);
            this.TopMenu.TabIndex = 0;
            this.TopMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.WorldDataTab);
            this.Tabs.Controls.Add(this.InterfaceTab);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 24);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(876, 588);
            this.Tabs.TabIndex = 1;
            this.Tabs.SelectedIndexChanged += new System.EventHandler(this.Tabs_SelectedIndexChanged);
            // 
            // WorldDataTab
            // 
            this.WorldDataTab.Controls.Add(this.WorldDataOutBox);
            this.WorldDataTab.Location = new System.Drawing.Point(4, 22);
            this.WorldDataTab.Name = "WorldDataTab";
            this.WorldDataTab.Padding = new System.Windows.Forms.Padding(3);
            this.WorldDataTab.Size = new System.Drawing.Size(868, 562);
            this.WorldDataTab.TabIndex = 0;
            this.WorldDataTab.Text = "World Data";
            this.WorldDataTab.UseVisualStyleBackColor = true;
            // 
            // WorldDataOutBox
            // 
            this.WorldDataOutBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorldDataOutBox.Location = new System.Drawing.Point(3, 3);
            this.WorldDataOutBox.Name = "WorldDataOutBox";
            this.WorldDataOutBox.ReadOnly = true;
            this.WorldDataOutBox.Size = new System.Drawing.Size(862, 556);
            this.WorldDataOutBox.TabIndex = 0;
            this.WorldDataOutBox.Text = "";
            // 
            // InterfaceTab
            // 
            this.InterfaceTab.Controls.Add(this.InterfaceSplit);
            this.InterfaceTab.Location = new System.Drawing.Point(4, 22);
            this.InterfaceTab.Name = "InterfaceTab";
            this.InterfaceTab.Padding = new System.Windows.Forms.Padding(3);
            this.InterfaceTab.Size = new System.Drawing.Size(868, 562);
            this.InterfaceTab.TabIndex = 1;
            this.InterfaceTab.Text = "Interface";
            this.InterfaceTab.UseVisualStyleBackColor = true;
            // 
            // InterfaceSplit
            // 
            this.InterfaceSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InterfaceSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.InterfaceSplit.Location = new System.Drawing.Point(3, 3);
            this.InterfaceSplit.Name = "InterfaceSplit";
            this.InterfaceSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // InterfaceSplit.Panel1
            // 
            this.InterfaceSplit.Panel1.Controls.Add(this.OutputBox);
            // 
            // InterfaceSplit.Panel2
            // 
            this.InterfaceSplit.Panel2.Controls.Add(this.buttonInput);
            this.InterfaceSplit.Panel2.Controls.Add(this.inputPromptLabel);
            this.InterfaceSplit.Panel2.Controls.Add(this.comboBoxVerb);
            this.InterfaceSplit.Size = new System.Drawing.Size(862, 556);
            this.InterfaceSplit.SplitterDistance = 437;
            this.InterfaceSplit.TabIndex = 2;
            // 
            // OutputBox
            // 
            this.OutputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputBox.Location = new System.Drawing.Point(0, 0);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(862, 437);
            this.OutputBox.TabIndex = 0;
            this.OutputBox.Text = "";
            // 
            // OpenWorldDataDialog
            // 
            this.OpenWorldDataDialog.FileName = "openFileDialog1";
            this.OpenWorldDataDialog.Filter = "XML Files|*.xml";
            // 
            // comboBoxVerb
            // 
            this.comboBoxVerb.FormattingEnabled = true;
            this.comboBoxVerb.Location = new System.Drawing.Point(0, 16);
            this.comboBoxVerb.Name = "comboBoxVerb";
            this.comboBoxVerb.Size = new System.Drawing.Size(184, 21);
            this.comboBoxVerb.TabIndex = 0;
            // 
            // inputPromptLabel
            // 
            this.inputPromptLabel.AutoSize = true;
            this.inputPromptLabel.Location = new System.Drawing.Point(0, 0);
            this.inputPromptLabel.Name = "inputPromptLabel";
            this.inputPromptLabel.Size = new System.Drawing.Size(57, 13);
            this.inputPromptLabel.TabIndex = 1;
            this.inputPromptLabel.Text = "I want to...";
            // 
            // buttonInput
            // 
            this.buttonInput.Location = new System.Drawing.Point(190, 16);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(75, 23);
            this.buttonInput.TabIndex = 2;
            this.buttonInput.Text = "Do it!";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // LNSimpleInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 612);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.TopMenu);
            this.MainMenuStrip = this.TopMenu;
            this.Name = "LNSimpleInterface";
            this.Text = "LudoNarrare Simple Interface";
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.WorldDataTab.ResumeLayout(false);
            this.InterfaceTab.ResumeLayout(false);
            this.InterfaceSplit.Panel1.ResumeLayout(false);
            this.InterfaceSplit.Panel2.ResumeLayout(false);
            this.InterfaceSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InterfaceSplit)).EndInit();
            this.InterfaceSplit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage WorldDataTab;
        private System.Windows.Forms.RichTextBox WorldDataOutBox;
        private System.Windows.Forms.TabPage InterfaceTab;
        private System.Windows.Forms.OpenFileDialog OpenWorldDataDialog;
        private System.Windows.Forms.RichTextBox OutputBox;
        private System.Windows.Forms.SplitContainer InterfaceSplit;
        private System.Windows.Forms.Label inputPromptLabel;
        private System.Windows.Forms.ComboBox comboBoxVerb;
        private System.Windows.Forms.Button buttonInput;
    }
}

