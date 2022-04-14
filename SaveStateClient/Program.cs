using System.Text;

namespace SaveStateClient
{
    internal static class Program
    {

        public static List<Plugin> loadedPlugins = new List<Plugin>();
        public static String pluginsFolder = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\SaveStateClient";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (!Directory.Exists(pluginsFolder))
            {
                Directory.CreateDirectory(pluginsFolder);
                ApplicationConfiguration.Initialize();
                Application.EnableVisualStyles();
                Application.Run(new DownloadPluginsForm());
                return;
            }
            LoadPlugins();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in loadedPlugins.ToList())
            {
                if (!item.isValidated)
                    loadedPlugins.Remove(item);
            }
            loadedPlugins.ForEach(plugin =>
            {
                stringBuilder.Append(plugin.pluginItem.pluginName + " as " + Path.GetFileName(plugin.pluginPath) + "\n");
            });



            MessageBox.Show(null, "Successfully loaded " + loadedPlugins.Count + " plugin(s). \n\n" + stringBuilder,
                          "Plugins loaded", MessageBoxButtons.OK,
                          MessageBoxIcon.Information,
                          MessageBoxDefaultButton.Button1);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }

        public static void LoadPlugins()
        {
            string[] fileEntries = Directory.GetFiles(pluginsFolder);
            foreach (string filePath in fileEntries)
                loadedPlugins.Add(new Plugin(filePath));
        }

        public static Plugin getLoadedPluginByName(String pluginName)
        {
            Plugin returningPlugin = null;
            loadedPlugins.ForEach((plugin) =>
            {
                if (plugin.pluginItem.pluginName.Equals(pluginName))
                {
                    returningPlugin = plugin;
                }
            });
            return returningPlugin;
        }


    }
}