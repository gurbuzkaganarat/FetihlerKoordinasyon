namespace FethlerV2
{
    partial class formparaTutanak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formparaTutanak));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new FethlerV2.DataSet1();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel14 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btnKaydet = new Guna.UI.WinForms.GunaAdvenceButton();
            this.lblBolgeNo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gunaAdvenceButton1 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.cmbDonem = new System.Windows.Forms.ComboBox();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.cmbKoy = new System.Windows.Forms.ComboBox();
            this.cmbBolge = new System.Windows.Forms.ComboBox();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataTable1TableAdapter = new FethlerV2.DataSet1TableAdapters.DataTable1TableAdapter();
            this.tblErzaklarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblErzaklarBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            this.dataTable1BindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.AutoSize = true;
            this.bunifuCustomLabel5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(677, 12);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(37, 21);
            this.bunifuCustomLabel5.TabIndex = 60;
            this.bunifuCustomLabel5.Text = "Köy";
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(458, 12);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(53, 21);
            this.bunifuCustomLabel1.TabIndex = 62;
            this.bunifuCustomLabel1.Text = "Bölge";
            // 
            // bunifuCustomLabel14
            // 
            this.bunifuCustomLabel14.AutoSize = true;
            this.bunifuCustomLabel14.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bunifuCustomLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.bunifuCustomLabel14.Location = new System.Drawing.Point(239, 12);
            this.bunifuCustomLabel14.Name = "bunifuCustomLabel14";
            this.bunifuCustomLabel14.Size = new System.Drawing.Size(76, 21);
            this.bunifuCustomLabel14.TabIndex = 82;
            this.bunifuCustomLabel14.Text = "Kategori";
            // 
            // btnKaydet
            // 
            this.btnKaydet.AnimationHoverSpeed = 0.07F;
            this.btnKaydet.AnimationSpeed = 0.03F;
            this.btnKaydet.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(110)))), ((int)(((byte)(49)))));
            this.btnKaydet.BorderColor = System.Drawing.Color.Transparent;
            this.btnKaydet.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnKaydet.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnKaydet.CheckedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.btnKaydet.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnKaydet.CheckedImage")));
            this.btnKaydet.CheckedLineColor = System.Drawing.Color.Transparent;
            this.btnKaydet.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnKaydet.FocusedColor = System.Drawing.Color.Transparent;
            this.btnKaydet.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.btnKaydet.Image = ((System.Drawing.Image)(resources.GetObject("btnKaydet.Image")));
            this.btnKaydet.ImageSize = new System.Drawing.Size(35, 35);
            this.btnKaydet.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnKaydet.Location = new System.Drawing.Point(518, 85);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.btnKaydet.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnKaydet.OnHoverForeColor = System.Drawing.Color.White;
            this.btnKaydet.OnHoverImage = null;
            this.btnKaydet.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.btnKaydet.OnPressedColor = System.Drawing.Color.Black;
            this.btnKaydet.Size = new System.Drawing.Size(126, 36);
            this.btnKaydet.TabIndex = 87;
            this.btnKaydet.Text = "Önizle";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // lblBolgeNo
            // 
            this.lblBolgeNo.AutoSize = true;
            this.lblBolgeNo.Location = new System.Drawing.Point(122, 12);
            this.lblBolgeNo.Name = "lblBolgeNo";
            this.lblBolgeNo.Size = new System.Drawing.Size(13, 13);
            this.lblBolgeNo.TabIndex = 88;
            this.lblBolgeNo.Text = "0";
            this.lblBolgeNo.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.panel1.Controls.Add(this.gunaAdvenceButton1);
            this.panel1.Controls.Add(this.cmbDonem);
            this.panel1.Controls.Add(this.cmbKategori);
            this.panel1.Controls.Add(this.cmbKoy);
            this.panel1.Controls.Add(this.cmbBolge);
            this.panel1.Controls.Add(this.bunifuCustomLabel2);
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Controls.Add(this.lblBolgeNo);
            this.panel1.Controls.Add(this.bunifuCustomLabel1);
            this.panel1.Controls.Add(this.btnKaydet);
            this.panel1.Controls.Add(this.bunifuCustomLabel5);
            this.panel1.Controls.Add(this.bunifuCustomLabel14);
            this.panel1.Location = new System.Drawing.Point(53, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 565);
            this.panel1.TabIndex = 89;
            // 
            // gunaAdvenceButton1
            // 
            this.gunaAdvenceButton1.AnimationHoverSpeed = 0.07F;
            this.gunaAdvenceButton1.AnimationSpeed = 0.03F;
            this.gunaAdvenceButton1.BaseColor = System.Drawing.Color.DarkCyan;
            this.gunaAdvenceButton1.BorderColor = System.Drawing.Color.Transparent;
            this.gunaAdvenceButton1.CheckedBaseColor = System.Drawing.Color.Gray;
            this.gunaAdvenceButton1.CheckedBorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton1.CheckedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.gunaAdvenceButton1.CheckedImage = null;
            this.gunaAdvenceButton1.CheckedLineColor = System.Drawing.Color.Transparent;
            this.gunaAdvenceButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaAdvenceButton1.FocusedColor = System.Drawing.Color.Transparent;
            this.gunaAdvenceButton1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gunaAdvenceButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.gunaAdvenceButton1.Image = ((System.Drawing.Image)(resources.GetObject("gunaAdvenceButton1.Image")));
            this.gunaAdvenceButton1.ImageSize = new System.Drawing.Size(35, 35);
            this.gunaAdvenceButton1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.gunaAdvenceButton1.Location = new System.Drawing.Point(681, 85);
            this.gunaAdvenceButton1.Name = "gunaAdvenceButton1";
            this.gunaAdvenceButton1.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.gunaAdvenceButton1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton1.OnHoverImage = null;
            this.gunaAdvenceButton1.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
            this.gunaAdvenceButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton1.Size = new System.Drawing.Size(126, 36);
            this.gunaAdvenceButton1.TabIndex = 114;
            this.gunaAdvenceButton1.Text = "Print";
            this.gunaAdvenceButton1.Click += new System.EventHandler(this.gunaAdvenceButton1_Click);
            // 
            // cmbDonem
            // 
            this.cmbDonem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDonem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDonem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.cmbDonem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbDonem.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbDonem.ForeColor = System.Drawing.SystemColors.Window;
            this.cmbDonem.FormattingEnabled = true;
            this.cmbDonem.Location = new System.Drawing.Point(24, 36);
            this.cmbDonem.Name = "cmbDonem";
            this.cmbDonem.Size = new System.Drawing.Size(182, 29);
            this.cmbDonem.TabIndex = 113;
            this.cmbDonem.SelectionChangeCommitted += new System.EventHandler(this.cmbDonem_SelectionChangeCommitted);
            // 
            // cmbKategori
            // 
            this.cmbKategori.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbKategori.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbKategori.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.cmbKategori.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbKategori.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbKategori.ForeColor = System.Drawing.SystemColors.Window;
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(243, 36);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(182, 29);
            this.cmbKategori.TabIndex = 112;
            this.cmbKategori.SelectionChangeCommitted += new System.EventHandler(this.cmbKategori_SelectionChangeCommitted);
            // 
            // cmbKoy
            // 
            this.cmbKoy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbKoy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbKoy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.cmbKoy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbKoy.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbKoy.ForeColor = System.Drawing.SystemColors.Window;
            this.cmbKoy.FormattingEnabled = true;
            this.cmbKoy.Location = new System.Drawing.Point(681, 36);
            this.cmbKoy.Name = "cmbKoy";
            this.cmbKoy.Size = new System.Drawing.Size(182, 29);
            this.cmbKoy.TabIndex = 111;
            this.cmbKoy.SelectionChangeCommitted += new System.EventHandler(this.cmbKoy_SelectionChangeCommitted);
            // 
            // cmbBolge
            // 
            this.cmbBolge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBolge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBolge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.cmbBolge.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbBolge.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbBolge.ForeColor = System.Drawing.SystemColors.Window;
            this.cmbBolge.FormattingEnabled = true;
            this.cmbBolge.Location = new System.Drawing.Point(462, 36);
            this.cmbBolge.Name = "cmbBolge";
            this.cmbBolge.Size = new System.Drawing.Size(182, 29);
            this.cmbBolge.TabIndex = 110;
            this.cmbBolge.SelectionChangeCommitted += new System.EventHandler(this.cmbBolge_SelectionChangeCommitted_1);
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(20, 12);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(67, 21);
            this.bunifuCustomLabel2.TabIndex = 91;
            this.bunifuCustomLabel2.Text = "Dönem";
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dataTable1BindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "FethlerV2.paraaa.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(24, 161);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(839, 384);
            this.reportViewer1.TabIndex = 89;
            // 
            // dataTable1TableAdapter
            // 
            this.dataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // formparaTutanak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(1358, 797);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formparaTutanak";
            this.Text = "formparaTutanak";
            this.Load += new System.EventHandler(this.formparaTutanak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblErzaklarBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel14;
        private Guna.UI.WinForms.GunaAdvenceButton btnKaydet;
        private System.Windows.Forms.Label lblBolgeNo;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private DataSet1TableAdapters.DataTable1TableAdapter dataTable1TableAdapter;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private System.Windows.Forms.ComboBox cmbKoy;
        private System.Windows.Forms.ComboBox cmbBolge;
        private System.Windows.Forms.ComboBox cmbDonem;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.BindingSource tblErzaklarBindingSource;
        private Guna.UI.WinForms.GunaAdvenceButton gunaAdvenceButton1;
    }
}