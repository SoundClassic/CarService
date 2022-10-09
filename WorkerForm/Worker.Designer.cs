namespace WorkerForm
{
    partial class Worker
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BidList = new System.Windows.Forms.DataGridView();
            this.ManagementBox = new System.Windows.Forms.GroupBox();
            this.RefreshBid = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.GroupBox();
            this.LFP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Search = new System.Windows.Forms.Button();
            this.Date = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.NumberBid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.History = new System.Windows.Forms.Button();
            this.ShelveBid = new System.Windows.Forms.Button();
            this.CompleteBid = new System.Windows.Forms.Button();
            this.TakeBid = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BidList)).BeginInit();
            this.ManagementBox.SuspendLayout();
            this.SearchBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BidList);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 500);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Заявки";
            // 
            // BidList
            // 
            this.BidList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BidList.Location = new System.Drawing.Point(10, 25);
            this.BidList.Name = "BidList";
            this.BidList.RowHeadersWidth = 51;
            this.BidList.RowTemplate.Height = 24;
            this.BidList.Size = new System.Drawing.Size(480, 465);
            this.BidList.TabIndex = 0;
            this.BidList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BidList_CellMouseDoubleClick);
            // 
            // ManagementBox
            // 
            this.ManagementBox.Controls.Add(this.RefreshBid);
            this.ManagementBox.Controls.Add(this.SearchBox);
            this.ManagementBox.Controls.Add(this.History);
            this.ManagementBox.Controls.Add(this.ShelveBid);
            this.ManagementBox.Controls.Add(this.CompleteBid);
            this.ManagementBox.Controls.Add(this.TakeBid);
            this.ManagementBox.Location = new System.Drawing.Point(520, 10);
            this.ManagementBox.Name = "ManagementBox";
            this.ManagementBox.Size = new System.Drawing.Size(300, 500);
            this.ManagementBox.TabIndex = 1;
            this.ManagementBox.TabStop = false;
            this.ManagementBox.Text = "Управление";
            // 
            // RefreshBid
            // 
            this.RefreshBid.Location = new System.Drawing.Point(10, 180);
            this.RefreshBid.Name = "RefreshBid";
            this.RefreshBid.Size = new System.Drawing.Size(280, 40);
            this.RefreshBid.TabIndex = 6;
            this.RefreshBid.Text = "Отобразить незавершенные заявки";
            this.RefreshBid.UseVisualStyleBackColor = true;
            this.RefreshBid.Click += new System.EventHandler(this.RefreshBid_Click);
            // 
            // SearchBox
            // 
            this.SearchBox.Controls.Add(this.LFP);
            this.SearchBox.Controls.Add(this.label3);
            this.SearchBox.Controls.Add(this.Search);
            this.SearchBox.Controls.Add(this.Date);
            this.SearchBox.Controls.Add(this.label2);
            this.SearchBox.Controls.Add(this.NumberBid);
            this.SearchBox.Controls.Add(this.label1);
            this.SearchBox.Location = new System.Drawing.Point(10, 230);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(280, 210);
            this.SearchBox.TabIndex = 5;
            this.SearchBox.TabStop = false;
            this.SearchBox.Text = "Поиск";
            // 
            // LFP
            // 
            this.LFP.Location = new System.Drawing.Point(70, 110);
            this.LFP.Name = "LFP";
            this.LFP.Size = new System.Drawing.Size(200, 30);
            this.LFP.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "ФИО:";
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(14, 164);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(260, 40);
            this.Search.TabIndex = 4;
            this.Search.Text = "Поиск";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // Date
            // 
            this.Date.Location = new System.Drawing.Point(70, 70);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(200, 30);
            this.Date.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата:";
            // 
            // NumberBid
            // 
            this.NumberBid.Location = new System.Drawing.Point(70, 30);
            this.NumberBid.Name = "NumberBid";
            this.NumberBid.Size = new System.Drawing.Size(200, 30);
            this.NumberBid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер:";
            // 
            // History
            // 
            this.History.Location = new System.Drawing.Point(10, 450);
            this.History.Name = "History";
            this.History.Size = new System.Drawing.Size(280, 40);
            this.History.TabIndex = 4;
            this.History.Text = "История заявок";
            this.History.UseVisualStyleBackColor = true;
            this.History.Click += new System.EventHandler(this.History_Click);
            // 
            // ShelveBid
            // 
            this.ShelveBid.Location = new System.Drawing.Point(10, 130);
            this.ShelveBid.Name = "ShelveBid";
            this.ShelveBid.Size = new System.Drawing.Size(280, 40);
            this.ShelveBid.TabIndex = 2;
            this.ShelveBid.Text = "Отложить заявку";
            this.ShelveBid.UseVisualStyleBackColor = true;
            this.ShelveBid.Click += new System.EventHandler(this.ShelveBid_Click);
            // 
            // CompleteBid
            // 
            this.CompleteBid.Location = new System.Drawing.Point(10, 80);
            this.CompleteBid.Name = "CompleteBid";
            this.CompleteBid.Size = new System.Drawing.Size(280, 40);
            this.CompleteBid.TabIndex = 1;
            this.CompleteBid.Text = "Завершить заявку";
            this.CompleteBid.UseVisualStyleBackColor = true;
            this.CompleteBid.Click += new System.EventHandler(this.CompleteBid_Click);
            // 
            // TakeBid
            // 
            this.TakeBid.Location = new System.Drawing.Point(10, 30);
            this.TakeBid.Name = "TakeBid";
            this.TakeBid.Size = new System.Drawing.Size(280, 40);
            this.TakeBid.TabIndex = 0;
            this.TakeBid.Text = "Принять заявку";
            this.TakeBid.UseVisualStyleBackColor = true;
            this.TakeBid.Click += new System.EventHandler(this.TakeBid_Click);
            // 
            // Worker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 523);
            this.Controls.Add(this.ManagementBox);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Worker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Worker";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BidList)).EndInit();
            this.ManagementBox.ResumeLayout(false);
            this.SearchBox.ResumeLayout(false);
            this.SearchBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView BidList;
        private System.Windows.Forms.GroupBox ManagementBox;
        private System.Windows.Forms.Button CompleteBid;
        private System.Windows.Forms.Button TakeBid;
        private System.Windows.Forms.Button ShelveBid;
        private System.Windows.Forms.Button History;
        private System.Windows.Forms.GroupBox SearchBox;
        private System.Windows.Forms.Button RefreshBid;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.DateTimePicker Date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NumberBid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LFP;
        private System.Windows.Forms.Label label3;
    }
}

