namespace MovieTime
{
    partial class FilePreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilePreview));
            this.mt_fileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txt_season = new System.Windows.Forms.ComboBox();
            this.lbl_season = new System.Windows.Forms.Label();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.mt_fileBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.btn_refresh = new System.Windows.Forms.ToolStripButton();
            this.navigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.mediasDataSet = new MovieTime.MediasDataSet();
            this.mt_fileBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.mt_fileTableAdapter = new MovieTime.MediasDataSetTableAdapters.mt_fileTableAdapter();
            this.tableAdapterManager = new MovieTime.MediasDataSetTableAdapters.TableAdapterManager();
            this.grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.mt_fileBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigator)).BeginInit();
            this.navigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediasDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_fileBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // mt_fileBindingSource
            // 
            this.mt_fileBindingSource.DataMember = "mt_file";
            // 
            // txt_season
            // 
            this.txt_season.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_season.FormattingEnabled = true;
            this.txt_season.Location = new System.Drawing.Point(105, 45);
            this.txt_season.Name = "txt_season";
            this.txt_season.Size = new System.Drawing.Size(239, 24);
            this.txt_season.TabIndex = 2;
            this.txt_season.TextChanged += new System.EventHandler(this.txt_season_TextChanged);
            // 
            // lbl_season
            // 
            this.lbl_season.AutoSize = true;
            this.lbl_season.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_season.Location = new System.Drawing.Point(32, 48);
            this.lbl_season.Name = "lbl_season";
            this.lbl_season.Size = new System.Drawing.Size(55, 16);
            this.lbl_season.TabIndex = 3;
            this.lbl_season.Text = "Season";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // mt_fileBindingNavigatorSaveItem
            // 
            this.mt_fileBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mt_fileBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("mt_fileBindingNavigatorSaveItem.Image")));
            this.mt_fileBindingNavigatorSaveItem.Name = "mt_fileBindingNavigatorSaveItem";
            this.mt_fileBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.mt_fileBindingNavigatorSaveItem.Text = "Save Data";
            this.mt_fileBindingNavigatorSaveItem.Click += new System.EventHandler(this.mt_fileBindingNavigatorSaveItem_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_refresh.Image")));
            this.btn_refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(23, 22);
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // navigator
            // 
            this.navigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.navigator.CountItem = this.bindingNavigatorCountItem;
            this.navigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.navigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.mt_fileBindingNavigatorSaveItem,
            this.btn_refresh});
            this.navigator.Location = new System.Drawing.Point(0, 0);
            this.navigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.navigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.navigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.navigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.navigator.Name = "navigator";
            this.navigator.PositionItem = this.bindingNavigatorPositionItem;
            this.navigator.Size = new System.Drawing.Size(674, 25);
            this.navigator.TabIndex = 0;
            this.navigator.Text = "bindingNavigator1";
            // 
            // mediasDataSet
            // 
            this.mediasDataSet.DataSetName = "MediasDataSet";
            this.mediasDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mt_fileBindingSource1
            // 
            this.mt_fileBindingSource1.DataMember = "mt_file";
            this.mt_fileBindingSource1.DataSource = this.mediasDataSet;
            // 
            // mt_fileTableAdapter
            // 
            this.mt_fileTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.mt_driveTableAdapter = null;
            this.tableAdapterManager.mt_fileTableAdapter = this.mt_fileTableAdapter;
            this.tableAdapterManager.mt_folderTableAdapter = null;
            this.tableAdapterManager.mt_mediaTableAdapter = null;
            this.tableAdapterManager.mt_ratingTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = MovieTime.MediasDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(12, 88);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(650, 342);
            this.grid.TabIndex = 4;
            this.grid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mt_fileDataGridView_MouseDoubleClick_1);
            // 
            // FilePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 457);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.lbl_season);
            this.Controls.Add(this.txt_season);
            this.Controls.Add(this.navigator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilePreview";
            this.Text = "FilePreview";
            this.Load += new System.EventHandler(this.FilePreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mt_fileBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigator)).EndInit();
            this.navigator.ResumeLayout(false);
            this.navigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediasDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_fileBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private MediasDataSet1 mediasDataSet1;
        private System.Windows.Forms.BindingSource mt_fileBindingSource;
        //private MediasDataSet1TableAdapters.mt_fileTableAdapter mt_fileTableAdapter;
        //private MediasDataSet1TableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ComboBox txt_season;
        private System.Windows.Forms.Label lbl_season;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        //private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton mt_fileBindingNavigatorSaveItem;
        private System.Windows.Forms.ToolStripButton btn_refresh;
        private System.Windows.Forms.BindingNavigator navigator;
        private MediasDataSet mediasDataSet;
        private System.Windows.Forms.BindingSource mt_fileBindingSource1;
        private MediasDataSetTableAdapters.mt_fileTableAdapter mt_fileTableAdapter;
        private MediasDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView grid;
    }
}