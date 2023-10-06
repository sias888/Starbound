using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{

    public static PlayerBulletPool instance;

    private List<GameObject> bullets;

    public int poolCount() {
        int val = 0;

        foreach(GameObject bullet in bullets) {
            if (bullet.activeInHierarchy == true)
                val++;
        }

        return val;
    }

    public GameObject bullet;
    public int MAX_BULLETS = 100;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        bullets = new List<GameObject>();
    }

    #nullable enable
    //Return inactive bullet, or null if no bullet can be returned
    public GameObject? GetBullet(Transform transform) {
        //If bullets contains inactive bullets, return one

        //Check if bullets contains any bullets at all:
        if (bullets.Count > 0) {
            //Debug.Log(bullets.Count);
            foreach(GameObject b in bullets) {
                if (!b.activeInHierarchy) {
                    return b;
                }
            }
        }

        //else if bullets.Count < MAX_BULLETS return new bullet
        if (bullets.Count < MAX_BULLETS) {
            GameObject newBullet = Instantiate(bullet, transform);
            newBullet.SetActive(false);
            bullets.Add(newBullet);
            return newBullet;
        }

        return null;
    }

    public List<GameObject>? GetBullets(int numRequested) {
        List<GameObject> returnBullets = new List<GameObject>();

        for (int i=0; i < numRequested; i++) {
            if (bullets.Count > 0) {
                foreach(GameObject b in bullets) {
                    if (!b.activeInHierarchy && !returnBullets.Contains(b)) {
                        returnBullets.Add(b);
                        break;
                    }
                }
            }
        }


        for (int i=returnBullets.Count; i < numRequested; i++) {
            if (bullets.Count < MAX_BULLETS) {
                GameObject newBullet = Instantiate(bullet);
                newBullet.SetActive(false);
                bullets.Add(newBullet);
                returnBullets.Add(newBullet);
                break;
            }
        }

        if (returnBullets.Count == numRequested)
            return returnBullets;

    
        return null;
    }
}
