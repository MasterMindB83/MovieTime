using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace MovieTime
{
    public partial class MediaEdit : Form
    {
        public int iMediaId;
        public String ConnectionString;
        public MediaEdit(String ConnectionString1,int iMediaId1)
        {
            InitializeComponent();
            ConnectionString = ConnectionString1;
            iMediaId = iMediaId1;
        }

        private void MediaEdit_Load(object sender, EventArgs e)
        {
            
        }


        public DataTable execQuery(String sQuery)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sQuery, conn))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            dataAdapter.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }

            return dt;
        }

        public void execNonQuery(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
