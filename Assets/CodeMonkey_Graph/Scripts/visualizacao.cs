using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualizacao : MonoBehaviour
{
    private bool isExpanded = false;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clique detectado!"); // Adiciona esta linha

        if (isExpanded)
        {
            // Volta para a escala original
            transform.localScale = originalScale;
            isExpanded = false;

            // Restaura a posição original (opcional)
            transform.position = new Vector3(0f, 0f, transform.position.z);
        }
        else
        {
            // Calcula a escala necessária para ocupar a tela
            Vector3 screenSize = new Vector3(Camera.main.orthographicSize * 2f * Camera.main.aspect, Camera.main.orthographicSize * 2f, 1f);
            transform.localScale = screenSize;
            isExpanded = true;

            // Move o objeto para a frente da câmera para que ocupe toda a tela
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * (transform.localScale.z / 2f);
        }
    }
}
