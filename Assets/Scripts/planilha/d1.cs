using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace grafico
{
    public class d1 : MonoBehaviour
    {
        public Text valorPressao;

        private mainSerial serialController;

        private void Start()
        {
            serialController = FindObjectOfType<mainSerial>();

            StartCoroutine(UpdatePressureText());
        }

        private IEnumerator UpdatePressureText()
        {
            while (true)
            {
                if (serialController != null)
                {
                    float lastPressao = serialController.GetLastPressao();


                    valorPressao.text = lastPressao.ToString("F2");

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
