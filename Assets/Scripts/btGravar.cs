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
                Debug.LogError("mainSerial não encontrado na cena!");
                return;
            }

            logFilePath = Application.dataPath + "/serial_data.txt";
            logStreamWriter = File.AppendText(logFilePath);

            if (logStreamWriter == null)
            {
                Debug.LogError("Não foi possível abrir o arquivo de log.");
                return;
            }

            serialController.OnDataReceived += HandleDataReceived;
        }

        private void HandleDataReceived(float altura, float velocidade, float pressao)
        {
            string logEntry = $" Altura: {altura}, Velocidade: {velocidade}, Pressão: {pressao}";
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
