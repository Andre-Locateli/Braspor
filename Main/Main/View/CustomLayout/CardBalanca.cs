using LiveCharts;
using LiveCharts.Wpf;
using Main.Model;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Main.View.CustomLayout
{
    public partial class CardBalanca : UserControl
    {

        public RedeClass rede;
        private EtiquetaClass etiqueta;

        private Thread thread;

        public event EventHandler LabelPesoChanged;
        public event EventHandler PesagemEndEvent;

        private string _labelPeso;
        public string LabelPesoTrigger
        {
            get { return _labelPeso; }
            set
            {
                if (_labelPeso != value)
                {
                    _labelPeso = value;
                    if (LabelPesoTrigger != null)
                    {
                        LabelPesoChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        private bool _pesagemEnd;
        public bool PesagemEnd
        {
            get { return _pesagemEnd; }
            set 
            { 
                _pesagemEnd = value;
                if (LabelPesoTrigger != null)
                {
                    PesagemEndEvent(this, EventArgs.Empty);
                }
            }
        }


        public bool Flag_pesando = false;
        public string OP = "";
        public string PRODUTO = "";
        private JsonPost infoPesagem = new JsonPost();
        public ProdutoGet _produto = new ProdutoGet();
        private bool flag_pesagem_aproximada = false;
        private decimal aux_peso = 0;
        public class JsonPost 
        {
            public string codigoProduto { get; set; }
            public string documento { get; set; }
            public string lote { get; set; }
            public string pesoReal { get; set; }
            public string pesoAlvo { get; set; }
            public string tolerencia { get; set; }
            public bool flag_sync { get; set; }

            public string descricao { get; set; }
        }

        public CardBalanca(RedeClass _rede, EtiquetaClass _etiqueta)
        {
            InitializeComponent();

            try
            {
                rede = _rede;
                etiqueta = _etiqueta;

                cartesianChart1.DisableAnimations = false;
                cartesianChart1.LegendLocation = LegendLocation.None;
                LiveCharts.SeriesCollection series = new LiveCharts.SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Produto",
                        Values = new ChartValues<double> { 0, 0, 0 },
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)),
                        PointGeometry = DefaultGeometries.Circle,
                        PointGeometrySize = 13,
                        PointForeground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(37, 145, 137)),
                        Fill = System.Windows.Media.Brushes.Transparent,
                        LineSmoothness =0.7,
                        StrokeThickness = 3,
                        IsHitTestVisible = true,
                    }
                };
                cartesianChart1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                cartesianChart1.Padding = new Padding(0);
                cartesianChart1.Series = series;

                lbl_name_balanca.Text = rede.nome;
                var timer = new Stopwatch();

                thread = new Thread(() =>
                {
                    while (true)
                    {
                        if (lbl_peso_indicador.IsHandleCreated) 
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                lbl_peso_indicador.Text = $"{Program.Registradores[rede.Id]}kg";
                                if (LabelPesoTrigger != null) 
                                {
                                    LabelPesoTrigger = $"{Program.Registradores[rede.Id]}kg";
                                }
                            });
                        }

                        if (Flag_pesando)
                        {
                            decimal valor_alvo = Convert.ToDecimal(_produto.peso_alvo);
                            decimal valor_max = _produto._peso_alvo_d + (_produto._peso_alvo_d * _produto.tolerencia_d);
                            decimal valor_min = _produto._peso_alvo_d - (_produto._peso_alvo_d * _produto.tolerencia_d);
                            decimal valor_atual = Convert.ToDecimal(lbl_peso_indicador.Text.Replace("kg", ""));

                            infoPesagem.pesoAlvo = valor_alvo.ToString();
                            infoPesagem.pesoReal = valor_atual.ToString();
                            infoPesagem.documento = OP;
                            infoPesagem.lote = _produto.lote;
                            infoPesagem.codigoProduto = _produto.descricao;
                            infoPesagem.tolerencia = _produto.tolerancia;
                            infoPesagem.descricao = _produto.descricao;

                            if (valor_atual > valor_max)
                            {
                                panel1.BackColor = Color.Red;
                                cartesianChart1.Series[0].Values[0] = Convert.ToDouble(valor_atual);
                                lblStatus.Invoke((MethodInvoker)delegate
                                {
                                    lblStatus.Text = "Fora do esperado";
                                });
                            }
                            else if (valor_atual == valor_alvo)
                            {
                                panel1.BackColor = Color.Green;
                                cartesianChart1.Series[0].Values[2] = Convert.ToDouble(valor_atual);
                                lblStatus.Invoke((MethodInvoker)delegate
                                {
                                    lblStatus.Text = "Aceitável";
                                });

                                if (timer.Elapsed.Seconds >= 10)
                                {
                                    TerminatePesagem(true,infoPesagem);                                    
                                    timer.Stop();
                                    flag_pesagem_aproximada = false;
                                    timer.Restart();
                                    timer = new Stopwatch();
                                }
                            }
                            else if (valor_atual >= valor_min)
                            {
                                panel1.BackColor = Color.Yellow;
                                cartesianChart1.Series[0].Values[1] = Convert.ToDouble(valor_atual);
                                lblStatus.Invoke((MethodInvoker)delegate
                                {
                                    lblStatus.Text = "Aproximado do valor";
                                });

                                if (flag_pesagem_aproximada == false)
                                {
                                    flag_pesagem_aproximada = true;
                                    timer.Start();
                                    aux_peso = valor_atual;
                                }

                                if (timer.Elapsed.Seconds >= 10)
                                {
                                    TerminatePesagem(true,infoPesagem);
                                    timer.Stop();
                                    flag_pesagem_aproximada = false;
                                    timer.Restart();
                                    timer = new Stopwatch();
                                }
                            }
                            else if (valor_atual < valor_min)
                            {
                                panel1.BackColor = Color.Transparent;
                                lblStatus.Invoke((MethodInvoker)delegate
                                {
                                    lblStatus.Text = "Aguardando peso";
                                });
                            }

                            if (aux_peso != valor_atual || valor_atual == 0 || valor_atual <= valor_min || valor_atual > valor_max)
                            {
                                aux_peso = valor_atual;
                                timer.Restart();
                            }

                            cartesianChart1.Invoke((MethodInvoker)delegate
                            {
                                cartesianChart1.Update();
                            });
                        }
                        Thread.Sleep(50);
                    }
                });
                thread.Start();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Inicia o processo de pesagem com base no produto e na op enviada como parametro.
        /// Preenche os campos e variaveis necessárias para execução.
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="_op"></param>
        public void InitializePesagem(ProdutoGet produto, string _op) 
        {
            try
            {
                OP = _op;
                Flag_pesando = true;

                this.Invoke((MethodInvoker)delegate 
                {
                    lblInfo.Text = $"Produto: {produto.descricao}\r\n\r\n" +
                    $"Lote: {produto.lote}\r\n\r\n" +
                    $"Peso Alvo: {produto.peso_alvo}\r\n\r\n" +
                    $"Peso Min: {produto._peso_alvo_d - (produto._peso_alvo_d * produto.tolerencia_d)}\r\n\r\n" +
                    $"Peso Max: {produto._peso_alvo_d + (produto._peso_alvo_d * produto.tolerencia_d)}\r\n\r\n" +
                    $"Tolerancia: {produto.tolerancia}%";
                });
                _produto = produto;
                PRODUTO = produto.descricao;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Função que finaliza o processo de pesagem, quando o parametro flag é igual a true.
        /// a função executa os comandos necessário quando é finalizado a pesagem.
        /// Quando o parametro é false, ele apenas cancelou e não finalizou a pesagem.
        /// </summary>
        /// <param name="flag"></param>
        public async void TerminatePesagem(bool flag, [Optional] JsonPost info)
        {
            try
            {
                Flag_pesando = false;

                if (flag)
                {
                    if (info != null) 
                    {
                        var post = await Program.HTTP.Post_Value("http://aephdo105481.protheus.cloudtotvs.com.br:4050/rest/VCDOC/pesagem", JsonConvert.SerializeObject(info));

                        Program.SQL.CRUDCommand("INSERT INTO Pesagem (NumeroOp, CodigoProduto, Descricao, PesoAlvo, PesoReal,Tolerencia,flag_sync, dateinsert)" +
                            " VALUES (@NumeroOp, @CodigoProduto, @Descricao, @PesoAlvo, @PesoReal, @Tolerencia, @flag_sync, @dateinsert)", "Pesagem",
                            new System.Collections.Generic.Dictionary<string, object>
                            {  { "@NumeroOp", info.documento },
                                { "@CodigoProduto", info.codigoProduto },
                                { "@Descricao", info.descricao },
                                { "@PesoAlvo", info.pesoAlvo },
                                { "@PesoReal", info.pesoReal },
                                { "@Tolerencia", info.tolerencia },
                                { "@flag_sync", post.ToString() },
                                { "@dateinsert", DateTime.Now} }); 
                    }

                    PesagemEnd = true;
                    //Aqui posso salvar todos os dados necessários e enviar para a impressora o Ticket

                }

                Task.Run( async() => 
                {
                    await Task.Delay(300);
                    _produto = new ProdutoGet();
                    cartesianChart1.Series[0].Values[0] = 0.00;
                    cartesianChart1.Series[0].Values[1] = 0.00;
                    cartesianChart1.Series[0].Values[2] = 0.00;

                    panel1.BackColor = Color.Transparent;

                    this.Invoke((MethodInvoker)delegate
                    {
                        lblInfo.Text = "";
                        lblStatus.Text = "";
                    });
                });

                OP = "";
                PRODUTO = "";

            }
            catch (Exception ex)
            {
            }
        }

        private void CardBalanca_Resize(object sender, EventArgs e)
        {
            try
            {
                LinearGradientBrush backgroundBrush = new LinearGradientBrush(
                    this.ClientRectangle,
                    Color.FromArgb(41, 46, 84),
                    Color.FromArgb(66, 74, 136),
                    LinearGradientMode.Vertical);

                Bitmap backgroundImage = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(backgroundImage))
                {
                    g.FillRectangle(backgroundBrush, this.ClientRectangle);
                }

                this.BackgroundImage = backgroundImage;
            }
            catch (Exception)
            {
            }
        }

    }
}
