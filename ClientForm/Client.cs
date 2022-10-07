using System;
using System.Collections.Generic;
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
            InitializeDataGrid();
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

        private void InitializeDataGrid()
        {
            BidList.AllowUserToAddRows = false;
            BidList.AllowDrop = false;
            BidList.AllowUserToDeleteRows = false;
            BidList.AllowUserToOrderColumns = false;
            BidList.AllowUserToResizeColumns = false;
            BidList.AllowUserToResizeRows = false;

            BidList.ColumnCount = 3;
            BidList.Columns[0].Name = "Номер заявки";
            BidList.Columns[0].Width = 142;
            BidList.Columns[1].Name = "Дата заявки";
            BidList.Columns[1].Width = 142;
            BidList.Columns[2].Name = "Статус заявки";
            BidList.Columns[2].Width = 142;

            List<BidDao> bids = new List<BidDao>();
            using (var access = new Access())
            { 
                foreach(var bid in access.Bids.Where(bid => bid.Status == Statuses.Active.ToString()))
                {
                    bids.Add(new BidDao()
                    {
                        NumberBid = bid.NumberBid,
                        Date = bid.Date,
                        Status = Statuses.Active
                    });
                }

                foreach (var bid in bids)
                {
                    BidList.Rows.Add(bid.NumberBid, bid.Date, bid.Status);
                }
            }
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
                    NumberBid = Helper.GenerateNumberBid(),
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

                BidDao bidAdd = new BidDao()
                {
                    NumberBid = bid.NumberBid,
                    Date = bid.Date,
                    Status = Statuses.Active
                };
                BidList.Rows.Add(bidAdd.NumberBid, bidAdd.Date, bidAdd.Status);
                BidList.Refresh();
            }
        }

        private void FindBid_Click(object sender, EventArgs e)
        {

        }
    }
}
