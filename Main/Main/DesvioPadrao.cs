using LiveCharts;
using Main.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class DesvioPadrao : Form
    {



        public List<double> lista_elementos = new List<double>();



        public DesvioPadrao()
        {
            InitializeComponent();
        }

        private void DesvioPadrao_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {

                lblMessage.Text = "";
                lista_elementos.Clear();

                //Trocar lista padrão por lista com o calculo estimado de uma folha.
                for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                {
                    lista_elementos.Add(SerialCommunicationService.indicador_addr[1].PB);
                    await Task.Delay(500);
                    lblMessage.Text += $"{SerialCommunicationService.indicador_addr[1].PB}\n";
                }

                decimal valor = 0;
                decimal dp = 0;
                decimal media = 0;

                foreach (decimal indice in lista_elementos)
                {
                    media += indice;
                }

                media = media / lista_elementos.Count;

                foreach (decimal individual in lista_elementos)
                {
                    valor += ((individual - media) * (individual - media)) / lista_elementos.Count;
                }


                dp = Sqrt(valor);

                decimal x_value = media *  (dp / 1000);

                decimal valor_final = Math.Round(media + x_value);

                lblMessage.Text += $"Quantidade Lista: {lista_elementos.Count}\n" +
                    $"Media dos Valores: {media}\n" +
                    $"DP: {dp}\n" +
                    $"VALOR FINAL: {valor_final}\n";

            }
            catch (Exception)
            {
            }
        }

        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
