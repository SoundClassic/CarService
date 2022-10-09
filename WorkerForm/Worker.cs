using System;
using System.Linq;
using System.Windows.Forms;
using CarService;

namespace WorkerForm
{
    public partial class Worker : Form
    {
        public Worker()
        {
            InitializeComponent();
            InitializeDataGrid();
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

            BidDao bidDao;
            using (var access = new Access())
            {
                foreach (var bid in access.Bids.Where(bid => bid.Status != Statuses.Completed.ToString()))
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

        private void TakeBid_Click(object sender, EventArgs e)
        {
            if(BidList.SelectedRows.Count != 1)
            {
                MessageBox.Show("Необходимо выбрать одну заявку!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string numberBid = BidList.SelectedRows[0].Cells[0].Value.ToString();
            using (var access = new Access())
            {
                Bid bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();
                if(bid.Status != Statuses.InWork.ToString())
                {
                    bid.Status = Statuses.InWork.ToString();
                    access.Bids.Attach(bid);
                    access.Entry(bid).Property(x => x.Status).IsModified = true;
                    access.SaveChanges();
                    BidList.SelectedRows[0].Cells[2].Value = bid.Status;
                }
                else
                {
                    MessageBox.Show("Эта заявка уже в работе!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void CompleteBid_Click(object sender, EventArgs e)
        {

        }

        private void ShelveBid_Click(object sender, EventArgs e)
        {

        }

        private void RefreshBid_Click(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void History_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Информация о выбранной заявке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BidList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("Привет!");
        }
    }
}
