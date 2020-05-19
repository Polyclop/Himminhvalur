using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourMainMenu : MonoBehaviour
{
    
    public void RetourMenu()
    {

        SceneManager.LoadScene("Menu");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
