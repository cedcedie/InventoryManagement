using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        private List<ProductClass> Product = new List<ProductClass>();

        public frmAddProduct()
        {
            InitializeComponent();
            string[] ListOfProductCategory = {
                "Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other"
            };

            foreach (string category in ListOfProductCategory)
            {
                cbCategory.Items.Add(category);
            }
        } 
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                var product = new ProductClass(
                    _ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description);

                Product.Add(product);

                gridViewProductList.DataSource = null;
                gridViewProductList.DataSource = Product; 
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Input Error");
            }
        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                throw new ArgumentException("Product name must contain only letters.");
            return name;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^\d+$"))
                throw new ArgumentException("Quantity must be a valid integer.");
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
                throw new ArgumentException("Selling price must be a valid number.");
            return Convert.ToDouble(price);
        }
    }
}
