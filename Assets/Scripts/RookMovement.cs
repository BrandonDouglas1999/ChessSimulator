using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovement : Piece
{
    public RookMovement(int piece, int x, int y, GameObject gameObject) : base(piece, x, y, gameObject)
    {
    }

    public override bool canMove(int x, int y)
    {
        if ((base.x == x ^ base.y == y) && !isBlockedMovement(x, y)) //if in same row/column
        {
            return true;
        }
        return false;
    }

    public override bool canCapture(int x, int y)
    {
        if ((base.x == x ^ base.y == y) && !isBlockedCapture(x, y)) //if in same row/column
        {
            return true;
        }
        return false;
    }

    private bool isBlockedMovement(int x, int y)
    {
        Vector3 currentLocation = new Vector3(base.x + 0.5F, 1f, base.y + 0.5F);
        Vector3 destination = new Vector3(x + 0.5F, 1f, y + 0.5F);

        if (Physics.Linecast(currentLocation, destination)) //if line isn't blocked
        {
            return true;
        }

        return false;
    }

    private bool isBlockedCapture(int x, int y)
    {
        if (base.x - x > 0) //destination to left
        {
            x += 1;
        }
        else if (base.x - x < 0) //destination to right
        {
            x -= 1;
        }

        if (base.y - y > 0) //Destination downwards
        {
            y += 1;
        }
        else if (base.y - y < 0) //Destination upwards
        {
            y -= 1;
        }

        Vector3 currentLocation = new Vector3(base.x + 0.5F, 1f, base.y + 0.5F);
        Vector3 destination = new Vector3(x + 0.5F, 1f, y + 0.5F);

        if (Physics.Linecast(currentLocation, destination)) //if line doesn't hit before target
        {
            return true;
        }

        return false;
    }
}
