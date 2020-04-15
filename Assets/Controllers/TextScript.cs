using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(clear());
        }
    }

    IEnumerator clear()
    {
        yield return new WaitForSeconds(2f);

        GetComponent<Text>().text = "";
        this.gameObject.SetActive(false);
    }
}
