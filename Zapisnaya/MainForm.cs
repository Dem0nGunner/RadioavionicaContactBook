using System;
using System.Windows.Forms;

namespace Zapisnaya
{   
    /// <summary>
    /// Класс главной формы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Поле с путём к файлу (может меняться по исполнению)
        /// </summary>
        private string fileName;
        /// <summary>
        /// Объект, осуществляющий работу с файлами excel
        /// </summary>
        private ExcelFileHelper filehelper = new ExcelFileHelper();
        /// <summary>
        /// Инициализация главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.Size = new System.Drawing.Size(this.Width-20,this.Height-100); //Сведение размеров таблицы
            //Инициализация объектов таблицы и подготовка прочих элементов, диактивация кнопок
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;
            toolStripButton4.Enabled = false;
            DelToolStripMenuItem.Enabled = false;
            DelAllToolStripMenuItem.Enabled = false;
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Имя";
            dataGridView1.Columns[1].Name = "Телефон/Несортированные контакты";
            dataGridView1.Columns[2].Name = "Местный телефон";
            dataGridView1.Columns[3].Name = "Эл-почта";
            dataGridView1.Columns[4].Name = "Комментарии";
        }
        /// <summary>
        /// Изменение элементов формы при изменении её размеров
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new System.Drawing.Size(this.Width-20, this.Height - 100);
        }
        /// <summary>
        /// Метод открытие файла по кнопке "Открыть"
        /// </summary>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //Вызов диалогового окна проводника
            openFileDialog.Filter = "Лист Excel (*.xlsx)|*.xlsx|Лист Excel (*.xls)|*.xls|Все файлы (*.*)|*.*"; //Фильтр данных
            if (openFileDialog.ShowDialog() == DialogResult.OK) //ответ от диалогового окна получен
                try
                {   
                    int Sela = dataGridView1.Rows.Count; //Подготовка кол-ва строк
                    for (int i = 0; i < Sela; i++) //по количеству строк таблицы
                        dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count-1]); //Предварительная очистка строк таблицы
                    string filePath = openFileDialog.FileName; //Запись пути к открываемому файлу
                    filehelper.OpenFile(filePath); //Вызов метода открытия файла класса ExcelFileHelper
                    string[,] addTable = filehelper.GetTable(); //Получение открытой таблицы вызовом метода класса ExcelFileHelper
                    dataGridView1.ColumnCount = addTable.GetLength(0); //На случай счёта некоректной ширины таблиц они приравниваются
                    for (int i = 0; i < addTable.GetLength(1); i++) //по строкам
                    {
                        string[] Row = new string[dataGridView1.ColumnCount]; //Предварительная инициализация массива строки таблицы
                        for (int j = 0; j < addTable.GetLength(0);j++) //по столбцам
                            Row[j] = addTable[j,i]; //Приравнивание элемента таблицы excel к элементу таблицы контактов
                        dataGridView1.Rows.Add(Row); //Добавление полной строки в таблицу
                    }
                    //Открытие возможности работы с данными при их наличии
                    toolStripButton2.Enabled = true;
                    toolStripButton3.Enabled = true;
                    toolStripButton4.Enabled = true;
                    DelToolStripMenuItem.Enabled=true;
                    DelAllToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex){MessageBox.Show(ex.Message);}
        }
        /// <summary>
        /// Метод открыти сохранения в новый файл по кнопке "Сохранить как"
        /// </summary>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); //Инициализация диалогового окна для сохранения файла
            saveFileDialog.Filter = "Лист Excel (*.xlsx)|*.xlsx|Лист Excel (*.xls)|*.xls"; //Шаблон фильтра сохранения файла
            if (saveFileDialog.ShowDialog() == DialogResult.OK) //ответ от диалогового окна получен
                try
                {
                    string filePath = saveFileDialog.FileName; //Запись пути сохранения файла
                    string[,] saveTable = new string[dataGridView1.ColumnCount, dataGridView1.RowCount]; //Инициализация для передачи таблицы
                    for (int i = 0; i < dataGridView1.ColumnCount; i++) //по колоннам
                        for (int j = 0; j < dataGridView1.RowCount; j++) //по строкам
                            saveTable[i, j] = dataGridView1[i,j].Value.ToString(); //Запись в передаваемую таблицу
                    filehelper.SaveAsFile(filePath, saveTable); //Метод сохранения в файл таблицы класса ExcelFileHelper
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// Метод сохранения открытого файла по нажатию кнопки "Сохранить"
        /// </summary>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string[,] saveTable = new string[dataGridView1.ColumnCount, dataGridView1.RowCount]; //Инициализация для передачи таблицы
                for (int i = 0; i < dataGridView1.ColumnCount; i++) //по колонкам
                    for (int j = 0; j < dataGridView1.RowCount; j++) //по строкам
                        if (dataGridView1[i, j].Value == null) //клетка пуста
                            saveTable[i, j] = ""; //Запись пустой строки вместо null
                        else //не пусто
                            saveTable[i, j] = dataGridView1[i, j].Value.ToString(); //Запись клетки
                filehelper.SaveFile(saveTable); //Вызов метода сохранения открытого файла
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// Метод открытия формы поиска по нажатию кнопки "Поиск"
        /// </summary>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(this); //Инициализация формы поиска
            searchForm.ShowDialog(); //Вызов формы поиска
        }
        /// <summary>
        /// Метод вызова формы добавления элемента по нажатию кнопки "Добавить контакт"
        /// </summary>
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddElementForm AddForm = new AddElementForm(this); //Инициализация формы добавления
            AddForm.ShowDialog(); //Вызов формы добавления
        }
        /// <summary>
        /// Метод удаления выделенных элементов по кнопке "Удалить выделенный элемент"
        /// </summary>
        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Sela = dataGridView1.SelectedRows.Count;//Подсчёт выделенных строк
            for (int i = 0; i < Sela; i++) //по выделенным строкам
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]); //Удаление выделенных строк
            //Диактивация элементов, зависящих от наличия контактов
            if (dataGridView1.Rows.Count == 0)//нет элементов в таблице
            {
                DelToolStripMenuItem.Enabled = false;
                DelAllToolStripMenuItem.Enabled = false;
                toolStripButton4.Enabled = false;
            }
        }
        /// <summary>
        /// Метод удаления все элементов по кнопке "Удалить всё"
        /// </summary>
        private void DelAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Sela = dataGridView1.Rows.Count; //Подготовка щётчика строк
            for (int i = 0; i < Sela; i++) //по строкам
                dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count-1]); //Удаление строки
            DelToolStripMenuItem.Enabled = false;
            DelAllToolStripMenuItem.Enabled = false;
            toolStripButton4.Enabled = false;
        }
        /// <summary>
        /// Метод добавления контакта
        /// </summary>
        /// <param name="element">Шаблон контакта</param>
        public void AddElementInData(string[] element)
        {
            dataGridView1.Rows.Add(element); //Запись контакта в конец таблицы
            //Активация элементов, зависящих от наличия строк
            DelToolStripMenuItem.Enabled = true;
            DelAllToolStripMenuItem.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = true;
        }
        /// <summary>
        /// Метод поиска и выделения контактов в таблице
        /// </summary>
        /// <param name="element">Шаблон поиска</param>
        /// <param name="mod">Тип поиска
        /// <list type="number">
        /// <item><term>0</term> точный поиск</item>
        /// <item><term>1</term> свободный поиск контактов</item>
        /// </list></param>
        public void SearchInData(string[] element,int mod)
        {
            if (mod == 0) //тип 0
                for (int i = 0; i < dataGridView1.Rows.Count; i++) //по строкам
                {
                    int[] tiket = new int[dataGridView1.Columns.Count]; //Инициализация пропускного билета
                    for (int j = 0; j < dataGridView1.Columns.Count; j++) //по колонкам
                        //Запись пропускного билета
                        if (dataGridView1[j, i] != null && !string.IsNullOrEmpty(element[j]))
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(element[j]))
                                tiket[j] = 1;
                            else
                                tiket[j] = 0;
                        else
                            tiket[j] = -1;
                    int right_ticket = -1; //Состояния пропускного билета
                    for (int j = 0; j < tiket.Length; j++)
                        if (tiket[j] != -1)
                            if (tiket[j] == 1) { right_ticket = 1; }
                            else { right_ticket = 0; break; }
                    if (right_ticket == 1)
                        dataGridView1.Rows[i].Selected = true; //Выделение найденного контакта
                }
            if (mod == 1) //тип 1
                for (int i = 0; i < dataGridView1.Rows.Count; i++) //по строкам
                {
                    int[] tiket = new int[dataGridView1.Columns.Count]; //Инициализация пропускного билета
                    for (int j = 0; j < dataGridView1.Columns.Count; j++) //по колонкам
                        if (dataGridView1[j, i] != null && !string.IsNullOrEmpty(element[j]))
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(element[j]))
                                tiket[j] = 1;
                            else
                                tiket[j] = 0;
                        else { tiket[j] = -1; }
                    int right_ticket = -1; //Состояния пропускного билета
                    for (int j = 0; j < tiket.Length; j++)
                        if (tiket[j] != -1)
                            if (tiket[j] == 1 && j != 1 && j != 2 && j != 3) { right_ticket = 1; }
                            else if (j == 1 || j == 2 || j == 3) { }
                            else { right_ticket = 0; break; }
                    if (right_ticket == -1)
                        for (int j = 1; j < 4; j++)
                        { if (tiket[j] != 1) { right_ticket = 0; } else { right_ticket = 1; break; } }
                    if (right_ticket == 1)
                        dataGridView1.Rows[i].Selected = true; //Выделение найденного контакта
                }
        }
        /// <summary>
        /// Метод удаления выделения
        /// </summary>
        public void ClearSelect()
        {
            dataGridView1.ClearSelection(); //Удаление всех выделений в таблице
        }
        /// <summary>
        /// Метод закрытия мини-приложения excel при закрытии формы
        /// </summary>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            filehelper.Dispose(); //Вызов метода закрытия Excel класса ExcelFileHelper
        }
        /// <summary>
        /// Метод обработки горячих клавиш
        /// </summary>
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.E) && (e.Modifiers == Keys.Control)&&(AddToolStripMenuItem.Enabled==true)) { AddToolStripMenuItem_Click(sender, e); }
            if ((e.KeyCode == Keys.Delete) &&(e.Modifiers == Keys.Shift) && (DelAllToolStripMenuItem.Enabled == true)) { DelAllToolStripMenuItem_Click(sender, e); }
            if ((e.KeyCode == Keys.O) && (e.Modifiers == Keys.Control) && (toolStripButton1.Enabled == true)) { toolStripButton1_Click(sender, e); }
            if ((e.KeyCode == Keys.A) && (e.Modifiers == Keys.Control) && (toolStripButton2.Enabled == true)) { toolStripButton2_Click(sender, e); }
            if ((e.KeyCode == Keys.S) && (e.Modifiers == Keys.Control) && (toolStripButton3.Enabled == true)) { toolStripButton3_Click(sender, e); }
            if ((e.KeyCode == Keys.P) && (e.Modifiers == Keys.Control) && (toolStripButton4.Enabled == true)) { toolStripButton4_Click(sender, e); }
        }
    }
}
