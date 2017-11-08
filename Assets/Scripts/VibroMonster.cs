using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibroMonster : MonoBehaviour {
    [SerializeField]
    private AnimationCurve animationVibro;

    private Vector3 positionInitial;
    [SerializeField]
    [Range(0.4f, 5)]
    private float timeMax = 4;
    private float vibrateamplificateur = 2.0f;

	// Use this for initialization
	void Start () {
        positionInitial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.timeSinceLevelLoad % timeMax;
        currentTime /= timeMax - 1;
        float positionY = animationVibro.Evaluate(currentTime);
        positionY *= vibrateamplificateur;
        positionY += positionInitial.y;
        Vector3 newPosition = new Vector3(positionInitial.x, positionY, positionInitial.z);
	}
}
