using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace grafico
{
    public class Altura : MonoBehaviour
    {
        public Text valorAltura; 

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
                   
                    float lastAltura = serialController.GetLastAltura();
                    Debug.Log("Ultm alt: " + lastAltura);

                    valorAltura.text = lastAltura.ToString("F2") + " m"; 

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
