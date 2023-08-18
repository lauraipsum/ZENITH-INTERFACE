using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace grafico
{
    public class Latitude : MonoBehaviour
    {
        public Text valorLatitude; 

        private mainSerial serialController; 

        private void Start()
        {
            
            serialController = FindObjectOfType<mainSerial>();
            StartCoroutine(UpdateLatitudeText());
        }

        private IEnumerator UpdateLatitudeText()
        {
            while (true)
            {
                if (serialController != null)
                {
                    float lastLatitude = serialController.GetLastLatitude();
                    Debug.Log("Ultm lat: " + lastLatitude); 

                    valorLatitude.text = lastLatitude.ToString("F2") + " º"; 

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
