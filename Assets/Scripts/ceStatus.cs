using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace grafico
{
    public class ceStatus : MonoBehaviour
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
                // Obt�m o �ltimo status CE do script mainSerial
                float statusCE = serialController.GetCEStatus();

                // Debug log para verificar o valor recebido do status CE
                Debug.Log("Status CE recebido: " + statusCE);

                // Se o status CE for 0, torna o objeto "on" invis�vel e o objeto "off" vis�vel
                if (statusCE == 0f)
                {
                    Debug.Log("Status CE igual a 0. Ativando o objeto off e desativando o objeto on.");
                    onObject.SetActive(false);
                    offObject.SetActive(true);
                }
                // Se o status CE for 1, torna o objeto "off" invis�vel e o objeto "on" vis�vel
                else if (statusCE == 1f)
                {
                    Debug.Log("Status CE igual a 1. Ativando o objeto on e desativando o objeto off.");
                    onObject.SetActive(true);
                    offObject.SetActive(false);
                }
                // Se o status CE for diferente de 0 ou 1, mant�m ambos os objetos vis�veis
                else
                {
                    Debug.Log("Status CE diferente de 0 e 1. Mantendo ambos os objetos vis�veis.");
                    onObject.SetActive(true);
                    offObject.SetActive(true);
                }
            }
        }
    }
}
