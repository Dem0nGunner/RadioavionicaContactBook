using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Zapisnaya
{   
    /// <summary>
    /// Класс формы на добавление
    /// </summary>
    public partial class AddElementForm : Form
    {
        /// <summary>
        /// Поле шаблон контакта
        /// </summary>
        string[] elementStr = new string[5];
        /// <summary>
        /// Ссылка на главную форму
        /// </summary>
        MainForm mainForm;
        /// <summary>
        /// Инициализация формы на добавление
        /// </summary>
        /// <param name="mainer">Ссылка на главную форму</param>
        public AddElementForm(MainForm mainer)
        {
            InitializeComponent();
            mainForm = mainer;
        }
        /// <summary>
        /// Метод обавления контакта
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1) //по типу добавления 0
            {   
                string FIO = textBox1.Text+' '+textBox2.Text+' '+textBox3.Text;
                FIO = Regex.Replace(FIO, "  ", " ");
                if (FIO.Last() == ' ')
                    FIO.Remove(FIO.Length - 1);
                elementStr[0] = FIO;
                elementStr[1] = textBox4.Text;
                elementStr[2] = textBox7.Text;
                elementStr[3] = textBox8.Text;
                elementStr[4] = richTextBox1.Text;
            }
            else if (tabControl1.SelectedTab == tabPage2) //по типу добавления 1
            {
                elementStr[0] = textBox5.Text;
                elementStr[1] = textBox6.Text;
                elementStr[4] = richTextBox2.Text;
            }
            bool reductStatus = true;
            if (string.IsNullOrWhiteSpace(elementStr[0]))
            {
                elementStr[0] = elementStr[0];
                MessageBox.Show("Заполните данные имени контакта");
                reductStatus = false;
            }
            if (string.IsNullOrEmpty(elementStr[1])&& string.IsNullOrEmpty(elementStr[2])&& string.IsNullOrEmpty(elementStr[3]))
            {
                MessageBox.Show("Заполните контактные данные");
                reductStatus = false;
            }
            if (reductStatus)
                mainForm.AddElementInData(elementStr);
            elementStr= new string[5];
        }
        /// <summary>
        /// Метод закрытия формы при нажатии кнопки "Закрыть"
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
