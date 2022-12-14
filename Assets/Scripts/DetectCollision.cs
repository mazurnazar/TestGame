using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] MovePlayer movePlayer;
    int collisionCount = 0;
    private void Awake()
    {
        movePlayer = transform.parent.gameObject.GetComponent<MovePlayer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // if collides with enemy block move it and continue movement of player
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.transform.parent.GetComponent<ObstacleController>().Points -= 10;
            movePlayer.Speed = -5;
            StartCoroutine(MoveBlock(collision.gameObject));
        }
    }
    IEnumerator MoveBlock(GameObject collision)
    {
        collision.tag = "Untagged";
        yield return new WaitForSeconds(0.1f);
        movePlayer.Speed = 40;
        collision.transform.position += new Vector3(0, 0, 10);
        yield return new WaitForSeconds(0.1f);
        collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
