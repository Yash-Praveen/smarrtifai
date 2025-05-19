using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private void Start()
    {
        StartCoroutine(ChangePos());
    }

    IEnumerator ChangePos()
    {
        yield return new WaitUntil(() => player.position.x > transform.position.x + 18);
        Vector2 pos = transform.position;
        pos.x += 50;
        transform.position = pos;
        StartCoroutine(ChangePos());
    }
}
