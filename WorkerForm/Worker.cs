using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CarService;

namespace WorkerForm
{
    public partial class Worker : Form
    {
        private bool change;

        private bool refresh;
        private bool search;
        private bool comment;

        public Worker()
        {
            InitializeComponent();
            InitializeDataGrid();
            change = false;
            refresh = false;
            search = false;
            comment = false;
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

            ShowBids(Methods.GetActiveBids());
        }

        private void ShowBids(List<BidDao> bids)
        {
            if (BidList.Rows.Count > 0)
            {
                BidList.Rows.Clear();
            }
            foreach (var bid in bids)
            {
                BidList.Rows.Add(bid.NumberBid, bid.Date, bid.Status);
            }
        }

        //-----События-Нажатия-----

        private void TakeBid_Click(object sender, EventArgs e)
        {
            if (refresh || search)
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

            Methods.ChangeBid(BidList.SelectedRows[0].Cells[0].Value.ToString(), Statuses.InWork.ToString(), null);
            BidList.SelectedRows[0].Cells[2].Value = Statuses.InWork.ToString();
        }

        private void CompleteBid_Click(object sender, EventArgs e)
        {
            if (refresh || search)
            {
                MessageBox.Show("Невозможно изменить статус!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BidList.SelectedRows.Count != 1)
            {
                MessageBox.Show("Необходимо выбрать одну заявку!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BidList.SelectedRows[0].Cells[2].Value.ToString() == Statuses.Active.ToString())
            {
                MessageBox.Show("Эта заявка еще не в работе!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string numberBid = BidList.SelectedRows[0].Cells[0].Value.ToString();
            if (!Methods.IsTimeToComplete(numberBid))
            {
                MessageBox.Show("Невозможно закрыть заявку! Положенное время не истекло.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.comment = true;
            string comment = InputBox.Show("Введите комментарий:");
            this.comment = false;
            Methods.ChangeBid(numberBid, Statuses.Completed.ToString(), comment);
            BidList.Rows.Remove(BidList.SelectedRows[0]);
        }

        private void ShelveBid_Click(object sender, EventArgs e)
        {
            if (refresh || search)
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

            this.comment = true;
            string comment = InputBox.Show("Введите комментарий:");
            this.comment = false;
            Methods.ChangeBid(BidList.SelectedRows[0].Cells[0].Value.ToString(), Statuses.Delayed.ToString(), comment);
            BidList.Rows.Remove(BidList.SelectedRows[0]);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            List<Control> controls = new List<Control>() { Date, NumberBid, LFP };
            List<Control> correct = new List<Control>();

            foreach (Control control in controls)
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

            if (correct.Count == 0)
            {
                MessageBox.Show("Введите хотя бы 1 корректный аргумент поиска!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<object> @params = new List<object>();
            for (byte i = 0; i < correct.Count; i++)
            {
                if (correct[i].Equals(Date))
                {
                    @params.Add(Date.Value.Date);
                }
                else if (correct[i].Equals(NumberBid))
                {
                    @params.Add(NumberBid.Text);
                }
                else
                {
                    @params.Add(LFP.Text);
                }
            }

            switch (@params.Count)
            {
                case 1:
                    ShowBids(Methods.Search(@params[0]));
                    break;
                case 2:
                    ShowBids(Methods.Search(@params[0], @params[1]));
                    break;
                case 3:
                    ShowBids(Methods.Search(@params[0], @params[1], @params[2]));
                    break;
            }
            search = true;
        }

        private void RefreshBid_Click(object sender, EventArgs e)
        {
            if (refresh || search)
            {
                ShowBids(Methods.GetActiveBids());
                refresh = false;
                search = false;
            }
        }

        private void History_Click(object sender, EventArgs e)
        {
            if (!refresh || search)
            {
                ShowBids(Methods.GetHistory());
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
            string info = Methods.GetInfo(numberBid);
            MessageBox.Show(info, "Информация");
        }

        //-----События-----

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            change = true;
        }

        private void Worker_Activated(object sender, EventArgs e)
        {
            if (!refresh && !search && !comment)
            {
                ShowBids(Methods.GetActiveBids());
            }
        }
    }
}
