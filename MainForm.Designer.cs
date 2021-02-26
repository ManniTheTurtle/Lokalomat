
namespace Lokalomat
{
    partial class MainForm
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wähleVerzeichnisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serializeToJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deserializeFromJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateienLöschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speicherLeerenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.serialisiereNeueControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sucheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inDateienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inAktivenControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.comboBoxEdit2);
            this.layoutControl1.Controls.Add(this.comboBoxEdit1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.listBox1);
            this.layoutControl1.Controls.Add(this.menuStrip1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1020, 290, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1395, 675);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl1.Appearance.BackColor2 = System.Drawing.Color.LightSkyBlue;
            this.labelControl1.Appearance.BorderColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseBorderColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(885, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(265, 19);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 12;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Location = new System.Drawing.Point(583, 51);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Size = new System.Drawing.Size(298, 20);
            this.comboBoxEdit2.StyleController = this.layoutControl1;
            this.comboBoxEdit2.TabIndex = 11;
            this.comboBoxEdit2.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit2_SelectedIndexChanged);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(162, 51);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(267, 20);
            this.comboBoxEdit1.StyleController = this.layoutControl1;
            this.comboBoxEdit1.TabIndex = 10;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 75);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1138, 588);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HorzLine.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.HorzLine.BackColor2 = System.Drawing.Color.White;
            this.gridView1.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.BackColor2 = System.Drawing.Color.LightSkyBlue;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.Lime;
            this.gridView1.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.ViewCaption.BackColor = System.Drawing.Color.MediumAquamarine;
            this.gridView1.Appearance.ViewCaption.BackColor2 = System.Drawing.Color.Turquoise;
            this.gridView1.Appearance.ViewCaption.Options.UseBackColor = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(1154, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(229, 641);
            this.listBox1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wähleVerzeichnisToolStripMenuItem,
            this.autoToolStripMenuItem,
            this.manuellToolStripMenuItem,
            this.sucheToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(12, 12);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1138, 35);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wähleVerzeichnisToolStripMenuItem
            // 
            this.wähleVerzeichnisToolStripMenuItem.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.wähleVerzeichnisToolStripMenuItem.Name = "wähleVerzeichnisToolStripMenuItem";
            this.wähleVerzeichnisToolStripMenuItem.Size = new System.Drawing.Size(145, 31);
            this.wähleVerzeichnisToolStripMenuItem.Text = "-> Wähle Verzeichnis <-";
            this.wähleVerzeichnisToolStripMenuItem.Click += new System.EventHandler(this.wähleVerzeichnisToolStripMenuItem_Click);
            // 
            // autoToolStripMenuItem
            // 
            this.autoToolStripMenuItem.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
            this.autoToolStripMenuItem.Size = new System.Drawing.Size(138, 31);
            this.autoToolStripMenuItem.Text = "-> Lade Assemblies <-";
            this.autoToolStripMenuItem.Click += new System.EventHandler(this.autoToolStripMenuItem_Click);
            // 
            // manuellToolStripMenuItem
            // 
            this.manuellToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serializeToJsonToolStripMenuItem,
            this.serialisiereNeueControlsToolStripMenuItem,
            this.deserializeFromJsonToolStripMenuItem,
            this.compareToolStripMenuItem,
            this.dateienLöschenToolStripMenuItem,
            this.speicherLeerenToolStripMenuItem});
            this.manuellToolStripMenuItem.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.manuellToolStripMenuItem.Name = "manuellToolStripMenuItem";
            this.manuellToolStripMenuItem.Size = new System.Drawing.Size(114, 31);
            this.manuellToolStripMenuItem.Text = "-> Lokalisation <-";
            // 
            // serializeToJsonToolStripMenuItem
            // 
            this.serializeToJsonToolStripMenuItem.Name = "serializeToJsonToolStripMenuItem";
            this.serializeToJsonToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.serializeToJsonToolStripMenuItem.Text = "Serialisiere alles";
            this.serializeToJsonToolStripMenuItem.Click += new System.EventHandler(this.serializeToJsonToolStripMenuItem_Click);
            // 
            // deserializeFromJsonToolStripMenuItem
            // 
            this.deserializeFromJsonToolStripMenuItem.Name = "deserializeFromJsonToolStripMenuItem";
            this.deserializeFromJsonToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.deserializeFromJsonToolStripMenuItem.Text = "Deserialize Json";
            this.deserializeFromJsonToolStripMenuItem.Click += new System.EventHandler(this.deserializeFromJsonToolStripMenuItem_Click);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.compareToolStripMenuItem.Text = "Compare";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // dateienLöschenToolStripMenuItem
            // 
            this.dateienLöschenToolStripMenuItem.Name = "dateienLöschenToolStripMenuItem";
            this.dateienLöschenToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.dateienLöschenToolStripMenuItem.Text = "Dateien Löschen";
            this.dateienLöschenToolStripMenuItem.Click += new System.EventHandler(this.dateienLöschenToolStripMenuItem_Click);
            // 
            // speicherLeerenToolStripMenuItem
            // 
            this.speicherLeerenToolStripMenuItem.Name = "speicherLeerenToolStripMenuItem";
            this.speicherLeerenToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.speicherLeerenToolStripMenuItem.Text = "Speicher leeren";
            this.speicherLeerenToolStripMenuItem.Click += new System.EventHandler(this.speicherLeerenToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(88, 31);
            this.testToolStripMenuItem.Text = "-> Testing <-";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1395, 675);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.menuStrip1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1142, 39);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridControl1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 63);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1142, 592);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.listBox1;
            this.layoutControlItem1.Location = new System.Drawing.Point(1142, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(233, 655);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.comboBoxEdit1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(421, 24);
            this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(30, 0, 0, 0);
            this.layoutControlItem4.Text = "Wähle Assembly:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(108, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.labelControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(873, 39);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(269, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.comboBoxEdit2;
            this.layoutControlItem5.Location = new System.Drawing.Point(421, 39);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(452, 24);
            this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(30, 0, 0, 0);
            this.layoutControlItem5.Text = "Wähle Xtra Dokument:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(108, 13);
            // 
            // serialisiereNeueControlsToolStripMenuItem
            // 
            this.serialisiereNeueControlsToolStripMenuItem.Name = "serialisiereNeueControlsToolStripMenuItem";
            this.serialisiereNeueControlsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.serialisiereNeueControlsToolStripMenuItem.Text = "Serialisiere neue Controls";
            this.serialisiereNeueControlsToolStripMenuItem.Click += new System.EventHandler(this.serialisiereNeueControlsToolStripMenuItem_Click);
            // 
            // sucheToolStripMenuItem
            // 
            this.sucheToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inDateienToolStripMenuItem,
            this.inAktivenControlsToolStripMenuItem});
            this.sucheToolStripMenuItem.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.sucheToolStripMenuItem.Name = "sucheToolStripMenuItem";
            this.sucheToolStripMenuItem.Size = new System.Drawing.Size(83, 31);
            this.sucheToolStripMenuItem.Text = "-> Suche <-";
            // 
            // inDateienToolStripMenuItem
            // 
            this.inDateienToolStripMenuItem.Name = "inDateienToolStripMenuItem";
            this.inDateienToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.inDateienToolStripMenuItem.Text = "In Dateien";
            // 
            // inAktivenControlsToolStripMenuItem
            // 
            this.inAktivenControlsToolStripMenuItem.Name = "inAktivenControlsToolStripMenuItem";
            this.inAktivenControlsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.inAktivenControlsToolStripMenuItem.Text = "In Assembly";
            // 
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 675);
            this.Controls.Add(this.layoutControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.ListBox listBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manuellToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private System.Windows.Forms.ToolStripMenuItem wähleVerzeichnisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serializeToJsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deserializeFromJsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateienLöschenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.ToolStripMenuItem speicherLeerenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serialisiereNeueControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sucheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inDateienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inAktivenControlsToolStripMenuItem;
    }
}

