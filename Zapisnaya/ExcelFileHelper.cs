using Microsoft.Office.Interop.Excel; //Импорт Excel-модуля
using System;
using System.IO;

namespace Zapisnaya
{
    /// <summary>
    /// Класс-помошник для работы с таблицами и файлами Excel
    /// <list type="table">
    /// <item><strong>Методы:</strong></item>
    /// <item><term>OpenFile(filepath)</term> открытие файла</item>
    /// <item><term>GetTable()</term> получение массива из файла</item>
    /// <item><term>SaveAsFile(filepath,table)</term> сохранение в новый файл</item>
    /// <item><term>SaveFile(table)</term> сохранение в открытый файл</item>
    /// <item><term>Dispose()</term> закрытие Excel</item>
    /// </list>
    /// </summary>
    internal class ExcelFileHelper : IDisposable
    {
        ///<summary>
        ///Поле приложения Excel
        ///(Хранит данные о мини-приложении Excel, используемого для работы)
        ///</summary>
        private Application m_objExcel;
        /// <summary>
        /// Поле книги/файла Excel
        /// </summary>
        private Workbook m_workbook;
        /// <summary>
        /// Конструктор класса по работе с файлами Excel
        /// </summary>
        public ExcelFileHelper()
        {
            m_objExcel = new Application(); //Инизиализация и запуск мини-приложения
            m_workbook = m_objExcel.Workbooks.Add(); //Создание нового файла
        }
        /// <summary>
        /// Метод открытия файла
        /// </summary>
        /// <param name="FilePath">Путь к файлу Excel</param>
        /// <returns><list type="table">
        /// <item>true - файл открыт</item>
        /// <item>false - ошибка открытия</item>
        /// </list></returns>
        internal bool OpenFile(string FilePath)
        {
            try
            {   
                if (File.Exists(FilePath)) //Файл существует
                {
                    m_objExcel.Workbooks.Close(); //Закрыть предыдущий лист, если он есть
                    m_workbook = m_objExcel.Workbooks.Open(FilePath,Editable:true); //Открыть файл
                }
                else //Файла не существует
                    m_workbook = m_objExcel.Workbooks.Add(); //Создать файл
                return true;
            }
            catch (Exception ex) { return false; }
        }
        /// <summary>Метод получения таблицы из Excel</summary>
        /// <returns>Таблица <strong>6-колонн X N-строк</strong></returns>
        internal string[,] GetTable()
        {
            Worksheet m_workSheet = (Worksheet)m_objExcel.ActiveSheet; //Выбор активной таблицы (листа) в файле
            var lastet = m_workSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell); //Список последней особой строки и столбца
            string[,] table = new string[5,lastet.Row]; //Инициализация пустой таблицы 6хn
            for (int i = 0; i <5; i++) //по столбцам
                for (int j = 0; j < lastet.Row; j++) //по строкам
                    table[i, j] = m_workSheet.Cells[j+1, i+1].Text; //Перенос содержимого из клетки excel таблицы в string[,]table
            return table; //Возврат заполненной таблицы 6-колонн X N-строк
        }
        /// <summary>
        /// Сохраняет таблицу в новый файл
        /// </summary>
        /// <param name="FilePath">Путь к файлу</param>
        /// <param name="table">Массив таблицы</param>
        /// <returns><list type="table">
        /// <item>true - файл сохранён</item>
        /// <item>false - ошибка сохранения</item>
        /// </list></returns>
        internal bool SaveAsFile(string FilePath, string[,]table)
        {
            try
            {
                Worksheet m_workSheet = (Worksheet)m_workbook.ActiveSheet; //Выбор активной таблицы (листа) в файле
                m_workSheet.Rows.Clear(); //Очистка таблицы excel перед заполнением
                for (int i = 0; i < table.GetLength(0); i++) //по строкам
                    for (int j = 0; j < table.GetLength(1); j++) //по столбцам
                        m_workSheet.Cells[j+1,i+1].Value = table[i, j]; //Перенос значений из string[,]table в excel таблицу
                m_workbook.SaveAs(FilePath); //Сохранение файла по пути
                return true;
            }
            catch (Exception ex) { return false; }
        }
        /// <summary>
        /// Сохраняет таблицу в открытый файл
        /// </summary>
        /// <param name="table">Массив таблицы</param>
        /// <returns><list type="table">
        /// <item>true - файл сохранён</item>
        /// <item>false - ошибка сохранения</item>
        /// </list></returns>
        internal bool SaveFile(string[,] table)
        {
            try
            {
                m_workbook = (Workbook)m_objExcel.ActiveWorkbook; //Выбор активного файла (книги) excel
                Worksheet m_workSheet = (Worksheet)m_workbook.ActiveSheet;  //Выбор активной таблицы (листа) в файле excel
                for (int i = 0; i < table.GetLength(0); i++) //по строкам
                    for (int j = 0; j < table.GetLength(1); j++) //по столбцам
                        m_workSheet.Cells[j + 1, i + 1].Value = table[i, j]; //Перенос значений из string[,]table в excel таблицу
                m_workbook.Save(); //Сохранение файла
                return true;
            }
            catch (Exception ex) { return false; }
        }
        /// <summary>
        /// Закрытие Excel
        /// </summary>
        public void Dispose()
        {   
            if (m_workbook!=null) //Приложение содержит файл
                m_workbook.Close(); //Закрыть файл перед завершением процесса
            m_objExcel.Quit(); //Завершение процесса мини-приложения
        }
    }
}
