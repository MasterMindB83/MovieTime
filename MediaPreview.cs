using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;

namespace MovieTime
{
    public partial class MediaPreview : Form
    {
        public MediaPreview()
        {
            InitializeComponent();
        }
        
        
        public String[] sVideoExtensions = new String[] { "avi", "mp4", "flv", "mkv", "xvid", "mov", "wmv" };
        public String[] sSeasons = new String[] { "sezona", "season", };

        Info info;
        Boolean bMoveInfo = false;

        String url;


        private void MediaPreview_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mediasDataSet.mt_media' table. You can move, or remove it, as needed.
            DataBase.init();
            info = new Info(this);
            info.Show();
            ResizeGrid();
            RefreshData();
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.Columns["id"].Visible = false;
            grid.Columns["folder"].Visible = false;
            grid.Columns["name"].Width = 150;
            grid.Columns["year"].Width = 80;
            txt_media_type.Text = "Movies";

            bMoveInfo = true;
            moveInfo();
            loadInfo();


        }
        public void moveInfo()
        {
            if (!bMoveInfo)
                return;
            info.Top = Top;
            info.Left = Right + 10;

        }
        public String formatUrl(String name)
        {
            return name.Replace(" ", "+");
        }
        public void loadInfo(){
             if (this.grid.CurrentCell == null)
                return;
            int currRow = grid.CurrentCell.RowIndex;

            String sMediaName = grid.Rows[currRow].Cells["name"].Value.ToString();
            String sId = grid.Rows[currRow].Cells["id"].Value.ToString();
            url="http://www.google.com/search?q=imdb+\""+formatUrl(sMediaName)+"\"&btnI";

            String sQuery = "select f.path + '\\' + m.path,d.name  from mt_media m,mt_folder f,mt_drive d where m.folder=f.id and f.drive = d.id and m.id="+sId;
            DataTable dt = DataBase.executeQuery(sQuery);
            String mediaPath="";
            String sDrive="";
            try
            {
                mediaPath = dt.Rows[0].ItemArray[0].ToString();
                sDrive = getDrivePath(dt.Rows[0].ItemArray[1].ToString());
            }
            catch (Exception ex)
            {
            }
            String targetDirectory = sDrive+mediaPath;
            String[] fileEntries = null; ;
            if(targetDirectory!="")
             fileEntries= Directory.GetFiles(targetDirectory,"*.htm*");

            info.loadMovie(fileEntries, targetDirectory);
        }
        public void RefreshData()
        {
            String sQuery = "select id,folder,name,year,genre from mt_media";
            String sWhere = "";

            sWhere = " where type='" + txt_media_type.Text + "'";

            if (txt_genre.Text != "")
                sWhere += " and upper(genre)='" + txt_genre.Text + "'";

            if (txt_naziv.Text != "")
                sWhere += " and upper(name) like upper('%" + txt_naziv.Text + "%')";

            if (txt_year.Text != "")
                sWhere += " and year like '%" + txt_year.Text + "%'";


            DataTable dt = DataBase.executeQuery(sQuery + sWhere);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dt;
            navigator.BindingSource = bindingSource;
            grid.DataSource = dt;
            loadInfo();
        }
        public String getDriveName(String sDriveLabel)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                try
                {
                    if (sDriveLabel == drive.VolumeLabel)
                        return drive.Name.Substring(0, 1);
                }
                catch { }
            }
            return null;
              
        }

        public String getDriveLabel(String sDriveName)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                try
                {
                    if (sDriveName.ToUpper() == drive.Name.Substring(0, 1).ToUpper())
                        return drive.VolumeLabel;
                }
                catch { }
            }
            return null;
        }
        public int getMaxId(String sTable )
        {
            string Query = "select isnull(max(id),0) from " + sTable;

            SqlConnection con = new SqlConnection(DataBase.ConnectionString);
            con.Open();
            SqlCommand comm = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            try
            {
                da.Fill(dt);
                con.Close();
                con.Dispose();
                return int.Parse(dt.Rows[0].ItemArray[0].ToString());
            }
            catch (Exception ex)
            {
                con.Close();
                con.Dispose();
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        
        public void ProcessDirectory(string targetDirectory, string startDirectory, int iFolderId, int iMediaType, int iStructure)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);


            Boolean bHasVideoFiles = false;

            foreach (String file in fileEntries)
                if (isVideoFile(file))
                    bHasVideoFiles = true;
            int iMediaId = 0;
            if (bHasVideoFiles)
                iMediaId = updateMedia(targetDirectory, startDirectory, iFolderId, iMediaType, iStructure);

            foreach (string fileName in fileEntries)
                if (isVideoFile(fileName))
                    UpdateFile(fileName, iMediaId, iMediaType);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, startDirectory, iFolderId, iMediaType, iStructure);
        }

        public Boolean isVideoFile(String sFile)
        {
            String sFileExtension = sFile.Substring(sFile.LastIndexOf(".") + 1);
            return inSearch(sFileExtension, sVideoExtensions);
        }
        
        

        public String getPreviousFolder(String sPath, int iOrder)
        {
            String sName = sPath;
            for (int i = 0; i < iOrder; i++)
            {
                try
                {
                    sName = sName.Substring(0, sName.LastIndexOf("\\"));
                }
                catch
                {
                    sName = "";
                }
            }
            try
            {
                sName = sName.Substring(sName.LastIndexOf("\\") + 1);
            }
            catch
            {
                sName = "";
            }

            if (sName.IndexOf(":") >= 0)
                sName = "";

            return sName;
        }
        
        public String duplicateApostrof(String sText)
        {
            return sText.Replace("'","''");
        }
        
        public int updateDrive(String sDriveVolume)// dodaje novi drive u bazu, ako treba i vraca id driva 
        {
            string Query = "select * from mt_drive where name = '" +duplicateApostrof( sDriveVolume) + "'";

            SqlConnection con = new SqlConnection(DataBase.ConnectionString);
            con.Open();
            SqlCommand comm = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);
            //DataView dv = dt.DefaultView;
            

            foreach (DataRow dr in dt.Rows)
            {
                con.Close();
                con.Dispose();
                    return int.Parse(dr.ItemArray[0].ToString());
            }

            int maxId = getMaxId("mt_drive")+1;

            Query = "insert into mt_drive (id,name) values (" + maxId + ",'" + duplicateApostrof(sDriveVolume) + "')";
            SqlCommand querySaveStaff = new SqlCommand(Query,con);

            try
            {
                querySaveStaff.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                return maxId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                return -1;
            }
        }
        public int updateFolder(String sDriveName, String sPath)
        {
            int DriveId = updateDrive(getDriveLabel(sDriveName));

            string Query = "select id from mt_folder where path = '" + duplicateApostrof(sPath) + "' and drive = " + DriveId;

            SqlConnection con = new SqlConnection(DataBase.ConnectionString);
            con.Open();
            SqlCommand comm = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);
            //DataView dv = dt.DefaultView;


            foreach (DataRow dr in dt.Rows)
            {
                con.Close();
                con.Dispose();
                return int.Parse(dr.ItemArray[0].ToString());
            }

            int maxId = getMaxId("mt_folder") + 1;

            Query = "insert into mt_folder (id,drive,path) values (" + maxId + "," + DriveId + ",'" + duplicateApostrof(sPath) + "')";
            SqlCommand querySaveStaff = new SqlCommand(Query, con);

            try
            {
                querySaveStaff.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                return maxId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                con.Dispose();
                return -1;
            }
        }
        public int updateMedia(string sDirectory, string sStartDirectory, int iFolderId, int iMediaType, int iStructure)
        {
            int iMediaId = 0;

            String sPath;
            try
            {
                sPath = sDirectory.Substring(sDirectory.IndexOf(sStartDirectory) + sStartDirectory.Length + 1); ;
            }
            catch
            {
                MessageBox.Show("In starting directory exists movies witch is not in subfolders.");
                return -1;
            }
                String sFOlderName = sPath;
            if (sPath.LastIndexOf("\\") >= 0)
                sFOlderName = sPath.Substring(sPath.LastIndexOf("\\")+1);

            if (iMediaType == 2 && hasSeasones(sFOlderName))//serija koja ima sezone
            {
                try
                {
                    sPath = sPath.Substring(0, sPath.LastIndexOf("\\"));
                }
                catch { }
            }

            //select media if exists end return MediaId
            foreach (DataRow dr in DataBase.executeQuery("select id from mt_media where path= '" + duplicateApostrof(sPath) + "' and folder = " + iFolderId).Rows)
                return int.Parse(dr.ItemArray[0].ToString());

            String sNaziv = mediaName(sPath);
            String sYear = getYear(sPath, iMediaId, iStructure);
            String sGenre = getGenre(sPath, iMediaType, iStructure);
            String sType = "Movies";
            if (iMediaType == 2)
                sType = "Series";

            //insert media
            iMediaId=getMaxId("mt_media")+1;
            String sQuery="insert into mt_media(id,folder,path,name,year,genre,ratings,fl_watched,type) values("+
                   iMediaId + "," +
                   iFolderId  + ",'" +
                   duplicateApostrof(sPath) + "','" +
                   duplicateApostrof(sNaziv) + "','" +
                   sYear + "','"+
                   sGenre+"', 0 ,'N','"+
                   sType + 
                   "')";

            DataBase.executeNonQuery(sQuery);

            return iMediaId;

        }
        public void UpdateFile(string sPath, int iMedia, int iType)
        {
            String sMediaPath = getMediaPath(iMedia);

            String sFilePath = sPath.Substring(sMediaPath.Length+1);

            //return if file is alredy in database	
            foreach (DataRow row in DataBase.executeQuery("select id from mt_file where path='" + duplicateApostrof(sFilePath) + "' and media = " + iMedia).Rows)
                return;
            String sSeason="";
            if(iType==2 && hasSeasones(getPreviousFolder( sPath,1)))
                sSeason=getPreviousFolder(sPath,1);

            //insert file to database
            String sQuery = "insert into mt_file(id,media,path,fl_watched,season) values("+
                   (getMaxId("mt_file")+1) + ","+
                   iMedia + ",'"+
                   duplicateApostrof(sFilePath) + "','N','"+
                   sSeason +
                   "')";

            DataBase.executeNonQuery(sQuery);
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
        public String getMediaPath(int iMedia)
        {
            String sPath = "";
            String sDrive = "";
            String sQuery = "select f.path + '\\' + m.path,d.name  from mt_media m,mt_folder f,mt_drive d where m.folder=f.id and f.drive = d.id and m.id=";
            DataTable table = DataBase.executeQuery(sQuery + iMedia);
            
            foreach (DataRow row in table.Rows)
            {
                sPath = row.ItemArray[0].ToString();
                sDrive = getDrivePath(row.ItemArray[1].ToString());
            }
            sPath = sDrive + sPath;

            return sPath;
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

        public Boolean hasSeasones(String sName)//upper folder
        {
            Boolean bSeason = false;
            //String sName = sPath.Substring(sPath.LastIndexOf("\\") + 1).ToUpper();

            foreach (String sSeason in sSeasons)
                if (sName.ToUpper().IndexOf(sSeason.ToUpper()) >= 0)
                    bSeason = true;
            if (isNumber(sName))
                bSeason = true;

            return bSeason;
        }
        public String getYear(String spath, int iMediaType, int iStructure)
        {
            String sYear = spath;
            if (inSearch(spath.Substring(spath.Length - 1), new String[] { ")", "]", "}" }))
                sYear = sYear.Substring(0, sYear.Length - 1);

            sYear = sYear.Substring(sYear.Length - 4);
            if (isNumber(sYear)&& sYear.Length>4)
                return sYear;

            sYear = "";

            if (iStructure == 3 || iStructure == 4)
                sYear = getPreviousFolder(spath, 1);
            if (iStructure == 2)
                sYear = getPreviousFolder(spath, 2);
            
            return sYear;
            
        }
        public String getGenre(String spath, int iMediaType, int iStructure)
        {
            String sGenre = "";
            if (iStructure == 2 || iStructure == 5)
                sGenre = getPreviousFolder(spath, 1);
            if (iStructure == 3)
                sGenre = getPreviousFolder(spath, 2);
            return sGenre;
        }
        
        public Boolean inSearch(String sText, String[] sTemplate)
        {
            foreach (String s in sTemplate)
                if (s == sText)
                    return true;
            return false;
        }
        
        public String mediaName(String mediaPath)
        {
            String sNaziv = mediaPath.Substring(mediaPath.LastIndexOf("\\") + 1);
            if (inSearch(sNaziv.Substring(sNaziv.Length - 1), new String[] { ")", "]", "}" }))
                sNaziv = sNaziv.Substring(0, sNaziv.Length - 1);
            if (isNumber(sNaziv.Substring(sNaziv.Length - 4)))
            {
                if(sNaziv.Length>4)
                  sNaziv = sNaziv.Substring(0, sNaziv.Length - 4);
            }
            if (inSearch(sNaziv.Substring(sNaziv.Length - 1), new String[] { "(", "[", "{","." }))
                sNaziv = sNaziv.Substring(0, sNaziv.Length - 1);
            if (sNaziv.Substring(sNaziv.Length - 1) == " ")
                sNaziv = sNaziv.Substring(0, sNaziv.Length - 1);

            return sNaziv;
        }
        
        public Boolean isNumber(String sText)
        {
            Boolean bNumber = false;

            for (int i = 0; i < sText.Length; i++)
            {
                if (inSearch(sText.Substring(i, 1), new String[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }))
                    bNumber = true;
                else
                    return(false);
            }

            return bNumber;
        }

        
        public void clearAll()
        {
            DialogResult dialog = MessageBox.Show("Do you realy want to clear all data?", "Information", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                DataBase.executeNonQuery("delete from mt_file");
                DataBase.executeNonQuery("delete from mt_media");
                DataBase.executeNonQuery("delete from mt_folder");
                DataBase.executeNonQuery("delete from mt_drive");
                RefreshData();
            }
        
        }
        public void previewMedia()
        {
            if (this.grid .CurrentCell == null)
                return;
            int currRow = grid.CurrentCell.RowIndex;

            String sMediaId = grid.Rows[currRow].Cells[0].Value.ToString();
            String sFilePath = "";
            int fileNo = 1;
            foreach (DataRow row in DataBase.executeQuery("select count(1) from mt_file where media= " + sMediaId).Rows)
                fileNo = int.Parse(row.ItemArray[0].ToString());
            if (fileNo == 1)
            {
                foreach (DataRow row in DataBase.executeQuery("select id from mt_file where media= " + sMediaId).Rows)
                    sFilePath = getFilePath(int.Parse(row.ItemArray[0].ToString()));
                try
                {
                    Process.Start(@sFilePath);
                }
                catch
                {

                }
            }
            else if (fileNo > 1)
            {
                FilePreview file = new FilePreview(DataBase.ConnectionString, sMediaId);
                file.ShowDialog();
            }
        }

        public void ResizeGrid()
        {
            grid.Width = this.Width - 50;
            grid.Height = this.Height - 165;
            moveInfo();
        }
        public String LoadUrl(String urlAddress)
        {
            String html = "";
            String data = "";
            try{
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message,"Greska");
                }
                return data;
            }

        
        //eventi
        



        /*private void mt_mediaDataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            previewMedia();
        }*/

        private void btn_refresh2_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btn_add_folder_Click(object sender, EventArgs e)
        {
            Structure structure = new Structure();
            structure.ShowDialog();

            int iMediaType = structure.Type;
            int iStructure = structure.FileStructure;

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (fbd.SelectedPath == "")
                return;

            Cursor.Current = Cursors.WaitCursor;

            String sDriveName = fbd.SelectedPath.Substring(0, 1);
            String sFolderPath = fbd.SelectedPath.Substring(3);

            int iFolderId = updateFolder(sDriveName, sFolderPath);

            ProcessDirectory(fbd.SelectedPath, fbd.SelectedPath, iFolderId, iMediaType, iStructure);


            Cursor.Current = Cursors.Default;
            RefreshData();
        }

        private void btn_clear_all2_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void txt_genre_TextChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void MediaPreview_Resize(object sender, EventArgs e)
        {
            ResizeGrid();
        }

        private void mt_mediaDataGridView2_MouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            previewMedia();
        }

        private void MediaPreview_Move(object sender, EventArgs e)
        {
            moveInfo();
        }

        private void grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            loadInfo();
        }

        private void btn_open_browser_Click(object sender, EventArgs e)
        {

            Process.Start(@url);
        }



        

        

        
    }
}
