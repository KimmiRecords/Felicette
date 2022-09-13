using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    //el ButtonManager tiene un diccionario con todos sus hijos botones
    //asi puedo acceder a ellos facilmente. por ahora igual no lo uso para nada

    Button[] _childrenButtons;
    Dictionary<string, Button> _botones = new Dictionary<string, Button>();

    void Start()
    {
        _childrenButtons = GetComponentsInChildren<Button>();

        for (int i = 0; i < _childrenButtons.Length; i++)
        {
            _botones.Add(_childrenButtons[i].gameObject.name, _childrenButtons[i]);
            print(_childrenButtons[i].gameObject.name);
        }
    }
}
