using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Vector2 firstTouchPos;
    private Vector2 secondTouchPos;
    private Vector2 currentSwipe;
    private float timeClick;
    private float currentAngle;
    private float startAngle;
    MovePlayer movePlayer;
    private bool isRotating;
    public bool IsRotating { get => isRotating; private set { } }
    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>(); 
        startAngle = transform.rotation.y;
        currentAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(movePlayer.CanMove) Swipe();
    }
    void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0);
        currentAngle += angle;
    }
    void ChangeSpeed(float speed)
    {
        movePlayer.Speed = speed;
    }
    void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
               
                //save began touch 2d point
                firstTouchPos = new Vector2(t.position.x, t.position.y);
                timeClick = Time.time;

            }
            if (t.phase == TouchPhase.Moved)
            {
                isRotating = true;
                ChangeSpeed(40);
                //save ended touch 2d point
                secondTouchPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondTouchPos.x - firstTouchPos.x, secondTouchPos.y - firstTouchPos.y);

                if (Time.time - timeClick > 0.1f)
                {
                    //normalize the 2d vector
                    currentSwipe.Normalize();

                    if (Mathf.Abs(currentSwipe.x) < 0.5f)
                    {
                        if (currentSwipe.y > 0) ChangeSpeed(movePlayer.MaxSpeed);
                        else if (currentSwipe.y < 0) ChangeSpeed(movePlayer.Speed);
                    }
                    if (Mathf.Abs(currentSwipe.y) < 0.5f)
                    {
                         Rotate(t.deltaPosition.x/10);
                    }
                }
            }
            if(t.phase==TouchPhase.Ended)
            {
                if (Mathf.Abs(currentAngle) < 45)
                    transform.rotation = Quaternion.Euler(0, startAngle, 0);
                else
                {
                    float angleToRotate = (currentAngle < 0)?-90 : 90;
  
                      transform.rotation = Quaternion.Euler(0, startAngle + angleToRotate , 0);
                    startAngle += angleToRotate;
                    currentAngle = 0;
                }
                
            }
        }
    }
}
