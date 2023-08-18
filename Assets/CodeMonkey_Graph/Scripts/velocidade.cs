using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace grafico
{
    public class velocidade : MonoBehaviour
    {
        public Text valorVelocidade; 

        private mainSerial serialController; 

        private void Start()
        {
            
            serialController = FindObjectOfType<mainSerial>();
            StartCoroutine(UpdateVelocityText());
        }

        private IEnumerator UpdateVelocityText()
        {
            while (true)
            {
                if (serialController != null)
                {
                    float lastVelocidade = serialController.GetLastVelocidade();
                    Debug.Log("Ultm veloc: " + lastVelocidade); 

                    valorVelocidade.text = lastVelocidade.ToString("F2") + " m/s^2"; 

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
