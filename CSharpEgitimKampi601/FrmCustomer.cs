using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;password=1234";

        void GetAllCustomers()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "select * from customers";
            var command= new NpgsqlCommand(query, connection);
            var adapter= new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string customerName=txtCustomerName.Text;
            string customerSurname=txtCustomerSurname.Text;
            string customerCity=txtCustomerCity.Text;
            var connection= new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into customers (CustomerName,CustomerSurname,CustomerCity) values(@customerName,@customerSurname,@customerCity)";
            var command= new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName",customerName);
            command.Parameters.AddWithValue("@customerSurname",customerSurname);
            command.Parameters.AddWithValue("@customerCity",customerCity);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Ekleme işlemi başarılı");
            GetAllCustomers() ;
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id=int.Parse(txtCustomerId.Text);   
            var connection= new NpgsqlConnection(connectionString); 
            connection.Open();
            string query = "delete from customers where customerID=@customerId";
            var command= new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerId",id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Silme işlemi başarılı");
            GetAllCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse( txtCustomerId.Text);
            string customerName = txtCustomerName.Text;
            string customerSurname=txtCustomerSurname.Text;
            string customerCity=txtCustomerCity.Text;
            var connection= new NpgsqlConnection(connectionString); 
            connection.Open();
            string query = "update customers set customerName=@customerName,customerSurname=@customerSurname,customerCity=@customerCity where customerId=@customerId";
            var command= new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName",customerName);
            command.Parameters.AddWithValue("@customerSurname",customerSurname);
            command.Parameters.AddWithValue("@customerCity",customerCity);
            command.Parameters.AddWithValue("@customerId",id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Güncelleme işlemi başarılı");
            GetAllCustomers();
            
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id=int.Parse( txtCustomerId.Text);
            var connection= new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "select * from customers where customerId=@customerId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerId",id);
            command.ExecuteNonQuery ();
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
    }
}
