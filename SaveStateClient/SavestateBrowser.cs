using MySql.Data.MySqlClient;

namespace SaveStateClient
{
    public partial class SavestateBrowser : Form
    {
        public SavestateBrowser()
        {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.Form1_Shown);
            label1.Visible = true;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "savestates",
                UserID = "user",
                Password = "123",
                SslMode = MySqlSslMode.Preferred,
            };

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                label1.Text = "Connecting to database...";
                conn.Open();
                label1.Text = "Fetching database information...";

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM test;";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            reader.GetString(0),
            reader.GetString(1),
            reader.GetInt32(2) == 1 ? "Yes" : "No",
            reader.GetString(4),reader.GetString(3)}, -1);
                            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});

                        }
                    }
                    label1.Text = "Done";
                    label1.Visible = false;

                }

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "savestates",
                UserID = "user",
                Password = "123",
                SslMode = MySqlSslMode.Preferred,
            };

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                listView1.Items.Clear();
                label1.Text = "Connecting to database...";
                conn.Open();
                label1.Text = "Fetching database information...";

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM test;";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            reader.GetString(0),
            reader.GetString(1),
            reader.GetInt32(2) == 1 ? "Yes" : "No",
            reader.GetString(4),reader.GetString(3)}, -1);
                            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});

                        }
                    }
                    label1.Text = "Done";
                    label1.Visible = false;

                }

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
