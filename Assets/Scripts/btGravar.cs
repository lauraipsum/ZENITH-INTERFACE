using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace grafico
{
    public class btGravar : MonoBehaviour
    {
        private mainSerial serialController;
        private string logFilePath;
        private StreamWriter logStreamWriter;

        private void Start()
        {
            serialController = FindObjectOfType<mainSerial>();
            if (serialController == null)
            {
                Debug.LogError("mainSerial n�o encontrado na cena!");
                return;
            }

            logFilePath = Application.dataPath + "/serial_data.txt";
            logStreamWriter = File.AppendText(logFilePath);

            if (logStreamWriter == null)
            {
                Debug.LogError("N�o foi poss�vel abrir o arquivo de log.");
                return;
            }

            serialController.OnDataReceived += HandleDataReceived;
        }

        private void HandleDataReceived(float altura, float velocidade, float pressao)
        {
            string logEntry = $" Altura: {altura}, Velocidade: {velocidade}, Press�o: {pressao}";
            logStreamWriter.WriteLine(logEntry);
            logStreamWriter.Flush();
        }

        private void OnDestroy()
        {
            if (logStreamWriter != null)
            {
                logStreamWriter.Close();
                logStreamWriter.Dispose();
            }
        }
    }
}
