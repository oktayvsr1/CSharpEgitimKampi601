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
    public partial class Frmemployee : Form
    {
        public Frmemployee()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;password=1234";
        void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "select * from Employees";
            var cmd= new NpgsqlCommand(query, connection);
            var adapter= new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);   
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "select * from Departments";
            var cmd = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "DepartmentId";
            cmbEmployeeDepartment.DataSource = dt;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void Frmemployee_Load(object sender, EventArgs e)
        {
            DepartmentList();
            EmployeeList();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string employeeName=txtEmployeeName.Text;
            string employeeSurname=txtEmployeeSurname.Text;
            int employeeSalary=int.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());
            var connection= new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into employees(EmployeeName,EmployeeSurname,EmployeeSalary,DepartmentId) values(@employeeName,@employeeSurname,@employeeSalary,@DepartmentId)";
            var cmd= new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@employeeName",employeeName);
            cmd.Parameters.AddWithValue("@employeeSurname",employeeSurname);
            cmd.Parameters.AddWithValue("@employeeSalary",employeeSalary);
            cmd.Parameters.AddWithValue("@DepartmentId",departmentId);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Personel Ekleme işlemi başarılı");
            EmployeeList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeId.Text);
            var connection= new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "delete from Employees where EmployeeId=@employeeId";
            var cmd = new NpgsqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@employeeId",id);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Silme işlmei başarılı");
            EmployeeList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int employeeId =int.Parse( txtEmployeId.Text);
            string employeName =txtEmployeeName.Text;
            string employeeSurname=txtEmployeeSurname.Text;
            int employeeSalary = int.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());
            var connection= new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "update Employees set EmployeeName=@employeeName,EmployeeSurname=@employeeSurname,EmployeeSalary=@employeeSalary,DepartmentId=@departmentId where EmployeeId=@employeeId";
            var cmd= new NpgsqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@employeeName",employeName);
            cmd.Parameters.AddWithValue("@employeeSurname",employeeSurname);
            cmd.Parameters.AddWithValue("@employeeSalary",employeeSalary);
            cmd.Parameters.AddWithValue("@departmentId",departmentId);
            cmd.Parameters.AddWithValue("@employeeId",employeeId);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Personel güncelleme işlemi başarılı");
            EmployeeList();


        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id =int.Parse( txtEmployeId.Text);
            var connection= new NpgsqlConnection( connectionString);
            connection.Open();
            string query = "Select * from Employees where EmployeeId=@employeeId";
            var cmd= new NpgsqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@employeeId",id);
            var adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close() ;

        }
    }
}
