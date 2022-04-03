using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Media;
using static Biblioteka.Class1;
using System.Threading;
using System.IO;

namespace Biblioteka
{
    public partial class Form2 : Form
    {
        static SpeechSynthesizer synth;
        C_Singelton s1 = C_Singelton.GetInstance();
        private int value = 100;
        //private int rate;
        public Form2()
        {
            InitializeComponent();
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SpeakCompleted += Synth_SpeakCompleted;
        }
        private void ReadlocalFile()
        {
            var open = new OpenFileDialog();

            open.ShowDialog();

            // Получить путь к файлу
            string path = open.FileName;

            if (path.Trim().Length == 0)
            {

                return;
            }

            var os = new StreamReader(path, Encoding.UTF8);
            string str = os.ReadToEnd();
            textBox1.Text = str;
        }
        //личное пустое пустое содержимое ToolStripMenuItem_Click(отправитель объекта, EventArgs e)
        //{
        //    textBox1.Text = "";
        //}

    private void button3_Click(object sender, EventArgs e)
        {
            //if (!textBox1.Text.Equals(String.Empty))
            //{
            //    synth.Speak(textBox1.Text);
            //}
            string text = textBox1.Text;

            if (text.Trim().Length == 0)
            {
                MessageBox.Show("Не удается прочитать пустой контент!","Сообщение об ошибке");
                return;
            }

            if (button3.Text == "Голос")
            {

                synth = new SpeechSynthesizer();

                new Thread(Speak).Start();

                button3.Text = "Остановить";

            }
            if(button3.Text == "Остановить")
            {

                synth.SpeakAsyncCancelAll(); // Прекратить чтение

                button3.Text = "Голос";
            }

        }
        private void Speak()
        {

            //synth.Rate = rate;
            //speech.SelectVoice("Microsoft Lili "); // Установить диктор (китайский)
            //speech.SelectVoice("Microsoft Anna "); // Английский
            synth.Volume = value;
            synth.SpeakAsync(textBox1.Text); // Метод чтения речи
            synth.SpeakCompleted += Synth_SpeakCompleted; // Событие привязки
        }

        private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            button3.Text = "Голос";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            if (openFileDialog1.FileName == String.Empty) return;
            try
            {
                var D = new System.IO.StreamReader(openFileDialog1.FileName, Encoding.GetEncoding("UTF-8"));
                textBox1.Text = D.ReadToEnd();
                D.Close();
            }
            catch (System.IO.FileNotFoundException D)
            {
                MessageBox.Show(D.Message + "нема файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (System.IO.IOException D)
            {
                MessageBox.Show(D.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = openFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var A=new System.IO.StreamWriter(saveFileDialog1.FileName,false,System.Text.Encoding.GetEncoding("UTF-8"));

                    A.Write(textBox1.Text);
                    A.Close();
                }


            catch (System.IO.IOException A)
                {
                    MessageBox.Show(A.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Clear();
            openFileDialog1.Filter = "текстовые файлы(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "текстовые файлы(*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            value = trackBar1.Value * 10;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.ReadlocalFile();
        }
    }
}
