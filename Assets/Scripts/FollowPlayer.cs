using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -5);

    //LateUpdate prevents jitter in camera when follows player
    void LateUpdate()
    {
        if (player)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
