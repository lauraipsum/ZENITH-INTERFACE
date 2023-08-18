using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace grafico
{
    public interface IDataReceiver
    {
        void ReceiveAltura(float altura);
        void ReceiveData(float value);
        void ReceivePressao(float pressao);
    }
}

