using System;
using System.Windows.Forms;

namespace Zapisnaya
{   
    /// <summary>
    /// Класс формы поиска
    /// </summary>
    public partial class SearchForm : Form
    {   
        /// <summary>
        /// Ссылка на главную форму
        /// </summary>
        MainForm mainForm;
        /// <summary>
        /// Инициализация формы поиска
        /// </summary>
        /// <param name="mainer">Ссылка на главную форму</param>
        public SearchForm(MainForm mainer)
        {
            InitializeComponent();
            mainForm = mainer;
        }
        /// <summary>
        /// Закрытие формы с сохраненением выделения кнопкой "Далее"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Закрытие формы с отменой выделения
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {   
            mainForm.ClearSelect();
            this.Close();
        }
        //Поиск и выделение при изменении данных в полях
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {   
            SearchYes(0);
        }
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            SearchYes(0);
        }
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            SearchYes(0);
        }
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            SearchYes(0);
        }
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            SearchYes(0);
        }
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SearchYes(1);
        }
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            SearchYes(1);
        }
        /// <summary>
        /// Ссылка на сортировку поиска по типу
        /// </summary>
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            SearchYes(1);
        }
        /// <summary>
        /// Функция формирования и сортировки запроса на поиск к главной форме
        /// </summary>
        /// <param name="mod">
        /// Тип поиска
        /// <list type="number">
        /// <item><term>0</term> точный поиск</item>
        /// <item><term>1</term> свободный поиск контактов</item>
        /// </list>
        /// </param>
        private void SearchYes(int mod)
        {
            mainForm.ClearSelect(); //Предварительно ссылка на метод очистки выделения таблицы контактов
            string[] zapros = new string[5]; //Инициализация запроса
            if (mod == 0) //тип 0 - точный поиск
            {
                //Запись данных в запрос
                zapros[0]= textBox1.Text;
                zapros[1] = textBox4.Text;
                zapros[2] = textBox7.Text;
                zapros[3] = textBox8.Text;
                zapros[4] = richTextBox1.Text;
                mainForm.SearchInData(zapros, mod); //Ссылка на метод поиска главной формы с передачей запроса и типа поиска
            }
            if (mod == 1) //тип 1 - свободный поиск
            {
                //Запись данных в запрос
                zapros[0] = textBox5.Text;
                zapros[1] = textBox6.Text;
                zapros[2] = textBox6.Text;
                zapros[3] = textBox6.Text;
                zapros[4] = richTextBox1.Text;
                mainForm.SearchInData(zapros, mod); //Ссылка на метод поиска главной формы с передачей запроса и типа поиска
            }
        }
    }
}
