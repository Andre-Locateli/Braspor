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
                                        //PesagemClass pesagem = new PesagemClass();
                                        //pesagem.Id = Convert.ToInt32(dr["id"]);
                                        //pesagem.CodigoProduto = Convert.ToString(dr["CodigoProduto"]);
                                        //pesagem.Descricao = Convert.ToString(dr["Descricao"]);
                                        //pesagem.PesoAlvo = Convert.ToString(dr["PesoAlvo"]);
                                        //pesagem.PesoReal = Convert.ToString(dr["PesoReal"]);

                                        //pesagem.Tolerencia = Convert.ToString(dr["Tolerencia"]);
                                        //pesagem.flag_sync = Convert.ToBoolean(dr["flag_sync"]);

                                        //pesagem.dateinsert = (DateTime)dr["dateinsert"];
                                        //list_return.Add(pesagem);
                                    }
                                    else
                                    {
                                        list_return.Add(dr[coluna_to_return]);
                                    }
                                }

                                if (tabela == "MateriaPrima")
                                {
                                    MateriaPrimaClass materia = new MateriaPrimaClass();
                                    materia.Id = Convert.ToInt32(dr["id"]);
                                    materia.Codigo = Convert.ToString(dr["Codigo"]);
                                    materia.Descricao = Convert.ToString(dr["Descricao"]);
                                    materia.Tolerancia_erro = Convert.ToDouble(dr["Tolerancia_erro"]);
                                    materia.Quantidade_minima = Convert.ToInt32(dr["quantidade_minima"]);
                                    materia.Status = Convert.ToBoolean(dr["bit_status"]);
                                    materia.DateInsert = Convert.ToDateTime(dr["dateinsert"]);
                                    list_return.Add(materia);
                                }

                                if(tabela == "Historico_Acoes")
                                {
                                    HistoricoAcoesModel historico = new HistoricoAcoesModel();
                                    historico.Id = Convert.ToInt32(dr["Id"]);
                                    historico.Id_usuario = Convert.ToInt32(dr["Id_usuario"]);
                                    historico.Nome_usuario = Convert.ToString(dr["Nome_usuario"]);
                                    historico.Acao = Convert.ToString(dr["Acao"]);
                                    historico.Descricao = Convert.ToString(dr["Descricao"]);
                                    historico.Dateinsert = Convert.ToDateTime(dr["dateinsert"]);
                                    list_return.Add(historico);
                                }

                                if (tabela == "Processos")
                                {
                                    if (coluna_to_return is null)
                                    {
                                        ProcessosModel process = new ProcessosModel();
                                        process.Id = Convert.ToInt32(dr["Id"]);
                                        process.IdUsuario = Convert.ToInt32(dr["Id_usuario"]);
                                        process.Id_Produto = Convert.ToInt32(dr["Id_Produto"]);
                                        process.Descricao = Convert.ToString(dr["Descricao"]);
                                        process.TempoExecucao = Convert.ToString(dr["Tempo_execucao"]);
                                        process.TotalContagem = Convert.ToInt32(dr["Total_contagem"]);
                                        process.PesoReferencia = Convert.ToDouble(dr["Peso_Referencia"]);
                                        process.PesoTotal = Convert.ToDouble(dr["Peso_total"]);
                                        process.dateinsert = Convert.ToDateTime(dr["dateinsert"]);
                                        list_return.Add(process);
                                    }
                                    else
                                    {
                                        list_return.Add(dr[coluna_to_return]);
                                    }                   
                                }

                                if (tabela == "Log_Processos")
                                {
                                    LogProcessosClass logprocessos = new LogProcessosClass();
                                    logprocessos.Id = Convert.ToInt32(dr["Id"]);
                                    logprocessos.Id_processo = Convert.ToInt32(dr["Id_processo"]);
                                    logprocessos.Peso_temporeal = Convert.ToDecimal(dr["Peso_temporeal"]);
                                    logprocessos.Peso_total = Convert.ToDecimal(dr["Peso_total"]);
                                    logprocessos.Tempo_execucao = Convert.ToString(dr["Tempo_execucao"]);
                                    logprocessos.dateinsert = Convert.ToDateTime(dr["dateinsert"]);
                                    list_return.Add(logprocessos);
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

                                if (tabela == "MateriaPrima") 
                                {
                                    MateriaPrimaClass materia = new MateriaPrimaClass();
                                    materia.Id = Convert.ToInt32(dr["id"]);
                                    materia.Codigo = Convert.ToString(dr["Codigo"]);
                                    materia.Descricao = Convert.ToString(dr["Descricao"]);
                                    materia.Tolerancia_erro = Convert.ToDouble(dr["Tolerancia_erro"]);
                                    materia.Quantidade_minima = Convert.ToInt32(dr["quantidade_minima"]);
                                    materia.Status = Convert.ToBoolean(dr["bit_status"]);
                                    materia.DateInsert = Convert.ToDateTime(dr["dateinsert"]);
                                    return materia;
                                }


                                if (tabela == "Historico_Acoes")
                                {
                                    HistoricoAcoesModel historico = new HistoricoAcoesModel();
                                    historico.Id = Convert.ToInt32(dr["Id"]);
                                    historico.Id_usuario = Convert.ToInt32(dr["Id_usuario"]);
                                    historico.Nome_usuario = Convert.ToString(dr["Nome_usuario"]);
                                    historico.Acao = Convert.ToString(dr["Acao"]);
                                    historico.Descricao = Convert.ToString(dr["Descricao"]);
                                    historico.Dateinsert = Convert.ToDateTime(dr["dateinsert"]);
                                    return historico;
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
