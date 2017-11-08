using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private int lifes = 3;

    [SerializeField]
    private Text textLifes;

    private const string TEXT_LIFES= "Lifes : ";
    private InfoPlayer infoPlayer;

    // Use this for initialization
    void Start () {
        textLifes.text = TEXT_LIFES + lifes;
        infoPlayer = FindObjectOfType<InfoPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LifeUp() {
        lifes++;
        textLifes.text = TEXT_LIFES + lifes;
    }

    public void PlayerDie() {
        lifes--;
        if (lifes <= 0) {
            infoPlayer.CountPlayerDie();
            SceneManager.LoadScene("DieMenu");
        } else {
            textLifes.text = TEXT_LIFES + lifes;
        }
    }

    public void LoadScene(string destination) {
        SceneManager.LoadScene(destination);
    }

    public void Quit() {
        Application.Quit();
    }
}
