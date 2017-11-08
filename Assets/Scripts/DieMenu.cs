using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieMenu : MonoBehaviour {

    [SerializeField]
    private Text textNbrDie;
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitToChangeScene());
        InfoPlayer infoPlayer = FindObjectOfType<InfoPlayer>();
        textNbrDie.text = "Nombre de mort " + infoPlayer.GetPlayerDieCount();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}

    private IEnumerator WaitToChangeScene() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("FirstLevel");
    }
}
