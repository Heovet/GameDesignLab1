using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalGunScript : MonoBehaviour
{
    Camera cam;
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;
    /*    public Transform pivot;*/
    public Transform m_transform;

    public AudioSource portalSounds;
    public AudioClip orangePortalSounds, bluePortalSounds;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        m_transform = this.transform;
    }

    private void lookAtMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        m_transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        lookAtMouse();
/*        //get mouse input and position
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        // rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y,offset.x)*Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0,0, angle);*/

        if(Input.GetMouseButtonDown(0))
        {
            portalSounds.clip = bluePortalSounds;
            portalSounds.Play();
            Vector2 cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
            GameObject bluePortal = GameObject.FindGameObjectWithTag("Blue Portal");
            if(bluePortal != null)
            {
                Destroy(bluePortal);
            }
            Instantiate(bluePortalPrefab, new Vector3(cursorPos.x, cursorPos.y), Quaternion.identity);

            bluePortal = this.gameObject;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            portalSounds.clip = orangePortalSounds;
            portalSounds.Play();
            Vector2 cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
            GameObject orangePortal = GameObject.FindGameObjectWithTag("Orange Portal");
            if (orangePortal != null)
            {
                Destroy(orangePortal);
            }
            Instantiate(orangePortalPrefab, new Vector3(cursorPos.x, cursorPos.y), Quaternion.identity);

            orangePortal = this.gameObject;
        }
    }
}
