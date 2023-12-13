using Main.Model;
using Main.View.PopupFolder;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Main.Helper
{
    public class SQLContext
    {
        public string TIPO = string.Empty;
        public SQLContext(string _TIPO)
        {
            try
            {
                TIPO = _TIPO;
            }
            catch (Exception)
            {
            }
        }

        public SqlConnection getConnectionSql()
        {
            try
            {
                //string t = "Data Source=10.0.0.12\\SQLEXPRESS;Initial Catalog=PROTHEUS; User ID=sa; Pwd=AEPH1234*#";
                SqlConnection con = new SqlConnection(TIPO);
                con.Open();
                con.Close();
                con.Dispose();
                return new SqlConnection(TIPO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                QuestionPopup question = new QuestionPopup("Não foi possível estabelecer a conexão com o banco de dados, por favor verifique a string de conexão e tente novamente.");
                question.ShowDialog();

                if (!string.IsNullOrWhiteSpace(question.Message)) 
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(question.Message);
                        con.Open();
                        con.Close();
                        con.Dispose();

                        //Salvar a string de banco de dados que deu certo, no arquivo de config.
                        //e mudar o program para acessar essa nova string.

                        XmlDocument doc = new XmlDocument();
                        string caminhoCompleto = Path.Combine(Application.StartupPath, Program.CFG.CaminhoConfig);
                        doc.Load(caminhoCompleto);
                        XmlElement elem = (XmlElement)doc.SelectSingleNode("//SQLConnection");
                        if (elem != null)
                        {
                            elem.InnerText = question.Message;
                        }
                        try
                        {
                            doc.Save(caminhoCompleto);
                            MessageBox.Show("String de Conexão SQL salva com sucesso!", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.CFG.sqlConnection = question.Message;
                        }
                        catch (Exception exSaveE)
                        {
                            MessageBox.Show($"Erro ao salvar String de Conexão SQL da Estação! {exSaveE}", "VC Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        return new SqlConnection(question.Message);
                    }
                    catch (Exception)
                    {
                        YesOrNo question_fatal_error = new YesOrNo("O software está encontrando dificuldades para acessar a base de dados, gostaria de reiniciar o programa para tentar a conexão novamente ?");
                        question_fatal_error.ShowDialog();
                        if (question_fatal_error.RESPOSTA) 
                        {
                            Application.Restart();
                        }
                    }
                }

                return new SqlConnection();
            }
        }

        public bool CRUDCommand(string querry, string tabela, [Optional] Dictionary<string, object> values)
        {
            try
            {
                using (SqlConnection con = getConnectionSql())
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(querry, con);

                        if (values.Count > 0)
                        {
                            foreach (var item in values)
                            {
                                if (item.Value == null)
                                {
                                    cmd.Parameters.Add(new SqlParameter(item.Key, " "));
                                }
                                else
                                {
                                    if (item.Value is int)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToInt32(item.Value)));
                                    }

                                    if (item.Value is decimal)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToDecimal(item.Value)));
                                    }

                                    if (item.Value is double)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToDouble(item.Value)));
                                    }

                                    if (item.Value is bool)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToBoolean(item.Value)));
                                    }

                                    if (item.Value is string)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToString(item.Value)));
                                    }

                                    if (item.Value is float)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToSingle(item.Value)));
                                    }

                                    if (item.Value is DateTime)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToDateTime(item.Value)));
                                    }
                                }

                            }
                        }

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<object> SelectList(string querry, string tabela, [Optional] string coluna_to_return,  [Optional] Dictionary<string, object> values, [Optional] int quantidade)
        {
            try
            {
                using (SqlConnection con = getConnectionSql())
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(querry, con);

                        if (values != null && values.Count > 0) { foreach (var item in values) { cmd.Parameters.Add(new SqlParameter(item.Key, item.Value)); } }

                        SqlDataReader dr = cmd.ExecuteReader();
                        List<object> list_return = new List<object>();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (tabela == "Rede")
                                {
                                    if (coluna_to_return is null)
                                    {
                                        RedeClass Rede = new RedeClass();
                                        Rede.Id = Convert.ToInt32(dr["Id"]);
                                        Rede.tipo = (string)dr["tipo"];
                                        if (dr["tipo_impressao"] != DBNull.Value)
                                        {
                                            Rede.tipo_impressao = (int)dr["tipo_impressao"];
                                        }
                                        if (dr["impressora"] != DBNull.Value)
                                        {
                                            Rede.impressora = (string)dr["impressora"];
                                        }
                                        if (dr["simplificar"] != DBNull.Value)
                                        {
                                            Rede.simplificar = (bool)dr["simplificar"];
                                        }
                                        Rede.fabricante = (string)dr["fabricante"];
                                        Rede.modelo = (string)dr["modelo"];
                                        Rede.protocolo = (string)dr["protocolo"];
                                        Rede.nome = (string)dr["nome"];
                                        if (dr["addr"] != DBNull.Value)
                                        {
                                            Rede.addr = Convert.ToInt32(dr["addr"]);
                                        }
                                        if (dr["baud_rate"] != DBNull.Value)
                                        {
                                            Rede.baud_rate = Convert.ToInt32(dr["baud_rate"]);
                                        }
                                        Rede.parent = (string)dr["parent"];
                                        Rede.full_name = (string)dr["full_name"];
                                        Rede.num_parent = Convert.ToInt32(dr["num_parent"]);
                                        Rede.IP = (string)dr["IP"];
                                        Rede.porta = Convert.ToInt32(dr["porta"]);
                                        if (dr["MAC"] != DBNull.Value)
                                        {
                                            Rede.MAC = (string)dr["MAC"];
                                        }
                                        if (dr["casasDecimais"] != DBNull.Value)
                                        {
                                            Rede.casasDecimais = Convert.ToInt32(dr["casasDecimais"]);
                                        }
                                        Rede.dateinsert = (DateTime)dr["dateinsert"];
                                        if (dr["dateupdate"] != DBNull.Value)
                                        {
                                            Rede.dateupdate = (DateTime)dr["dateupdate"];
                                        }

                                        list_return.Add(Rede);
                                    }
                                    else
                                    {
                                        list_return.Add(dr[coluna_to_return]);
                                    }
                                }
                                if (tabela == "Configuracao")
                                {
                                    if (coluna_to_return is null)
                                    {
                                        ConfiguracaoClass Config = new ConfiguracaoClass();
                                        Config.id = Convert.ToInt32(dr["id"]);
                                        Config.estacao = (string)dr["estacao"];
                                        Config.id_Impressora = Convert.ToInt32(dr["id_Impressora"]);
                                        Config.id_Etiqueta = Convert.ToInt32(dr["id_Etiqueta"]);
                                        Config.copias = Convert.ToInt32(dr["copias"]);
                                        if (dr["logo_empresa"] != DBNull.Value)
                                        {
                                            Config.logo_empresa = (byte[])dr[""];
                                        }
                                        Config.dateinsert = (DateTime)dr["dateinsert"];
                                        if (dr["dateupdate"] != DBNull.Value)
                                        {
                                            Config.dateupdate = (DateTime)dr["dateupdate"];
                                        }
                                        list_return.Add(Config);
                                    }
                                    else
                                    {
                                        list_return.Add(dr[coluna_to_return]);
                                    }
                                }
                                if (tabela == "Usuario")
                                {
                                    if (coluna_to_return is null)
                                    {
                                        UsuarioClass Usuario = new UsuarioClass();
                                        Usuario.Id = Convert.ToInt32(dr["Id"]);
                                        Usuario.Nome = Convert.ToString(dr["Nome"]);
                                        Usuario.Login = Convert.ToString(dr["login"]);
                                        Usuario.Senha = Convert.ToString(dr["senha"]);
                                        Usuario.Acesso = Convert.ToString(dr["acesso"]);
                                        list_return.Add(Usuario);
                                    }
                                    else
                                    {
                                        list_return.Add(dr[coluna_to_return]);
                                    }
                                }
                                if (tabela == "Etiqueta")
                                {
                                    if (coluna_to_return is null)
                                    {
                                        EtiquetaClass etiqueta = new EtiquetaClass();
                                        etiqueta.id = Convert.ToInt32(dr["id"]);
                                        etiqueta.nome_etiqueta = Convert.ToString(dr["nome_etiqueta"]);
                                        etiqueta.arquivo = Convert.ToString(dr["arquivo"]);
                                        etiqueta.dateinsert = (DateTime)dr["dateinsert"];
                                        if (dr["dateupdate"] != DBNull.Value)
                                        {
                                            etiqueta.dateupdate = (DateTime)dr["dateupdate"];
                                        }
                                        list_return.Add(etiqueta);
                                    }
                                    else
                                    {
                                        list_return.Add(dr[coluna_to_return]);
                                    }
                                }
                                if (tabela == "Pesagem")
                                {
                                    if (coluna_to_return is null)
                                    {
                                        PesagemClass pesagem = new PesagemClass();
                                        pesagem.Id = Convert.ToInt32(dr["id"]);
                                        pesagem.CodigoProduto = Convert.ToString(dr["CodigoProduto"]);
                                        pesagem.Descricao = Convert.ToString(dr["Descricao"]);
                                        pesagem.PesoAlvo = Convert.ToString(dr["PesoAlvo"]);
                                        pesagem.PesoReal = Convert.ToString(dr["PesoReal"]);

                                        pesagem.Tolerencia = Convert.ToString(dr["Tolerencia"]);
                                        pesagem.flag_sync = Convert.ToBoolean(dr["flag_sync"]);

                                        pesagem.dateinsert = (DateTime)dr["dateinsert"];
                                        list_return.Add(pesagem);
                                    }
                                    else
                                    {
                                        list_return.Add(dr[coluna_to_return]);
                                    }
                                }

                                if (tabela == "Produto")
                                {
                                    ProdutoClass produto = new ProdutoClass();
                                    produto.Id = Convert.ToInt32(dr["id"]);
                                    produto.Part_number = Convert.ToString(dr["part_number"]);
                                    produto.Descricao = Convert.ToString(dr["descricao"]);
                                    produto.part_number_cliente = Convert.ToString(dr["part_number_cliente"]);
                                    produto.PesoAlvo = Convert.ToSingle(dr["Peso_alvo"]);
                                    produto.Tolerancia = Convert.ToSingle(dr["Tolerancia"]);
                                    string imageBase64 = dr["Foto"].ToString();
                                    produto.Foto = Convert.FromBase64String(imageBase64);
                                    //produto.Foto = Convert.ToBase64String(Convert.ToString(dr["Foto"]));
                                    produto.DateInsert = (DateTime)dr["dateinsert"];
                                    produto.CodigoEarn = Convert.ToString(dr["CodigoEarn"]);
                                    list_return.Add(produto);
                                }

                                if (tabela == "Bandeja")
                                {
                                    BandejaClass bandeja = new BandejaClass();
                                    bandeja.Id = Convert.ToInt32(dr["id"]);
                                    bandeja.Codigo = Convert.ToString(dr["Codigo"]);
                                    bandeja.Descricao = Convert.ToString(dr["descricao"]);
                                    bandeja.PesoAlvo = Convert.ToSingle(dr["Peso_alvo"]);
                                    bandeja.Tolerancia = Convert.ToSingle(dr["Tolerancia"]);
                                    bandeja.Quantidade_produtos = Convert.ToDouble(dr["Quantidade_Produtos"]);
                                    string imageBase64 = dr["Foto"].ToString();
                                    bandeja.Foto = Convert.FromBase64String(imageBase64);
                                    //produto.Foto = Convert.ToBase64String(Convert.ToString(dr["Foto"]));
                                    bandeja.DateInsert = (DateTime)dr["dateinsert"];
                                    list_return.Add(bandeja);
                                }

                                if (tabela == "Recipiente")
                                {
                                    RecipienteClass recipiente = new RecipienteClass();
                                    recipiente.Id = Convert.ToInt32(dr["id"]);
                                    recipiente.Package = Convert.ToString(dr["Package"]);
                                    recipiente.Descricao = Convert.ToString(dr["descricao"]);
                                    recipiente.PesoAlvo = Convert.ToSingle(dr["Peso_alvo"]);
                                    recipiente.Tolerancia = Convert.ToSingle(dr["Tolerancia"]);
                                    string imageBase64 = dr["Foto"].ToString();
                                    recipiente.Foto = Convert.FromBase64String(imageBase64);
                                    //produto.Foto = Convert.ToBase64String(Convert.ToString(dr["Foto"]));
                                    recipiente.DateInsert = (DateTime)dr["dateinsert"];
                                    list_return.Add(recipiente);
                                }

                                if (tabela == "tipoReceita")
                                {
                                    TipoReceitaClass Trecipiente = new TipoReceitaClass();
                                    Trecipiente.Id = Convert.ToInt32(dr["id"]);
                                    Trecipiente.TipoItem = Convert.ToString(dr["Tipo_item"]);
                                    Trecipiente.DateInsert = (DateTime)dr["dateinsert"];
                                    list_return.Add(Trecipiente);
                                }

                                if (tabela == "Receita") 
                                {
                                    ReceitaClass receita = new ReceitaClass();
                                    receita.Id = Convert.ToInt32(dr["id"]);
                                    receita.Nome = Convert.ToString(dr["Nome"]);
                                    receita.Codigo = Convert.ToString(dr["Codigo"]);
                                    receita.PKSKF = Convert.ToString(dr["PkSKF"]);
                                    receita.Id_Produto = Convert.ToInt32(dr["id_produto"]);
                                    receita.Id_Recipiente = Convert.ToInt32(dr["id_recipiente"]);
                                    receita.Id_Bandeja = Convert.ToInt32(dr["id_bandeja"]);
                                    receita.Quantidade_pecas = Convert.ToInt32(dr["Quantidade_pecas"]);
                                    receita.Quantidade_bandejas = Convert.ToInt32(dr["Quantidade_bandejas"]);
                                    receita.Operador = Convert.ToString(dr["Operador"]);
                                    receita.Status = Convert.ToInt32(dr["Status"]);
                                    receita.DateInsert = (DateTime)dr["dateinsert"];
                                    list_return.Add(receita);
                                }


                                if (tabela  == "LogReceita") 
                                {
                                    ReceitaLogClass receitaLog = new ReceitaLogClass();
                                    receitaLog.Id = Convert.ToInt32(dr["id"]);
                                    receitaLog.Codigo = Convert.ToString(dr["Codigo"]);
                                    receitaLog.Nome = Convert.ToString(dr["Nome"]);
                                    receitaLog.id_receita = Convert.ToInt32(dr["id_receita"]);
                                    receitaLog.id_Recipiente = Convert.ToInt32(dr["id_Recipiente"]);
                                    receitaLog.Peso_Recipiente = Convert.ToSingle(dr["Peso_Recipiente"]);

                                    if (dr["Peso_Recipiente_Pesado"] != DBNull.Value) 
                                    {
                                        receitaLog.Peso_Recipiente_Pesado = Convert.ToSingle(dr["Peso_Recipiente_Pesado"]);
                                    }
                               
                                    receitaLog.id_Bandeja = Convert.ToInt32(dr["id_Bandeja"]);
                                    receitaLog.Qtd_Bandeja = Convert.ToInt32(dr["Qtd_Bandeja"]);

                                    if (dr["Qtd_Bandeja_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Qtd_Bandeja_Pesado = Convert.ToInt32(dr["Qtd_Bandeja_Pesado"]);
                                    }
                                    
                                    receitaLog.Peso_Bandejas = Convert.ToSingle(dr["Peso_Bandejas"]);
                                   
                                    if (dr["Peso_Bandejas_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Peso_Bandejas_Pesado = Convert.ToSingle(dr["Peso_Bandejas_Pesado"]);
                                    }

                                    receitaLog.id_Produto = Convert.ToInt32(dr["id_Produto"]);
                                    receitaLog.Qtd_Pecas = Convert.ToInt32(dr["Qtd_Pecas"]);

                                    if (dr["Qtd_Pecas_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Qtd_Pecas_Pesado = Convert.ToInt32(dr["Qtd_Pecas_Pesado"]);
                                    }

                                    receitaLog.Peso_Pecas = Convert.ToSingle(dr["Peso_Pecas"]);

                                    if (dr["Peso_Pecas_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Peso_Pecas_Pesado = Convert.ToSingle(dr["Peso_Pecas_Pesado"]);
                                    }
                                   
                                    receitaLog.Estacao = Convert.ToString(dr["Estacao"]);
                                    receitaLog.Operador = Convert.ToString(dr["Operador"]);
                                    receitaLog.Status = Convert.ToInt32(dr["Status"]);
                                    receitaLog.dateinsert = (DateTime)dr["dateinsert"];
                                    list_return.Add(receitaLog);
                                }

                                if (tabela == "CustomReceitaInfo") 
                                {
                                    CustomReceitaInfo customReceita = new CustomReceitaInfo();
                                    customReceita.id = Convert.ToInt32(dr["Id"]);
                                    customReceita.Nome = Convert.ToString(dr["Nome"]);
                                    customReceita.Codigo = Convert.ToString(dr["Codigo"]);
                                    customReceita.QuantidadePecas = Convert.ToInt32(dr["Quantidade_pecas"]);
                                    customReceita.QuantidadeBandejas = Convert.ToInt32(dr["Quantidade_bandejas"]);
                                    customReceita.Status = Convert.ToInt32(dr["Status"]);
                                    customReceita.Date = Convert.ToDateTime(dr["Data"]);
                                    list_return.Add(customReceita);
                                }

                                if (tabela == "Log_bandeja_receita") 
                                {
                                    Log_bandeja_receitaClass logBand = new Log_bandeja_receitaClass();
                                    logBand.Id = Convert.ToInt32(dr["Id"]);
                                    logBand.Id_Log_Receita = Convert.ToInt32(dr["id_log_receita"]);
                                    logBand.Numero_Bandejas = Convert.ToInt32(dr["Numero_Bandeja"]);
                                    logBand.Peso_Bandeja = Convert.ToSingle(dr["Peso_bandeja"]);
                                    logBand.Peso_Produto = Convert.ToSingle(dr["Peso_Produto"]);
                                    list_return.Add(logBand);
                                }
                            }
                        }
                        return list_return;
                    }
                    catch (Exception ex)
                    {
                        return new List<object>();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<object>();
            }
        }

        public object SelectObject(string querry, string tabela, [Optional] Dictionary<string, object> values, [Optional] int quantidade)
        {
            try
            {
                using (SqlConnection con = getConnectionSql())
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(querry, con);

                        if (values != null && values.Count > 0) { foreach (var item in values) { cmd.Parameters.Add(new SqlParameter(item.Key, item.Value)); } }

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (tabela == "Contador Produtos")
                                {
                                    return Convert.ToInt32(dr["contador"]);
                                }

                                if (tabela == "Usuario")
                                {
                                    UsuarioClass Usuario = new UsuarioClass();
                                    Usuario.Id = Convert.ToInt32(dr["Id"]);
                                    Usuario.Nome = Convert.ToString(dr["Nome"]);
                                    Usuario.Login = Convert.ToString(dr["login"]);
                                    Usuario.Senha = Convert.ToString(dr["senha"]);
                                    Usuario.Acesso = Convert.ToString(dr["acesso"]);
                                    return Usuario;
                                }

                                if (tabela == "Produto") 
                                {
                                    ProdutoClass produto = new ProdutoClass();
                                    produto.Id = Convert.ToInt32(dr["id"]);
                                    produto.Part_number = Convert.ToString(dr["part_number"]);
                                    produto.Descricao = Convert.ToString(dr["descricao"]);
                                    produto.PesoAlvo = Convert.ToSingle(dr["Peso_alvo"]);
                                    produto.Tolerancia = Convert.ToSingle(dr["Tolerancia"]);
                                    produto.part_number_cliente = Convert.ToString(dr["part_number_cliente"]);
                                    string imageBase64 = dr["Foto"].ToString();
                                    produto.Foto = Convert.FromBase64String(imageBase64);
                                    //produto.Foto = Convert.ToBase64String(Convert.ToString(dr["Foto"]));
                                    produto.DateInsert = (DateTime)dr["dateinsert"];
                                    produto.CodigoEarn = Convert.ToString(dr["CodigoEarn"]);
                                    return produto;
                                }

                                if (tabela == "Bandeja")
                                {
                                    BandejaClass bandeja = new BandejaClass();
                                    bandeja.Id = Convert.ToInt32(dr["id"]);
                                    bandeja.Codigo = Convert.ToString(dr["Codigo"]);
                                    bandeja.Descricao = Convert.ToString(dr["descricao"]);
                                    bandeja.PesoAlvo = Convert.ToSingle(dr["Peso_alvo"]);
                                    bandeja.Tolerancia = Convert.ToSingle(dr["Tolerancia"]);
                                    bandeja.Quantidade_produtos = Convert.ToDouble(dr["Quantidade_Produtos"]);
                                    string imageBase64 = dr["Foto"].ToString();
                                    bandeja.Foto = Convert.FromBase64String(imageBase64);
                                    //produto.Foto = Convert.ToBase64String(Convert.ToString(dr["Foto"]));
                                    bandeja.DateInsert = (DateTime)dr["dateinsert"];
                                    return bandeja;
                                }

                                if (tabela == "Recipiente")
                                {
                                    RecipienteClass recipiente = new RecipienteClass();
                                    recipiente.Id = Convert.ToInt32(dr["id"]);
                                    recipiente.Package = Convert.ToString(dr["Package"]);
                                    recipiente.Descricao = Convert.ToString(dr["descricao"]);
                                    recipiente.PesoAlvo = Convert.ToSingle(dr["Peso_alvo"]);
                                    recipiente.Tolerancia = Convert.ToSingle(dr["Tolerancia"]);
                                    string imageBase64 = dr["Foto"].ToString();
                                    recipiente.Foto = Convert.FromBase64String(imageBase64);
                                    //produto.Foto = Convert.ToBase64String(Convert.ToString(dr["Foto"]));
                                    recipiente.DateInsert = (DateTime)dr["dateinsert"];
                                    return recipiente;
                                }

                                if (tabela == "tipoReceita")
                                {
                                    TipoReceitaClass Trecipiente = new TipoReceitaClass();
                                    Trecipiente.Id = Convert.ToInt32(dr["id"]);
                                    Trecipiente.TipoItem = Convert.ToString(dr["Tipo_item"]);
                                    Trecipiente.DateInsert = (DateTime)dr["dateinsert"];
                                    return Trecipiente;
                                }

                                if (tabela == "Receita")
                                {
                                    ReceitaClass receita = new ReceitaClass();
                                    receita.Id = Convert.ToInt32(dr["id"]);
                                    receita.Nome = Convert.ToString(dr["Nome"]);
                                    receita.Codigo = Convert.ToString(dr["Codigo"]);
                                    receita.PKSKF = Convert.ToString(dr["PkSKF"]);
                                    receita.Id_Produto = Convert.ToInt32(dr["id_produto"]);
                                    receita.Id_Bandeja = Convert.ToInt32(dr["id_bandeja"]);
                                    receita.Id_Recipiente = Convert.ToInt32(dr["id_recipiente"]);
                                    receita.Quantidade_pecas = Convert.ToInt32(dr["Quantidade_pecas"]);
                                    receita.Quantidade_bandejas = Convert.ToInt32(dr["Quantidade_bandejas"]);
                                    receita.Operador = Convert.ToString(dr["Operador"]);
                                    receita.Status = Convert.ToInt32(dr["Status"]);
                                    receita.DateInsert = (DateTime)dr["dateinsert"];
                                    return receita;
                                }

                                if (tabela == "LogReceita")
                                {
                                    ReceitaLogClass receitaLog = new ReceitaLogClass();
                                    receitaLog.Id = Convert.ToInt32(dr["id"]);
                                    receitaLog.Codigo = Convert.ToString(dr["Codigo"]);
                                    receitaLog.Nome = Convert.ToString(dr["Nome"]);
                                    receitaLog.id_receita = Convert.ToInt32(dr["id_receita"]);
                                    receitaLog.id_Recipiente = Convert.ToInt32(dr["id_Recipiente"]);
                                    receitaLog.Peso_Recipiente = Convert.ToSingle(dr["Peso_Recipiente"]);

                                    if (dr["Peso_Recipiente_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Peso_Recipiente_Pesado = Convert.ToSingle(dr["Peso_Recipiente_Pesado"]);
                                    }

                                    receitaLog.id_Bandeja = Convert.ToInt32(dr["id_Bandeja"]);
                                    receitaLog.Qtd_Bandeja = Convert.ToInt32(dr["Qtd_Bandeja"]);

                                    if (dr["Qtd_Bandeja_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Qtd_Bandeja_Pesado = Convert.ToInt32(dr["Qtd_Bandeja_Pesado"]);
                                    }

                                    receitaLog.Peso_Bandejas = Convert.ToSingle(dr["Peso_Bandejas"]);

                                    if (dr["Peso_Bandejas_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Peso_Bandejas_Pesado = Convert.ToSingle(dr["Peso_Bandejas_Pesado"]);
                                    }

                                    receitaLog.id_Produto = Convert.ToInt32(dr["id_Produto"]);
                                    receitaLog.Qtd_Pecas = Convert.ToInt32(dr["Qtd_Pecas"]);

                                    if (dr["Qtd_Pecas_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Qtd_Pecas_Pesado = Convert.ToInt32(dr["Qtd_Pecas_Pesado"]);
                                    }

                                    receitaLog.Peso_Pecas = Convert.ToSingle(dr["Peso_Pecas"]);

                                    if (dr["Peso_Pecas_Pesado"] != DBNull.Value)
                                    {
                                        receitaLog.Peso_Pecas_Pesado = Convert.ToSingle(dr["Peso_Pecas_Pesado"]);
                                    }

                                    receitaLog.Estacao = Convert.ToString(dr["Estacao"]);
                                    receitaLog.Operador = Convert.ToString(dr["Operador"]);
                                    receitaLog.Status = Convert.ToInt32(dr["Status"]);
                                    receitaLog.dateinsert = (DateTime)dr["dateinsert"];
                                    return receitaLog;
                                }

                                if (tabela == "Acessos")
                                {
                                    PermissaoClass per = new PermissaoClass();
                                    per.Id = Convert.ToInt32(dr["Id"]);
                                    per.Acesso = Convert.ToString(dr["Acesso"]);

                                    per.Pesagem_View = Convert.ToBoolean(dr["pesagem_view"]);
                                    per.Pesagem_add = Convert.ToBoolean(dr["pesagem_add"]);
                                    per.Pesagem_edit = Convert.ToBoolean(dr["pesagem_edit"]);
                                    per.Pesagem_remove = Convert.ToBoolean(dr["pesagem_remove"]);
                                    per.Pesagem_search = Convert.ToBoolean(dr["pesagem_search"]);

                                    per.Relatorio_View = Convert.ToBoolean(dr["relatorio_view"]);
                                    per.Relatorio_add = Convert.ToBoolean(dr["relatorio_add"]);
                                    per.Relatorio_edit = Convert.ToBoolean(dr["relatorio_edit"]);
                                    per.Relatorio_remove = Convert.ToBoolean(dr["relatorio_remove"]);
                                    per.Relatorio_search = Convert.ToBoolean(dr["relatorio_search"]);

                                    per.Rede_View = Convert.ToBoolean(dr["rede_view"]);
                                    per.Rede_add = Convert.ToBoolean(dr["rede_add"]);
                                    per.Rede_edit = Convert.ToBoolean(dr["rede_edit"]);
                                    per.Rede_remove = Convert.ToBoolean(dr["rede_remove"]);
                                    per.Rede_search = Convert.ToBoolean(dr["rede_search"]);

                                    per.Sistema_View = Convert.ToBoolean(dr["sistema_view"]);
                                    per.Sistema_add = Convert.ToBoolean(dr["sistema_add"]);
                                    per.Sistema_edit = Convert.ToBoolean(dr["sistema_edit"]);
                                    per.Sistema_remove = Convert.ToBoolean(dr["sistema_remove"]);
                                    per.Sistema_search = Convert.ToBoolean(dr["sistema_search"]);

                                    per.Usuario_View = Convert.ToBoolean(dr["usuario_view"]);
                                    per.Usuario_add = Convert.ToBoolean(dr["usuario_add"]);
                                    per.Usuario_edit = Convert.ToBoolean(dr["usuario_edit"]);
                                    per.Usuario_remove = Convert.ToBoolean(dr["usuario_remove"]);
                                    per.Usuario_search = Convert.ToBoolean(dr["usuario_search"]);

                                    per.receita_view= Convert.ToBoolean(dr["receita_view"]);
                                    per.receita_add = Convert.ToBoolean(dr["receita_add"]);
                                    per.receita_edit = Convert.ToBoolean(dr["receita_edit"]);
                                    per.receita_remove = Convert.ToBoolean(dr["receita_remove"]);
                                    per.receita_search = Convert.ToBoolean(dr["receita_search"]);

                                    per.tipoReceita_view = Convert.ToBoolean(dr["tipoReceita_view"]);
                                    per.tipoReceita_add = Convert.ToBoolean(dr["tipoReceita_add"]);
                                    per.tipoReceita_edit = Convert.ToBoolean(dr["tipoReceita_edit"]);
                                    per.tipoReceita_remove = Convert.ToBoolean(dr["tipoReceita_remove"]);
                                    per.tipoReceita_search = Convert.ToBoolean(dr["tipoReceita_search"]);

                                    per.Recipiente_view = Convert.ToBoolean(dr["Recipiente_view"]);
                                    per.Recipiente_add = Convert.ToBoolean(dr["Recipiente_add"]);
                                    per.Recipiente_edit = Convert.ToBoolean(dr["Recipiente_edit"]);
                                    per.Recipiente_remove = Convert.ToBoolean(dr["Recipiente_remove"]);
                                    per.Recipiente_search = Convert.ToBoolean(dr["Recipiente_search"]);

                                    per.Bandeja_view = Convert.ToBoolean(dr["Bandeja_view"]);
                                    per.Bandeja_add = Convert.ToBoolean(dr["Bandeja_add"]);
                                    per.Bandeja_edit = Convert.ToBoolean(dr["Bandeja_edit"]);
                                    per.Bandeja_remove = Convert.ToBoolean(dr["Bandeja_remove"]);
                                    per.Bandeja_search = Convert.ToBoolean(dr["Bandeja_search"]);

                                    per.Produto_view = Convert.ToBoolean(dr["Produto_view"]);
                                    per.Produto_add = Convert.ToBoolean(dr["Produto_add"]);
                                    per.Produto_edit = Convert.ToBoolean(dr["Produto_edit"]);
                                    per.Produto_remove = Convert.ToBoolean(dr["Produto_remove"]);
                                    per.Produto_search = Convert.ToBoolean(dr["Produto_search"]);

                                    per.Id_Usuario = Convert.ToInt32(dr["id_usuario"]);

                                    return per;
                                }
                            }
                        }
                        return null;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
                return new object();
            }
        }

        public DataTable SelectDataGrid(string comando, string tabela, [Optional] Dictionary<string, object> values)
        {
            using (SqlConnection con = getConnectionSql())
            {
                try
                {
                    con.Open();
                    DataTable dtb = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(comando, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        if (values != null && values.Count > 0)
                        {
                            foreach (var item in values)
                            {
                                cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                            }
                                
                        }

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Interaction.MsgBox("Erro: " + ex.Message, MsgBoxStyle.Information);
                    return new DataTable();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public int InsertAndSelectLasRow(string querry, string tabela, [Optional] Dictionary<string, object> values)
        {
            try
            {
                using (SqlConnection con = getConnectionSql())
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(querry, con);

                        if (values.Count > 0)
                        {
                            foreach (var item in values)
                            {
                                if (item.Value == null)
                                {
                                    cmd.Parameters.Add(new SqlParameter(item.Key, " "));
                                }
                                else
                                {
                                    if (item.Value is int)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToInt32(item.Value)));
                                    }

                                    if (item.Value is decimal)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToDecimal(item.Value)));
                                    }

                                    if (item.Value is double)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToDouble(item.Value)));
                                    }

                                    if (item.Value is bool)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToBoolean(item.Value)));
                                    }

                                    if (item.Value is string)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToString(item.Value)));
                                    }

                                    if (item.Value is float)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToSingle(item.Value)));
                                    }

                                    if (item.Value is DateTime)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(item.Key, Convert.ToDateTime(item.Value)));
                                    }
                                }

                            }
                        }
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                return Convert.ToInt32(dr["Last_Id"]);
                            }
                        }

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
