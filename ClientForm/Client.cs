using System;
using System.Linq;
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
                foreach (var item in access.TypeWorks)
                {
                    TypeWorksList.Items.Add(item.Type);
                }
            }
        }

        private string GenerateNumberBid()
        {
            Random random = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
            int num = random.Next(1000, 10000);
            string word = "";
            for (byte i = 0; i < 4; i++)
            {
                word += (char)random.Next('A', 'Z' + 1);
            }
            return word + num;
        }

        private void CreateBid_Click(object sender, EventArgs e)
        {
            #region Проверка ФИО
            LFP.Text = LFP.Text.Trim();
            if (!LFP.Text.IsCorrectLFP())
            {
                MessageBox.Show("Введенное имя некорректно!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] splitLFP = LFP.Text.Split(' ');
            if (splitLFP.Length != 3)
            {
                LFP.Text = "";
                foreach (string word in splitLFP)
                {
                    if (word != "")
                    {
                        LFP.Text += word + " ";
                    }
                }
                LFP.Text = LFP.Text.Trim();
            }
            #endregion

            if (BrandsList.SelectedItem == null)
            {
                MessageBox.Show("Выбран некорректная марка автомобиля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TypeWorksList.SelectedItem == null)
            {
                MessageBox.Show("Выбран некорректный вид работы!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var access = new Access())
            {
                Bid bid = new Bid()
                {
                    NumberBid = GenerateNumberBid(),
                    LFP = LFP.Text,
                    Brand = BrandsList.SelectedItem.ToString(),
                    Type = access.TypeWorks
                                 .Where(type => type.Type == TypeWorksList.SelectedItem.ToString())
                                 .First(),
                    Date = DateTime.Now,
                    Status = Statuses.Active.ToString(),
                    Succes = false
                };
                access.Bids.Add(bid);
                access.SaveChanges();
            }
        }

        private void FindBid_Click(object sender, EventArgs e)
        {

        }
    }
}
