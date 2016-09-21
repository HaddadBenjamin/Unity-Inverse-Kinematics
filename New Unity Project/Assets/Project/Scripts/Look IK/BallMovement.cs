using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private float moveUpSpeed;
    [SerializeField]
    private float moveRightSpeed;

    private Transform myTransform;
    private Vector3 initialPosition;
    #endregion

    #region Properties
    public float MoveUpSpeed
    {
        get
        {
            return moveUpSpeed;
        }

        set
        {
            moveUpSpeed = value;
        }
    }

    public float MoveRightSpeed
    {
        get
        {
            return moveRightSpeed;
        }

        set
        {
            moveRightSpeed = value;
        }
    }
    #endregion

    #region Unity Behaviour
    void Awake()
    {
        this.myTransform = transform;

        //this.myTransform.position = new Vector3(this.myTransform.position.x - this.moveRightSpeed * 3.0f / 2.0f,
        //                                        this.myTransform.position.y  - this.moveUpSpeed * 1.5f / 2.0f,
        //                                        this.myTransform.position.z);

        this.initialPosition = this.myTransform.position;
    }

    void FixedUpdate()
    {
        float positionX = this.initialPosition.x + Mathf.PingPong(Time.time * this.moveRightSpeed, 4.0f);
        float positionY = this.initialPosition.y + Mathf.PingPong(Time.time * this.moveUpSpeed, 1.5f);

        this.myTransform.position = new Vector3(positionX, positionY, this.initialPosition.z);
    }
    #endregion
}