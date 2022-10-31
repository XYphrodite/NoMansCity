using System.Security;
using System.Text;
using System.Windows.Forms;

namespace NoMansCity
{
    public partial class NoManCityForm : Form
    {
        List<Image> images = new List<Image>();
        public NoManCityForm()
        {
            InitializeComponent();
            richTextBox1.ReadOnly = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int height=0, width=0;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Picture files|*.jpeg;*.png;*.jpg";  // file types, that will be allowed to upload
            dialog.Multiselect = true; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                if (dialog.FileNames.Length < 2)
                {
                    MessageBox.Show("You must load more than 2 pictures", "Amount error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    try
                    {
                        Image loadedImage = Image.FromFile(dialog.FileNames[i]);
                        images.Add(loadedImage);
                        switch (i)
                        {
                            case 0:
                                height = loadedImage.Height;
                                width = loadedImage.Width;
                                pictureBox1.Image = loadedImage;
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                break;
                            case 1:
                                pictureBox2.Image = loadedImage;
                                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                                break;
                            case 2:
                                pictureBox3.Image = loadedImage;
                                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                                break;
                            case 3:
                                pictureBox4.Image = loadedImage;
                                pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                                break;
                            default:
                                break;
                        }
                        if (loadedImage.Height != height || loadedImage.Width != width)
                        {
                            MessageBox.Show("Sizes of pictures isn't identical", "Size Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (SecurityException ex)
                    {
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                            "Error message: " + ex.Message + "\n\n" +
                            "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot display the image: " + dialog.FileNames[i].Substring(dialog.FileNames[i].LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
                richTextBox1.Text += dialog.FileNames.Length.ToString() + " pictures was loaded" + Environment.NewLine;
            }

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NoManCityBtn_Click(object sender, EventArgs e)
        {
            if (images.Count < 2)
            {
                MessageBox.Show("You must load more than 2 pictures", "Amount error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Bitmap> bitmaps = new List<Bitmap>();
            foreach(var img in images)
            {
                bitmaps.Add((Bitmap)img);
            }
            Bitmap newBitmap = new Bitmap(images[0]);
            pictureBox5.Image= newBitmap;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            for(int i = 10; i < bitmaps[0].Height; i++)
            {
                for (int j = 10; j< bitmaps[0].Width; j++)
                {

                }
            }
        }
    }
}