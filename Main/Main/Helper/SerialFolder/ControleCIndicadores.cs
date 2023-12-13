using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.Helper.SerialFolder
{
    public class ControleCIndicadores
    {
        private SerialPort _Porta;
        public cIndicadores[] _Indicadores = new cIndicadores[101];
        public List<int> CicloP_Indicadores;
        public List<int> CicloS_Indicadores;
        private List<int> ListaSolicitacao;
        private List<int> ListaResposta;
        private int indicador_atual;
        private int indicadorP_atual = 1;
        private int indicadorS_atual = 4;
        private int contador_Request = 0;
        private int _Endereco_Inicial = 0;
        private int _NumeroIndicadores = 0;
        delegate void funcaoThread();
        private Thread _Thread;
        private System.Threading.Timer _Timer;
        public ulong Total_cycle = 0;
        public bool HardkeyRunning = false;
        public int[] int_RequestCycle = new int[101];
        public int[] int_ResponseCycle = new int[101];
        
        public ControleCIndicadores(SerialPort porta, int NumeroIndicadores, int Endereco_Inicial, Form form)
        {
            _Porta = porta;
            Inicialize(Endereco_Inicial);
            indicador_atual = Endereco_Inicial;
            _Endereco_Inicial = Endereco_Inicial;
            _NumeroIndicadores = NumeroIndicadores;
            for (int index = Endereco_Inicial; index <= (Endereco_Inicial + NumeroIndicadores) - 1; index++)
            {
                _Indicadores[index] = new cIndicadores(index, _Porta, null, form);
                CicloP_Indicadores.Add(index);
            }
        }

        public ControleCIndicadores(SerialPort porta, List<int> Enderecos, Form form)
        {
            _Porta = porta;
            Inicialize(Enderecos[0]);
            for (var index = 0; index <= Enderecos.Count; index++)
            {
                _Indicadores[index + 1] = new cIndicadores(Enderecos[index], _Porta, null, form);
                CicloP_Indicadores.Add(Enderecos[index]);
            }
        }

        public ControleCIndicadores(SerialPort porta, int NumeroIndicadores, int Endereco_Inicial)
        {
            _Porta = porta;
            Inicialize(Endereco_Inicial);
            indicador_atual = Endereco_Inicial;
            for (int index = Endereco_Inicial; index <= (Endereco_Inicial + NumeroIndicadores) - 1; index++)
            {
                _Indicadores[index] = new cIndicadores(index, _Porta);
                CicloP_Indicadores.Add(index);
            }
        }

        public void Inicialize(int endereco = 1)
        {
            CicloP_Indicadores = new List<int>();
            CicloS_Indicadores = new List<int>();
            ListaSolicitacao = new List<int>();
            ListaResposta = new List<int>();
            ListaSolicitacao.Add(endereco);
        }

        public void Set_Label(Label label, int index_indicador)
        {
            if ((_Indicadores[index_indicador]) != null)
                _Indicadores[index_indicador].Label = label;
        }

        public SerialPort Porta
        {
            get
            {
                return _Porta;
            }
            set
            {
                _Porta = value;
            }
        }

        private void Next_IndicadorP()
        {
            bool bFlag = false;
            foreach (var value in CicloP_Indicadores)
            {
                if (bFlag == true)
                {
                    ListaSolicitacao.Add(value);
                    indicadorP_atual = value;
                    return;
                }
                if (value == indicadorP_atual)
                    bFlag = true;
            }
            if (CicloP_Indicadores.Count != 0)
            {
                ListaSolicitacao.Add(CicloP_Indicadores[0]);
                indicadorP_atual = CicloP_Indicadores[0];
            }
        }

        private void Next_IndicadorS()
        {
            bool bFlag = false;
            foreach (var value in CicloS_Indicadores)
            {
                if (bFlag == true)
                {
                    ListaSolicitacao.Add(value);
                    indicadorS_atual = value;
                    return;
                }
                if (value == indicadorS_atual)
                    bFlag = true;
            }
            if (CicloS_Indicadores.Count != 0)
            {
                ListaSolicitacao.Add(CicloS_Indicadores[0]);
                indicadorS_atual = CicloS_Indicadores[0];
            }
        }

        private void addinIndicadorP(int endereco)
        {
            bool bFlag = false;
            foreach (var value in CicloP_Indicadores)
            {
                if (value == endereco)
                    return;
            }
            if (bFlag == false)
            {
                if (!CicloP_Indicadores.Contains(endereco))
                    CicloP_Indicadores.Add(endereco);
                CicloS_Indicadores.Remove(endereco);
            }
        }

        private void addinIndicadorS(int endereco)
        {
            bool bFlag = false;
            foreach (var value in CicloS_Indicadores)
            {
                if (value == endereco)
                    return;
            }
            if (bFlag == false)
            {
                if (!CicloS_Indicadores.Contains(endereco))
                    CicloS_Indicadores.Add(endereco);
                CicloP_Indicadores.Remove(endereco);
            }
        }

        public void Request_Inicialize()
        {
            _Timer = new System.Threading.Timer(Request, "Auto", 0, 100);
        }

        public void Request_Inicialize(string type)
        {
            if (type == "FULL_SPEED")
            {
                _Thread = new Thread(Comunicacao);
                _Thread.Start();
            }
        }

        public void Request_Abort(string type)
        {
            if (type == "THREAD")
                _Thread.Abort();
        }

        private void Request(object var)
        {
            if (_Porta.IsOpen & ListaSolicitacao.Count != 0)
            {
                _Indicadores[ListaSolicitacao[0]].Request();
                ListaResposta.Add(ListaSolicitacao[0]);
                ListaSolicitacao.Remove(ListaSolicitacao[0]);
            }
            contador_Request += 1;
            if (contador_Request < 10)
                Next_IndicadorP();
            else
            {
                Next_IndicadorS();
                contador_Request = 0;
            }
            if (var == "")
                Receive();
        }

        private void Receive()
        {
            if (_Porta.IsOpen)
            {
                try
                {
                    byte[] pacote_recebido = _Indicadores[ListaResposta[0]].Read();
                    if ((pacote_recebido != null))
                    {
                        int i = 0;
                        foreach (var value in ListaResposta)
                        {
                            if (value == pacote_recebido[0])
                                break;
                            else
                                i += 1;
                        }
                        do
                        {
                            if (i == 0)
                                break;
                            addinIndicadorS(ListaResposta[0]);
                            ListaResposta.Remove(ListaResposta[0]);
                            i += 1;
                        }
                        while (true);
                        addinIndicadorP(ListaResposta[0]);
                        ListaResposta.Remove(ListaResposta[0]);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private long millis, current_Millis01, current_Millis02;
        private long ciclos = 0;

        public bool running = true;
        private void Comunicacao()
        {
            // 'Pode ser que quando todos os indicadores são desligados, ele entra em um execption
            // 'Ai ele sai do while (1), posso tentar colocar um try aqui e no catch 
            // 'Limpar todoas as variaveis e dar um goto para o evento principal, assim retomando o loop



            try
            {
                //BACKWHILE:
                while (true)
                {
                    if (!running)
                    {
                    }
                    else
                        try
                        {
                            if (_Porta.IsOpen)
                            {
                                millis = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                if (_Porta.BytesToRead == 0)
                                {
                                    if ((millis - current_Millis01 > 5))
                                    {
                                        int count_request = 0;
                                        send_again:
                                        ;
                                        _Indicadores[indicador_atual].Request();
                                        _Indicadores[indicador_atual].QtdSolicitacao += 1;
                                        int_RequestCycle[indicador_atual] += 1;
                                        millis = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                        current_Millis02 = millis;
                                        do
                                        {
                                            millis = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                            if (_Porta.BytesToRead == 9)
                                                break;
                                            else if ((millis - current_Millis02 > 100))
                                            {
                                                count_request += 1;
                                                current_Millis02 = millis;
                                                if (count_request == 2)
                                                {
                                                    _Indicadores[indicador_atual].QtdError += 3;
                                                    // ' Console.WriteLine(indicador_atual)
                                                    next_read(indicador_atual);
                                                    break;
                                                }
                                                else
                                                    //Console.WriteLine($"Indicador {indicador_atual} Quantidade de Erro: {_Indicadores[indicador_atual].QtdError}");
                                                goto send_again;
                                            }
                                        }
                                        while (true);
                                        current_Millis01 = millis;
                                    }
                                }
                                else if (_Porta.BytesToRead == 9)
                                {
                                    Generical_Read();
                                    next_read();
                                    Thread.Sleep(5);
                                    ciclos += 1;
                                    Total_cycle += 1;
                                }
                                else
                                {
                                    byte[] bytes_lixo = new byte[_Porta.BytesToRead + 1];
                                    _Porta.Read(bytes_lixo, 0, _Porta.BytesToRead);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                }
            }
            catch (Exception ex)
            {
                CicloP_Indicadores.Clear();
                for (int index = _Endereco_Inicial; index <= (_Endereco_Inicial + _NumeroIndicadores) - 1; index++)
                    CicloP_Indicadores.Add(index);
                CicloS_Indicadores.Clear();
                ListaSolicitacao.Clear();
                ListaResposta.Clear();
                indicadorP_atual = 1;
                indicadorS_atual = 4;
                contador_Request = 0;
                Total_cycle = 0;
                //goto BACKWHILE;
            }
        }

        private void next_read(int addr_secundario = 0)
        {
            int i = 0;
            foreach (var value in CicloP_Indicadores)
            {
                if (indicador_atual == value)
                {
                    if (i + 1 >= CicloP_Indicadores.Count)
                        indicador_atual = CicloP_Indicadores[0];
                    else
                        indicador_atual = CicloP_Indicadores[i + 1];
                    break;
                }
                i += 1;
            }
            if ((addr_secundario != 0))
            {
                if ((CicloP_Indicadores.Contains(addr_secundario)))
                    CicloP_Indicadores.Remove(addr_secundario);
                if ((!CicloS_Indicadores.Contains(addr_secundario)))
                    CicloS_Indicadores.Add(addr_secundario);
            }

            if (ciclos > 5 & CicloS_Indicadores.Count != 0)
            {
                if ((!CicloP_Indicadores.Contains(CicloS_Indicadores[0])))
                    CicloP_Indicadores.Add(CicloS_Indicadores[0]);
                if ((CicloS_Indicadores.Contains(CicloS_Indicadores[0])))
                    CicloS_Indicadores.Remove(CicloS_Indicadores[0]);
                ciclos = 0;
            }

            if (addr_secundario > 0)
                _Indicadores[addr_secundario].QtdSolicitacao += 1;
        }

        private void Generical_Read()
        {
            if (_Porta.IsOpen)
            {
                try
                {
                    var bytes_para_ler = _Porta.BytesToRead;
                    if (bytes_para_ler > 0)
                    {
                        byte[] pacote_recebido;
                        pacote_recebido = new byte[bytes_para_ler - 1 + 1];
                        _Porta.Read(pacote_recebido, 0, bytes_para_ler);
                        byte[] CRC_byte = _Indicadores[ListaSolicitacao[0]].CRC(pacote_recebido);
                        if (CRC_byte[0] != pacote_recebido[pacote_recebido.Length - 2] & CRC_byte[1] != pacote_recebido[pacote_recebido.Length - 1])
                            Console.WriteLine("CRC INCORRETO");
                        int endereco = pacote_recebido[0];
                        int_ResponseCycle[endereco] += 1;
                        int_RequestCycle[endereco] = 0;
                        int_ResponseCycle[endereco] = 0;
                        _Indicadores[endereco]._Form.Invoke(new funcaoThread(() => _Indicadores[endereco].sPesoIndicador(pacote_recebido)));
                        if ((_Indicadores[endereco]._Label != null))
                            _Indicadores[endereco]._Form.Invoke(new funcaoThread(_Indicadores[endereco].teste));
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
