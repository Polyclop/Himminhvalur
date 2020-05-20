using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using Rewired;


public class MainMenu : MonoBehaviour
{
    public int choixmenu;
    public GameObject[] buttons;


    int playerID = 0;
    Player player;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }
    

    private void Update()
    {

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {

            choixmenu++;
            
            if ( choixmenu > 4 )
            {
                choixmenu = 1;
                //EventSystem.current.SetSelectedGameObject(this.buttonPlay);

            }
            
        }

        if (player.GetButtonDown("Vertical") && player.GetAxis("Vertical") > 0)
        {
            choixmenu--;

            if (choixmenu < 1 )
            {
                choixmenu = 4;
                //EventSystem.current.SetSelectedGameObject(this.buttonQuit);

            }
            
        }

        EventSystem.current.SetSelectedGameObject(this.buttons[choixmenu-1]);

        if ((player.GetButtonDown("Interact")) || (Input.GetKeyDown(KeyCode.Return)))
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
                    Credits();
                    break;
                case 4:
                    QuitGame();
                    break;

                default:
                    break;

            }


        }
    }

    public void PlayGame()
    {

        SceneManager.LoadScene("IntroHistoire");
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


}
