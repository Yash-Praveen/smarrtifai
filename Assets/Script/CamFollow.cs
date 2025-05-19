using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = player.position.x + 7;
        transform.position = pos;
    }
}
