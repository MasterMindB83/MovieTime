using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace MovieTime
{
    public partial class Info : Form
    {
        String[] htmls=null;
        int current;
        String title;
        String yearUrl;
        String year;
        String summery="",storyline="";
        Boolean bSummery = true;
        String directory="";
        String pg="";
        String time = "";

        String imagesUrl = "";
        String videosUrl = "";
        String imagesCount="";
        String videosCount="";

        Color blue;
        List<Matrix> genres=new List<Matrix>();
        Form mainForm;
        public Info(Form mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Info_Load(object sender, EventArgs e)
        {
            //blue = Color.DeepSkyBlue;
            blue = Color.FromArgb(12,111,220);
        }

        //procedure

        public void loadMovie(String[] htmls,String directory)
        {
            this.htmls = htmls;
            this.directory = directory;
            current = 0;
            loadHtml();
        }

        public String Substring(String text, int pos1, int pos2)
        {
            String result = "";
            try
            {
                result = text.Substring(pos1, pos2);
            }
            catch (Exception ex)
            {
            }


            return result;
        }
        public void loadHtml()
        {
            ResetInfo();
            if (htmls.Length==0)
                return;
            try
            {
                genres.Clear();
                String html = System.IO.File.ReadAllText(htmls[current]);

                //title
                int pos1 = html.IndexOf("<div class=\"title_wrapper\">\n<h1 itemprop=\"name\" class=\"\">");
                int pos2=html.IndexOf("&nbsp;",pos1);
                title = Substring(html,pos1+57, pos2-pos1-57);
                //txt_title.Text = title;

                //year
                pos1 = html.IndexOf("titleYear\">");
                String pom=html.Substring(pos1+12);
                pos1 = pom.IndexOf(">");
                pos2 = pom.IndexOf("</a>",pos1);
                year = Substring(pom,pos1+1, pos2 - pos1-1);
                //txt_year.Text = "("+year+")";

                pos1 = html.IndexOf("http://www.imdb.com/year");
                pos2 = html.IndexOf("\"", pos1 + 24);
                yearUrl = Substring(html,pos1, pos2 - pos1);

                txt_title.Text = title+" ("+year+")";

                //ratings
                pos1 = html.IndexOf("<span class=\"rating\">");
                pos2 = html.IndexOf("<",pos1+1);
                String ratings = Substring(html,pos1 + 21, pos2 - pos1 - 21);
                txt_ratings.Text = ratings;

                pos1 = html.IndexOf("\"star-rating-value\"");
                pom = html.Substring(pos1 + 19);

                //user ratings
                pos1 = pom.IndexOf(">");
                pos2 = pom.IndexOf("<",pos1);
                String userRatings = Substring(pom,pos1 + 1, pos2 - pos1 - 1);
                if (userRatings != "0")
                {
                    txt_you.Text = userRatings;
                    lbl_you.Text = "You";
                }

                //poster
                pos1 = html.IndexOf("<div class=\"poster\">");
                pos1 = html.IndexOf("src=\"", pos1);
                pom = html.Substring(pos1+5);
                pos2 = pom.IndexOf("\"");
                String poster = Substring(pom,1,pos2-1);
                String folderPath = htmls[current].Substring(0,htmls[current].LastIndexOf("\\"));
                poster = folderPath + poster;
                try
                {
                    img_profile.Image = Image.FromFile(poster);
                }
                catch (Exception ex)
                {
                    img_profile.Image = null;
                }

                //trailer
                pos1 = html.IndexOf("title=\"Trailer\" src=\"");
                pos1+=21;
                pos2=html.IndexOf("\"",pos1);
                String video = Substring(html,pos1, pos2 - pos1);
                video = folderPath + video;
                try
                {
                    img_video.Image = Image.FromFile(video);
                }
                catch (Exception ex)
                {
                    img_video.Image = null;
                }

                //summary
                pos1 = html.IndexOf("<div class=\"summary_text\" itemprop=\"description\">\n                    ");
                pos2 = html.IndexOf("\n",pos1+75);
                summery = Substring(html,pos1+70, pos2 - pos1-70);
                txt_description.Text = summery;

                pos1 = html.IndexOf("contentRating\" content=\"");
                pos2 = html.IndexOf("\"",pos1+24);
                pg = Substring(html,pos1 + 24, pos2 - pos1 - 24);

                //time
                pos1 = html.IndexOf("duration\" datetime");
                pom = html.Substring(pos1 + 17);
                pos1 = pom.IndexOf(">\n                        ");
                pos2 = pom.IndexOf("\n", pos1 + 10);
                time = Substring(pom,pos1+26, pos2 - pos1-26);

                //genre
                String pomGenre = "";
                pos1 = html.IndexOf("itemprop=\"genre\">");
                pom = html;
                int pos3;// = pom2.IndexOf("a href=\"http://www.imdb.com/genre");
                while (pos1 > -1)
                {
                    pom = pom.Substring(pos1 + 17);
                    pos2 = pom.IndexOf("</span>");
                    pomGenre = Substring(pom,0, pos2);

                    pos3 = html.IndexOf("a href=\"http://www.imdb.com/genre/"+pomGenre);
                    pos2 = html.IndexOf("\"",pos3+20);
                    String genreUrl = Substring(html,pos3+8, pos2 - pos3-8);


                    Matrix genre1 = new Matrix(pomGenre, genreUrl);
                    genres.Add(genre1);


                    pos1 = pom.IndexOf("itemprop=\"genre\">");
                }

                List<Matrix> deleteList=new List<Matrix>();
                foreach(Matrix genreMatrix in genres){
                    if(genreMatrix.genre.Length>50)
                        deleteList.Add(genreMatrix);

                }
                foreach(Matrix genreMatrix in deleteList){
                    genres.Remove(genreMatrix);

                }
                pomGenre="";
                //<a href=\"http://www.sears.com/fitness-sports-treadmills/c-1020252\">here</a>
                foreach (Matrix genreMatrix in genres)
                {
                    if (pomGenre == "")
                    {
                        pomGenre = genreMatrix.genre;

                    }
                    else
                        pomGenre += "," + genreMatrix.genre;
                }
                
                createGenre(10000);

                //storyline
                pos1 = html.IndexOf("itemprop=\"description\">\n            <p>\n");
                pos2 = html.IndexOf("<em class=\"nobr\">",pos1+40);
                storyline = Substring(html,pos1 + 40, pos2-pos1-40);

                
                //views
                pos1 = html.IndexOf("ratingCount\">");
                pos2 = html.IndexOf("</span>", pos1 + 13);
                String ratingsCount = Substring(html,pos1 + 13, pos2 - pos1 - 13);
                txt_views.Text = ratingsCount;

                //trailer time
                pos1 = html.IndexOf("caption\">");
                pos1 = html.IndexOf("<div style=\"float: left;\">",pos1+9);
                pom = html.Substring(pos1+26);
                pos2 = pom.IndexOf(" <span class=\"ghost\">");
                String trailerTime = Substring(pom,0, pos2);
                if(trailerTime.Length<9)
                    txt_trailer.Text = trailerTime + " | Trailer";

                //images and videos
                pos1 = pom.IndexOf("<a href=\"");
                pos2 = pom.IndexOf("\">", pos1 + 9);
                videosUrl = Substring(pom,pos1 + 9, pos2 - pos1 - 9);
                pom = pom.Substring(pos2+2);
                pos2 = pom.IndexOf("<");
                videosCount = Substring(pom,0, pos2);
                if(videosCount.IndexOf("VIDEO")==-1)
                    videosCount = "";


                pos1 = pom.IndexOf("href=\"");
                pos2 = pom.IndexOf("\"", pos1 + 6);
                imagesUrl = Substring(pom,pos1 + 6, pos2 - pos1 - 6);
                pom = pom.Substring(pos2 + 1);
                pos1 = pom.IndexOf(">");
                pos2 = pom.IndexOf("<", pos1 + 1);
                imagesCount = Substring(pom,pos1 + 1, pos2 - pos1 - 1);
                if(imagesCount.IndexOf("IMAGE")==-1)
                  imagesCount = "";
                if(videosCount != "" || imagesCount !="")
                    txt_images.Text = videosCount + " | " + imagesCount;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greska");
            }                  
        }
        public void ResetInfo()
        {
            txt_title.Text = "No info";
            //txt_year.Text = "";
            txt_genre.Text = "";
            txt_ratings.Text = "";
            txt_you.Text = "";
            txt_description.Text = "";
            txt_views.Text = "";
            txt_trailer.Text = "";
            txt_images.Text = "";
            txt_you.Text = "";
            lbl_you.Text = "";

            title="";
            yearUrl="";
            year="";
            summery = "";
            storyline = "";
            bSummery = true;
            pg = "";
            time = "";

            imagesUrl = "";
            videosUrl = "";
            imagesCount = "";
            videosCount = "";

            img_profile.Image = null;
            img_video.Image = null;
        }

        public static void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        private void txt_summary_storyline_Click(object sender, EventArgs e)
        {
            bSummery = !bSummery;
            if (bSummery)
            {
                txt_summary.Text = "Summary";
                txt_description.Text = summery;
                btn_summary_storyline.Text="Summary";
            }
            else
            {
                txt_summary.Text = "Storyline";
                txt_description.Text = storyline;
                btn_summary_storyline.Text = "Storyline";
            }
        }
        public String getText(int position,String text)
        {
            String sReturn = "";
            try
            {
                String pom = text.Substring(0, position);

                int pos1 = position-1;
                while (pos1 >= 0)
                {
                    String c=pom.Substring(pos1, 1);
                    if (c == " " || c == ",")
                        break;
                    pos1--;
                }
                pom = text.Substring(position);

                int pos2 = 0;
                
                while (pos2 < pom.Length)
                {
                    String c=pom.Substring(pos2, 1);
                    if ( c == " " || c == ",")
                        break;
                    pos2++;
                }
                if (pos2 == -1)
                    pos2 = text.Length - 1;
                else
                    pos2=pos2 + position;
                if (pos2 > pos1 + 1)
                    sReturn = text.Substring(pos1 + 1, pos2 - pos1 - 1);
            }
            catch(Exception ex)
            {
            }
            return sReturn;

        }
        public String getText2(int position, String text)
        {
            String sReturn = "";
            try
            {
                String pom = text.Substring(0, position);

                int pos1 = position - 1;
                while (pos1 >= 0)
                {
                    String c = pom.Substring(pos1, 1);
                    if (c == "|")
                        break;
                    pos1--;
                }
                pom = text.Substring(position);

                int pos2 = 0;

                while (pos2 < pom.Length)
                {
                    String c = pom.Substring(pos2, 1);
                    if (c == "|")
                        break;
                    pos2++;
                }
                if (pos2 == -1)
                    pos2 = text.Length - 1;
                else
                    pos2 = pos2 + position;
                if (pos2 > pos1 + 1)
                    sReturn = text.Substring(pos1 + 1, pos2 - pos1 - 1);

                if (sReturn != "")
                {
                    String c = sReturn.Substring(0, 1);
                    pos1 = 0;
                    while (c == " ")
                    {
                        pos1++;
                        c = sReturn.Substring(pos1, 1);
                    }
                    sReturn = sReturn.Substring(pos1);

                    pos1 = sReturn.Length-1;
                    c = sReturn.Substring(pos1, 1);
                    while (c == " ")
                    {
                        pos1--;
                        c = sReturn.Substring(pos1, 1);
                    }
                    sReturn = sReturn.Substring(0,pos1+1);
                }
            }
            catch (Exception ex)
            {
            }
            return sReturn;

        }
        public void createGenre(int position)
        {
            
            String word = getText(position, txt_genre.Text);

            if (pg.Length < 10)
                txt_genre.Text = pg + " | ";
            if (time.Length < 10)
                txt_genre.AppendText(time + " | ");
            else
                txt_genre.Text = "";

            Boolean bFirst=true;
            foreach (Matrix genreMatrix in genres)
            {
                if (word == genreMatrix.genre)
                {
                    if (bFirst)
                    {
                        AppendText(txt_genre, genreMatrix.genre, blue);
                        bFirst = false;
                    }
                    else
                    {
                        txt_genre.AppendText(",");
                        AppendText(txt_genre, genreMatrix.genre, blue);
                    }
                }
                else
                {
                    if (bFirst)
                    {
                        txt_genre.AppendText(genreMatrix.genre);
                        bFirst = false;
                    }
                    else
                    {
                        txt_genre.AppendText("," + genreMatrix.genre);
                    }
                }
            }

            txt_genre.AppendText(" | ");
        }
        public void createTitle(int position)
        {
            String word = getText(position, txt_title.Text);
            txt_title.Text = title+" (";
            if (word == "(" + year + ")")
                AppendText(txt_title, year, blue);
            else
                txt_title.AppendText(year);

            txt_title.AppendText(")");
        }
        //eventi

        private void btn_open_folder_Click(object sender, EventArgs e)
        {

            if (directory!="")
                Process.Start(@directory);
        }
        private void btn_open_browser_Click(object sender, EventArgs e)
        {
            if (htmls.Length > 0)
                Process.Start(@htmls[current]);
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (current > 0)
                current--;
            loadHtml();
        }

        private void txt_year_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start(yearUrl);
        }



        private void txt_genre_MouseClick_1(object sender, MouseEventArgs e)
        {

            int x = e.X;
            int y = e.Y;

            int position = txt_genre.SelectionStart;

            String word = getText(position, txt_genre.Text);
            String genreUrl="";
            foreach (Matrix genreMatrix in genres)
            {
                if (word == genreMatrix.genre)
                {
                    genreUrl = genreMatrix.url;
                    break;
                }
            }

            if (genreUrl != "")
                Process.Start(genreUrl);
        }

        private void txt_genre_MouseMove(object sender, MouseEventArgs e)
        {
            int position = txt_genre.GetCharIndexFromPosition(new Point(e.X, e.Y));
            createGenre(position);
        }
        private void btn_next_Click(object sender, EventArgs e)
        {
            if (current < htmls.Length - 1)
                current++;
            loadHtml();

        }

        private void txt_genre_MouseLeave(object sender, EventArgs e)
        {

            createGenre(10000);
        }



        

        private void txt_title_MouseMove(object sender, MouseEventArgs e)
        {
            int position = txt_title.GetCharIndexFromPosition(new Point(e.X, e.Y));
            createTitle(position);
        }

        private void txt_title_MouseLeave(object sender, EventArgs e)
        {
            createTitle(10000);
        }

        private void txt_title_MouseClick(object sender, MouseEventArgs e)
        {

            int x = e.X;
            int y = e.Y;

            //int position = txt_genre.SelectionStart;
            int position = txt_title.GetCharIndexFromPosition(new Point(e.X, e.Y));

            String word = getText(position, txt_title.Text);
            if (word == "(" + year + ")")
            {
                if (yearUrl != "")
                    Process.Start(@yearUrl);
            }

        }

        private void txt_images_MouseMove(object sender, MouseEventArgs e)
        {
            int position = txt_images.GetCharIndexFromPosition(new Point(e.X, e.Y));
            String word = getText2(position, txt_images.Text);

            txt_images.Text="";
            if (videosCount == word)
                AppendText(txt_images, videosCount, blue);
            else
                txt_images.Text = videosCount;

            txt_images.AppendText(" | ");

            if (imagesCount == word)
                AppendText(txt_images, imagesCount, blue);
            else
                txt_images.AppendText(imagesCount);
        }

        private void txt_images_MouseClick(object sender, MouseEventArgs e)
        {

            int position = txt_images.GetCharIndexFromPosition(new Point(e.X, e.Y));
            String word = getText2(position, txt_images.Text);

            if (word == videosCount && videosUrl != "")
                Process.Start(@videosUrl);
            if (word == imagesCount && imagesUrl != "")
                Process.Start(imagesUrl);
        }

        private void txt_images_MouseLeave(object sender, EventArgs e)
        {
            txt_images.Text="";
            txt_images.Text = videosCount + " | " + imagesCount;
        }



        

    }
    class Matrix
    {
        public String genre;
        public String url;
        public Matrix(String genre,String url)
        {
            this.genre = genre;
            this.url = url;
        }
    }
}
