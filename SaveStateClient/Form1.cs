using System.Diagnostics;
using System.Text;

namespace SaveStateClient
{
    public partial class Form1 : Form
    {

        private Plugin currentPlugin;
        public Form1()
        {
            InitializeComponent();
            Program.loadedPlugins.ForEach(plugin =>
            {
                comboBox1.Items.Add(plugin.pluginItem.pluginName);
            });
            if (comboBox1.Items.Count == 0)
            {
                if (MessageBox.Show("There is no plugin that could be loaded successfully.\nPlease make sure to get at least one Plugin right.\n\nDo you want to open the plugin folder?", "No plugin loaded", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    Process.Start("explorer.exe", Program.pluginsFolder);
                Environment.Exit(0);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("'" + currentPlugin.pluginItem.pluginName + "' by " + currentPlugin.pluginItem.pluginAuthor + "\n\nRunning version: " + currentPlugin.pluginItem.pluginVersion, currentPlugin.pluginPath, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPlugin = Program.getLoadedPluginByName(comboBox1.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!currentPlugin.pluginItem.backupAll)
                if (MessageBox.Show("The chosed plugin will not Backup your savestate before rewriting.\nPlease backup your data manually\nAre you sure?", "Backup disabled. Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    return;



        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Program.pluginsFolder);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", currentPlugin.pluginItem.saveGameDirectory);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Program.loadedPlugins.Clear();
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

            Program.loadedPlugins.ForEach(plugin =>
            {
                comboBox1.Items.Add(plugin.pluginItem.pluginName);
            });
            if (comboBox1.Items.Count == 0)
            {
                if (MessageBox.Show("There is no plugin that could be loaded successfully.\nPlease make sure to get at least one Plugin right.\n\nDo you want to open the plugin folder?", "No plugin loaded", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    Process.Start("explorer.exe", Program.pluginsFolder);
                Environment.Exit(0);
            }
            comboBox1.SelectedIndex = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            new SavestateBrowser().ShowDialog();
        }
    }
}