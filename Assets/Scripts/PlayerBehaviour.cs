using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speedX = 2;
    [SerializeField] private float speedY = 1;

    void Start()
    {
        
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (inputX!= 0)
        {
            if(inputX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            } else transform.localScale = new Vector3(1, 1, 1);

            Vector3 currentPos = transform.position;
            currentPos.x = currentPos.x + speedX * inputX;
            transform.position = currentPos;
        }

        float inputY = Input.GetAxis("Vertical");

        if (inputY != 0)
        {
            Vector3 currentPos = transform.position;
            currentPos.y = currentPos.y + speedY * inputY;
            transform.position = currentPos;
        }
    }
}
