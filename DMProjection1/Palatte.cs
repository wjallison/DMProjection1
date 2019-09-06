using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMProjection1
{
    public partial class Palatte : Form
    {
        FolderBrowserDialog fileLoc;
        //System.Windows.Forms.OpenFileDialog fileLoc;

        int selected = 0;

        public delegate void SelectUpdateDel(Image im);
        public event SelectUpdateDel SelectUpdateEvent;

        public Palatte()
        {
            InitializeComponent();

            fileLoc = new FolderBrowserDialog();
            fileLoc.ShowDialog();

            string folderLoc = fileLoc.SelectedPath;
            //List<Image> imgLst = new List<Image>();
            List<String> files = new List<string>();
            files.AddRange(System.IO.Directory.GetFiles(folderLoc, String.Format("*.{0}", "png")));

            //for(int i = 0; i < files.Count; i++)
            //{
            //    imgLst.Add(Image.FromFile(files[i]));
            //}
            //System.IO.FileInfo[] files = System.IO.DirectoryInfo

            //Load list of tiles
            for(int i = 0; i < files.Count; i++)
            {
                PictureBox p = new PictureBox();
                p.Width = 150;
                p.Height = 150;
                p.Image = Image.FromFile(files[i]);
                p.Image = (Image)(new Bitmap(p.Image, new Size(p.Width, p.Height)));
                p.Location = new Point((i / 4 * 145), (i % 4 * 145));
                p.Click += ClickReceiver;
                flowLayoutPanel1.Controls.Add(p);
                
            }
        }

        public void ClickReceiver(object sender, EventArgs e)
        {
            selected = flowLayoutPanel1.Controls.IndexOf((PictureBox)sender);

            for(int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                ((PictureBox)flowLayoutPanel1.Controls[i]).BorderStyle = BorderStyle.None;
            }

            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;

            SelectUpdateEvent(((PictureBox)sender).Image);
        }
    }
}
