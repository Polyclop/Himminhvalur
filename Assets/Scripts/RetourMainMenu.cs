using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;


public class RetourMainMenu : MonoBehaviour
{
    int playerID = 0;
    Player player;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    public void RetourMenu()
    {
        SceneManager.LoadScene("Menu");
    }

 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || player.GetAnyButtonDown())
        {
            RetourMenu();
        }
    }
}
