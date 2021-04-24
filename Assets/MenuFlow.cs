using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFlow : MonoBehaviour
{

    public TMPro.TextMeshProUGUI[] texts;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (i < texts.Length - 1)
        {
            if(Input.GetKeyDown("space"))
            {
                Debug.Log("Spaced pressed");
                texts[i].enabled = false;
                i++;
                texts[i].enabled = true;
            }
        }
    }
}
