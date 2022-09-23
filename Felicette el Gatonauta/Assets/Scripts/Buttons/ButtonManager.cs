using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    //el ButtonManager tiene un diccionario con todos sus hijos botones
    //asi puedo acceder a ellos facilmente.

    //se encarga de habilitar los botones de los niveles a medida que los desbloqueas

    Button[] _childrenButtons;
    Dictionary<string, Button> _botones = new Dictionary<string, Button>();

    void Start()
    {
        _childrenButtons = GetComponentsInChildren<Button>();

        for (int i = 0; i < _childrenButtons.Length; i++)
        {
            _botones.Add(_childrenButtons[i].gameObject.name, _childrenButtons[i]);
            _childrenButtons[i].gameObject.SetActive(false);
            print(_childrenButtons[i].gameObject.name);
        }

        ActivateLevelButtons();
        UpdateLevelButtonsColor();
    }

    public void ActivateLevelButtons()
    {
        for (int i = 0; i < (PlayerPrefs.GetInt("nivelesCompletados") + 1); i++)
        {
            _childrenButtons[i].gameObject.SetActive(true);
            print("habilite el boton " + _childrenButtons[i].name);
        }
    }

    public void UpdateLevelButtonsColor()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("nivelesCompletados"); i++)
        {
            ColorBlock cb = _childrenButtons[i].colors;
            cb.normalColor = Color.green;
            _childrenButtons[i].colors = cb;
            print("pinte el boton " + _childrenButtons[i].name);
        }
    }
}
