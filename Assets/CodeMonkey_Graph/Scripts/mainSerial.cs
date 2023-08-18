using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Globalization;

namespace grafico
{
    public class mainSerial : MonoBehaviour, IDataReceiver
    {
        private SerialPort serialPort;
        public string serialPortName = "COM5"; 
        public int baudRate = 115200; 

        private Window_Graph_Acelerometro graphController;
        private Window_Graph_Altura graphAltura;
        private Window_Graph_Pressao graphPressao;

        private float lastAltura;
        private float lastAceleracao;
        private float lastPressao;
        private float lastTemperatura;
        private float lastLatitude;
        private float lastLongitude;





        public event System.Action<float, float, float> OnDataReceived;

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

            graphController = FindObjectOfType<Window_Graph_Acelerometro>();
            graphAltura = FindObjectOfType<Window_Graph_Altura>();
            graphPressao = FindObjectOfType<Window_Graph_Pressao>();

            if (graphController != null)
            {
                graphController.SetSerialController(this); // Passa a referência da instância de mainSerial
            }
            if (serialPort.IsOpen)
            {
                Debug.Log("PORTA ABERTA");
                try
                {
                    string data = serialPort.ReadLine();
                    Debug.Log("Dados reccebidos: " + data);
                                                         
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
                    string[] values = data.Split(',');

                    if (values.Length >= 3) 
                    {

                        /* 
                         CASO NECESSÁRIA ALTERAÇÃO
                         
                         */
                        float altura = float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat);
                        float aceleracao = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat); //modulo formula 
                        float pressao = float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat);
                        float temperatura = float.Parse(values[3], CultureInfo.InvariantCulture.NumberFormat);
                        float latitude = float.Parse(values[4], CultureInfo.InvariantCulture.NumberFormat);
                        float longitude = float.Parse(values[5], CultureInfo.InvariantCulture.NumberFormat);



                        Debug.Log("Altura: " + altura + " Aceleracao: " + aceleracao + " Pressão: " + pressao + "Temperatura" + temperatura + "Latitude" + latitude + "Longitude" + longitude);

                        lastAltura = altura;
                        lastAceleracao = aceleracao;
                        lastPressao = pressao;
                        lastTemperatura = temperatura;
                        lastLatitude= latitude;
                        lastLongitude= longitude;

                        graphController.ReceiveData(aceleracao);
                        graphAltura.ReceiveAltura(altura);
                        graphPressao.ReceivePressao(pressao);

                        // Notifica os observadores sobre os dados recebidos
                        OnDataReceived?.Invoke(altura, aceleracao, pressao);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Erro ao ler dados da porta serial: " + ex.Message);
                }
            }
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

        public float GetLastLongitude()
        {
            return lastLongitude;
        }


        public void ReceiveAltura(float altura)
        {
        }
        public void ReceiveData(float value)
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
