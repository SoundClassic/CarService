using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using CarService;

namespace WorkerForm
{
    public partial class Worker : Form
    {
        private bool refresh;
        private bool change;

        public Worker()
        {
            InitializeComponent();
            InitializeDataGrid();
            refresh = false;
            change = false;
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

            ShowBids(bid => bid.Status == Statuses.Active.ToString() ||
                            bid.Status == Statuses.InWork.ToString());
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

                if (status == Statuses.Completed.ToString())
                {
                    bid.Succes = true;
                    access.Entry(bid).Property(x => x.Succes).IsModified = true;
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    bid.Comment = comment;
                    access.Entry(bid).Property(x => x.Comment).IsModified = true;
                }
                access.SaveChanges();
            }
        }

        private bool IsTimeToComplete(string numberBid)
        {
            using(var access = new Access())
            {
                Bid bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();

                return (DateTime.Now - bid.Date).Minutes > bid.Type.AlottMinTime;
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

            string numberBid = BidList.SelectedRows[0].Cells[0].Value.ToString();
            if (!IsTimeToComplete(numberBid))
            {
                MessageBox.Show("Невозможно закрыть заявку! Положенное время не истекло.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string comment = InputBox.Show("Введите комментарий:");
            ChangeBid(numberBid, Statuses.Completed.ToString(), comment);
            BidList.Rows.Remove(BidList.SelectedRows[0]);
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

            if (BidList.SelectedRows[0].Cells[2].Value.ToString() != Statuses.InWork.ToString())
            {
                MessageBox.Show("Эта заявка еще не принята!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ShowBids(bid => bid.Status == Statuses.Active.ToString() ||
                                bid.Status == Statuses.InWork.ToString());
                refresh = false;
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            List<Control> controls = new List<Control>() {Date, NumberBid, LFP };
            List<Control> correct = new List<Control>();

            foreach(Control control in controls)
            {
                if (!string.IsNullOrWhiteSpace(control.Text))
                {
                    if (control.Equals(NumberBid))
                    {
                        if (!control.Text.IsNumberBid())
                        {
                            MessageBox.Show("Введенный номер некорректный!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        correct.Add(control);
                    }
                    else if (control.Equals(LFP))
                    {
                        if (!control.Text.IsCorrectLFP())
                        {
                            MessageBox.Show("Введенное ФИО ненекорректно!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        correct.Add(control);
                    }
                    else
                    {
                        if (change)
                        {
                            correct.Add(control);
                        }
                    }
                }
            }

            if(correct.Count == 0)
            {
                MessageBox.Show("Введите хотя бы 1 корректный аргумент поиска!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Expression<Predicate<Bid>> lambda;
            if (correct[0].Equals(Date))
            {
                lambda = bid => bid.Date.Date == Date.Value.Date;
            }
            else if (correct[0].Equals(NumberBid))
            {
                lambda = bid => bid.NumberBid == NumberBid.Text;
            }
            else
            {
                lambda = bid => bid.LFP == LFP.Text;
            }

            for(byte i = 1; i < correct.Count; i++)
            {
                var @params = lambda.Parameters;
                var body = lambda.Body;
                if (correct[i].Equals(NumberBid))
                {
                    var newCheck = Expression.Equal(Expression.PropertyOrField(@params[0], "NumberBid"), Expression.Constant(NumberBid.Text));
                    var fullExpression = Expression.And(body, newCheck);
                    lambda = Expression.Lambda<Predicate<Bid>>(fullExpression, @params);
                }
                else
                {
                    var newCheck = Expression.Equal(Expression.PropertyOrField(@params[0], "LFP"), Expression.Constant(LFP.Text));
                    var fullExpression = Expression.And(body, newCheck);
                    lambda = Expression.Lambda<Predicate<Bid>>(fullExpression, @params);
                }
            }

            ShowBids(lambda.Compile());
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
            if (BidList.SelectedRows.Count != 1)
            {
                MessageBox.Show("Необходимо выбрать одну заявку!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string numberBid = BidList.SelectedRows[0].Cells[0].Value.ToString();
            using(var access = new Access())
            {
                Bid bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();
                string info = string.Empty;

                info += $"Номер заявки: {bid.NumberBid}\n\r";
                info += $"ФИО: {bid.LFP}\n\r";
                info += $"Марка автомобиля: {bid.Brand}\n\r";
                info += $"Тип работы: {bid.Type.Type}\n\r";
                info += $"Стоимость работы: {bid.Type.Cost}\n\r";
                info += $"Дата и время: {bid.Date}\n\r";
                info += $"Статус: {bid.Status}\n\r";
                if (!string.IsNullOrEmpty(bid.Comment))
                {
                    info += $"Комментарий: {bid.Comment}\n\r";
                }

                MessageBox.Show(info, "Информация");
            }
        }

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            change = true;
        }
    }
}
