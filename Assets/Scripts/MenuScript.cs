using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    Handler gamehandler;
    GameObject mainui;

    private void Start() {
        gamehandler = GameObject.FindGameObjectWithTag("FindMe").GetComponent<Handler>();
        mainui = GameObject.Find("MainUI");
    }

    public void Beginner() {
        gamehandler.NewGame(9,9,10);
        gamehandler.ResetBoard();
        mainui.SetActive(false);
    }

    public void Advanced() {
        gamehandler.NewGame(16,16,40);
        gamehandler.ResetBoard();
        mainui.SetActive(false);
    }

    public void Expert() {
        gamehandler.NewGame(30,16,99);
        gamehandler.ResetBoard();
        mainui.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
    }
}