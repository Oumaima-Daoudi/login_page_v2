
using System.Data.SqlClient;


namespace login_page_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (IsValidUser(username, password))
            {
                Form2 mainForm = new Form2();

                this.Hide();
                mainForm.ShowDialog();
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect. Accès refusé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private bool IsValidUser(string username, string password)
        {
            string connectionString = "Data Source=OUMAIMA_HP\\SQLEXPRESS;Initial Catalog=testdatabase;Integrated Security=True";

            try  
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM [user_table] WHERE username = @Username AND password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }


}
