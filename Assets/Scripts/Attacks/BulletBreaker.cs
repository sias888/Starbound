using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBreaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BulletBreakerAudio.instance.PlayClip();
        Destroy(gameObject, 5f);
    }

}
