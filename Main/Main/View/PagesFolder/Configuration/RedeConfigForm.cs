using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Main.Helper;
using Main.Model;
using Main.View.CommunicationFolder;
using Main.View.PopupFolder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Main.View.PagesFolder.Configuration
{
    public partial class RedeConfigForm : Form
    {
        TreeNode selectedNode;
        public RedeConfigForm()
        {
            InitializeComponent();
            pnBalança.Visible = false;
            pnImpressora.Visible = false;
            tvRede.ImageList = ListaIcones;
            TreeNodeLoad();
            txtIP1.TextChanged += TextChanged;
            txtIP2.TextChanged += TextChanged;
            txtIP3.TextChanged += TextChanged;
            txtIP4.TextChanged += TextChanged;
            txtPorta.TextChanged += TextChanged;
            txtIPIMP1.TextChanged += TextChanged;
            txtIPIMP2.TextChanged += TextChanged;
            txtIPIMP3.TextChanged += TextChanged;
            txtIPIMP4.TextChanged += TextChanged;
            txtPortaImp.TextChanged += TextChanged;
            vLoadInstalledPrinters();
        }

        private void tvRede_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right){
                TreeNode clickedNode = tvRede.GetNodeAt(e.X, e.Y);
                if(clickedNode != null)
                {
                    tvRede.SelectedNode = clickedNode;
                }
            }
            else if(e.Button == MouseButtons.Left)
            {
                TreeNode clickedNode = tvRede.GetNodeAt(e.X, e.Y);
                if (clickedNode != null)
                {
                    if(clickedNode.Tag == tvRede)
                    {
                        pnBalança.Visible = false;
                    }else
                    {
                        if(selectedNode != null)
                        {
                            RedeClass redeClicked = (RedeClass)clickedNode.Tag;
                            try
                            {
                                RedeClass redeSelected = (RedeClass)selectedNode.Tag;
                                if (redeClicked.Id != redeSelected.Id)
                                {
                                    pnBalança.Visible = false;
                                }
                            }
                            catch (Exception)
                            {
                                pnBalança.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        private void MenuItem_Opening(object sender, CancelEventArgs e)
        {
            if(tvRede.SelectedNode.Tag == tvRede)
            {
                btnExcluir.Visible = false;
                btnEditar.Visible = false;
                btnNovaBalança.Visible = true;
                btnNovaImpressora.Visible = true;
            }
            else if(tvRede.SelectedNode.Tag is RedeClass)
            {
                btnEditar.Visible = true;
                btnExcluir.Visible = true;
                btnNovaBalança.Visible = false;
                btnNovaImpressora.Visible = false;
            }
        }

        private void btnNovaBalança_Click(object sender, EventArgs e)
        {
            CleanInfoBalancas();
            selectedNode = tvRede.SelectedNode;
            pnBalança.Visible = true;
            pnImpressora.Visible = false;
        }
        private void btnNovaImpressora_Click(object sender, EventArgs e)
        {
            CleanInfoImpressora();
            selectedNode = tvRede.SelectedNode;
            pnImpressora.Visible = true;
            pnBalança.Visible = false;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tvRede.SelectedNode.Tag is RedeClass) //verifica se é da classe redeClass
            {
                RedeClass itemRede = (RedeClass)tvRede.SelectedNode.Tag;
                selectedNode = tvRede.SelectedNode;
                if (itemRede.tipo == "Balança")
                {
                    pnBalança.Visible = true;
                    pnImpressora.Visible = false;
                    cbFabricante.Text = itemRede.fabricante;
                    cbModelo.Text = itemRede.modelo;
                    cbProtocolo.Text = itemRede.protocolo;
                    txtNome.Text = itemRede.nome;
                    string[] IP_teil = itemRede.IP.Split('.');
                    txtIP1.Text = IP_teil[0];
                    txtIP2.Text = IP_teil[1];
                    txtIP3.Text = IP_teil[2];
                    txtIP4.Text = IP_teil[3];
                    txtPorta.Text = itemRede.porta.ToString();
                    txtEndereco.Text = itemRede.addr.ToString();
                    if (itemRede.casasDecimais != null)
                    {
                        //cbDecimais.Text = itemRede.casasDecimais.ToString();
                    }
                    
                }else if (itemRede.tipo == "Impressora")
                {
                    pnBalança.Visible = false;
                    pnImpressora.Visible = true;
                    cbTipoImp.SelectedIndex = itemRede.tipo_impressao;
                    cbImpressoras.Text = itemRede.impressora;
                    chkSimples.Checked = itemRede.simplificar;
                    cbFabricanteImp.Text = itemRede.fabricante;
                    cbModeloImp.Text = itemRede.modelo;
                    cbProtocoloImp.Text = itemRede.protocolo;
                    txtNomeImp.Text = itemRede.nome;
                    string[] IP_teil = itemRede.IP.Split('.');
                    txtIPIMP1.Text = IP_teil[0];
                    txtIPIMP2.Text = IP_teil[1];
                    txtIPIMP3.Text = IP_teil[2];
                    txtIPIMP4.Text = IP_teil[3];
                    txtPortaImp.Text = itemRede.porta.ToString();

                }
                
            }
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (tvRede.SelectedNode.Tag is RedeClass) //verifica se é da classe redeClass
            {
                RedeClass itemRede = (RedeClass)tvRede.SelectedNode.Tag;
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Id", itemRede.Id);
                bool result = Program.SQL.CRUDCommand("delete from Rede where Id = @Id", "Rede", parametros);
                if (result)
                {
                    if(itemRede.tipo == "Balança")
                    {
                        MessageBox.Show("A balança foi excluida com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnBalança.Visible = false;
                        CleanInfoBalancas();
                    }else if (itemRede.tipo == "Impressora")
                    {
                        MessageBox.Show("A impressora foi excluida com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnImpressora.Visible = false;
                        CleanInfoImpressora();
                    }
                    TreeNodeLoad();
                }
            }
        }

        private void cbFabricante_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbModelo.Items.Clear();
            cbProtocolo.Items.Clear();
            
            if (cbFabricante.Text == "AEPH do Brasil")
            {
                cbModelo.Items.Add("Matrix");
                cbModelo.Items.Add("Orion");
                cbModelo.Items.Add("Onix");
                cbModelo.Items.Add("Egeo");
            }

        }

        private void cbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProtocolo.Items.Clear();
            if (cbFabricante.Text == "AEPH do Brasil")
            {
                cbProtocolo.Items.Add("TCA");
                cbProtocolo.Items.Add("RTU");
            }

        }

        private void TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox txt = sender as System.Windows.Forms.TextBox;
            if (txt != null)
            {
                bool isNumeric = Int32.TryParse(txt.Text, out int a);
                if (!isNumeric)
                {
                    txt.Text = "";
                }
                if(txt.Name == "txtIP1" || txt.Name == "txtIP2" || txt.Name == "txtIP3"|| txt.Name == "txtIP4" | txt.Name == "txtIPIMP1" || txt.Name == "txtIPIMP2" || txt.Name == "txtIPIMP3" || txt.Name == "txtIPIMP4")
                {
                    if(a >= 256)
                    {
                        txt.Text = "";
                    }
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string IP = txtIP1.Text + "." + txtIP2.Text + "." + txtIP3.Text + "." + txtIP4.Text;
            IP = IP.Replace(" ", "");
            //if (cbFabricante.Text != "" && cbProtocolo.Text != "" && cbModelo.Text != "" && txtNome.Text != "" && IP != "" && txtPorta.Text != "" && cbDecimais.Text != "")
            if (cbFabricante.Text != "" && cbProtocolo.Text != "" && cbModelo.Text != "" && txtNome.Text != "" && txtEndereco.Text != "")
                {
                   /*if (ValidarCampoIP(txtIP1.Text, txtIP2.Text, txtIP3.Text, txtIP4.Text) == false)
                   {
                       MessageBox.Show("O IP digitado já existe, escolha outro!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       return;
                   }*/
                if (tvRede.SelectedNode.Tag == tvRede)
                {
                    YesOrNo question = new YesOrNo("Deseja mesmo adicionar a Balança na rede?");
                    question.ShowDialog();

                    if (question.RESPOSTA)
                    {
                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                        parametros.Add("@tipo", "Balança");
                        parametros.Add("@fabricante", cbFabricante.Text);
                        parametros.Add("@modelo", cbModelo.Text);
                        parametros.Add("@protocolo", cbProtocolo.Text);
                        parametros.Add("@nome", txtNome.Text);
                        parametros.Add("@parent", selectedNode.Text);
                        parametros.Add("@full_name", cbModelo.Text + "-" + txtNome.Text);
                        parametros.Add("@addr", txtEndereco.Text);
                        parametros.Add("@num_parent", 0);
                        parametros.Add("@IP", IP);
                        parametros.Add("@porta", 0);
                        //parametros.Add("@casasDecimais", cbDecimais.Text);
                        parametros.Add("@dateinsert", DateTime.Today);
                        bool result = Program.SQL.CRUDCommand("insert into Rede (tipo, fabricante, modelo, protocolo, nome, parent, full_name, num_parent, IP, porta,addr, dateinsert) values (@tipo, @fabricante, @modelo, @protocolo, @nome, @parent, @full_name, @num_parent, @IP, @porta, @addr, @dateinsert)", "Rede", parametros);
                        
                        if (result)
                        {
                            InfoPopup info = new InfoPopup("Balança cadastrada com sucesso.", "A balança foi cadastrada e inserida no banco de dados.");
                            info.ShowDialog();
                            //MessageBox.Show("Balança adicionada com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pnBalança.Visible = false;
                            CleanInfoBalancas();
                            TreeNodeLoad();
                        }

                    }
                }

                else if(tvRede.SelectedNode.Tag.GetType() == typeof(RedeClass))
                {
                    RedeClass RedeNode = (RedeClass)tvRede.SelectedNode.Tag;
                    YesOrNo question = new YesOrNo($"Deseja mesmo editar a Balança {RedeNode.full_name} na rede?");
                    question.ShowDialog();


                    if (question.RESPOSTA)
                    {
                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                        parametros.Add("@id", RedeNode.Id);
                        parametros.Add("@fabricante", cbFabricante.Text);
                        parametros.Add("@modelo", cbModelo.Text);
                        parametros.Add("@protocolo", cbProtocolo.Text);
                        parametros.Add("@nome", txtNome.Text);
                        parametros.Add("@parent", RedeNode.parent);
                        parametros.Add("@full_name", cbModelo.Text + "-" + txtNome.Text);
                        parametros.Add("@num_parent", 0);
                        parametros.Add("@IP", IP);
                        parametros.Add("@porta", 0);
                        //parametros.Add("@casasDecimais", cbDecimais.Text);
                        parametros.Add("@dateupdate", DateTime.Today);
                        bool result = Program.SQL.CRUDCommand("update Rede set fabricante = @fabricante, modelo = @modelo, protocolo = @protocolo, nome = @nome, parent = @parent, full_name = @full_name, num_parent = @num_parent, IP = @IP, porta = @porta, dateupdate = @dateupdate where Id=@Id", "Rede", parametros);
                        if (result)
                        {
                            MessageBox.Show("Balança editada com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pnBalança.Visible = false;
                            CleanInfoBalancas();
                            TreeNodeLoad();
                        }
                    }
                }
            
            }
            else
            {
                MessageBox.Show("Preencha os campos em branco!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSalvarImp_Click(object sender, EventArgs e)
        {
            string IP = txtIPIMP1.Text + "." + txtIPIMP2.Text + "." + txtIPIMP3.Text + "." + txtIPIMP4.Text;
            IP = IP.Replace(" ", "");
            if (cbTipoImp.SelectedIndex == 1)
            {
                if (cbFabricanteImp.Text == "" || cbProtocoloImp.Text == "" || cbModeloImp.Text == "" || txtNomeImp.Text == "")
                {
                    MessageBox.Show("Preencha os campos em branco!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (cbTipoImp.SelectedIndex == 2)
            {
                if (cbFabricanteImp.Text == "" || cbProtocoloImp.Text == "" || cbModeloImp.Text == "" || txtNomeImp.Text == "" || IP == "..." || txtPortaImp.Text == "")
                {
                    MessageBox.Show("Preencha os campos em branco!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ValidarCampoIP(txtIPIMP1.Text, txtIPIMP2.Text, txtIPIMP3.Text, txtIPIMP4.Text) == false)
                {
                    MessageBox.Show("O IP digitado já existe, escolha outro!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if(cbTipoImp.SelectedIndex == 0)
            {
                if (txtNomeImp.Text == "" || cbImpressoras.Text == "")
                {
                    MessageBox.Show("Preencha os campos em branco!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (tvRede.SelectedNode.Tag == tvRede)
            {
                YesOrNo question = new YesOrNo("Deseja mesmo adicionar a Impressora na rede?");
                question.ShowDialog();

                if (question.RESPOSTA)
                {
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("@tipo", "Impressora");
                    parametros.Add("@tipo_impressao", cbTipoImp.SelectedIndex);
                    parametros.Add("@impressora", cbImpressoras.Text);
                    parametros.Add("@simplificar", chkSimples.Checked);
                    parametros.Add("@fabricante", cbFabricanteImp.Text);
                    parametros.Add("@modelo", cbModeloImp.Text);
                    parametros.Add("@protocolo", cbProtocoloImp.Text);
                    parametros.Add("@nome", txtNomeImp.Text);
                    parametros.Add("@parent", "-");
                    if (cbTipoImp.SelectedIndex == 0)
                    {
                        parametros.Add("@full_name", txtNomeImp.Text);
                    }
                    else
                    {
                        parametros.Add("@full_name", cbModeloImp.Text + "-" + txtNomeImp.Text);
                    }    
                    parametros.Add("@num_parent", 0);
                    parametros.Add("@IP", IP);
                    parametros.Add("@porta", txtPortaImp.Text);
                    parametros.Add("@dateinsert", DateTime.Today);
                    bool result = Program.SQL.CRUDCommand("insert into Rede (tipo, tipo_impressao, impressora, simplificar, fabricante, modelo, protocolo, nome, parent, full_name, num_parent, IP, porta, dateinsert) values (@tipo, @tipo_impressao, @impressora, @simplificar, @fabricante, @modelo, @protocolo, @nome, @parent, @full_name, @num_parent, @IP, @porta, @dateinsert)", "Rede", parametros);
                    if (result)
                    {
                        MessageBox.Show("Impressora adicionada com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnImpressora.Visible = false;
                        CleanInfoImpressora();
                        TreeNodeLoad();
                    }
                }
            }
            else if (tvRede.SelectedNode.Tag is RedeClass)
            {
                RedeClass RedeNode = (RedeClass)tvRede.SelectedNode.Tag;
                
                YesOrNo question = new YesOrNo($"Deseja mesmo editar a Impressora {RedeNode.full_name} na rede?");
                question.ShowDialog();

                if (question.RESPOSTA)
                {
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("@id", RedeNode.Id);
                    parametros.Add("@tipo_impressao", cbTipoImp.SelectedIndex);
                    parametros.Add("@impressora", cbImpressoras.Text);
                    parametros.Add("@simplificar", chkSimples.Checked);
                    parametros.Add("@fabricante", cbFabricanteImp.Text);
                    parametros.Add("@modelo", cbModeloImp.Text);
                    parametros.Add("@protocolo", cbProtocoloImp.Text);
                    parametros.Add("@nome", txtNomeImp.Text);
                    parametros.Add("@parent", RedeNode.parent);
                    if (cbTipoImp.SelectedIndex == 0)
                    {
                        parametros.Add("@full_name", txtNomeImp.Text);
                    }
                    else
                    {
                        parametros.Add("@full_name", cbModeloImp.Text + "-" + txtNomeImp.Text);
                    }
                    parametros.Add("@num_parent", 0);
                    parametros.Add("@IP", IP);
                    parametros.Add("@porta", txtPortaImp.Text);
                    parametros.Add("@dateupdate", DateTime.Today);
                    bool result = Program.SQL.CRUDCommand("update Rede set tipo_impressao = @tipo_impressao, impressora = @impressora, simplificar = @simplificar, fabricante = @fabricante, modelo = @modelo, protocolo = @protocolo, nome = @nome, parent = @parent, full_name = @full_name, num_parent = @num_parent, IP = @IP, porta = @porta, dateupdate = @dateupdate where Id=@Id", "Rede", parametros);
                    if (result)
                    {
                        MessageBox.Show("Impressora editada com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnImpressora.Visible = false;
                        CleanInfoImpressora();
                        TreeNodeLoad();
                    }
                }
            }
        }
        private void CleanInfoBalancas()
        {
            selectedNode = null;
            cbFabricante.Text = "";
            cbFabricante.SelectedIndex = -1;
            cbModelo.Text = "";
            cbModelo.SelectedIndex = -1;
            cbProtocolo.Text = "";
            cbProtocolo.SelectedIndex = -1;
            //cbDecimais.Text = "";
            //cbDecimais.SelectedIndex = -1;
            txtNome.Text = "";
            txtIP1.Text = "";
            txtIP2.Text = "";
            txtIP3.Text = "";
            txtIP4.Text = "";
            txtPorta.Text = "";
        }

        private void CleanInfoImpressora()
        {
            selectedNode = null;
            cbTipoImp.Text = "";
            cbTipoImp.SelectedIndex = -1;
            cbImpressoras.Text = "";
            cbImpressoras.SelectedIndex = -1;
            chkSimples.Checked = false;
            cbFabricanteImp.Text = "";
            cbFabricanteImp.SelectedIndex = -1;
            cbModeloImp.Text = "";
            cbModeloImp.SelectedIndex = -1;
            cbProtocoloImp.Text = "";
            cbProtocoloImp.SelectedIndex = -1;
            txtNomeImp.Text = "";
            txtIPIMP1.Text = "";
            txtIPIMP2.Text = "";
            txtIPIMP3.Text = "";
            txtIPIMP4.Text = "";
            txtPortaImp.Text = "";
        }

        private void TreeNodeLoad()
        {
            tvRede.Nodes.Clear();
            List<Object> result = Program.SQL.SelectList("select * from Rede", "Rede");
            TreeNode rootNode = new TreeNode(Environment.MachineName);

            if(result.Count != 0)
            {
                //POVOAR TODOS OS ROOTS DOS PC´s CADASTRADOS NA TABELA REDE
                List<TreeNode> rootsNodes = new List<TreeNode>();
                foreach (RedeClass redeNode in result)
                {
                    if(redeNode.parent != "-")
                    {
                        rootNode = new TreeNode(redeNode.parent);
                        rootNode.Tag = tvRede;
                        rootNode.ContextMenuStrip = MenuItem;
                        rootNode.ImageIndex = 0;
                        rootNode.SelectedImageIndex = 0;
                        if (rootsNodes.Count == 0)
                        {
                            rootsNodes.Add(rootNode);
                        }
                        else
                        {
                            bool bParentExist = false;
                            foreach (TreeNode item in rootsNodes)
                            {
                                if (rootNode.Text == item.Text)
                                {
                                    bParentExist = true;
                                    break;
                                }
                            }
                            if (bParentExist == false)
                            {
                                rootsNodes.Add(rootNode);
                            }
                        }
                    }
                }

                //POVOAR AS BALANCAS E IMPRESSORAS NOS ROOTS
                foreach (RedeClass redeNode in result)
                {
                    TreeNode ItemNode = new TreeNode(redeNode.full_name);
                    ItemNode.Tag = redeNode;
                    ItemNode.ContextMenuStrip = MenuItem;
                    if(redeNode.tipo == "Balança")
                    {
                        ItemNode.ImageIndex = 1;
                        ItemNode.SelectedImageIndex = 1;
                    }else if (redeNode.tipo == "Impressora")
                    {
                        ItemNode.ImageIndex = 2;
                        ItemNode.SelectedImageIndex = 2;
                    }
                    foreach (TreeNode item in rootsNodes)
                    {
                        if (redeNode.parent == item.Text)
                        {
                            item.Nodes.Add(ItemNode);
                        }
                    }
                }

                //POVOAR AS IMPRESSORAS NO ROOT
                foreach (RedeClass redeNode in result)
                {
                    if(redeNode.tipo == "Impressora" && redeNode.parent == "-")
                    {
                        TreeNode ImpNode = new TreeNode(redeNode.full_name);
                        ImpNode.Tag = redeNode;
                        ImpNode.ContextMenuStrip = MenuItem;
                        ImpNode.ImageIndex = 2;
                        ImpNode.SelectedImageIndex = 2;
                        rootsNodes.Add(ImpNode);
                    }
                }
                bool b_MyBase = false;
                foreach (TreeNode item in rootsNodes)
                {
                    if(item.Text == Environment.MachineName)
                    {
                        b_MyBase = true;
                    }
                }
                if(b_MyBase == false)
                {
                    rootNode = new TreeNode(Environment.MachineName);
                    rootNode.Tag = tvRede;
                    rootNode.ContextMenuStrip = MenuItem;
                    rootNode.ImageIndex = 0;
                    rootNode.SelectedImageIndex = 0;
                    rootsNodes.Add(rootNode);
                }

                foreach (TreeNode item in rootsNodes)
                {
                    tvRede.Nodes.Add(item);
                }
            }
            else
            {
                rootNode.Tag = tvRede;
                rootNode.ContextMenuStrip = MenuItem;
                rootNode.ImageIndex = 0;
                rootNode.SelectedImageIndex = 0;
                tvRede.Nodes.Add(rootNode);
            }
            
            tvRede.ExpandAll();
        }

        private void tvRede_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode clickedNode = tvRede.GetNodeAt(e.X, e.Y);
                if (clickedNode != null)
                {
                    tvRede.SelectedNode = clickedNode;
                    btnEditar_Click(sender, e);
                }
            }
        }

        private void cbFabrincateImp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbModeloImp.Items.Clear();
            cbProtocoloImp.Items.Clear();
            if (cbFabricanteImp.Text == "Zebra")
            {
                cbModeloImp.Items.Add("ZD420");
            }
         }

        private void cbModeloImp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProtocoloImp.Items.Clear();
            if (cbModeloImp.Text == "ZD420")
            {
                cbProtocoloImp.Items.Add("ZLP");
            }
        }

        private bool ValidarCampoIP(string IP1, string IP2, string IP3, string IP4)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string IP = IP1 + "." + IP2 + "." + IP3 + "." + IP4;
            IP = IP.Replace(" ", "");
            parametros.Add("@IP", IP);
            List<Object> result = Program.SQL.SelectList("select * from Rede where IP=@IP", "Rede", values: parametros);
            if (result.Count > 0)
            {
                if (result.Count > 1)
                {
                    return false;
                }
                else
                {
                    RedeClass Rede = (RedeClass)result[0];
                    if (tvRede.SelectedNode.Tag == tvRede)
                    {
                        //NOVO
                        return false;
                    }else if (tvRede.SelectedNode.Tag is RedeClass)
                    {
                        RedeClass RedeNode = (RedeClass)tvRede.SelectedNode.Tag;
                        if(Rede.Id != RedeNode.Id)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        private void RedeConfigForm_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                List<object> _rede = Program.SQL.SelectList("SELECT * FROM Rede WHERE parent = @parent", "Rede", null, new Dictionary<string, object>() { { "@parent", Environment.MachineName.Trim() } });
                Program.REDE = _rede.Cast<RedeClass>().ToList();

                CommunicationForms communication = Application.OpenForms.OfType<CommunicationForms>().FirstOrDefault();
                //communication.LoadAll();
            }
            catch (Exception)
            {
            }
        }

        private void vLoadInstalledPrinters()
        {
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                cbImpressoras.Items.Add(printerName);
            }
        }

        private void cbTipoImp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipoImp.SelectedIndex == 0)
            {
                pProtocolo.Visible = false;
                pEthernet.Visible = false;
                pInstalled.Visible = true;
            }
            else if (cbTipoImp.SelectedIndex == 1)
            {
                pProtocolo.Visible = true;
                pEthernet.Visible = false;
                pInstalled.Visible = false;
            }
            else if (cbTipoImp.SelectedIndex == 2)
            {
                pProtocolo.Visible = true;
                pEthernet.Visible = true;
                pInstalled.Visible = false;
            }
            else
            {
                pProtocolo.Visible = false;
                pEthernet.Visible = false;
                pInstalled.Visible = false;
            }
        }

        private void txtEndereco_Leave(object sender, EventArgs e)
        {
            try
            {
                var list = Program.SQL.SelectList("SELECT * FROM Rede WHERE addr = @addr AND tipo = 'Balança'", "Rede", null,
                    new Dictionary<string, object>() 
                    {
                        {"@addr", txtEndereco.Text}
                    });

                if (list.Count > 0) 
                {
                    txtEndereco.Text = "";
                    InfoPopup popup = new InfoPopup("Campo endereço já em uso!", "O campo de endereço digitado, já está sendo usado por uma balança cadastrada no sistema.");
                    popup.Show();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
