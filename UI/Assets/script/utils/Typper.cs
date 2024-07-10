using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typper : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float timeBetweenLetters = .1f;
    public string phrase;

    [NaughtyAttributes.Button]
    public void startType()
    {
        StartCoroutine(Type(phrase));
    }

    IEnumerator Type(string s)
    {
        textMesh.text = "";
        foreach (char i in s.ToCharArray())
        {
            textMesh.text += i;
            yield return new WaitForSeconds(timeBetweenLetters);
        }


    }
}
