using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Моя_картограмма
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.label1.Text = "Программа  Ассистент ДРИ разработана на участке НФИ ЦФДИ НВАТЭ." + "\n"+ " Автор:  Белобродский В.А. (belobrodsky@yandex.ru)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
