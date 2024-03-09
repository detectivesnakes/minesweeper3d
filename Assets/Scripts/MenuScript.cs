using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour {
    Handler gamehandler;
    [SerializeField] GameObject mainui;
    [SerializeField] GameObject gameui;

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI bombs;
    float elapsed;

    [System.Obsolete]
    private void Start() {
        gamehandler = GameObject.FindGameObjectWithTag("FindMe").GetComponent<Handler>();
        mainui.SetActive(true);
        gameui.SetActive(false);
    }

    public void Beginner() {
        Camera.main.fieldOfView = 40;
        gamehandler.NewGame(9,9,10);
        gamehandler.ResetBoard();
        mainui.SetActive(false);
        gameui.SetActive(true);
        bombs.text = ("10");
    }

    public void Advanced() {
        Camera.main.fieldOfView = 50;
        gamehandler.NewGame(16,16,40);
        gamehandler.ResetBoard();
        mainui.SetActive(false);
        gameui.SetActive(true);
        bombs.text = ("40");
    }

    public void Expert() {
        Camera.main.fieldOfView = 60;
        gamehandler.NewGame(30,16,99);
        gamehandler.ResetBoard();
        mainui.SetActive(false);
        gameui.SetActive(true);
        bombs.text = ("99");
    }

    public void QuitGame(){
        Application.Quit();
    }
}