using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace grafico
{
    public class bmpStatus : MonoBehaviour
    {
        public GameObject onObject;  // Objeto que representa o estado "on"
        public GameObject offObject; // Objeto que representa o estado "off"
        public mainSerial serialController; // Refer�ncia ao script mainSerial

        // Start is called before the first frame update
        void Start()
        {
            // Certifique-se de que os objetos "on" e "off" sejam inicialmente vis�veis
            onObject.SetActive(true);
            offObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            // Verifica se o script mainSerial est� dispon�vel
            if (serialController != null)
            {
                // Obt�m o �ltimo status MPU do script mainSerial
                float statusMPU = serialController.GetLastAceleracao();

                // Se o status MPU for 0, torna o objeto "on" invis�vel e o objeto "off" vis�vel
                if (statusMPU == 0f)
                {
                    onObject.SetActive(false);
                    offObject.SetActive(true);
                }
                // Se o status MPU for 1, torna o objeto "off" invis�vel e o objeto "on" vis�vel
                else if (statusMPU == 1f)
                {
                    onObject.SetActive(true);
                    offObject.SetActive(false);
                }
                // Se o status MPU for diferente de 0 ou 1, mant�m ambos os objetos vis�veis
                else
                {
                    onObject.SetActive(true);
                    offObject.SetActive(true);
                }
            }
        }
    }
}