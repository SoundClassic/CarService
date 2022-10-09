using System;
using System.Windows.Forms;

namespace WorkerForm
{
    public partial class InputBox : Form
    {
        private static InputBox newInputBox;
        private static string returnString;


        public InputBox()
        {
            InitializeComponent();
        }

        public static string Show(string inputBoxText)
        {
            newInputBox = new InputBox();
            newInputBox.text.Text = inputBoxText;
            newInputBox.ShowDialog();
            return returnString;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            returnString = InputText.Text;
            newInputBox.Dispose();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            returnString = string.Empty;
            newInputBox.Dispose();
        }
    }
}
