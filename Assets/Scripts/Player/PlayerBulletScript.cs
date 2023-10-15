using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

    bool updateVelocity = true;
    Rigidbody2D myRigidBody;
    Animator animator;

    List<GameObject> enemiesAlreadyHit = new List<GameObject>();

    public float SPEED = 10f;

    private float DAMAGE = 7.5f;

    public void SetDamage(float f) {
        DAMAGE = f;
    }

    private void OnEnable() {
        //transform.localPosition = Vector3.zero;
        //animator.Play("BulletShoot");
        DestroyAfter(1.5f);
        updateVelocity = true;
    }

    public void DestroyAfter(float f) {
        //Invoke("Destroy", f);
        StartCoroutine(DE(f));
    }

    IEnumerator DE(float f) {
        yield return new WaitForSeconds(f);
        enemiesAlreadyHit.Clear();
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        StopCoroutine("DE");
        enemiesAlreadyHit.Clear();
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Vector2 velocity = new Vector2(0, SPEED);
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        //myRigidBody.velocity = velocity;
        animator = gameObject.GetComponent<Animator>();
        updateVelocity = true;
    }

    void FixedUpdate() {
        Vector3 velocity = Vector3.zero;
        if (updateVelocity) velocity = new Vector3(0, SPEED, 0);
        transform.localPosition += velocity * Time.fixedDeltaTime;
    }


    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Triggered!");
        try{
            if (col.gameObject.tag == "Enemies") {
                if (!enemiesAlreadyHit.Contains(col.gameObject)) {
                    col.gameObject.GetComponentInParent<Enemy>().TakeDamage(DAMAGE * ScoreManager.instance.GetScaling());
                    enemiesAlreadyHit.Add(col.gameObject);
                    animator.Play("BulletExplode");
                    //MeleeImpactAudio.instance.PlayClip();
                    updateVelocity = false;
                    DestroyAfter(0.1f);
                }
            }
        } catch{}

        if (col.gameObject.tag == "UpperBound") {
            DestroyAfter(0f);
        }
    }

}
