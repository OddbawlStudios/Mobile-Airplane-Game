using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AirplaneController : MonoBehaviour
{

    [SerializeField] private float speed, minSpeed, maxSpeed, speedMultiplier;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxUpRotation;
    [SerializeField] private float maxDownRotation;
    [SerializeField] private float rot;
    private float rotCur;
    [SerializeField] private Sprite airplaneGFX;
    [SerializeField] private GameObject gfx;
    private float timeValue = 0.0f;
    private Rigidbody2D rb;
    private bool flyingUp;
    [SerializeField] private Camera cam;
    [SerializeField]Vector3 lowerLeft, upperRight;
    [SerializeField] private float offset;
    private float distanceTraveled;
    [SerializeField] private TMP_Text distanceText;
    private bool alive = true;
    private float timeAlive;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        airplaneGFX = GetComponentInChildren<SpriteRenderer>().sprite;
        Vector3 upperRightScreen = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 lowerLeftScreen = new Vector3(0, 0, 0);
        upperRight = cam.ScreenToWorldPoint(upperRightScreen);
        lowerLeft = cam.ScreenToWorldPoint(lowerLeftScreen);
    }

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        rot = Mathf.Clamp(rot, maxDownRotation, maxUpRotation);

        if(alive)
        {
            timeAlive += Time.deltaTime;
        }
        distanceText.text = timeAlive.ToString("0");

        if (Input.GetMouseButton(0))
        {
            flyingUp = true;
            speed--;
            rot++;
        }
        else
        {
            flyingUp = false;
            speed++;
            rot--;
        }
        if(transform.position.y <= lowerLeft.y + offset)
        {
            Debug.Log("Player has hit bottom of screen.");
        }
        if (transform.position.y >= upperRight.y - offset)
        {
            Debug.Log("Player has hit top of screen.");
        }

        gfx.transform.rotation = Change(0f,0f,rot);
    }

    private void FixedUpdate()
    {
        if(flyingUp)
        {
            rb.velocity = new Vector2(0f, speed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(0f, -speed * Time.deltaTime);
        }
    }

    private Quaternion Change(float x, float y, float z)
    {
        return new Quaternion(x, y, z, 1);
    }

}
