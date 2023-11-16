using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace grafico
{
    public interface IDataReceiver
    {
        void ReceiveAltura(float altura);
        void ReceiveAceleracao(float aceleracao);
        void ReceivePressao(float pressao);
    }
}

