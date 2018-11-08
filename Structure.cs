using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MovieTime
{
    public partial class Structure : Form
    {
        public int Type { get; set; }
        public int FileStructure { get; set; }

        public Structure()
        {
            InitializeComponent();
        }
        public int getType()
        {
            if(txt_type.Text=="Movies")
              return 1;
            if(txt_type.Text=="Series")
              return 2;
            return -1;
        }
        private void btn_1_Click(object sender, EventArgs e)
        {
            Type = getType();
            FileStructure = 1;
            Close();
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            Type = getType();
            FileStructure = 2;
            Close();

        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            Type = getType();
            FileStructure = 3;
            Close();

        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            Type = getType();
            FileStructure = 4;
            Close();

        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            Type = getType();
            FileStructure = 5;
            Close();

        }

        private void Structure_Load(object sender, EventArgs e)
        {
            this.FileStructure = 1;
            this.txt_type.Text = "Movies";
        }
    }
}
