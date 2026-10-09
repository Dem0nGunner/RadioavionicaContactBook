using System;
using System.Windows.Forms;

namespace Zapisnaya
{
    /// <summary>
    /// Класс вступительной формы
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Инициализация вступительной формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод открытия главной формы при нажатии на кнопку "Открыть книгу"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {   
            try
            {
                this.Hide(); //Скрыть вступительную форму
                MainForm mainForm = new MainForm(); //Инициализация главной формы
                mainForm.ShowDialog(); //Отображение главной формы
                this.Show(); //Возвращение к вступительной форме при закрытии главной
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Метод открытия формы инструкции при нажатии на кнопку "Инструкция" 
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); //Скрыть вступительную форму
            DiscriptionForm discriptionForm = new DiscriptionForm(); //Инициализация формы инструкции
            discriptionForm.ShowDialog(); //Отображение формы инструкции
            this.Show(); //Возвращение к вступительной форме при закрытии инструкции
        }
    }
}
