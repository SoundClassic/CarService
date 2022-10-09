using System;
using System.Linq;
using System.Windows.Forms;
using CarService;

namespace WorkerForm
{
    public partial class Worker : Form
    {
        private bool refresh;

        public Worker()
        {
            InitializeComponent();
            InitializeDataGrid();
            refresh = false;
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

            ShowBids(bid => bid.Status != Statuses.Completed.ToString() &&
                            bid.Status != Statuses.Delayed.ToString());
        }

        private void ShowBids(Predicate<Bid> predicate)
        {
            if(BidList.Rows.Count > 0)
            {
                BidList.Rows.Clear();
            }
            using (var access = new Access())
            {
                BidDao bidDao;
                foreach (var bid in access.Bids.Where(predicate.Invoke))
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

        private void ChangeBid(string numberBid, string status, string comment)
        {
            using (var access = new Access())
            {
                Bid bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();
                access.Bids.Attach(bid);
                bid.Status = status;
                access.Entry(bid).Property(x => x.Status).IsModified = true;
                if (!string.IsNullOrEmpty(comment))
                {
                    bid.Comment = comment;
                    access.Entry(bid).Property(x => x.Comment).IsModified = true;
                }
                access.SaveChanges();
            }
        }

        private void TakeBid_Click(object sender, EventArgs e)
        {
            if (refresh)
            {
                MessageBox.Show("Невозможно изменить статус!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(BidList.SelectedRows.Count != 1)
            {
                MessageBox.Show("Необходимо выбрать одну заявку!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(BidList.SelectedRows[0].Cells[2].Value.ToString() == Statuses.InWork.ToString())
            {
                MessageBox.Show("Эта заявка уже в работе!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChangeBid(BidList.SelectedRows[0].Cells[0].Value.ToString(), Statuses.InWork.ToString(), null);
            BidList.SelectedRows[0].Cells[2].Value = Statuses.InWork.ToString();
        }

        private void CompleteBid_Click(object sender, EventArgs e)
        {

        }

        private void ShelveBid_Click(object sender, EventArgs e)
        {
            if (refresh)
            {
                MessageBox.Show("Невозможно изменить статус!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BidList.SelectedRows.Count != 1)
            {
                MessageBox.Show("Необходимо выбрать одну заявку!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BidList.SelectedRows[0].Cells[2].Value.ToString() == Statuses.Delayed.ToString())
            {
                MessageBox.Show("Эта заявка уже отложена!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string comment = InputBox.Show("Введите комментарий:");
            ChangeBid(BidList.SelectedRows[0].Cells[0].Value.ToString(), Statuses.Delayed.ToString(), comment);
            BidList.Rows.Remove(BidList.SelectedRows[0]);
        }

        private void RefreshBid_Click(object sender, EventArgs e)
        {
            if (refresh)
            {
                ShowBids(bid => bid.Status != Statuses.Completed.ToString() &&
                                bid.Status != Statuses.Delayed.ToString());
                refresh = false;
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void History_Click(object sender, EventArgs e)
        {
            if (!refresh)
            {
                ShowBids(bid => bid.Status == Statuses.Completed.ToString() ||
                                bid.Status == Statuses.Delayed.ToString());
                refresh = true;
            }
        }

        private void BidList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("Привет!");
        }
    }
}
