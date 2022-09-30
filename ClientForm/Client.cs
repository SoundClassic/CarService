using System;
using System.Windows.Forms;
using CarService;

namespace ClientForm
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            InitializeCatalog();
        }

        private void InitializeCatalog()
        {
            BrandsList.Items.Add(Brands.Toyota.ToString());
            BrandsList.Items.Add(Brands.Lexus.ToString());
            using(var access = new Access())
            {
                //foreach(var item in access.TypeWorks)
                //{
                //    TypeWorksList.Items.Add(item.TypeWork);
                //}
                var type = new TypeWork()
                {
                    Type = "Диагностика",
                    Cost = 1000
                };
                access.TypeWorks.Add(type);
                access.SaveChanges();
                LFP.Text = $"{type.Id_TypeWork} {type.Type} {type.Cost}";
            }
        }

        private void CreateBid_Click(object sender, EventArgs e)
        {

        }

        private void FindBid_Click(object sender, EventArgs e)
        {

        }
    }
}
