using System.IO.Compression;
using System.Net;
using System.Text;

namespace SaveStateClient
{
    public partial class DownloadPluginsForm : Form
    {
        public DownloadPluginsForm()
        {
            InitializeComponent();
            label2.Text = "Connecting to Server...";

            const string url = "ftp://nakruf331.bplaced.net/DefaultPlugins/DefaultPlugins.zip";
            var credentials = new NetworkCredential("nakruf331_z", "123");

            // Query size of the file to be downloaded
            WebRequest sizeRequest = WebRequest.Create(url);
            sizeRequest.Credentials = credentials;
            sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            int size = (int)sizeRequest.GetResponse().ContentLength;

            progressBar1.Maximum = size;

            label2.Text = "Downloading...";
            // Download the file
            WebRequest request = WebRequest.Create(url);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (Stream ftpStream = request.GetResponse().GetResponseStream())
            using (Stream fileStream = File.Create(Program.pluginsFolder + "\\DefaultPlugins.zip"))
            {
                byte[] buffer = new byte[10240];
                int read;
                while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, read);
                    int position = (int)fileStream.Position;
                    label2.Text = position + "%";
                    progressBar1.Value = position;
                }
            }
            label2.Text = "Extracting files...";
            ZipFile.ExtractToDirectory(Program.pluginsFolder + "\\DefaultPlugins.zip", Program.pluginsFolder);
            label2.Text = "Cleaning up...";
            File.Delete(Program.pluginsFolder + "\\DefaultPlugins.zip");
            GC.Collect();
            label2.Text = "Starting";
            Dispose();

            Program.LoadPlugins();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in Program.loadedPlugins.ToList())
            {
                if (!item.isValidated)
                    Program.loadedPlugins.Remove(item);
            }
            Program.loadedPlugins.ForEach(plugin =>
            {
                stringBuilder.Append(plugin.pluginItem.pluginName + " as " + Path.GetFileName(plugin.pluginPath) + "\n");
            });



            MessageBox.Show(null, "Successfully loaded " + Program.loadedPlugins.Count + " plugin(s). \n\n" + stringBuilder,
                          "Plugins loaded", MessageBoxButtons.OK,
                          MessageBoxIcon.Information,
                          MessageBoxDefaultButton.Button1);

            Application.Run(new Form1());

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }



    }


}
