using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePosition : MonoBehaviour
{
    //este script determina el movimiento de la base antes del despegue

    public float moveSpeed;
    bool isMoving;
    BasePositionDirection dir;
    Vector3 move;

    void Start()
    {
        EventManager.Subscribe("BasePositionDown", StartMoveBase);
        EventManager.Subscribe("BasePositionUp", EndMoveBase);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveBase();
        }
    }

    public void StartMoveBase(params object[] parameters)
    {
        isMoving = true;

        if (parameters[0] is BasePositionDirection)
        {
            dir = (BasePositionDirection)parameters[0];
        }
        else
        {
            print("ojo que el primer parametro que me pasaste no es un BasePositionDirection");
        }
    }

    public void MoveBase()
    {
        if (dir == BasePositionDirection.left)
        {
            move = Vector3.left;
        }
        else
        {
            move = Vector3.right;
        }

        transform.position += move * moveSpeed * Time.deltaTime;
    }

    public void EndMoveBase(params object[] parameters)
    {
        isMoving = false;
    }
}
