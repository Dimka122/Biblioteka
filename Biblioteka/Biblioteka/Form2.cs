﻿using System;
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

namespace Biblioteka
{
    public partial class Form2 : Form
    {
        static SpeechSynthesizer synth;
        C_Singelton s1 = C_Singelton.GetInstance();
        public Form2()
        {
            InitializeComponent();
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SpeakCompleted += Synth_SpeakCompleted;
        }
        private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            MessageBox.Show("Speech end", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(String.Empty))
            {
                synth.Speak(textBox1.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            if (openFileDialog1.FileName == String.Empty) return;
            try
            {
                var D=new System.IO.StreamReader(openFileDialog1.FileName,Encoding.GetEncoding("UTF-8"));
                textBox1.Text=D.ReadToEnd();
                D.Close();
            }
            catch(System.IO.FileNotFoundException A)
            {
                MessageBox.Show(A.Message+"нема файла","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            catch (System.IO.IOException A)
            {
                MessageBox.Show(A.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = openFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var D=new System.IO.StreamWriter(saveFileDialog1.FileName,false,System.Text.Encoding.GetEncoding("UTF-8"));

                    D.Write(textBox1.Text);
                    D.Close();
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
    }
}
