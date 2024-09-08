using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public SOint SOint;

    public TextMeshProUGUI uiTextValue;
    void Start()
    {
        uiTextValue.text = SOint.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uiTextValue.text = SOint.value.ToString();
    }
}
