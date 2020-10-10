using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speedX = 2;
    [SerializeField] private float speedY = 1;
    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (inputX!= 0)
        {
            if(inputX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            } else transform.localScale = new Vector3(1, 1, 1);

            rigid.AddForce(new Vector2(inputX*speedX*Time.deltaTime, 0), ForceMode2D.Impulse);
            /*Vector3 currentPos = transform.position;
            currentPos.x = currentPos.x + speedX * inputX;
            transform.position = currentPos;*/
        }

        float inputY = Input.GetAxis("Vertical");

        if (inputY != 0)
        {
            rigid.AddForce(new Vector2(0, inputY*speedY*Time.deltaTime), ForceMode2D.Impulse);
            /*Vector3 currentPos = transform.position;
            currentPos.y = currentPos.y + speedY * inputY;
            transform.position = currentPos;*/
        }
    }
}
