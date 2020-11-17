using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
    public class ProductDal
    {
        public List<Product> GetAll()
        {
            using (EtradeContext context = new EtradeContext()) //Using, Etrade nesnesi bellekte fazla yer kapladığı için iş bitiminde garbage collectoru beklemeden nesneyi zorla bellekten atar.
            {
                return context.Products.ToList(); //Entitiy frameworkta veritabanındaki verileri listeler.
            }

        }

        public List<Product> GetByName(string key)//Koleksiyona göre daha performanslıdır.
        {
            using (EtradeContext context = new EtradeContext()) 
            {
                return context.Products.Where(p => p.Name.Contains(key)).ToList();
            }

        }

        //Ürün fiyatına göre listeler.
        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            using (EtradeContext context = new EtradeContext())
            {
                return context.Products.Where(p => p.UnitPrice >= min &&p.UnitPrice<=max).ToList();
                
            }

        }

        //Tek bir ürün getirme
        public Product GetById(int id)
        {
            using (EtradeContext context = new EtradeContext())
            {
                return context.Products.FirstOrDefault(p => p.Id == id);//Bu id ye uygun ilk veriyi getir. Veri yoksa default dön.
            }

        }
        public void Add(Product product)
        {
            using (EtradeContext context = new EtradeContext()) 
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            using (EtradeContext context = new EtradeContext())
            {
                var entity = context.Entry(product); // gönderilen product'i veritabanındaki product ile eşitler.
                entity.State = EntityState.Modified; // durumunu güncellendi olarak işaretler.


                context.SaveChanges();
            }
        }

        public void Delete(Product product)
        {
            using (EtradeContext context = new EtradeContext())
            {
                var entity = context.Entry(product); // gönderilen product'i veritabanındaki product ile eşitler.
                entity.State = EntityState.Deleted; // durumunu güncellendi olarak işaretler.


                context.SaveChanges();
            }
        }
    }
}
