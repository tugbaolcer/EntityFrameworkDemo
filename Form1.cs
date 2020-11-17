using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadProducts();
        }

        ProductDal _productDal = new ProductDal();

   
        private void Form1_Load(object sender, EventArgs e)
        { 
        }


        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
               
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)
            });
            MessageBox.Show("Added !");
            LoadProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productDal.Update(new Product
            {
                Id=Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name=tbxNameUpdate.Text,
                UnitPrice=Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount=Convert.ToInt32(tbxStockAmountUpdate.Text)
            });
            MessageBox.Show("Updated !");
            LoadProducts();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Product
            {
                Id=Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value)
            });
            LoadProducts();
            MessageBox.Show("Removed!");
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProducts(tbxSearch.Text);
        }


        private void SearchProducts(string key)
        {
            //dgwProducts.DataSource = _productDal.GetAll().Where(p=>p.Name.ToLower().Contains(key.ToLower())).ToList();
            /*
             Yukarıdaki gibi koleksiyondan veri alınacağı zaman büyük harf küçük harf duyarlılığı oluşur.
             Bunu gidermek adına koleksiyondaki ismi tolower ile kiçik harfe dönüştürürüz. Sonrasında dışarıdan gelen keyi de küçük harfe dönüştürerek
             bu hasassiyetten kurtulmuş oluruz.
             
             */


            //Direk veritabanından böyle çekilir.
            var result = _productDal.GetByName(key);
            dgwProducts.DataSource = result;

            /*Koleskisyonlarda oluşan büyük harf küçük harf duyarlılığı veritabanından çektiğimizde oluşmamaktadır.*/
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            _productDal.GetById(1);
        }
    }
}
