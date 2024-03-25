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
        public mainSerial serialController; // Referência ao script mainSerial

        // Start is called before the first frame update
        void Start()
        {
            // Certifique-se de que os objetos "on" e "off" sejam inicialmente visíveis
            onObject.SetActive(true);
            offObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            // Verifica se o script mainSerial está disponível
            if (serialController != null)
            {
                // Obtém o último status MPU do script mainSerial
                float statusMPU = serialController.GetLastAceleracao();

                // Se o status MPU for 0, torna o objeto "on" invisível e o objeto "off" visível
                if (statusMPU == 0f)
                {
                    onObject.SetActive(false);
                    offObject.SetActive(true);
                }
                // Se o status MPU for 1, torna o objeto "off" invisível e o objeto "on" visível
                else if (statusMPU == 1f)
                {
                    onObject.SetActive(true);
                    offObject.SetActive(false);
                }
                // Se o status MPU for diferente de 0 ou 1, mantém ambos os objetos visíveis
                else
                {
                    onObject.SetActive(true);
                    offObject.SetActive(true);
                }
            }
        }
    }
}