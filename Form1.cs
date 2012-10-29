using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KR_ASOIU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Height = 414;
            this.Width = 714;
           
            int N = Convert.ToInt32(textBox6.Text); // Число РС
            double T0 = Convert.ToDouble(textBox5.Text);        // Вр. дооработки запроса на РС
            double Tp = Convert.ToDouble(textBox4.Text);        // Вр. формирования запроса на РС
            int C = Convert.ToInt32(textBox7.Text);      // Кол-во процессоров сервера
            double Tn = Convert.ToDouble(textBox10.Text);        // Вр. обработки запроса в процессоре
            int M = Convert.ToInt32(textBox9.Text);      // Кол-во дисков сервера;
            double Td = Convert.ToDouble(textBox8.Text);        // Вр. обработки запроса в сервере
            double Pii;       // Вер-ть обращения к i-му диску
            double G;         // Вер-ть пост. з-са после обр-ки снова в процессор

            double b;
            double v, v1, v2;   //интенсивность фонового потока
            double TnL;
            double TdL;
            double L, L1, L2, Ld, D1;

            double TTp;       // Время цикла системы
            double Ppc;       //загрузка РС
            double Pn;        //Загрузка процессора
            double Pd;        //Загрузка диска
            double Pu;        //Загрузка пользователя РС
            double Tr;        //Время реакции системы
            double Nw;
            double Np;
            int i;
            double K1 = Convert.ToDouble(textBox1.Text);
            double K2 = Convert.ToDouble(textBox2.Text);
            double delta = Convert.ToDouble(textBox3.Text);     //коэф-ты
            double temp;
           

             if (M!=0) {
                 Pii = 1 /(double)M;
             }
                 else Pii=0;

              G=0;
              b=(1-G); //b=1

              v1=C/(b*Tn);     //интенсивность фонового потока запросов в процессоры
              v2=1/(b*Pii*Td); //интенсивность фонового потока запросов в диски

              if (v1<v2) {
                  v=v1;
              }
              else v=v2;

              L2=K1*v*(N-1)/N; //среднее значение суммарной интенсивности
              
            
              do{
                L1=L2;

                TnL = Tn * b / (1 - Math.Pow(L1*Tn/C, C));  //процессорное время
                TdL=Td*b*Pii/(1-b*Pii*L1*Td);       //время дисков

                L=(N-1)/(T0+Tp+(TnL+TdL));
                D1=Math.Abs(L1-L)/K2;
                L2=L1-D1;
                i = 1;
                i=i+1;             //количество итераций
                temp = Math.Abs((L1 - L) / L);
              }
              while (temp > delta);

          
              TTp=T0+Tp+(TnL+TdL);
              Ppc=(T0+Tp)/TTp;
              Ld=N/TTp;
              Pn=(Tn*b*Ld)/C;
              Pd=b*Td*Pii*Ld;
              Pu=Tp/TTp;
              Tr=TTp-Tp;
              Nw=N*((T0+Tp)/TTp);  //количество работающих PC
              Np=N*(T0/TTp);      //количество PC, формир. запрос

              //Вывод
            textBox14.Text = Convert.ToString(Ppc);
            textBox13.Text = Convert.ToString(Pn);
            textBox12.Text = Convert.ToString(Pd);
            textBox11.Text = Convert.ToString(TTp);
            textBox18.Text = Convert.ToString(Pu);
            textBox17.Text = Convert.ToString(Tr);
            textBox16.Text = Convert.ToString(Nw);
            textBox15.Text = Convert.ToString(Np);
          //  MessageBox.Show(Convert.ToString(M));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
      
    }
}
