using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class TextShower : MonoBehaviour
{
    //este script muestra textitos
    //con uno de estos pero mas complejo esta hecho el tutorial

    protected TMPro.TextMeshProUGUI _yo;
    protected Color _originalColor;
    protected int _textIndex;

    public float fadeTime;

    [TextArea(1, 3)]
    public string[] textos;

    public virtual void Start()
    {
        _yo = GetComponent<TMPro.TextMeshProUGUI>();
        _originalColor = _yo.color;
        _textIndex = 0;
        _yo.text = textos[_textIndex];
        StartCoroutine(NextText());
    }

    public virtual IEnumerator FadeOutText()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            _yo.color = Color.Lerp(_yo.color, Color.clear, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _yo.color = Color.clear;
        yield return null;
    }
    public virtual IEnumerator FadeInText()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            _yo.color = Color.Lerp(Color.clear, _originalColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _yo.color = _originalColor;
        yield return null;

    }
    public virtual IEnumerator NextText()
    {
        //APAGO EL TEXTO QUE ESTE DE ANTES
        yield return new WaitForSeconds(fadeTime * 2);
        StartCoroutine(FadeOutText());
        yield return new WaitForSeconds(fadeTime);

        //CAMBIO AL TEXTO ACTUAL
        if (_textIndex < textos.Length)
        {
            _textIndex++;
        }
        _yo.text = textos[_textIndex];

        //PRENDO EL TEXTO ACTUAL
        StartCoroutine(FadeInText());
    }
}
