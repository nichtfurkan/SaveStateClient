using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace SaveStateClient
{


    class Plugin
    {

        public String pluginPath { get; }
        public PluginItem pluginItem { get; set; }
        public bool isValidated { get; set; }
        public class PluginItem
        {
            public String pluginName;
            public String pluginAuthor;
            public String pluginVersion;
            public String saveGameDirectory;
            public bool createNewDirectory;
            public bool backupAll;
            public String newDirectoryName;
            public String backupDirectoryName;
        }

        public Plugin(String pluginPath)
        {
            this.pluginPath = pluginPath;
            isValidated = true;
            validatePlugin();

        }

        private void validatePlugin()
        {

            if (!File.Exists(pluginPath) || !pluginPath.EndsWith(".json"))
                showError("'pluginPath' directory does not exist or is not a valid '.json'.\nCurrent value: '" + pluginPath + "'");
            try { pluginItem = JsonConvert.DeserializeObject<PluginItem>(File.ReadAllText(pluginPath)); } catch (Exception e) { showError("An Error encountered while Deserializing JSON: " + e.Message); }
            if (pluginItem == null)
            {
                return;
            }

            if (pluginItem.pluginName == null || pluginItem.pluginName == "")
                showError("'pluginName' cannot be found or must contain a valid value.\nCurrent value: '" + pluginItem.pluginName + "'");

            Program.loadedPlugins.ForEach(plugin =>
            {
                if (pluginItem.pluginName.Equals(plugin.pluginItem.pluginName))
                {
                    showError("There is already a Plugin that has the name '" + pluginItem.pluginName + "' in '" + Path.GetFileName(plugin.pluginPath) + "'");
                }
            });

            if (pluginItem.pluginAuthor == null || pluginItem.pluginAuthor == "")
                showError("'pluginName' cannot be found or must contain a valid value.\nCurrent value: '" + pluginItem.pluginAuthor + "'");

            if (pluginItem.pluginAuthor.Length > 10)
                showError("'pluginAuthor' cannot be longer than 10 Characters.\nCurrent value: '" + pluginItem.pluginAuthor + "'");



            if (pluginItem.pluginVersion == null || pluginItem.pluginVersion == "")
                showError("'pluginVersion' cannot be found or must contain a valid value.\nCurrent value: '" + pluginItem.pluginVersion + "'");

            if (Regex.Matches(pluginItem.pluginVersion, @"[a-zA-Z]").Count > 0)
                showError("'pluginVersion' must contain only Numbers or special characters.\nCurrent value: '" + pluginItem.pluginVersion + "'");


            if (pluginItem.newDirectoryName == null || pluginItem.newDirectoryName == "")
                showError("'newDirectoryName' cannot be found or must contain a valid value.\nCurrent value: '" + pluginItem.newDirectoryName + "'");


            if (pluginItem.saveGameDirectory == null || pluginItem.saveGameDirectory == "")
                showError("'saveGameDirectory' cannot be found or must contain a valid value.\nCurrent value: '" + pluginItem.saveGameDirectory + "'");

            if (pluginItem.backupDirectoryName == null || pluginItem.backupDirectoryName == "")
                showError("'backupDirectoryName' cannot be found or must contain a valid value.\nCurrent value: '" + pluginItem.backupDirectoryName + "'");

            pluginItem.saveGameDirectory = pluginItem.saveGameDirectory.Replace("%USER%", Environment.UserName);

            if (!Directory.Exists(pluginItem.saveGameDirectory))
                showError("Cant find directory for 'saveGameDirectory'. Please make sure that you have the game installed correctly.\nCurrent value: '" + pluginItem.saveGameDirectory + "'");


        }

        private void showError(String message)
        {
            isValidated = false;
            MessageBox.Show(null, "An error occoured while reading a plugin (" + Path.GetFileName(pluginPath) + ").\n\n" + message,
                                   "Error while reading Plugin", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);

            return;
        }



    }

}
