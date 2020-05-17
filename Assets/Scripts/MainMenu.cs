using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public int choixmenu;


   public void PlayGame()   
    {
        
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {

            choixmenu += 1;
            
            if ( choixmenu > 3 )

            {
                choixmenu = 1;
            }
            //highlight
        }
        if ((Input.GetButtonDown("Fire1")) || (Input.GetKeyDown(KeyCode.Return)))
        {
            switch (choixmenu)
            {
                case 1:
                    PlayGame();
                    break;
                case 2:
                    //controles
                    break;
                case 3:
                    Application.Quit();
                    break;

                default:
                    break;

            }
            

        }


        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            choixmenu -= 1;

            if (choixmenu < 1 )

            {
                choixmenu = 3;
            }
            //highlight
        }
    }

}
