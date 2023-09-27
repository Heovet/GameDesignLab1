using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private Transform destination;

    public bool isOrange;
    public float distance = 0.5f;

    private int cooldown= 0;

    public AudioSource teleportSound;

    // Start is called before the first frame update
    void Start()
    {
/*        if (isOrange)
        {
            destination = GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Transform>();
        } */       
    }

    private void FixedUpdate()
    {
        if (isOrange)
        {
            destination = GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Transform>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            Debug.Log("Portal!");
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
            teleportSound.Play();
        }
    }
}
