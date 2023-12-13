using Main.Model;
using Main.View.CustomLayout;
using Main.View.PopupFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace Main.View.CadastroFolder
{
    public partial class CadastroNovaReceita : Form
    {

        public event EventHandler ItemSelecionadoTrigger;

        private string _itemSelecionado;
        public string ItemSelecionado
        {
            get { return _itemSelecionado; }
            set
            {
                if (_itemSelecionado != value)
                {
                    _itemSelecionado = value;
                    if (ItemSelecionado != null)
                    {
                        if (ItemSelecionadoTrigger != null)
                        {
                            ItemSelecionadoTrigger(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private bool type_moviment = true;
        
        public CadastroNovaReceita()
        {
            InitializeComponent();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            type_moviment = true;
            pn_produtos.AllowDrop = true;
            pn_bandeja.AllowDrop = true;
            pn_recipiente.AllowDrop = true;
            pn_bandeja_escolhida.Tag = new BandejaClass();
            load_f_l_panels();

            btnAdicionar.Enabled = Program._permissaoUsuario.receita_add;
        }

        private ReceitaClass _receita_atual { get; set; }
        public CadastroNovaReceita(ReceitaClass rec)
        {
            InitializeComponent();

            lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
            lblUsuario.Text = Program._usuarioLogado.Nome;
            lblAcesso.Text = Program._usuarioLogado.Acesso;

            type_moviment = false;
            _receita_atual = rec;   
            pn_produtos.AllowDrop = true;
            pn_bandeja.AllowDrop = true;
            pn_recipiente.AllowDrop = true;
            pn_bandeja_escolhida.Tag = new BandejaClass();
            load_f_l_panels();

            txtCodigo.Text = rec.Codigo;
            txtNome.Text = rec.Nome;
            txtQuantidadePecas.Text = rec.Quantidade_pecas.ToString();
            txtQuantidadeBandeja.Text = rec.Quantidade_bandejas.ToString();
            txtpkskf.Text = rec.PKSKF;

            ProdutoClass prod = (ProdutoClass)Program.SQL.SelectObject("SELECT * FROM Produto WHERE Id = @Id", "Produto", new Dictionary<string, object>()
            {
                {"@Id", rec.Id_Produto}
            });
            Label lblProdutoInfo = new Label()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(5, 12, 5, 5),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Regular)
            };
            lblProdutoInfo.Text = $"Código: {prod.Part_number}\n" +
                                  $"Descrição: {prod.Descricao}\n" +
                                  $"Peso Alvo: {prod.PesoAlvo}\n" +
                                  $"Tolerancia: {prod.Tolerancia}\n";
            pn_produto_selecionado.Controls.Clear();
            pn_produto_selecionado.Controls.Add(lblProdutoInfo);
            pn_produto_selecionado.Tag = prod;

            BandejaClass band = (BandejaClass)Program.SQL.SelectObject("SELECT * FROM Bandeja WHERE Id = @Id", "Bandeja", new Dictionary<string, object>()
            {
                {"@Id", rec.Id_Bandeja}
            });
            if (band != null) 
            {
                Label lblbandeja = new Label() { Dock = DockStyle.Fill, Padding = new Padding(5, 12, 5, 5), ForeColor = Color.White, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
                lblbandeja.Text = $"Código: {band.Codigo}\n" +
                                      $"Descrição: {band.Descricao}\n" +
                                      $"Peso Alvo: {band.PesoAlvo}\n" +
                                      $"Tolerancia: {band.Tolerancia}\n";
                pn_bandeja_escolhida.Controls.Clear();
                pn_bandeja_escolhida.Controls.Add(lblbandeja);
                pn_bandeja.Tag = band;
            }
            RecipienteClass recipi = (RecipienteClass)Program.SQL.SelectObject("SELECT * FROM Recipiente WHERE Id = @Id", "Recipiente", new Dictionary<string, object>()
            {
                {"@Id", rec.Id_Recipiente}
            });
            Label lblRecipienteInfo = new Label() { Dock = DockStyle.Fill, Padding = new Padding(5, 12, 5, 5), ForeColor = Color.White, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            lblRecipienteInfo.Text = $"Código: {recipi.Package}\n" +
                          $"Descrição: {recipi.Descricao}\n" +
                          $"Peso Alvo: {recipi.PesoAlvo}\n" +
                          $"Tolerancia: {recipi.Tolerancia}\n";
            pn_recipiente_escolhida.Controls.Clear();
            pn_recipiente_escolhida.Controls.Add(lblRecipienteInfo);
            pn_recipiente_escolhida.Tag = recipi;

            btnAdicionar.Text = "Editar";
        }

        private void CadastroNovaReceita_Resize(object sender, EventArgs e)
        {
            this.Update();
            this.Refresh();
        }

        private void Tempp_ItemSelecionadoTrigger(object sender, EventArgs e)
        {
            try
            {
                this.Invalidate();
                RowCardCustom itemSelecionado = (RowCardCustom)sender;
                if (itemSelecionado.TipoVariavel == "Produto")
                {
                    ProdutoClass prod = (ProdutoClass)itemSelecionado.ItemSelecionado;
                }
                else if (itemSelecionado.TipoVariavel == "Bandeja")
                {
                    BandejaClass band = (BandejaClass)itemSelecionado.ItemSelecionado;
                }
                else if (itemSelecionado.TipoVariavel == "Recipiente")
                {
                    RecipienteClass rec = (RecipienteClass)itemSelecionado.ItemSelecionado;
                }
            }
            catch (Exception)
            {
            }
        }

        private void pn_produto_selecionado_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(ProdutoClass)))
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
            catch (Exception)
            {
            }
        }

        private void pn_bandeja_escolhida_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(BandejaClass)))
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
            catch (Exception)
            {
            }
        }

        private void pn_recipiente_escolhida_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(RecipienteClass)))
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
            catch (Exception)
            {
            }
        }

        private void pn_produto_selecionado_DragDrop(object sender, DragEventArgs e)
        {
            ProdutoClass info = (ProdutoClass)e.Data.GetData(typeof(ProdutoClass));
            Label lblProdutoInfo = new Label() 
            { 
                Dock = DockStyle.Fill, 
                Padding = new Padding(5,12,5,5), 
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Regular)
            };
            lblProdutoInfo.Text = $"Código: {info.Part_number}\n" +
                                  $"Descrição: {info.Descricao}\n" +
                                  $"Peso Alvo: {info.PesoAlvo}\n" +
                                  $"Tolerancia: {info.Tolerancia}\n";
            pn_produto_selecionado.Controls.Clear();
            pn_produto_selecionado.Controls.Add(lblProdutoInfo);
            pn_produto_selecionado.Tag = info;
        }

        private void pn_bandeja_escolhida_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                BandejaClass info = (BandejaClass)e.Data.GetData(typeof(BandejaClass));
                Label lblbandeja = new Label() { Dock = DockStyle.Fill, Padding = new Padding(5, 12, 5, 5), ForeColor = Color.White, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
                lblbandeja.Text = $"Código: {info.Codigo}\n" +
                                      $"Descrição: {info.Descricao}\n" +
                                      $"Peso Alvo: {info.PesoAlvo}\n" +
                                      $"Tolerancia: {info.Tolerancia}\n";

                pn_bandeja_escolhida.Controls.Clear();
                pn_bandeja_escolhida.Controls.Add(lblbandeja);
                pn_bandeja.Tag = info;

                if (info.Id != 0) 
                {
                    txtQuantidadeBandeja.Enabled = true;
                }

            }
            catch (Exception)
            {
            }
        }

        private void pn_recipiente_escolhida_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                RecipienteClass info = (RecipienteClass)e.Data.GetData(typeof(RecipienteClass));
                Label lblRecipienteInfo = new Label() { Dock = DockStyle.Fill, Padding = new Padding(5, 12, 5, 5), ForeColor = Color.White, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
                lblRecipienteInfo.Text = $"Código: {info.Package}\n" +
                          $"Descrição: {info.Descricao}\n" +
                          $"Peso Alvo: {info.PesoAlvo}\n" +
                          $"Tolerancia: {info.Tolerancia}\n";

                pn_recipiente_escolhida.Controls.Clear();
                pn_recipiente_escolhida.Controls.Add(lblRecipienteInfo);
                pn_recipiente_escolhida.Tag = info;
            }
            catch (Exception)
            {
            }
        }

        private void btnNovaReceita_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { btnPaintBorder_Click(txtCodigo); return;  }
                if (string.IsNullOrWhiteSpace(txtNome.Text)) { btnPaintBorder_Click(txtNome);  return; }
                BandejaClass band = (BandejaClass)pn_bandeja.Tag;
                if (band != null) { if (string.IsNullOrWhiteSpace(txtQuantidadeBandeja.Text)) { btnPaintBorder_Click(txtQuantidadeBandeja); return; } }
                if (string.IsNullOrWhiteSpace(txtQuantidadePecas.Text)) { btnPaintBorder_Click(txtQuantidadePecas);  return; }
                if (string.IsNullOrWhiteSpace(txtpkskf.Text)) { btnPaintBorder_Click(txtpkskf); return; }

                if (pn_produto_selecionado.Tag == null) { btnPaintBorder_Click(pn_produto_selecionado); return; }
                if (pn_recipiente_escolhida.Tag == null) { btnPaintBorder_Click(pn_recipiente_escolhida);  return; }

                ProdutoClass prod = (ProdutoClass)pn_produto_selecionado.Tag;
               
                RecipienteClass recip = (RecipienteClass)pn_recipiente_escolhida.Tag;
                bool result = false;

                if (band == null) { band = new BandejaClass(); }
                    
                if (type_moviment)
                {
                    result = Program.SQL.CRUDCommand("INSERT INTO RECEITA (Nome, Codigo, id_produto, id_bandeja, id_recipiente, Quantidade_pecas, Quantidade_bandejas, Operador, Status, dateinsert, PkSKF) VALUES " +
                                                            "(@Nome, @Codigo, @id_produto, @id_bandeja, @id_recipiente, @Quantidade_pecas, @Quantidade_bandejas, @Operador, @Status, @dateinsert, @PkSKF)", "RECEITA",
                                                            new Dictionary<string, object>()
                                                            {
                                                                {"@Nome", txtNome.Text},
                                                                {"@PkSKF", txtpkskf.Text},
                                                                {"@Codigo", txtCodigo.Text},
                                                                {"@id_produto", prod.Id},
                                                                {"@id_bandeja", band.Id},
                                                                {"@id_recipiente", Convert.ToInt32(recip.Id)},
                                                                {"@Quantidade_pecas", Convert.ToInt32(txtQuantidadePecas.Text)},
                                                                {"@Quantidade_bandejas", Convert.ToInt32(txtQuantidadeBandeja.Text)},
                                                                {"@Operador", Program._usuarioLogado.Nome},
                                                                {"@Status", 0},
                                                                {"@dateinsert", DateTime.Now},
                                                            });
                }
                else 
                {
                    result = Program.SQL.CRUDCommand("UPDATE RECEITA  SET Nome = @Nome, Codigo = @Codigo, id_produto = @id_produto, id_bandeja = @id_bandeja, id_recipiente = @id_recipiente, Quantidade_pecas = @Quantidade_pecas, Quantidade_bandejas = @Quantidade_bandejas, Operador = @Operador, Status = @Status, PkSKF = @PkSKF WHERE Id = @Id ", "RECEITA",
                                                        new Dictionary<string, object>()
                                                        {
                                                                {"@Nome", txtNome.Text},
                                                                {"@Codigo", txtCodigo.Text},
                                                                {"@PkSKF", txtpkskf.Text},
                                                                {"@id_produto", Convert.ToInt32(prod.Id)},
                                                                {"@id_bandeja", Convert.ToInt32(band.Id)},
                                                                {"@id_recipiente", Convert.ToInt32(recip.Id)},
                                                                {"@Quantidade_pecas", Convert.ToInt32(txtQuantidadePecas.Text)},
                                                                {"@Quantidade_bandejas", Convert.ToInt32(txtQuantidadeBandeja.Text)},
                                                                {"@Operador", Program._usuarioLogado.Nome},
                                                                {"@Status", 0},
                                                                {"@Id", _receita_atual.Id},
                                                        });
                }
                
                if (result) 
                {
                    Clean();
                    if (type_moviment)
                    {
                        InfoPopup info = new InfoPopup("Cadastro com sucesso.", "A receita já foi salva com sucesso na base de dados.");
                        info.ShowDialog();
                    }
                    else 
                    {
                        InfoPopup info = new InfoPopup("Alterado com sucesso.", "A receita já foi alterado com sucesso na base de dados.");
                        info.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Clean() 
        {
            try
            {
                try
                {
                    btnAdicionar.Text = "Adicionar";
                    txtCodigo.Text = "";
                    txtNome.Text = "";
                    txtQuantidadePecas.Text = "";
                    txtQuantidadeBandeja.Text = "";
                    pn_produto_selecionado.Controls.Clear();
                    pn_bandeja_escolhida.Controls.Clear();
                    pn_recipiente_escolhida.Controls.Clear();

                    load_f_l_panels();

                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {
            }
        }

        private void txt_searchProduto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;

                if (txt.Text.Trim() == "Buscar pela descrição") 
                {
                    return;
                }

                if (txt.Name == "txt_searchProduto") 
                {
                    foreach (RowCardCustom card in f_l_cardProdutos.Controls)
                    {
                        ProdutoClass produto = (ProdutoClass)card.elemento;

                        if (!produto.Descricao.ToLowerInvariant().Contains(txt.Text.ToLowerInvariant()))
                        {
                            card.Visible = false;
                        }
                        else 
                        {
                            card.Visible = true;
                        }
                    }
                }

                else if (txt.Name == "txt_searchBandeja")
                {
                    foreach (RowCardCustom card in f_l_cardBandeja.Controls)
                    {
                        BandejaClass band = (BandejaClass)card.elemento;

                        if (!band.Descricao.ToLowerInvariant().Contains(txt.Text.ToLowerInvariant()))
                        {
                            card.Visible = false;
                        }
                        else
                        {
                            card.Visible = true;
                        }
                    }
                }

                else if (txt.Name == "txt_searchRecipiente")
                {
                    foreach (RowCardCustom card in f_l_cardRecipiente.Controls)
                    {
                        RecipienteClass recip = (RecipienteClass)card.elemento;

                        if (!recip.Descricao.ToLowerInvariant().Contains(txt.Text.ToLowerInvariant()))
                        {
                            card.Visible = false;
                        }
                        else
                        {
                            card.Visible = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnListaReceita_Click(object sender, EventArgs e)
        {
            try
            {
                YesOrNo question = new YesOrNo("Você tem certeza que deseja fechar a página ??\nTodos os campos preenchidos não serão salvos.");
                question.ShowDialog();
                if (question.RESPOSTA)
                {
                    ItemSelecionado = "teste";
                }   
            }
            catch (Exception)
            {
            }
        }

        private void pcb_clear_panelEvent(object sender, EventArgs e)
        {
            try
            {
                Label pic = (Label)sender;
                if (pic.Tag.ToString() == "Produto") 
                {
                    pn_produto_selecionado.Controls.Clear();
                }
                if (pic.Tag.ToString() == "Bandeja")
                {
                    pn_bandeja_escolhida.Controls.Clear();
                    txtQuantidadeBandeja.Enabled = false;
                }
                if (pic.Tag.ToString() == "Recipiente")
                {
                    pn_recipiente_escolhida.Controls.Clear();
                }
            }
            catch (Exception)
            {
            }
        }

        private void txt_searchProduto_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt.Text.Trim() == "Buscar pela descrição") 
                {
                    txt.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void txt_searchProduto_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (string.IsNullOrWhiteSpace(txt.Text)) 
                {
                    txt.Text = " Buscar pela descrição";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnPaintBorder_Click(object sender)
        {
            // Obtém a cor selecionada para a borda
            Color borderColor = Color.Red; // Exemplo: vermelho

            // Pinta a borda do item desejado
            Control item = (Control)sender; // Substitua "textBox1" pelo nome do controle desejado
            using (Graphics g = item.CreateGraphics())
            {
                Pen pen = new Pen(borderColor, 1); // Largura da borda = 2
                Rectangle rect = new Rectangle(0, 0, item.Width - 1, item.Height - 1);
                g.DrawRectangle(pen, rect);
            }
        }

        private void load_f_l_panels() 
        {
            try
            {
                List<object> produtos = Program.SQL.SelectList("SELECT * FROM" +
                 " Produto", "Produto", null, new Dictionary<string, object>() { });

                List<object> bandeja = Program.SQL.SelectList("SELECT * FROM" +
                    " Bandeja", "Bandeja", null, new Dictionary<string, object>() { });

                List<object> recipiente = Program.SQL.SelectList("SELECT * FROM" +
                    " Recipiente", "Recipiente", null, new Dictionary<string, object>() { });

                Task.Run(() =>
                {
                    foreach (ProdutoClass _prod in produtos)
                    {
                        RowCardCustom tempp = new RowCardCustom(_prod);
                        tempp.ItemSelecionadoTrigger += Tempp_ItemSelecionadoTrigger;
                        //f_l_cardProdutos.Invoke(new MethodInvoker(delegate { f_l_cardProdutos.Controls.Add(tempp); }));
                        f_l_cardProdutos.Controls.Add(tempp);

                    }
                });

                Task.Run(() =>
                {
                    f_l_cardBandeja.Controls.Add(new RowCardCustom(new BandejaClass() {  Codigo = "Sem bandeja", Descricao = "Sem bandeja", PesoAlvo = 0, Tolerancia = 0, Quantidade_produtos = 0 , Id = 0, DateInsert = DateTime.Now}));
                    foreach (BandejaClass _band in bandeja)
                    {
                        RowCardCustom tempb = new RowCardCustom(_band);
                        tempb.ItemSelecionadoTrigger += Tempp_ItemSelecionadoTrigger;
                        f_l_cardBandeja.Controls.Add(tempb);
                        //f_l_cardBandeja.Invoke(new MethodInvoker(delegate { f_l_cardBandeja.Controls.Add(tempb); }));
                    }
                });

                Task.Run(() =>
                {
                    foreach (RecipienteClass _recip in recipiente)
                    {
                        RowCardCustom tempr = new RowCardCustom(_recip);
                        tempr.ItemSelecionadoTrigger += Tempp_ItemSelecionadoTrigger;
                        f_l_cardRecipiente.Controls.Add(tempr);
                        //f_l_cardRecipiente.Invoke(new MethodInvoker(delegate { f_l_cardRecipiente.Controls.Add(tempr); }));
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            try
            {
                ReceitaClass rect = (ReceitaClass)Program.SQL.SelectObject("SELECT * FROM Receita WHERE Codigo = @Codigo", "Receita",
                new Dictionary<string, object>()
                {
                    {"@Codigo", txtCodigo.Text}
                });

                if (rect != null)
                {
                    txtCodigo.Text = "";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Panel pn = (System.Windows.Forms.Panel)sender;
            System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
            int borderSize = 2;
            ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        }
    }
}
