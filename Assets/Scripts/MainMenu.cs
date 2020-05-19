using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public Image controles;
    public int choixmenu;
    public GameObject buttonPlay;
    public GameObject buttonControl;
    public GameObject buttonQuit;
    
 


    public void PlayGame()   
    {
        
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Controles()
    {

        SceneManager.LoadScene("Controles");

    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");

    }

    private void Update()
    {

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {

            choixmenu += 1;
            
            if ( choixmenu > 3 )

            {
                choixmenu = 1;
                EventSystem.current.SetSelectedGameObject(
                     this.buttonPlay);

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
                    Controles();
                    break;
                case 3:
                    QuitGame();
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
                EventSystem.current.SetSelectedGameObject(
                     this.buttonQuit);

            }
            
        }
    }

}
