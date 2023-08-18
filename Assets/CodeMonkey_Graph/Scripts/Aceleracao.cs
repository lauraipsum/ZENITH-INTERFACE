using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace grafico
{
    public class Aceleracao : MonoBehaviour
    {
        public Text valorAceleracao; 

        private mainSerial serialController; 

        private void Start()
        {
            
            serialController = FindObjectOfType<mainSerial>();
            StartCoroutine(UpdateAceleracaoText());
        }

        private IEnumerator UpdateAceleracaoText()
        {
            while (true)
            {
                if (serialController != null)
                {
                    float lastAceleracao = serialController.GetLastAceleracao();
                    Debug.Log("Ultm veloc: " + lastAceleracao); 

                    valorAceleracao.text = lastAceleracao.ToString("F2") + " m/s^2"; 

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
