using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zapisnaya
{   
    /// <summary>
    /// Класс формы инструкции
    /// </summary>
    public partial class DiscriptionForm : Form
    {   
        /// <summary>
        /// Инициализация формы инструкции
        /// </summary>
        public DiscriptionForm()
        {
            InitializeComponent();
            //Выравнивание по краям формы
            tabControl1.Size = new Size(this.Size.Width - 14, this.Size.Height - 40);
            pictureBox1.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox2.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox3.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox4.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox5.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox6.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox7.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox8.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
        }
        /// <summary>
        /// Метод выравнивание элементов при изменении размера окна инструкции
        /// </summary>
        private void DiscriptionForm_Resize(object sender, EventArgs e)
        {
            //Выравнивание по краям формы
            tabControl1.Size = new Size(this.Size.Width-14, this.Size.Height-40);
            pictureBox1.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox2.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox3.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox4.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox5.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox6.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox7.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
            pictureBox8.Size = new Size(this.Size.Width - 14, this.Size.Height - 65);
        }
    }
}
