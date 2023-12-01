using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login_page_v2
{
    public partial class Form2 : Form
    {

        private string connectionString = "Data Source=OUMAIMA_HP\\SQLEXPRESS;Initial Catalog=testdatabase;Integrated Security=True";
        private DataTable filiereTable;
        public Form2()
        {
            InitializeComponent();
            LoadFiliereData();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void LoadFiliereData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id_filiere, nom_filiere FROM [filiere_table]";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    filiereTable = new DataTable();
                    adapter.Fill(filiereTable);
                    dataGridView1.DataSource = filiereTable;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nomFiliere = textBox1.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO [filiere_table] (nom_filiere) VALUES (@nom_filiere)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nom_filiere", nomFiliere);
                    command.ExecuteNonQuery();
                }
            }

            LoadFiliereData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int filiereId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["id_filiere"].Value);


                string newNomFiliere = Microsoft.VisualBasic.Interaction.InputBox("Enter the new filiere name:", "Modify Filiere", dataGridView1.Rows[selectedRowIndex].Cells["nom_filiere"].Value.ToString());

                if (!string.IsNullOrWhiteSpace(newNomFiliere))
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "UPDATE [filiere_table] SET nom_filiere = @nom_filiere WHERE id_filiere = @id_filiere";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@nom_filiere", newNomFiliere);
                            command.Parameters.AddWithValue("@id_filiere", filiereId);
                            command.ExecuteNonQuery();
                        }
                    }

                    LoadFiliereData();
                }
            }
        }

        private void ClearTextBoxes()
        {
            textBox1.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int filiereId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["id_filiere"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM [filiere_table] WHERE id_filiere = @id_filiere";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_filiere", filiereId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadFiliereData();
                ClearTextBoxes();
            }
        }
    }
}
