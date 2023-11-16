using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace grafico
{
    public class Longitude : MonoBehaviour
    {
        public Text valorLongitude; 

        private mainSerial serialController; 

        private void Start()
        {
            
            serialController = FindObjectOfType<mainSerial>();
            StartCoroutine(UpdateLongitudeText());
        }

        private IEnumerator UpdateLongitudeText()
        {
            while (true)
            {
                if (serialController != null)
                {
                    float lastLongitude = serialController.GetLastLongitude();

                    valorLongitude.text = lastLongitude.ToString("F2") + " �"; 

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
