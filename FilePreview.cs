using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace MovieTime
{
    public partial class FilePreview : Form
    {
        int iMediaId;
        String ConnectionString;
        public FilePreview(String sConnectionString1,String sMediaId1)
        {
            InitializeComponent();
            iMediaId = int.Parse(sMediaId1);
            ConnectionString = sConnectionString1;
        }

        private void mt_fileBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.mt_fileBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mediasDataSet);

        }
        
        private void FilePreview_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mediasDataSet1.mt_file' table. You can move, or remove it, as needed.
            fillSeason();
            RefreshData();
            setColumns();

        }
        //procedure
        public void setColumns()
        {
            grid.Columns["path"].Width = 550;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.Columns["id"].Visible = false;
        }
        public void RefreshData()
        {

            String sQuery = "select id,path from mt_file";
            String sWhere = " where media=" + iMediaId + " and season ='" + txt_season.Text + "'  order by path ";


            DataTable dt = DataBase.executeQuery(sQuery+sWhere);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dt;
            navigator.BindingSource = bindingSource;
            grid.DataSource = dt;
        }
        

        public String getDrivePath(String sDriveLabel)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                try
                {
                    if (drive.VolumeLabel == sDriveLabel)
                        return drive.Name;
                }
                catch { }
            }
            MessageBox.Show("Drive: " + sDriveLabel + " is not connected.");
            return "";
        }
        public String getFilePath(int iFile)
        {
            String sPath = "";
            String sDrive = "";

            DataTable table = DataBase.executeQuery("select f.path + '\\' + m.path + '\\' + fi.path,d.name from mt_file fi,mt_media m,mt_folder f,mt_drive d where fi.media=m.id and m.folder = f.id and f.drive=d.id and fi.id=" + iFile);
            foreach (DataRow row in table.Rows)
            {
                sPath = row.ItemArray[0].ToString();
                sDrive = getDrivePath(row.ItemArray[1].ToString());
            }
            if (sDrive != "")
                sPath = sDrive + sPath;
            else
                sPath = "";

            return sPath;
        }

        void fillSeason()
        {
            DataTable table = DataBase.executeQuery("select distinct(season) season from mt_file where media="+iMediaId+" order by season");
            txt_season.DataSource = table;
            txt_season.ValueMember = "season";
            txt_season.DisplayMember = "season";

        }

        //eventi
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        
       /*private void mt_fileDataGridView_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            int currRow = grid.CurrentCell.RowIndex;
            String sFileId = grid.Rows[currRow].Cells[0].Value.ToString();
            String sFilePath = getFilePath(int.Parse(sFileId));
            try
            {
                Process.Start(@sFilePath);
            }
            catch
            {

            }
        }*/

        private void txt_season_TextChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void mt_fileDataGridView_MouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            int currRow = grid.CurrentCell.RowIndex;
            String sFileId = grid.Rows[currRow].Cells[0].Value.ToString();
            String sFilePath = getFilePath(int.Parse(sFileId));
            try
            {
                Process.Start(@sFilePath);
            }
            catch
            {

            }

        }

    }
}
