using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiController : MonoBehaviour
{
    [SerializeField]
    private Transform[] gunsTransformList;
    [SerializeField]
    private float timeToFire = 2;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletVelocity = 10;
    [SerializeField]
    private int lifes = 3;
    [SerializeField]
    private float spriteBlinkingTotalDuration = 1.0f;


    private int lifesOriginal;
    private float spriteBlinkingTimer = 0.0f;
    private float spriteBlinkingTotalTimer = 0.0f;
    private float spriteBlinkingMiniDuration;
    private float spriteBlinkingMiniDurationOriginal;
    private bool startBlinking = false;
    private SpriteRenderer sprite;
    private Animator enemiAnimationController;

    // Use this for initialization
    void Start() {
        enemiAnimationController = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Fire());
        spriteBlinkingMiniDuration = spriteBlinkingTotalDuration/2.0f;

        spriteBlinkingMiniDurationOriginal = spriteBlinkingMiniDuration;
        lifesOriginal = lifes;
    }

    // Update is called once per frame
    void Update() {
        if (startBlinking == true) {
            SpriteBlinkingEffect();
        }
    }

    private void EnemiDie()
    {
        lifes--;
        if (lifes <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "BulletPlayer")  {
            Destroy(collision.collider.gameObject);
            EnemiDie();
            startBlinking = true;
        }
    }
    
    private IEnumerator Fire() {
        while (true) {
            yield return new WaitForSeconds(timeToFire);
            enemiAnimationController.SetBool("isAttacking", true);
            yield return new WaitForSeconds(1.0f);
            foreach (Transform t in gunsTransformList) {
                GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = t.right * bulletVelocity;
                Destroy(bullet, 5);
                enemiAnimationController.SetBool("isAttacking", false);
            }
        }
    }

    private void SpriteBlinkingEffect() {
        spriteBlinkingTotalTimer += Time.deltaTime;

        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration) {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            sprite.enabled = true;
            spriteBlinkingMiniDuration -= spriteBlinkingMiniDurationOriginal / (float)lifesOriginal;
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration) {
            spriteBlinkingTimer = 0.0f;

            if (sprite.enabled == true) {
                sprite.enabled = false; 
            } else {
                sprite.enabled = true;
            }
        }
    }
}
