using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed;
    private Vector2 direction;

    public bool destroyed = false;

    private void OnEnable() {
        Invoke("Destroy", 10f);
    }

    private void Destroy() {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void DestroyAfter(float f) {
        Invoke("Destroy", f);
    }


    private void Update() {
        direction.Normalize();
        transform.position += new Vector3(direction.x, direction.y, 1) * speed * Time.deltaTime; 
    }

    public void SetSpeed(float s) {
        speed = s;
    }

    public void SetDirection(float x, float y) {
        direction.x = x;
        direction.y = y;
    }
    public void SetDirection(Vector2 v) {
        direction.x = v.x;
        direction.y = v.y;
    }
    public void SetDirection(Vector3 v) {
        direction.x = v.x;
        direction.y = v.y;
    }

    public Vector2 GetDirection() {
        return direction;
    }
}
