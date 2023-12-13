using Main.Model;
using Main.Model.EtiquetaFolder;
using Main.Properties;
using Main.View.CustomLayout;
using Main.View.PopupFolder;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
///
///>Quando a receita não tiver bandeja, travar o txtbox da quantidade 
///bandeja em 1 e não deixar o operador alterar, a não ser que ele coloque 
///uma bandeja.
///
namespace Main.View.PagesFolder.PesagemFolder
{
    public partial class PesagemProcess : Form
    {

        //public event EventHandler ItemRepesarTrigger;

        //private ReceitaLogClass _itemRepesar;
        //public ReceitaLogClass ItemRepesar
        //{
        //    get { return _itemRepesar; }
        //    set
        //    {
        //        if (_itemRepesar != value)
        //        {
        //            _itemRepesar = value;
        //            if (ItemRepesar != null)
        //            {
        //                if (ItemRepesarTrigger != null)
        //                {
        //                    ItemRepesarTrigger(this, EventArgs.Empty);
        //                }
        //            }
        //        }
        //    }
        //}

        //public event EventHandler ListarNovamenteTrigger;

        //private bool _listarNovamente;
        //public bool ListarNovamente
        //{
        //    get { return _listarNovamente; }
        //    set
        //    {
        //        if (_listarNovamente != value)
        //        {
        //            _listarNovamente = value;
        //            if (ListarNovamente != null)
        //            {
        //                if (ListarNovamenteTrigger != null)
        //                {
        //                    ListarNovamenteTrigger(this, EventArgs.Empty);
        //                }
        //            }
        //        }
        //    }
        //}

        //private Timer _timerReadPeso = new Timer();
        ////private int _situacao_pesagem = 0;

        //private int __situacao_pesagem;
        //private int _situacao_pesagem_auxiliar { get; set; }
        //public int _situacao_pesagem
        //{
        //    get { return __situacao_pesagem; }
        //    set
        //    {
        //        if (_situacao_pesagem_auxiliar != value) 
        //        {
        //            _situacao_pesagem_auxiliar = value;
        //            RefreshUI();
        //        }
        //        __situacao_pesagem = value;
        //    }
        //}

        //private ProdutoClass produto_pesar { get; set; }
        //private RecipienteClass recipiente_pesar { get; set; }
        //private BandejaClass bandeja_class { get; set; }
        ////private List<Log_bandeja_receitaClass> log_bandeja { get; set; }
        //private PesagemInformacoes informacoes_Pesagem_atua { get; set; }
        //public class PesagemInformacoes 
        //{
        //    public double _pesoRecipiente { get; set; }
        //    public double _pesoBandeja { get; set; }
        //    public List<Decimal> _pesagens { get; set; }
        //    public double _pesoTotalProdutos { get; set; }
        //}

        //private Stopwatch millis = new Stopwatch();
        //private double peso_anterior = 0;
        //private long millis_aux = 0;
        //public ReceitaLogClass _log_receita { get; set; }
        ////public ReceitaClass _rec_executar { get; set; }impr

        //private ReceitaClass receita_impressao { get; set; }
        //public PesagemProcess(ReceitaLogClass log)
        //{

        //    InitializeComponent();
        //    ReceitaClass rec_executar = (ReceitaClass)Program.SQL.SelectObject("SELECT * FROM Receita WHERE id = @Id", "Receita", new Dictionary<string, object>() { { "@id", log.id_receita } });
        //    if (rec_executar == null) { this.Close(); }
        //    _log_receita = log;
        //    receita_impressao = rec_executar;
        //    produto_pesar = (ProdutoClass)Program.SQL.SelectObject("SELECT * FROM" +
        //                        " Produto WHERE id = @Id", "Produto", new Dictionary<string, object>() { {"@Id", _log_receita.id_Produto} });

        //    bandeja_class = (BandejaClass)Program.SQL.SelectObject("SELECT * FROM" +
        //                        " Bandeja WHERE id = @Id", "Bandeja", new Dictionary<string, object>() { { "@Id", _log_receita.id_Bandeja } });

        //    recipiente_pesar = (RecipienteClass)Program.SQL.SelectObject("SELECT * FROM" +
        //                            " Recipiente WHERE id = @Id", "Recipiente", new Dictionary<string, object>() { { "@Id", _log_receita.id_Recipiente } });


        //    informacoes_Pesagem_atua = new PesagemInformacoes();


        //    lbl_time.Text = $"{DateTime.Now.Day.ToString("D2")}/{DateTime.Now.Month.ToString("D2")}/{DateTime.Now.Year}";
        //    lblUsuario.Text = Program._usuarioLogado.Nome;
        //    lblAcesso.Text = Program._usuarioLogado.Acesso;

        //    _timerReadPeso.Interval = 50;
        //    _timerReadPeso.Tick += _timerReadPeso_Tick;
        //    _timerReadPeso.Start();

        //    if (rec_executar.Id_Bandeja == 0)
        //    {
        //        f_l_items.Controls.Add(new PesagemRowInfo(produto_pesar, rec_executar.Quantidade_pecas, 0));
        //    }
        //    else 
        //    {
        //        for (int i = 0; i <= rec_executar.Quantidade_bandejas -1; i++)
        //        {
        //            f_l_items.Controls.Add(new PesagemRowInfo(produto_pesar, rec_executar.Quantidade_pecas, 0));
        //        }
        //    }

        //    foreach (object item in f_l_items.Controls)
        //    {
        //        LinhaStruct n1 = new LinhaStruct();
        //        n1.dProduto = 0; n1.dBandeja = 0;
        //        ListaLinha.Add(n1);
        //    }

        //    millis.Start();
        //    lblQtdTotal.Text = (_log_receita.Qtd_Pecas * _log_receita.Qtd_Bandeja).ToString();
        //    lblNomeProduto.Text = produto_pesar.Descricao;
        //    lblCodigoPartnumber.Text = produto_pesar.Part_number;
        //    lblPackageRecipiente.Text = recipiente_pesar.Package;
        //    lblCodigoReceita.Text = rec_executar.Codigo;

        //}

        //private double d_Recipiente = 0;
        //private double d_Bandeja = 0;
        //private double d_Produtos = 0;
        //private int iTempo = 1000;
        //private int iQtdBandejas = 1;
        //private bool bBandejaCheck = false, bProdutosCheck = false, bRecipienteCheck = false;
        //private bool bIniciado = false;
        //private bool bPopUP_Load = true;

        //private bool bUpdateUI = false;
        //public struct LinhaStruct
        //{
        //    public double dProduto;
        //    public double dBandeja;
        //}

        //private List<LinhaStruct> ListaLinha = new List<LinhaStruct>();

        //private void btnNovamente_Click(object sender, EventArgs e)
        //{
        //    //Arrumar essa parte aqui.

        //    this.Invoke(new MethodInvoker(delegate
        //    {
        //        if (_log_receita != null)
        //        {
        //            ProdutoClass prod = (ProdutoClass)Program.SQL.SelectObject("SELECT * FROM Produto WHERE id = @Id", "Produto",
        //                new Dictionary<string, object>() { { "@Id", _log_receita.id_Produto } });

        //            RecipienteClass recip = (RecipienteClass)Program.SQL.SelectObject("SELECT * FROM Recipiente WHERE id = @Id", "Recipiente",
        //                new Dictionary<string, object>() { { "@Id", _log_receita.id_Recipiente } });

        //            BandejaClass bande = (BandejaClass)Program.SQL.SelectObject("SELECT * FROM Bandeja WHERE id = @Id", "Bandeja",
        //                new Dictionary<string, object>() { { "@Id", _log_receita.id_Bandeja } });

        //            if (bande == null) { bande = new BandejaClass(); }

        //            //Fechar e carregar o item.
        //            int result = Program.SQL.InsertAndSelectLasRow("INSERT INTO LogReceita (id_receita,Nome, Codigo, id_Recipiente, Peso_Recipiente, " +
        //                "id_Bandeja, Qtd_Bandeja, Peso_Bandejas, " +
        //                "id_Produto, Qtd_Pecas, Peso_Pecas, Status, " +
        //                "Estacao, Operador, dateinsert) " +
        //                " VALUES " +
        //                "(@id_receita,@Nome, @Codigo, @id_Recipiente, @Peso_Recipiente,@id_Bandeja, @Qtd_Bandeja, " +
        //                "@Peso_Bandejas,@id_Produto, @Qtd_Pecas, @Peso_Pecas, @Status, @Estacao, @Operador, @dateinsert);SELECT SCOPE_IDENTITY() AS Last_Id;", "LogReceita",
        //                new Dictionary<string, object>()
        //                {
        //                    {"@id_receita", _log_receita.id_receita},
        //                    {"@Nome", _log_receita.Nome},
        //                    {"@Codigo", _log_receita.Codigo},
        //                    {"@id_Recipiente", _log_receita.id_Recipiente},
        //                    {"@Peso_Recipiente", recip.PesoAlvo},
        //                    {"@id_Bandeja", _log_receita.id_Bandeja},
        //                    {"@Qtd_Bandeja", _log_receita.Qtd_Bandeja},
        //                    {"@Peso_Bandejas", bande.PesoAlvo},
        //                    {"@id_Produto", _log_receita.id_Produto},
        //                    {"@Qtd_Pecas", _log_receita.Qtd_Pecas},
        //                    {"@Peso_Pecas", prod.PesoAlvo},
        //                    {"@Status", 0},
        //                    {"@Estacao", Environment.MachineName},
        //                    {"@Operador", Program._usuarioLogado.Nome},
        //                    {"@dateinsert", DateTime.Now},
        //                });

        //            if (result > 0)
        //            {

        //                for (int i = 0; i < _log_receita.Qtd_Bandeja; i++)
        //                {
        //                    var log_bandeja_linha = Program.SQL.CRUDCommand("INSERT INTO Log_bandeja_receita " +
        //                            "(id_log_receita, Numero_Bandeja, Peso_Bandeja, Peso_Produto) VALUES " +
        //                            "(@id_log_receita, @Numero_Bandeja, @Peso_Bandeja, @Peso_Produto)", "Log_bandeja_receita",
        //                            new Dictionary<string, object>()
        //                            {
        //                                {"@id_log_receita", result},
        //                                {"@Numero_Bandeja", i+1},
        //                                {"@Peso_Bandeja", 0},
        //                                {"@Peso_Produto", 0},
        //                            });
        //                }

        //                ReceitaLogClass newLog = (ReceitaLogClass)Program.SQL.SelectObject("SELECT * FROM LogReceita WHERE id = @Id", "LogReceita", new Dictionary<string, object>() { { "@id", result } });

        //                if (newLog != null)
        //                {
        //                    ItemRepesar = newLog;
        //                }

        //                this.Close();
        //            }
        //        }
        //        this.Close();
        //    }));
        //}

        //private void PesagemProcess_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (_log_receita.Status == 1)
        //        {
        //            //informacoes_Pesagem_atua._pesoRecipiente = _log_receita.Peso_Recipiente_Pesado;
        //            //informacoes_Pesagem_atua._pesoTotalProdutos = _log_receita.Peso_Pecas_Pesado;
        //            //informacoes_Pesagem_atua._pesoBandeja = _log_receita.Peso_Bandejas_Pesado;

        //            //A Pesagem já tem informações, preciso carregar elas para ele retomar.
        //            var temp_bandeja = Program.SQL.SelectList("Select * from Log_bandeja_receita WHERE id_log_receita = @id_log_receita", "Log_bandeja_receita",
        //                        null, new Dictionary<string, object>()
        //                        {
        //                                    {"@id_log_receita", _log_receita.Id}
        //                        });
        //            int counter_temp = 0;
        //            foreach (Log_bandeja_receitaClass band in temp_bandeja)
        //            {
        //                if (counter_temp + 1 == band.Numero_Bandejas)
        //                {
        //                    LinhaStruct linha = ListaLinha[counter_temp];
        //                    linha.dBandeja = Math.Round(band.Peso_Bandeja,3);
        //                    linha.dProduto = Math.Round(band.Peso_Produto,3);
        //                    ListaLinha[counter_temp] = linha;
        //                    PesagemRowInfo currentPesagem = (PesagemRowInfo)f_l_items.Controls[counter_temp];
                            
        //                    //if (linha.dBandeja != 0 || linha.dProduto != 0)
        //                    //{
        //                    bIniciado = true;
        //                    //}

        //                    currentPesagem.UpdateQuantity(Convert.ToInt32(linha.dProduto / produto_pesar.PesoAlvo));
        //                    this.Refresh();
        //                    this.Update();
        //                    currentPesagem.Refresh();
        //                    currentPesagem.Update();
        //                    counter_temp++;
        //                }
        //            }
        //            informacoes_Pesagem_atua._pesoRecipiente = Math.Round(_log_receita.Peso_Recipiente_Pesado,3);
        //            informacoes_Pesagem_atua._pesoBandeja = 0;
        //            double d_ProdutoMais = Math.Round(_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo + (_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo * produto_pesar.Tolerancia) / 100,3);
        //            double d_ProdutoMenos = Math.Round(_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo - (_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo * produto_pesar.Tolerancia) / 100,3);

        //            int i = 0;
        //            foreach (LinhaStruct linha_db_save in ListaLinha)
        //            {
        //                //ADICIONAR LOGICA DE CARREGAR O PESOTOTALPRODUTOS QUANDO TIVER FINALIZADO E DENTRO DA MARGEM DE MAIS E MENOS;
        //                if (linha_db_save.dProduto >= d_ProdutoMenos && linha_db_save.dProduto <= d_ProdutoMais)
        //                {
        //                    IndicadorClass indicador = (IndicadorClass)Program.Registradores.First().Value;
        //                    double dPesoParcial = Math.Round(indicador.PB - linha_db_save.dBandeja - informacoes_Pesagem_atua._pesoRecipiente,3);
        //                    if(dPesoParcial >= linha_db_save.dProduto) informacoes_Pesagem_atua._pesoTotalProdutos += Math.Round(linha_db_save.dProduto,3);
        //                }

        //                //informacoes_Pesagem_atua._pesoTotalProdutos += linha_db_save.dProduto;
        //                //if (ListaLinha.Count > i + 1 && ListaLinha[i + 1].dBandeja != 0)
        //                //{
        //                informacoes_Pesagem_atua._pesoBandeja += Math.Round(linha_db_save.dBandeja,3);
        //                //}
        //                i += 1;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void btnListagem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ListarNovamente = true;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void PesagemProcess_Resize(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        RefreshUI();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{
        //    System.Windows.Forms.Panel pn = (System.Windows.Forms.Panel)sender;
        //    System.Drawing.Color COLOR_BORDER = System.Drawing.Color.FromArgb(237, 237, 237);
        //    int borderSize = 2;
        //    ControlPaint.DrawBorder(e.Graphics, pn.ClientRectangle, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid, COLOR_BORDER, borderSize, ButtonBorderStyle.Solid);
        //}

        //private void btnNovamente_VisibleChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (btnNovamente.Visible == true) 
        //        {
        //            try
        //            {
        //                pictureBox5.Visible = false;
        //                _timerReadPeso.Stop();

        //                //Program.com.ImpressoraPrint(new EtiquetaInfo() { earn = produto_pesar.CodigoEarn, packCaixa = receita_impressao.PKSKF, partNumber = produto_pesar.part_number_cliente, produtoProduzido = receita_impressao.Codigo, quantidadePecas = (receita_impressao.Quantidade_pecas * receita_impressao.Quantidade_bandejas).ToString(), date = Convert.ToString(GetJulianDay()) }, 2);

        //                if (produto_pesar.CodigoEarn.Length == 12)
        //                {
        //                    Program.com.ImpressoraPrint(new EtiquetaInfo() { earn = produto_pesar.CodigoEarn, packCaixa = receita_impressao.PKSKF, partNumber = produto_pesar.part_number_cliente, produtoProduzido = receita_impressao.Codigo, quantidadePecas = (receita_impressao.Quantidade_pecas * receita_impressao.Quantidade_bandejas).ToString(), date = Convert.ToString(GetJulianDay()) }, 2);
        //                }
        //                else
        //                {
        //                    InfoPopup aviso = new InfoPopup("Atenção !!", "A etiqueta não será impressa, o código EARN não está correto no cadastro do produto");
        //                    aviso.ShowDialog();
        //                }

        //                lblwarningReset.Visible = true;

        //                Task.Run(() =>
        //                {
        //                    Stopwatch millis = new Stopwatch();
        //                    long passedMilis = 0;
        //                    millis.Start();
        //                    int counter_time = 10;
        //                    while (millis.ElapsedMilliseconds <= 10000) 
        //                    {
        //                        if (passedMilis + 1000 <= millis.ElapsedMilliseconds) 
        //                        {
        //                            passedMilis += 1000;
        //                            this.Invoke(new MethodInvoker(delegate 
        //                            {
        //                                lblwarningReset.Text = $"A receita vai reiniciar em {counter_time--}";
        //                            }));
        //                        }
        //                    }
        //                    btnNovamente_Click(sender, e);
        //                });
        //            }
        //            catch (Exception)
        //            {

        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //private int qtd_aux_anterior = 0;

        //private void PesagemProcess_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    if (_timerReadPeso != null)
        //    {
        //        _timerReadPeso.Stop();
        //        _timerReadPeso = null;
        //    }
        //}

        //private void PesagemProcess_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (this.Visible == false) 
        //    {
        //        if(_timerReadPeso != null) {
        //            _timerReadPeso.Stop();
        //            _timerReadPeso = null;
        //        }
        //    }
        // }


        //private void v_UpdateRecipienteBandeja()
        //{
        //    var r2d231 = Program.SQL.CRUDCommand("UPDATE LogReceita SET " +
        //                                        "Peso_Recipiente_Pesado = @Peso_Recipiente_Pesado, " +
        //                                        "Qtd_Bandeja_Pesado = @Qtd_Bandeja_Pesado, " +
        //                                        "Peso_Bandejas_Pesado = @Peso_Bandejas_Pesado, " +
        //                                        "Status = 1, " +
        //                                        "Observacoes = @Observacoes " +
        //                                        "WHERE id = @id",
        //                                        "LogReceita",
        //                                        new Dictionary<string, object>()
        //                                        {
        //                                            {"@Peso_Recipiente_Pesado", informacoes_Pesagem_atua._pesoRecipiente},
        //                                            {"@Qtd_Bandeja_Pesado", iQtdBandejas},
        //                                            {"@Peso_Bandejas_Pesado", informacoes_Pesagem_atua._pesoBandeja},
        //                                            {"@Observacoes", rtxtAnotacoes.Text},
        //                                            {"@id", _log_receita.Id}
        //                                        });

        //    var r3 = Program.SQL.CRUDCommand("UPDATE Log_bandeja_receita SET Peso_Bandeja = @Peso_Bandeja " +
        //        "WHERE id_log_receita = @id_log_receita AND Numero_Bandeja = @Numero_Bandeja", "Log_bandeja_receita",
        //        new Dictionary<string, object>()
        //        {
        //                                           {"@Peso_Bandeja", ListaLinha[iQtdBandejas -1].dBandeja },
        //                                           {"@id_log_receita", _log_receita.Id },
        //                                           {"@Numero_Bandeja", iQtdBandejas },
        //        });
        //}

        //private void _timerReadPeso_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        IndicadorClass indicador = (IndicadorClass)Program.Registradores.First().Value;

        //        lblPeso.Invoke(new MethodInvoker(delegate
        //        {
        //            lblPeso.Text = $"{indicador.PB} kg";
        //        }));

        //        bool tudo_finalizado = true;
        //        bool bBandeja = true;
        //        double d_BandejaMais = 0, d_BandejaMenos = 0;
        //        if (bandeja_class == null)
        //        {
        //            bBandeja = false;
        //        }
        //        double d_RecipienteMais = Math.Round(recipiente_pesar.PesoAlvo + (recipiente_pesar.PesoAlvo * recipiente_pesar.Tolerancia) / 100,3);
        //        double d_RecipienteMenos = Math.Round(recipiente_pesar.PesoAlvo - (recipiente_pesar.PesoAlvo * recipiente_pesar.Tolerancia) / 100,3);
        //        if (bBandeja) d_BandejaMais = Math.Round((bandeja_class.PesoAlvo + (bandeja_class.PesoAlvo * bandeja_class.Tolerancia) / 100),3);
        //        if (bBandeja) d_BandejaMenos = Math.Round((bandeja_class.PesoAlvo - (bandeja_class.PesoAlvo * bandeja_class.Tolerancia) / 100),3);
        //        double d_ProdutoMais = Math.Round(_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo + (_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo * produto_pesar.Tolerancia) / 100,3);
        //        double d_ProdutoMenos = Math.Round(_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo - (_log_receita.Qtd_Pecas * produto_pesar.PesoAlvo * produto_pesar.Tolerancia) / 100,3);
        //        double d_Peso = Math.Round(indicador.PB - informacoes_Pesagem_atua._pesoTotalProdutos,3);

        //        double dProdutoAux = 0, dBandejaAux = 0;
        //        double dAux = Math.Round(indicador.PB - informacoes_Pesagem_atua._pesoRecipiente,3);
        //        double dAuxBand = Math.Round(indicador.PB - informacoes_Pesagem_atua._pesoBandeja,3);

        //        int index_temp = 0;
        //        double pa = Math.Round(indicador.PB - informacoes_Pesagem_atua._pesoRecipiente,3);
        //        double p1 = 0;

        //        //PESO NEGATIVO
        //        if (indicador.PB < 0)
        //        { 
        //            InfoPopup popup_Negativo = new InfoPopup("Alerta de PESO NEGATIVO!","Não é possivel iniciar/continuar o processo!\n\n PESO NEGATIVO!\n\n Para continuar, zere manualmente a balança e clique em 'OK'.");
        //            _timerReadPeso.Stop();
        //            popup_Negativo.ShowDialog();
        //            _timerReadPeso.Start();
        //            return;
        //        }

        //        if (bIniciado)
        //        {
        //            //POPUP DE CARREGAMENTO PARA RETOMADA SE A BALANÇA ESTIVER VAZIA, MAS ALGUMA PESAGEM JA FOI FEITA
        //            if (indicador.PB < d_RecipienteMenos && bPopUP_Load && _log_receita.Status != 0)
        //            {
        //                YesOrNo popup_Question = new YesOrNo("A Balança se encontra vazia, porem o processo já foi iniciado anteriormente. \n Adicione o valor da ultima receita e selecione 'Sim' para continuar. \n Selecione 'Não' para recomeçar a receita do Zero. \n Atenção! Essa ação não pode ser desfeita!");
        //                _timerReadPeso.Stop();
        //                popup_Question.ShowDialog();
        //                _timerReadPeso.Start();
        //                if (popup_Question.RESPOSTA == false)
        //                {
        //                    bPopUP_Load = false;
        //                }
        //                else
        //                {
        //                    PesagemProcess_Load(sender, e);
        //                    return;
        //                }
        //            }


        //            if (indicador.PB >= d_RecipienteMenos)
        //            {
        //                foreach (PesagemRowInfo row in f_l_items.Controls)
        //                {
        //                    p1 = Math.Round(p1 + ListaLinha[index_temp].dBandeja,3);
        //                    if (pa < p1 - 0.002 && bBandeja)
        //                    {
        //                        Console.WriteLine($" PA Menor que p1\n\r pa:{pa} e p1: {p1} e bbBandeja True");
        //                        row.UpdateQuantity(0);
        //                        LinhaStruct teste = ListaLinha[index_temp];
        //                        informacoes_Pesagem_atua._pesoBandeja = Math.Round(informacoes_Pesagem_atua._pesoBandeja - teste.dBandeja,3);
        //                        if (informacoes_Pesagem_atua._pesoBandeja <= 0)
        //                        {
        //                            informacoes_Pesagem_atua._pesoBandeja = 0;
        //                        }
        //                        teste.dBandeja = 0;
        //                        ListaLinha[index_temp] = teste;
        //                    }
        //                    //Exatamente aqui, ele
        //                    //está zerando o valor.
        //                    p1 = Math.Round(p1 +  ListaLinha[index_temp].dProduto,3);
        //                    if (pa < p1 - 0.002)
        //                    {
        //                        Console.WriteLine($" PA Menor que p1\n\r pa:{pa} e p1: {p1}");
        //                        row.PesagemFinalizada = false;
        //                        row.UpdateQuantity(0);
        //                        LinhaStruct teste = ListaLinha[index_temp];
        //                        informacoes_Pesagem_atua._pesoTotalProdutos = Math.Round(informacoes_Pesagem_atua._pesoTotalProdutos - teste.dProduto,3);
        //                        teste.dProduto = 0;
        //                        ListaLinha[index_temp] = teste;
        //                    }
        //                    index_temp++;
        //                }
        //                index_temp = 0;

        //                //IDENTIFICAR LINHA ATUAL
        //                foreach (LinhaStruct row in ListaLinha)
        //                {
        //                    if (row.dBandeja == 0 || (row.dProduto == 0 || row.dProduto < d_ProdutoMenos))
        //                    {
        //                        if (iQtdBandejas == 1) break;
        //                        iQtdBandejas = index_temp + 1;
        //                        break;
        //                    }else if (row.dProduto >= Math.Round(d_ProdutoMenos* iQtdBandejas,3) && row.dProduto <= Math.Round(d_ProdutoMais* iQtdBandejas,3))
        //                    {
        //                        iQtdBandejas++;
        //                        break;
        //                    }
        //                    index_temp++;
        //                }
        //                if (iQtdBandejas <= _log_receita.Qtd_Bandeja)
        //                {
        //                    if (informacoes_Pesagem_atua._pesoRecipiente == 0)
        //                    {
        //                        _situacao_pesagem = 0;
        //                    }
        //                    else if (bBandeja && ListaLinha[iQtdBandejas - 1].dBandeja == 0)
        //                    {
        //                        _situacao_pesagem = 1; bRecipienteCheck = false;
        //                        bBandejaCheck = false; bProdutosCheck = false;
        //                    }
        //                    else if (ListaLinha[iQtdBandejas - 1].dProduto == 0)
        //                    {
        //                        _situacao_pesagem = 2; bRecipienteCheck = false;
        //                        bBandejaCheck = true; bProdutosCheck = false;
        //                    }
        //                    else if(bBandeja && ListaLinha[iQtdBandejas - 1].dBandeja > 0 && ListaLinha[iQtdBandejas - 1].dProduto < d_ProdutoMenos)
        //                    {
        //                        _situacao_pesagem = 2; bRecipienteCheck = true;
        //                        bBandejaCheck = true; bProdutosCheck = false;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                _situacao_pesagem = 0;
        //                foreach (PesagemRowInfo row in f_l_items.Controls)
        //                {
        //                    LinhaStruct teste = ListaLinha[index_temp];
        //                    teste.dProduto = 0; teste.dBandeja = 0;
        //                    ListaLinha[index_temp] = teste;
        //                    index_temp++;
        //                    row.UpdateQuantity(0);
        //                }
        //            }

        //            switch (_situacao_pesagem)
        //            {
        //                //Caixa
        //                case 0:
        //                    if (indicador.PB < d_RecipienteMenos)
        //                    {
        //                        pcb_imageStattus.Image = Resources.caixaAdd;
        //                        iQtdBandejas = 1; bRecipienteCheck = false; bBandejaCheck = false; bProdutosCheck = false;
        //                        informacoes_Pesagem_atua = new PesagemInformacoes();
        //                        lblQtdReal.Text = "0";
        //                        btnListagem.Visible = false;
        //                        btnNovamente.Visible = false;
        //                        PesagemRowInfo currentPesagem = (PesagemRowInfo)f_l_items.Controls[iQtdBandejas - 1];
        //                        currentPesagem.PesagemFinalizada = false; currentPesagem.UpdateQuantity(0);
        //                    }
        //                    else if (indicador.PB >= d_RecipienteMenos && indicador.PB <= d_RecipienteMais)
        //                    {
        //                        if (millis_aux == 0)
        //                        {
        //                            millis_aux = millis.ElapsedMilliseconds;
        //                            peso_anterior = Math.Round(indicador.PB,3);
        //                        }
        //                        if (millis.ElapsedMilliseconds - millis_aux > iTempo)
        //                        {
        //                            if (bRecipienteCheck == false)
        //                            {
        //                                informacoes_Pesagem_atua._pesoRecipiente = Math.Round(indicador.PB,3);
        //                                bRecipienteCheck = true;
        //                                v_UpdateRecipienteBandeja();
        //                            }
        //                            pcb_imageStattus.Invoke(new MethodInvoker(delegate
        //                            {
        //                                pcb_imageStattus.Image = Resources.caixaIconr;
        //                            }));
        //                            //await Task.Delay(3000);
        //                            _situacao_pesagem = 1;
        //                            millis_aux = millis.ElapsedMilliseconds;
        //                        }
        //                    }
        //                    break;

        //                //Bandeja
        //                case 1:
        //                    if (indicador.PB < d_RecipienteMenos)
        //                    {
        //                        _situacao_pesagem = 0;
        //                    }
        //                    else
        //                    {
        //                        if (d_Peso >= Math.Round(informacoes_Pesagem_atua._pesoRecipiente - 0.002,3) && d_Peso < Math.Round(informacoes_Pesagem_atua._pesoRecipiente + informacoes_Pesagem_atua._pesoBandeja + d_BandejaMenos - 0.002, 3) && bBandeja)
        //                        {
        //                            pcb_imageStattus.Invoke(new MethodInvoker(delegate
        //                            {
        //                                pcb_imageStattus.Image = Resources.bandejaAdd;
        //                            }));
        //                            //await Task.Delay(3000);
        //                            millis_aux = millis.ElapsedMilliseconds;
        //                        }
        //                        else if (d_Peso >= Math.Round(informacoes_Pesagem_atua._pesoRecipiente + informacoes_Pesagem_atua._pesoBandeja + d_BandejaMenos - 0.002, 3) && d_Peso <= Math.Round(informacoes_Pesagem_atua._pesoRecipiente + informacoes_Pesagem_atua._pesoBandeja + d_BandejaMais - 0.002, 3) && bBandeja)
        //                        {
        //                            if (millis.ElapsedMilliseconds - millis_aux > iTempo)
        //                            {
        //                                pcb_imageStattus.Invoke(new MethodInvoker(delegate
        //                                {
        //                                    pcb_imageStattus.Image = Resources.bandejaIconr;
        //                                }));
        //                                //await Task.Delay(3000);
        //                                if (bBandejaCheck == false)
        //                                {
        //                                    d_Bandeja = Math.Round(d_Peso - informacoes_Pesagem_atua._pesoRecipiente,3);
        //                                    LinhaStruct n1 = ListaLinha[iQtdBandejas - 1];
        //                                    n1.dBandeja = Math.Round(d_Bandeja - informacoes_Pesagem_atua._pesoBandeja,3);
        //                                    ListaLinha[iQtdBandejas - 1] = n1;
        //                                    informacoes_Pesagem_atua._pesoBandeja = Math.Round(d_Peso - informacoes_Pesagem_atua._pesoRecipiente,3);
        //                                    bRecipienteCheck = false;
        //                                    bBandejaCheck = true;
        //                                    bProdutosCheck = false;
        //                                    v_UpdateRecipienteBandeja();
        //                                }
        //                                _situacao_pesagem = 2;
        //                                millis_aux = millis.ElapsedMilliseconds;
        //                            }
        //                        }
        //                        else if (bBandeja == false)
        //                        {
        //                            _situacao_pesagem = 2;
        //                        }
        //                    }
        //                    break;

        //                //Produto
        //                case 2:
        //                    if (indicador.PB < Math.Round(informacoes_Pesagem_atua._pesoRecipiente + d_BandejaMenos - 0.002, 3))
        //                    {
        //                        _situacao_pesagem = 1; informacoes_Pesagem_atua._pesoBandeja = Math.Round(informacoes_Pesagem_atua._pesoBandeja - d_Bandeja ,3);
        //                        PesagemRowInfo currentPesagem = (PesagemRowInfo)f_l_items.Controls[iQtdBandejas - 1];
        //                        currentPesagem.PesagemFinalizada = false;
        //                        currentPesagem.UpdateQuantity(0);
        //                    }
        //                    else
        //                    {
        //                        if (d_Peso >= Math.Round(informacoes_Pesagem_atua._pesoRecipiente + informacoes_Pesagem_atua._pesoBandeja - 0.002, 3) && d_Peso <= Math.Round(informacoes_Pesagem_atua._pesoRecipiente + informacoes_Pesagem_atua._pesoBandeja + d_ProdutoMais - 0.002, 3))
        //                        {
        //                            double dPesoProdutos = Math.Round( d_Peso - informacoes_Pesagem_atua._pesoBandeja - informacoes_Pesagem_atua._pesoRecipiente,3);
        //                            int iQtdProduto = Convert.ToInt32(dPesoProdutos / produto_pesar.PesoAlvo);
        //                            double dPesoPeca = Math.Round((dPesoProdutos * 1.0)/ iQtdProduto,3);
        //                            if (iQtdProduto == 0) dPesoPeca = 0;
        //                            //_log_receita.Qtd_Pecas
        //                            if (dPesoPeca < Math.Round(d_ProdutoMenos/ _log_receita.Qtd_Pecas,3))
        //                            {
        //                                if(iQtdProduto != 0) iQtdProduto -= 1;
        //                            }

        //                            PesagemRowInfo currentPesagem = (PesagemRowInfo)f_l_items.Controls[iQtdBandejas - 1];
        //                            if (currentPesagem.PesagemFinalizada == false)
        //                            {
        //                                pcb_imageStattus.Invoke(new MethodInvoker(delegate
        //                                {
        //                                    pcb_imageStattus.Image = Resources.produtoaddr;
        //                                }));
        //                                if (qtd_aux_anterior != iQtdProduto)
        //                                {
        //                                    currentPesagem.UpdateQuantity(iQtdProduto);
        //                                    qtd_aux_anterior = iQtdProduto;

        //                                    var r2d231 = Program.SQL.CRUDCommand("UPDATE LogReceita SET " +
        //                                        " Peso_Recipiente_Pesado = @Peso_Recipiente_Pesado, " +
        //                                       // "Qtd_Bandeja_Pesado = @Qtd_Bandeja_Pesado, " +
        //                                       // "Peso_Bandejas_Pesado = @Peso_Bandejas_Pesado, " +
        //                                       // "Qtd_Pecas_Pesado = @Qtd_Pecas_Pesado, " +
        //                                       // "Peso_Pecas_Pesado = @Peso_Pecas_Pesado, " +
        //                                        "Status = 1, " +
        //                                        "Observacoes = @Observacoes " +
        //                                        "WHERE id = @id",
        //                                        "LogReceita",
        //                                        new Dictionary<string, object>()
        //                                        {
        //                                            {"@Peso_Recipiente_Pesado", informacoes_Pesagem_atua._pesoRecipiente},
        //                                          //  {"@Qtd_Bandeja_Pesado", iQtdBandejas },
        //                                          //  {"@Peso_Bandejas_Pesado", informacoes_Pesagem_atua._pesoBandeja},
        //                                           // {"@Qtd_Pecas_Pesado", iQtdProduto},
        //                                          //  {"@Peso_Pecas_Pesado",dPesoProdutos},
        //                                            {"@Observacoes", rtxtAnotacoes.Text},
        //                                            {"@id", _log_receita.Id}
        //                                        });

        //                                    var r3 = Program.SQL.CRUDCommand("UPDATE Log_bandeja_receita SET Peso_Bandeja = @Peso_Bandeja, Peso_Produto = @Peso_Produto " +
        //                                        "WHERE id_log_receita = @id_log_receita AND Numero_Bandeja = @Numero_Bandeja", "Log_bandeja_receita",
        //                                        new Dictionary<string, object>()
        //                                        {
        //                                           {"@Peso_Bandeja", ListaLinha[iQtdBandejas -1].dBandeja },
        //                                           {"@Peso_Produto", dPesoProdutos },
        //                                           {"@id_log_receita", _log_receita.Id },
        //                                           {"@Numero_Bandeja", iQtdBandejas },
        //                                        });
        //                                }

        //                                //lblQtdReal.Text = Math.Ceiling((informacoes_Pesagem_atua._pesoTotalProdutos + dPesoProdutos) / produto_pesar.PesoAlvo).ToString();
        //                                lblQtdReal.Text = GetTotal().ToString();
        //                            }
        //                            else
        //                            {
        //                                pcb_imageStattus.Invoke(new MethodInvoker(delegate
        //                                {
        //                                    pcb_imageStattus.Image = Resources.produtoadd;
        //                                }));
        //                                if (bProdutosCheck == false && d_Peso >= Math.Round(informacoes_Pesagem_atua._pesoRecipiente + informacoes_Pesagem_atua._pesoBandeja + d_ProdutoMenos - 0.002, 3) && d_Peso <= Math.Round(informacoes_Pesagem_atua._pesoRecipiente + informacoes_Pesagem_atua._pesoBandeja + d_ProdutoMais - 0.002, 3))
        //                                {
        //                                    //informacoes_Pesagem_atua._pesoTotalProdutos += _rec_executar.Quantidade_pecas * produto_pesar.PesoAlvo;
        //                                    LinhaStruct n1 = ListaLinha[iQtdBandejas - 1];
        //                                    n1.dProduto = dPesoProdutos;
        //                                    ListaLinha[iQtdBandejas - 1] = n1;
        //                                    iQtdBandejas++;
        //                                    informacoes_Pesagem_atua._pesoTotalProdutos = 0;
        //                                    foreach (LinhaStruct item in ListaLinha)
        //                                    {
        //                                        informacoes_Pesagem_atua._pesoTotalProdutos = Math.Round(informacoes_Pesagem_atua._pesoTotalProdutos + item.dProduto, 3);
        //                                    }
        //                                    bRecipienteCheck = false;
        //                                    bBandejaCheck = false;
        //                                    bProdutosCheck = true;

        //                                    if (iQtdBandejas - 1 >= _log_receita.Qtd_Bandeja)
        //                                    {
        //                                        _situacao_pesagem = 4;
        //                                    }
        //                                    else
        //                                    {
        //                                        currentPesagem.PesagemFinalizada = true;
        //                                        var r2 = Program.SQL.CRUDCommand("UPDATE LogReceita SET Peso_Recipiente_Pesado = @Peso_Recipiente_Pesado, " +
        //                                            "Qtd_Bandeja_Pesado = @Qtd_Bandeja_Pesado, " +
        //                                            "Peso_Bandejas_Pesado = @Peso_Bandejas_Pesado, " +
        //                                            "Qtd_Pecas_Pesado = @Qtd_Pecas_Pesado, " +
        //                                            "Peso_Pecas_Pesado = @Peso_Pecas_Pesado, " +
        //                                            "Status = 1, " +
        //                                            "Observacoes = @Observacoes " +
        //                                            "WHERE id = @id",
        //                                            "LogReceita",
        //                                            new Dictionary<string, object>()
        //                                            { 
        //                                                {"@Peso_Recipiente_Pesado", informacoes_Pesagem_atua._pesoRecipiente},
        //                                                {"@Qtd_Bandeja_Pesado", iQtdBandejas - 1},
        //                                                {"@Peso_Bandejas_Pesado", informacoes_Pesagem_atua._pesoBandeja},
        //                                                {"@Qtd_Pecas_Pesado", iQtdProduto},
        //                                                {"@Peso_Pecas_Pesado", informacoes_Pesagem_atua._pesoTotalProdutos},
        //                                                {"@Observacoes", rtxtAnotacoes.Text},
        //                                                {"@id", _log_receita.Id}
        //                                            });

        //                                        var r3 = Program.SQL.CRUDCommand("UPDATE Log_bandeja_receita SET Peso_Bandeja = @Peso_Bandeja, Peso_Produto = @Peso_Produto " +
        //                                            "WHERE id_log_receita = @id_log_receita AND Numero_Bandeja = @Numero_Bandeja", "Log_bandeja_receita",
        //                                            new Dictionary<string, object>()
        //                                            {
        //                                                {"@Peso_Bandeja", ListaLinha[iQtdBandejas -2].dBandeja },
        //                                                {"@Peso_Produto", ListaLinha[iQtdBandejas -2].dProduto },
        //                                                {"@id_log_receita", _log_receita.Id },
        //                                                {"@Numero_Bandeja", iQtdBandejas-1 },

        //                                            });
        //                                        _situacao_pesagem = 1;

        //                                    }
        //                                }
        //                                else if (bProdutosCheck == true)
        //                                {
                                            
        //                                }
                                        
        //                            }
        //                        }

        //                    }
        //                    break;

        //                //Final de processo
        //                case 4:
        //                    int iQtdProduto2 = Convert.ToInt32(informacoes_Pesagem_atua._pesoTotalProdutos / produto_pesar.PesoAlvo);
        //                    var r = Program.SQL.CRUDCommand("UPDATE LogReceita SET Peso_Recipiente_Pesado = @Peso_Recipiente_Pesado, " +
        //                                                    "Qtd_Bandeja_Pesado = @Qtd_Bandeja_Pesado, " +
        //                                                    "Peso_Bandejas_Pesado = @Peso_Bandejas_Pesado, " +
        //                                                    "Qtd_Pecas_Pesado = @Qtd_Pecas_Pesado, " +
        //                                                    "Peso_Pecas_Pesado = @Peso_Pecas_Pesado, " +
        //                                                    "Status = 2, " +
        //                                                    "Observacoes = @Observacoes, " +
        //                                                    "datefim = @datefim " +
        //                                                    "WHERE id = @id",
        //                        "LogReceita",
        //                        new Dictionary<string, object>()
        //                        {
        //                        {"@Peso_Recipiente_Pesado", informacoes_Pesagem_atua._pesoRecipiente},
        //                        {"@Qtd_Bandeja_Pesado", iQtdBandejas - 1},
        //                        {"@Peso_Bandejas_Pesado", informacoes_Pesagem_atua._pesoBandeja},
        //                        {"@Qtd_Pecas_Pesado", iQtdProduto2},
        //                        {"@Peso_Pecas_Pesado", informacoes_Pesagem_atua._pesoTotalProdutos},
        //                        {"@Observacoes", rtxtAnotacoes.Text},
        //                        {"@datefim", DateTime.Now},
        //                        {"@id", _log_receita.Id}
        //                        });
        //                    pcb_imageStattus.Invoke(new MethodInvoker(delegate
        //                    {
        //                        pcb_imageStattus.Image = Resources.FinalizaProcesso;
        //                    }));
        //                    btnListagem.Visible = true;
        //                    btnNovamente.Visible = true;
        //                    //lblQtdReal.Text = Math.Floor((informacoes_Pesagem_atua._pesoTotalProdutos) / produto_pesar.PesoAlvo).ToString();
        //                    lblQtdReal.Text = GetTotal().ToString();
        //                    break;

        //                default:
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            if (indicador.PB <= 0)
        //            {
        //                bIniciado = true;
        //                lblPeso.Invoke(new MethodInvoker(delegate
        //                {
        //                    lblPeso.ForeColor = Color.Black;
        //                }));
        //            }
        //            else
        //            {
        //                pcb_imageStattus.Invoke(new MethodInvoker(delegate
        //                {
        //                    pcb_imageStattus.Image = Resources.errorZero;
        //                }));

        //                lblPeso.Invoke(new MethodInvoker(delegate
        //                {
        //                    lblPeso.ForeColor = Color.Red;
        //                }));
        //            }
        //        }

        //      //  Console.WriteLine($"{_timerReadPeso.Enabled}");
        //        //_timerReadPeso.Start();

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //private void RefreshUI() 
        //{
        //    try
        //    {
        //        this.Invoke(new MethodInvoker(delegate 
        //        {
        //            foreach (PesagemRowInfo row in f_l_items.Controls)
        //            {
        //                row.Size = new Size(f_l_items.Width, row.Height);
        //                row.Update();
        //                row.Refresh();
        //                row.Invalidate();
        //            }
        //        }));

        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private int GetJulianDay()
        //{
        //    // Creates an instance of the JulianCalendar.
        //    JulianCalendar myCal = new JulianCalendar();
        //    DateTime currentDate = DateTime.Now;
        //    int julianDay = currentDate.DayOfYear;

        //    return julianDay;
        //}

        //private int GetTotal() 
        //{
        //    try
        //    {
        //        int somador = 0;
        //        foreach (PesagemRowInfo rowPesagem in f_l_items.Controls)
        //        {
        //            somador += rowPesagem.quantidadeReal;
        //        }

        //        return somador;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

    }
}
