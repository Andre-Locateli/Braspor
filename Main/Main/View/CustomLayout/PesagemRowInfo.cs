using LiveCharts.Dtos;
using Main.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Main.View.CustomLayout
{
    public partial class PesagemRowInfo : UserControl
    {
        private bool _pesagemFinalizada;
        public bool PesagemFinalizada
        {
            get { return _pesagemFinalizada; }
            set { _pesagemFinalizada = value; }
        }

        private int quantidadeAlvo { get; set; }
        public int quantidadeReal { get; set; }
        public double size { get; set; }
        private int _quantidadeSave { get; set; }

        private System.Drawing.Color COLOR_BORDER { get; set; }
        public PesagemRowInfo(ProdutoClass _produto, int _quantidadeAlvo, int _quantidadeReal=0)
        {
            InitializeComponent();
            ChangeElementColor(System.Drawing.Color.FromArgb(255, 102, 102));
            quantidadeAlvo = _quantidadeAlvo;
            quantidadeReal = _quantidadeReal;
            lblQtdReal.Text = $"{_quantidadeReal}";
            lblAlvo.Text = $"{_quantidadeAlvo}";
            
            size = Math.Round((panel1.Size.Width -7 - 5 * _quantidadeAlvo)* 1.0 / _quantidadeAlvo);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = (Panel)sender;
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }

        public void UpdateQuantity(int quantidadeAtual) 
        {
            try
            {
                if (quantidadeAtual > quantidadeReal)
                {
                    Console.WriteLine($"ADICIONEI {quantidadeAtual}");
                    while (quantidadeAtual > quantidadeReal)
                    {
                        if (quantidadeReal == quantidadeAlvo)
                        {
                            return;
                        }
                        this.Invoke(new MethodInvoker(delegate
                        {
                            f_l_box.Controls.Add(new Panel() { BackColor = COLOR_BORDER, Size = new System.Drawing.Size(Convert.ToInt32(size), this.Height), Margin = new Padding(0, 0, 5, 0) });
                        }));
                        quantidadeReal++;
                    }

                }

                else if (quantidadeAtual < quantidadeReal)
                {
                    Console.WriteLine($"REMOVI {quantidadeAtual}");
                    while (quantidadeAtual < quantidadeReal)
                    {
                        if (quantidadeAtual < 0)
                        {
                            quantidadeAtual = 0;
                        }
                        this.Invoke(new MethodInvoker(delegate
                        {
                            try
                            {
                                f_l_box.Controls.RemoveAt(0);
                                quantidadeReal--;
                            }
                            catch (Exception)
                            {

                            }
                        }));
                    }

                }

                if (quantidadeAtual == quantidadeAlvo)
                {
                    //ChangeElementColor(System.Drawing.Color.FromArgb(127, 213, 52));
                    PesagemFinalizada = true;
                }

                double valor = quantidadeReal;
                double Alvovalor = Math.Round(Convert.ToDouble(quantidadeAlvo) / 2);

                if (_quantidadeSave != quantidadeAtual) 
                { 
                    _quantidadeSave = quantidadeAtual;

                    if (valor < Alvovalor)
                    {
                        ChangeElementColor(System.Drawing.Color.FromArgb(255, 102, 102));
                    }
                    else if (quantidadeAtual == quantidadeAlvo)
                    {
                        ChangeElementColor(System.Drawing.Color.FromArgb(127, 213, 52));
                    }
                    else if (valor >= Alvovalor)
                    {
                        ChangeElementColor(System.Drawing.Color.FromArgb(255, 198, 64));
                    }
                }



                lblQtdReal.Invoke(new MethodInvoker(delegate 
                {
                    lblQtdReal.Text = $"{quantidadeReal}";
                }));

            }
            catch (Exception)
            {
            }
        }

        public void UpdateQuantityLayout(int quantidadeAtual)
        {
            try
            {
                if (quantidadeAtual > quantidadeReal)
                {
                    f_l_box.Controls.Clear();
                    while (quantidadeAtual > quantidadeReal)
                    {
                        if (quantidadeReal == quantidadeAlvo)
                        {
                            return;
                        }
                        this.Invoke(new MethodInvoker(delegate
                        {
                            f_l_box.Controls.Add(new Panel() { BackColor = COLOR_BORDER, Size = new System.Drawing.Size(Convert.ToInt32(size), this.Height), Margin = new Padding(0, 0, 5, 0) });
                        }));
                        quantidadeReal++;
                    }
                }

                else if (quantidadeAtual < quantidadeReal)
                {
                    while (quantidadeAtual < quantidadeReal)
                    {
                        if (quantidadeAtual < 0)
                        {
                            quantidadeAtual = 0;
                        }
                        this.Invoke(new MethodInvoker(delegate
                        {
                            try
                            {
                                f_l_box.Controls.RemoveAt(0);
                                quantidadeReal--;
                            }
                            catch (Exception)
                            {

                            }
                        }));
                    }

                }

                if (quantidadeAtual == quantidadeAlvo)
                {
                    //ChangeElementColor(System.Drawing.Color.FromArgb(127, 213, 52));
                    //PesagemFinalizada = true;
                }

                double valor = quantidadeReal;
                double Alvovalor = Math.Round(Convert.ToDouble(quantidadeAlvo) / 2);

             //   if (_quantidadeSave != quantidadeAtual)
            //    {
                  //  _quantidadeSave = quantidadeAtual;

                    if (valor < Alvovalor)
                    {
                        ChangeElementColor(System.Drawing.Color.FromArgb(255, 102, 102));
                    }
                    else if (quantidadeAtual == quantidadeAlvo)
                    {
                        ChangeElementColor(System.Drawing.Color.FromArgb(127, 213, 52));
                    }
                    else if (valor >= Alvovalor)
                    {
                        ChangeElementColor(System.Drawing.Color.FromArgb(255, 198, 64));
                    }
              //  }



                lblQtdReal.Invoke(new MethodInvoker(delegate
                {
                    lblQtdReal.Text = $"{quantidadeReal}";
                }));

            }
            catch (Exception)
            {
            }
        }

        public void PesagemRowInfo_Resize(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    size = Math.Round((panel1.Size.Width - 7 - 5 * quantidadeAlvo) * 1.0 / quantidadeAlvo);
                    UpdateQuantityLayout(0);
                    UpdateQuantity(_quantidadeSave);
                    UpdateQuantityLayout(_quantidadeSave);
                }));
           
            }
            catch (Exception)
            {
            }
        }

        private void ChangeElementColor(System.Drawing.Color _color) 
        {
            try
            {
                COLOR_BORDER = _color;
                panel1.Invalidate();
                f_l_box.Invalidate();

                foreach (Panel pn in f_l_box.Controls)
                {
                    pn.BackColor = COLOR_BORDER;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
