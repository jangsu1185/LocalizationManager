using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour
{
    public string key;
    [SerializeField]
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        if (text == null) 
        {
            text = GetComponent<Text>();
        }
        SetText();
    }

    void SetText() 
    {
        text.text = LocalizationManager.Instance.GetText(key).Replace("\\n", "\n");
    }

}
