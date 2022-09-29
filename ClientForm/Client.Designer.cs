namespace ClientForm
{
    partial class Client
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
            this.FillBox = new System.Windows.Forms.GroupBox();
            this.BidBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LFP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BrandsList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TypeWork = new System.Windows.Forms.ComboBox();
            this.FindBid = new System.Windows.Forms.Button();
            this.CreateBid = new System.Windows.Forms.Button();
            this.FindBox = new System.Windows.Forms.GroupBox();
            this.NumberBid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BidList = new System.Windows.Forms.DataGridView();
            this.FillBox.SuspendLayout();
            this.BidBox.SuspendLayout();
            this.FindBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BidList)).BeginInit();
            this.SuspendLayout();
            // 
            // FillBox
            // 
            this.FillBox.Controls.Add(this.CreateBid);
            this.FillBox.Controls.Add(this.TypeWork);
            this.FillBox.Controls.Add(this.label3);
            this.FillBox.Controls.Add(this.BrandsList);
            this.FillBox.Controls.Add(this.label2);
            this.FillBox.Controls.Add(this.LFP);
            this.FillBox.Controls.Add(this.label1);
            this.FillBox.Location = new System.Drawing.Point(10, 10);
            this.FillBox.Name = "FillBox";
            this.FillBox.Size = new System.Drawing.Size(300, 300);
            this.FillBox.TabIndex = 0;
            this.FillBox.TabStop = false;
            this.FillBox.Text = "Заполнить заявку (Создать)";
            // 
            // BidBox
            // 
            this.BidBox.Controls.Add(this.BidList);
            this.BidBox.Location = new System.Drawing.Point(320, 10);
            this.BidBox.Name = "BidBox";
            this.BidBox.Size = new System.Drawing.Size(500, 500);
            this.BidBox.TabIndex = 1;
            this.BidBox.TabStop = false;
            this.BidBox.Text = "Заявки";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО:";
            // 
            // LFP
            // 
            this.LFP.Location = new System.Drawing.Point(10, 60);
            this.LFP.Name = "LFP";
            this.LFP.Size = new System.Drawing.Size(280, 30);
            this.LFP.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Марка автомобиля:";
            // 
            // BrandsList
            // 
            this.BrandsList.FormattingEnabled = true;
            this.BrandsList.Location = new System.Drawing.Point(10, 130);
            this.BrandsList.Name = "BrandsList";
            this.BrandsList.Size = new System.Drawing.Size(280, 30);
            this.BrandsList.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Вид работы:";
            // 
            // TypeWork
            // 
            this.TypeWork.FormattingEnabled = true;
            this.TypeWork.Location = new System.Drawing.Point(10, 200);
            this.TypeWork.Name = "TypeWork";
            this.TypeWork.Size = new System.Drawing.Size(280, 30);
            this.TypeWork.TabIndex = 5;
            // 
            // FindBid
            // 
            this.FindBid.Location = new System.Drawing.Point(10, 130);
            this.FindBid.Name = "FindBid";
            this.FindBid.Size = new System.Drawing.Size(280, 51);
            this.FindBid.TabIndex = 6;
            this.FindBid.Text = "Найти заявку";
            this.FindBid.UseVisualStyleBackColor = true;
            // 
            // CreateBid
            // 
            this.CreateBid.Location = new System.Drawing.Point(10, 240);
            this.CreateBid.Name = "CreateBid";
            this.CreateBid.Size = new System.Drawing.Size(280, 51);
            this.CreateBid.TabIndex = 7;
            this.CreateBid.Text = "Создать заявку";
            this.CreateBid.UseVisualStyleBackColor = true;
            // 
            // FindBox
            // 
            this.FindBox.Controls.Add(this.label5);
            this.FindBox.Controls.Add(this.label4);
            this.FindBox.Controls.Add(this.NumberBid);
            this.FindBox.Controls.Add(this.FindBid);
            this.FindBox.Location = new System.Drawing.Point(10, 320);
            this.FindBox.Name = "FindBox";
            this.FindBox.Size = new System.Drawing.Size(300, 190);
            this.FindBox.TabIndex = 2;
            this.FindBox.TabStop = false;
            this.FindBox.Text = "Найти заявку";
            // 
            // NumberBid
            // 
            this.NumberBid.Location = new System.Drawing.Point(10, 60);
            this.NumberBid.Name = "NumberBid";
            this.NumberBid.Size = new System.Drawing.Size(280, 30);
            this.NumberBid.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 22);
            this.label4.TabIndex = 8;
            this.label4.Text = "Номер заявки:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(10, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(270, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "*Если заявка не найдена, создайте заявку";
            // 
            // BidList
            // 
            this.BidList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BidList.Location = new System.Drawing.Point(7, 30);
            this.BidList.Name = "BidList";
            this.BidList.RowHeadersWidth = 51;
            this.BidList.RowTemplate.Height = 24;
            this.BidList.Size = new System.Drawing.Size(480, 460);
            this.BidList.TabIndex = 0;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 523);
            this.Controls.Add(this.FindBox);
            this.Controls.Add(this.BidBox);
            this.Controls.Add(this.FillBox);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client";
            this.FillBox.ResumeLayout(false);
            this.FillBox.PerformLayout();
            this.BidBox.ResumeLayout(false);
            this.FindBox.ResumeLayout(false);
            this.FindBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BidList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FillBox;
        private System.Windows.Forms.GroupBox BidBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LFP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox BrandsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TypeWork;
        private System.Windows.Forms.Button CreateBid;
        private System.Windows.Forms.Button FindBid;
        private System.Windows.Forms.GroupBox FindBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NumberBid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView BidList;
    }
}

