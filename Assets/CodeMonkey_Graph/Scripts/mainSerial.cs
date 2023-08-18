using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

namespace grafico
{
    public class mainSerial : MonoBehaviour, IDataReceiver
    {
        private SerialPort serialPort;
        public string serialPortName = "COM3"; 
        public int baudRate = 115200; 

        private Window_Graph_Acelerometro graphController;
        private Window_Graph_Altura graphAltura;
        private Window_Graph_Pressao graphPressao;

        private float lastAltura;
        private float lastVelocidade;
        private float lastPressao;

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
                    Debug.Log("Dados reccebidos: " + data); // Imprime os dados recebidos
                                                         
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
                        float altura = float.Parse(values[0]);
                        float velocidade = float.Parse(values[1]);
                        float pressao = float.Parse(values[2]);

                        Debug.Log("Altura: " + altura + " Velocidade: " + velocidade + " Pressão: " + pressao);

                        lastAltura = altura;
                        lastVelocidade = velocidade;
                        lastPressao = pressao;

                        graphController.ReceiveData(velocidade);
                        graphAltura.ReceiveAltura(altura);
                        graphPressao.ReceivePressao(pressao);
                        // Notifica os observadores sobre os dados recebidos
                        OnDataReceived?.Invoke(altura, velocidade, pressao);
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

        public float GetLastVelocidade()
        {
            return lastVelocidade;
        }

        public float GetLastPressao()
        {
            return lastPressao;
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
