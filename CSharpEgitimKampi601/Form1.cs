using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        CustomerOperations customerOperations = new CustomerOperations();
        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount = int.Parse(txtShoppingCount.Text)
            };
            customerOperations.AddCustomer(customer);
            MessageBox.Show("Müşteri ekleme işlemi başarılı");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Customer> cutomers = customerOperations.GetAllCustomer();
            dataGridView1.DataSource = cutomers;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                CustomerId = txtCustomerId.Text,
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount = int.Parse(txtShoppingCount.Text)
            };
            customerOperations.UpdateCustomer(customer);
            MessageBox.Show("Güncelleme işlemi başarılı");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;

            customerOperations.DeleteCustomer(customerId);
            MessageBox.Show("Silme işlemi başarılı");
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            string customerId=txtCustomerId.Text;
            Customer customer= customerOperations.GetCustomerById(customerId);
            dataGridView1.DataSource = new List<Customer>() { customer };
        }
    }
}
