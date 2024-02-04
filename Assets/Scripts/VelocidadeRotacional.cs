using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace grafico
{
    public class VelocidadeRotacional : MonoBehaviour
    {
        public Text valorVelocidadeRotacional; 

        private mainSerial serialController; 

        private void Start()
        {
            
            serialController = FindObjectOfType<mainSerial>();
            StartCoroutine(UpdateVelocidadeRotacionalText());
        }

        private IEnumerator UpdateVelocidadeRotacionalText()
        {
            while (true)
            {
                if (serialController != null)
                {
                    float lastVelocidadeRotacional = serialController.GetLastVelocidadeRotacional();


                    valorVelocidadeRotacional.text = lastVelocidadeRotacional.ToString("F2") + " rad/s"; 

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
