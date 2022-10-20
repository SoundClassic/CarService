using System;
using System.Collections.Generic;
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
            TypeWorksList.Items.AddRange(Methods.GetTypeWorks().ToArray());
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
        }

        private void ShowBids()
        {
            if (BidList.Rows.Count > 0)
            {
                BidList.Rows.Clear();
            }
            foreach(var bid in Methods.GetActiveBids())
            {
                BidList.Rows.Add(bid.NumberBid, bid.Date, bid.Status);
            }
        }

        private void AddBidInGrid(string numberBid, DateTime date, string status)
        {
            BidList.Rows.Add(numberBid, date, status);
            MessageBox.Show($"Номер вашей заявки: {numberBid}", "Уведомление!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static async Task AddBidAsync(string numberBid, string LFP, 
            string brand, string typeWork, DateTime date)
        {
            await Task.Run(() =>
            {
                Methods.AddBid(numberBid, LFP, brand, typeWork, date);
            });
        }

        //------События------

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

            string numberBid = Methods.GenerateNumberBid();
            DateTime date = DateTime.Now;

            _ = AddBidAsync(numberBid, 
                            LFP.Text,
                            BrandsList.SelectedItem.ToString(),
                            TypeWorksList.SelectedItem.ToString(),
                            date);

            AddBidInGrid(numberBid, date, Statuses.Active.ToString());

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

            try
            {
                BidDao bid = Methods.FindDeleayedBid(NumberBid.Text);
                AddBidInGrid(bid.NumberBid, bid.Date, bid.Status);
                NumberBid.Text = "";
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Заявка не найдена!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void BidList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (BidList.SelectedRows.Count != 1)
            {
                MessageBox.Show("Необходимо выбрать одну заявку!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(BidList.SelectedRows[0].Cells[2].Value.ToString() != Statuses.Completed.ToString() &&
               BidList.SelectedRows[0].Cells[2].Value.ToString() != Statuses.Delayed.ToString())
            {
                return;
            }

            string numberBid = BidList.SelectedRows[0].Cells[0].Value.ToString();
            string info = Methods.GetInfo(numberBid);
            MessageBox.Show(info, "Квитанция");
            ShowBids();
        }

        private void Client_Activated(object sender, EventArgs e)
        {
            if(BidList.Rows.Count == 0)
            {
                ShowBids();
                return;
            }

            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach(DataGridViewRow row in BidList.Rows)
            {
                rows.Add(row);
            }

            foreach (var row in rows)
            {
                string numberBid = row.Cells[0].Value.ToString();
                var bid = Methods.FindBid(numberBid);

                if (bid.Status != Statuses.Active.ToString())
                {
                    BidList.Rows[BidList.Rows.IndexOf(row)].Cells[2].Value = bid.Status;
                }
            }
        }
    }
}
