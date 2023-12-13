using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class cIndicadores
{
    private double _dPeso, _dFator_Multiplicacao;
    private int _iCasas_Decimais, _iEndereco;
    private bool _bEstavel;
    private string _sPeso;
    private new System.Threading.Timer _Timer;
    private SerialPort _Porta;
    public Label _Label;
    delegate void funcaoThread();
    public Form _Form;

    public cIndicadores(int endereco, SerialPort porta, Label label, Form form)
    {
        _iEndereco = endereco;
        _Porta = porta;
        _Label = label;
        _Form = form;
    }

    public cIndicadores(int endereco, SerialPort porta)
    {
        _iEndereco = endereco;
        _Porta = porta;
    }

    public cIndicadores(int endereco)
    {
        _iEndereco = endereco;
    }
    public cIndicadores()
    {
    }

    public double dPeso
    {
        get
        {
            return _dPeso;
        }
        set
        {
            _dPeso = value;
        }
    }
    public double dFator_Multiplicacao
    {
        get
        {
            return _dFator_Multiplicacao;
        }
        set
        {
            _dFator_Multiplicacao = value;
        }
    }

    public int iCasas_Decimais
    {
        get
        {
            return _iCasas_Decimais;
        }
        set
        {
            _iCasas_Decimais = value;
        }
    }

    public string sPeso
    {
        get
        {
            return _sPeso;
        }
        set
        {
            _sPeso = value;
        }
    }

    public bool bEstavel
    {
        get
        {
            return _bEstavel;
        }
        set
        {
            _bEstavel = value;
        }
    }

    public int iEndereco
    {
        get
        {
            return _iEndereco;
        }
        set
        {
            _iEndereco = value;
        }
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

    public Label Label
    {
        get
        {
            return _Label;
        }
        set
        {
            _Label = value;
        }
    }

    private int _qtdErro;
    public int QtdError
    {
        get
        {
            return _qtdErro;
        }
        set
        {
            _qtdErro = value;
        }
    }

    private int _qtdSolicitacao;
    public int QtdSolicitacao
    {
        get
        {
            return _qtdSolicitacao;
        }
        set
        {
            _qtdSolicitacao = value;
        }
    }

    private string _nome;
    public string Nome
    {
        get
        {
            return _nome;
        }
        set
        {
            _nome = value;
        }
    }


    public double get_Peso(byte _BYTE5, byte _BYTE6, byte _BYTE7)
    {
        double _peso = _BYTE5 * 65536 + _BYTE6 * 256 + _BYTE7;
        // Dim _peso As Double = _BYTE6 * 256 + _BYTE7
        return _peso;
    }
    public string Conversao_Bit(byte[] data)
    {
        string bit = null;
        foreach (var valor in data)
        {
            string aux_bit = Convert.ToString(valor, 2).PadLeft(8);
            bit = bit + aux_bit;
        }
        return bit;
    }
    public void sPesoIndicador(byte[] pacote_recebido)
    {
        try
        {
            switch (pacote_recebido[1])
            {
                case 3 // função leitura de registradores
               :
                    {
                        _dPeso = get_Peso(pacote_recebido[4], pacote_recebido[5], pacote_recebido[6]);
                        byte[] data_aux = new byte[1];
                        data_aux[0] = pacote_recebido[3];
                        string STS1 = Conversao_Bit(data_aux);
                        var ponto_decimal = STS1.Substring(5, 3);
                        var peso_negativo = STS1.Substring(4, 1);
                        var estavel = STS1.Substring(3, 1);
                        var saturacao = STS1.Substring(2, 1);
                        var sobrecarga = STS1.Substring(1, 1);
                        string Status = "";
                        // MsgBox(pacote_recebido(4))
                        switch (ponto_decimal)
                        {
                            case "000":
                                {
                                    _dFator_Multiplicacao = 1;
                                    _iCasas_Decimais = 0;
                                    break;
                                }

                            case "001":
                                {
                                    _dFator_Multiplicacao = 0.1;
                                    _iCasas_Decimais = 1;
                                    break;
                                }

                            case "010":
                                {
                                    _dFator_Multiplicacao = 0.01;
                                    _iCasas_Decimais = 2;
                                    break;
                                }

                            case "011":
                                {
                                    _dFator_Multiplicacao = 0.001;
                                    _iCasas_Decimais = 3;
                                    break;
                                }

                            case "100":
                                {
                                    _dFator_Multiplicacao = 0.0001;
                                    _iCasas_Decimais = 4;
                                    break;
                                }
                        }
                        _dPeso = _dPeso * _dFator_Multiplicacao;

                        switch (peso_negativo)
                        {
                            case "1":
                                {
                                    _dPeso = _dPeso;
                                    break;
                                }

                            case "0":
                                {
                                    _dPeso = _dPeso;
                                    break;
                                }
                        }
                        sPeso = dPeso.ToString();
                        //Console.WriteLine(sPeso);
                        switch (estavel)
                        {
                            case "0":
                                {
                                    break;
                                }

                            case "1":
                                {
                                    break;
                                }
                        }

                        switch (saturacao)
                        {
                            case "0":
                                {
                                    Status = "";
                                    break;
                                }

                            case "1":
                                {
                                    Status = "Satura";
                                    sPeso = Status;
                                    break;
                                }
                        }

                        switch (sobrecarga)
                        {
                            case "0":
                                {
                                    Status = "";
                                    break;
                                }

                            case "1":
                                {
                                    Status = "Sobre";
                                    sPeso = Status;
                                    break;
                                }
                        }
                        // Console.WriteLine("Status: " & Status & " Peso: " & peso)
                        if ((Status == ""))
                        {
                        }
                        else
                        {
                        }

                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            try
            {
                if (!_Porta.IsOpen)
                    _Porta.Open();
            }
            catch (Exception ex2)
            {
            }
        }
    }

    public void Request_Inicialize()
    {
        _Timer = new System.Threading.Timer(Request, "Auto", 0, 100);
    }

    public void Request_Finalize()
    {
        _Timer.Dispose();
    }

    public void Request([Optional] object value)
    {
        if (_Porta.IsOpen & _iEndereco != 0)
        {
            byte[] dados = new byte[4];
            dados[0] = Convert.ToByte("0");
            dados[1] = Convert.ToByte("10");
            dados[2] = Convert.ToByte("0");
            dados[3] = Convert.ToByte("2");
            var frame = comando_Modbus_Ind(Convert.ToByte(_iEndereco), Convert.ToByte("3"), dados, dados.Length);
            _Porta.Write(frame, 0, frame.Length);
            Thread.Sleep(5);
            if (value == "Auto")
            {
                Thread.Sleep(100);
                Read();
            }
        }
    }

    public byte[] Read()
    {
        if (_Porta.IsOpen & _iEndereco != 0)
        {
            try
            {
                var bytes_para_ler = _Porta.BytesToRead;
                if (bytes_para_ler > 0)
                {
                    byte[] pacote_recebido;
                    pacote_recebido = new byte[bytes_para_ler - 1 + 1];
                    _Porta.Read(pacote_recebido, 0, bytes_para_ler);
                    byte[] CRC_byte = CRC(pacote_recebido);
                    if (CRC_byte[0] != pacote_recebido[pacote_recebido.Length - 2] & CRC_byte[1] != pacote_recebido[pacote_recebido.Length - 1])
                    {
                        Console.WriteLine("CRC INCORRETO");
                        return null;
                    }
                    _Form.Invoke(new funcaoThread(() => sPesoIndicador(pacote_recebido)));
                    if ((_Label != null))
                        _Form.Invoke(new funcaoThread(teste));
                    return pacote_recebido;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        return null;
    }

    public void teste()
    {
        _Label.Text = sPeso;
    }

    public byte[] comando_Modbus_Ind(byte endereco_Escravo, byte codigo_Funcao, byte[] dados, int numero_dados)
    {
        int i = 0;
        byte[] frame = new byte[numero_dados + 3 + 1];
        frame[0] = endereco_Escravo;
        frame[1] = codigo_Funcao;
        while ((i != numero_dados))
        {
            frame[i + 2] = dados[i];
            i = i + 1;
        }
        byte[] crc = CRC(frame);
        frame[numero_dados + 2] = crc[0];
        frame[numero_dados + 3] = crc[1];
        return frame;
    }

    public byte[] CRC(byte[] data)
    {
        ushort CRCFull = 0xFFFF;
        byte CRCHigh = 0xF;
        byte CRCLow = 0xFF;
        char CRCLSB;
        byte[] result = new byte[2];
        // MsgBox(data.Length)
        for (int i = 0; i <= (data.Length) - 3; i++)
        {
            CRCFull = System.Convert.ToUInt16(CRCFull ^ data[i]);

            for (int j = 0; j <= 7; j++)
            {
                CRCLSB = Strings.ChrW(CRCFull & 0x1);
                CRCFull = System.Convert.ToUInt16((CRCFull >> 1) & 0x7FFF);

                if (Convert.ToInt32(CRCLSB) == 1)
                    CRCFull = System.Convert.ToUInt16(CRCFull ^ 0xA001);
            }
        }
        CRCHigh = System.Convert.ToByte((CRCFull >> 8) & 0xFF);
        CRCLow = System.Convert.ToByte(CRCFull & 0xFF);
        return new byte[2] { CRCLow, CRCHigh };
    }

    public string DisplayValue(byte[] values)
    {
        string result = string.Empty;
        foreach (byte item in values)
            result += string.Format("{0:X2} ", item);
        return result;
    }

}
