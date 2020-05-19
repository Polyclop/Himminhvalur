using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class ButtonThroughKeySelection : MonoBehaviour
{
    string key;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            EventSystem.current.SetSelectedGameObject(
                     this.gameObject);
        }
    }
}
