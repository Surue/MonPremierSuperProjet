using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {
    [Header("Physics")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float force = 10;
    [Header("Jump")]
    [SerializeField]
    private Transform positionRaycastJump;
    [SerializeField]
    private float radiusRaycastJump;
    [SerializeField]
    private LayerMask layerMaskJump;
    [SerializeField]
    private float forceJump = 5;
    [Header("Fire gun super sonic lol boum")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform gunTransform;
    [SerializeField]
    private float bulletVelocity = 4;
    [SerializeField]
    private float timeToFire = 0.5f;
    [SerializeField]

    //[TextArea]
    //private string message;

    private float lastTimeFire = 0;

    private Transform spawnTransform;
    private Rigidbody2D rigid;
    private GameManager gameManager;
    private Animator playerAnimationController;
    private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        playerAnimationController = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        spawnTransform = GameObject.Find("Spawn").transform;
        gameManager = FindObjectOfType<GameManager>(); //Bouffe une chiée de performance
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxis("Horizontal");
        renderer.flipX = horizontalInput < 0;
        playerAnimationController.SetFloat("speedX", Mathf.Abs(horizontalInput));

        Vector2 forceDirection = new Vector2(horizontalInput, 0);
        forceDirection *= force;

        rigid.AddForce(forceDirection);

        bool isTouchingFloor = Physics2D.OverlapCircle(positionRaycastJump.position, radiusRaycastJump, layerMaskJump);
        if(Input.GetAxis("Jump") > 0 && isTouchingFloor) {
            playerAnimationController.SetTrigger("Jump");
            rigid.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        } else {
            playerAnimationController.SetBool("isGrounded", isTouchingFloor);
        }

        if (Input.GetAxis("Fire1") > 0) {
            Fire();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "BulletEnemi") {
            Destroy(collision.collider.gameObject);
            gameManager.PlayerDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Limit"){
            transform.position = spawnTransform.position;
            Vector2 vect = new Vector2(0, 0);
            rigid.velocity = vect;
            gameManager.PlayerDie();
        }

        if(collision.tag == "LifeUp") {
            Destroy(collision.gameObject);
            gameManager.LifeUp();
        }

        if(collision.tag == "EndLevel") {
            gameManager.NextLevel();
        }
    }

    private void Fire() {
        if (Time.realtimeSinceStartup - lastTimeFire > timeToFire) {
            Vector3 positionDebugEnd = gunTransform.position + gunTransform.right;
            Debug.DrawLine(gunTransform.position, positionDebugEnd, Color.red, 5);
            GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = gunTransform.right * bulletVelocity;
            Destroy(bullet, 5);
            lastTimeFire = Time.realtimeSinceStartup;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(positionRaycastJump.position, radiusRaycastJump);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(positionRaycastJump.position, radiusRaycastJump);
    }
}
