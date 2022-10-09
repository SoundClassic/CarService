using System;
using System.Linq;
using System.Threading.Tasks;
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
            BidList.ReadOnly = true;

            BidList.ColumnCount = 3;
            BidList.Columns[0].Name = "Номер заявки";
            BidList.Columns[0].Width = 142;
            BidList.Columns[1].Name = "Дата заявки";
            BidList.Columns[1].Width = 142;
            BidList.Columns[2].Name = "Статус заявки";
            BidList.Columns[2].Width = 142;

            using (var access = new Access())
            {
                BidDao bidDao;
                foreach (var bid in access.Bids.Where(bid => bid.Status == Statuses.Active.ToString() ||
                                                             bid.Status == Statuses.InWork.ToString()))
                {
                    bidDao = new BidDao()
                    {
                        NumberBid = bid.NumberBid,
                        Date = bid.Date,
                        Status = Statuses.Active
                    };
                    BidList.Rows.Add(bid.NumberBid, bid.Date, bid.Status);
                }
            }
        }

        private void AddBidInGrid(string numberBid, DateTime date)
        {
            BidDao bidAdd = new BidDao()
            {
                NumberBid = numberBid,
                Date = date,
                Status = Statuses.Active
            };
            BidList.Rows.Add(bidAdd.NumberBid, bidAdd.Date, bidAdd.Status);

            MessageBox.Show($"Номер вашей заявки: {numberBid}", "Уведомление!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static async Task AddBidAsync(string numberBid, string LFP, 
            string brand, string typeWork, DateTime date)
        {
            await Task.Run(() =>
            {
                using (var access = new Access())
                {
                    Bid bid = new Bid()
                    {
                        NumberBid = numberBid,
                        LFP = LFP,
                        Brand = brand,
                        Type = access.TypeWorks
                                     .Where(type => type.Type == typeWork)
                                     .First(),
                        Date = date,
                        Status = Statuses.Active.ToString(),
                        Succes = false
                    };
                    access.Bids.Add(bid);
                    access.SaveChanges();
                }
            });
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

            string numberBid = Helper.GenerateNumberBid();
            DateTime date = DateTime.Now;

            _ = AddBidAsync(numberBid, 
                            LFP.Text,
                            BrandsList.SelectedItem.ToString(),
                            TypeWorksList.SelectedItem.ToString(),
                            date);

            AddBidInGrid(numberBid, date);

            LFP.Text = "";
            BrandsList.Text = "";
            BrandsList.SelectedItem = null;
            TypeWorksList.Text = "";
            TypeWorksList.SelectedItem = null;
        }

        private void FindBid_Click(object sender, EventArgs e)
        {
            NumberBid.Text = NumberBid.Text.Trim().ToUpper();

            if (!NumberBid.Text.IsNumberBid())
            {
                MessageBox.Show("Введенный номер заявки не корректный!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using(var access = new Access())
            {
                Bid bid = new Bid();
                try
                {
                    bid = access.Bids
                                .Where(findBid => findBid.NumberBid == NumberBid.Text &&
                                       findBid.Status == Statuses.Delayed.ToString())
                                .First();
                    bid.Date = DateTime.Now;
                    bid.Status = Statuses.Active.ToString();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Заявка не найдена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                access.Bids.Attach(bid);
                access.Entry(bid).Property(x => x.Status).IsModified = true;
                access.SaveChanges();

                AddBidInGrid(bid.NumberBid, bid.Date);

                NumberBid.Text = "";
            }
        }
    }
}
