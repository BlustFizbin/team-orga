using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float smooth = 5;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, playerPos,Time.deltaTime*smooth);
    }
}
