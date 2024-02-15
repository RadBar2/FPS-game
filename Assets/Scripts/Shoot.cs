using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shoot : MonoBehaviour
{

    public AudioClip gunshot;
    public float cooldown = 0.5f;
    public GameObject bulletHole;

    AudioSource audioSource;
    private float lastShot;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("Fire1") && Time.time > lastShot)
        {
            audioSource.PlayOneShot(gunshot);

            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if(Physics.Raycast(ray, out var hit))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().Die();
                }
            }

            else
            {
                Instantiate(bulletHole, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
            }
        }

        
    }
}
