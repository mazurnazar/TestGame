using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private MovePlayer movePlayer;
    private void Awake()
    {
        movePlayer = player.GetComponent<MovePlayer>();
    }
    
    void Update()
    {
        // move camera with same speed as player
        if(movePlayer.CanMove)
        transform.position += Vector3.forward * Time.deltaTime * player.GetComponent<MovePlayer>().Speed;
    }
}
