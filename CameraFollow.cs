using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 vel;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()

    {
        if (player.GetComponent<Char>().isDead)
            return;
        //Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, 5f);
        Vector3 target = new Vector3(player.transform.position.x + 3f, player.transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, 0.5f);
    }
}
