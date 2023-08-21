using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace grafico
{
    public class Temperatura : MonoBehaviour
    {
        public Text valorTemperatura; 

        private mainSerial serialController; 

        private void Start()
        {
            
            serialController = FindObjectOfType<mainSerial>();
            StartCoroutine(UpdateTemperaturaText());
        }

        private IEnumerator UpdateTemperaturaText()
        {
            while (true)
            {
                if (serialController != null)
                {
                    float lastTemperatura = serialController.GetLastTemperatura();


                    valorTemperatura.text = lastTemperatura.ToString("F2") + " ºC"; 

                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
