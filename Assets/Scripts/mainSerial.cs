using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Globalization;
using System.Text.RegularExpressions;

using static grafico.mainSerial;

namespace grafico
{
    public class mainSerial : MonoBehaviour, IDataReceiver
    {
        private SerialPort serialPort;
        public string serialPortName = "COM4"; 
        public int baudRate = 9600; 

       // private Window_Graph_Aceleracao graphAceleracao;
        //private Window_Graph_Altura graphAltura;
        //private Window_Graph_Pressao graphPressao;

        private float lastAltura;
        private float lastAceleracao;
        private float lastPressao;
        private float lastTemperatura;
        private float lastLatitude;
        private float lastVelocidadeRotacional;

        //public event System.Action<float, float, float> OnDataReceived;

        private float statusMPU = -1; // Variável para armazenar o status MPU
        private float statusCE = -1; // Variável para armazenar o status ce_ef




        private void Start()
        {
           
            serialPort = new SerialPort(serialPortName, baudRate);
            try
            {
                serialPort.Open();
            }
            catch (System.Exception)
            {
                Debug.LogError("Erro ao abrir Porta Serial " + serialPortName);
            }

            
            //graphAltura = FindObjectOfType<Window_Graph_Altura>();
           // graphAceleracao = FindObjectOfType<Window_Graph_Aceleracao>();
           //graphPressao = FindObjectOfType<Window_Graph_Pressao>();


            if (serialPort.IsOpen)
            {
                Debug.Log("PORTA ABERTA");
                try
                {
                    string data = serialPort.ReadLine();
                    Debug.Log("Dados recebidos: " + data);
                                                         
                }
                catch (System.Exception)
                {
                    Debug.Log("Erro ao abrir Porta Serial");
                }
            }
        }





        private void Update()
            {
                if (serialPort.IsOpen)
                {
                    try
                    {       

                        string data = serialPort.ReadLine();

                        Debug.Log("Dados recebidos: " + data);


                    // Verifica se a linha recebida é um status ou dados
                    if (data.StartsWith("Status"))
                    {
                        HandleStatusLine(data);
                    }
                    else if (data.StartsWith("!"))
                    {
                        data = data.Substring(1);
                        HandleDataLine(data);
                    }
                    else
                    {
                        // Caso a linha não corresponda a nenhum caso conhecido, apenas ignore
                        Debug.Log("Linha recebida não reconhecida.");
                    }


                }
                    catch (System.Exception ex)
                    {
                        Debug.LogError("Erro ao ler dados da porta serial: " + ex.Message);
                    }
                }
            }

        private void HandleStatusLine(string data)
        {
            if (data.StartsWith("Status MPU:"))
            {
                // Extrair e processar o status MPU
                string pattern = @"Status MPU:\s*([\d.-]+)";
                Match match = Regex.Match(data, pattern);
                if (match.Success)
                {
                    string valueString = match.Groups[1].Value;
                    statusMPU = float.Parse(valueString, CultureInfo.InvariantCulture.NumberFormat);
                    Debug.Log("STATUS MPU : " + statusMPU);

                }
            }
            else if (data.StartsWith("Status CE_EF:"))
            {
                // Extrair e processar o status MPU
                string pattern = @"Status CE_EF:\s*([\d.-]+)";
                Match match = Regex.Match(data, pattern);
                if (match.Success)
                {
                    string valueString = match.Groups[1].Value;
                    statusCE = float.Parse(valueString, CultureInfo.InvariantCulture.NumberFormat);
                    Debug.Log("STATUS CE_EF : " + statusCE);

                }
            }
        }

        public float GetMPUStatus()
        {
            return statusMPU;
        }

        public float GetCEStatus()
        {
            return statusCE;
        }

        private void HandleDataLine(string data)
        {
            string[] values = data.Split(' ');

            float aceleracaox = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat);
            float aceleracaoy = float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat);
            float aceleracaoz = float.Parse(values[3], CultureInfo.InvariantCulture.NumberFormat);
            float aceleracao = Mathf.Sqrt((aceleracaox * aceleracaox) + (aceleracaoy * aceleracaoy) + (aceleracaoz * aceleracaoz));

            //float vrx = float.Parse(values[3], CultureInfo.InvariantCulture.NumberFormat);
            //float vry = float.Parse(values[4], CultureInfo.InvariantCulture.NumberFormat);
            //float vrz = float.Parse(values[5], CultureInfo.InvariantCulture.NumberFormat);
            //float VelocidadeRotacional = Mathf.Sqrt((vrx * vrx) + (vry * vry) + (vrz * vrz));

            //float temperatura = float.Parse(values[6], CultureInfo.InvariantCulture.NumberFormat);

            float pressao = float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat);

            //float latitude = float.Parse(values[8], CultureInfo.InvariantCulture.NumberFormat);

            //float altura = float.Parse(values[9], CultureInfo.InvariantCulture.NumberFormat);

            //Debug.Log("Altura: " + altura + " Aceleracao: " + aceleracao + " Pressão: " + pressao + "Temperatura" + temperatura + "Latitude" + latitude + "VelocidadeRotacional" + VelocidadeRotacional);


            lastAceleracao = aceleracao; //MPU

                                         
            //lastVelocidadeRotacional = VelocidadeRotacional; //MPU
                                         
            //lastTemperatura = temperatura; //MPU E BMP

            //lastAltura = altura;
            lastPressao = pressao;
            //lastLatitude= latitude;

            ////graphAltura.ReceiveAltura(altura);
            //// graphAceleracao.ReceiveAceleracao(aceleracao);
            ////graphPressao.ReceivePressao(pressao);

            //// Notifica os observadores sobre os dados recebidos
            //OnDataReceived?.Invoke(altura, aceleracao, pressao);
        }

            // Métodos para obter o último valor 


            public float GetLastAltura()
                {
                    return lastAltura;
                }

            public float GetLastAceleracao()
            {
                return lastAceleracao;
            }

            public float GetLastPressao()
            {
                return lastPressao;
            }
            public float GetLastTemperatura()
            {
                return lastTemperatura;
            }
            public float GetLastLatitude()
            {
                return lastLatitude;
            }

            public float GetLastVelocidadeRotacional()
            {
                return lastVelocidadeRotacional;
            }


            public void ReceiveAltura(float altura)
            {
            }
            public void ReceiveAceleracao(float aceleracao)
            {
            }
            public void ReceivePressao(float pressao)
            {
            }

            private void OnDestroy()
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
    }
}
