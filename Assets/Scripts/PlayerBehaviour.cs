using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speedX = 2;
    [SerializeField] private float speedY = 1;

    [SerializeField] private GameObject flowerObject;
    [SerializeField] private Transform parent;
    private Rigidbody2D rigid;
    private Transform spawnPoint;

    private bool cooldown;

    void Start()
    {
        cooldown = true;
        rigid = GetComponent<Rigidbody2D>();
        spawnPoint = transform.GetChild(1);
    }

    void FixedUpdate()
    {

        float inputX = Input.GetAxis("Horizontal");

        if (inputX != 0)
        {
            if (inputX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            } else transform.localScale = new Vector3(1, 1, 1);

            rigid.AddForce(new Vector2(inputX * speedX * Time.deltaTime, 0), ForceMode2D.Impulse);
        }

        float inputY = Input.GetAxis("Vertical");

        if (inputY != 0)
        {
            rigid.AddForce(new Vector2(0, inputY * speedY * Time.deltaTime), ForceMode2D.Impulse);
        }

        if (cooldown) { 
            if (rigid.velocity.x > 1 || rigid.velocity.x < -1 || rigid.velocity.y > 1 || rigid.velocity.y < -1)
            {
                cooldown = false;
                GameObject currentSpawnObject;
                currentSpawnObject = Instantiate(flowerObject);
                currentSpawnObject.transform.position = spawnPoint.position;
                currentSpawnObject.transform.parent = parent;
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.4f);
        cooldown = true;
    }
}
