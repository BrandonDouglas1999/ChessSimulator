using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : Piece
{
    public BishopMovement(int piece, int x, int y, GameObject gameObject) : base(piece, x, y, gameObject)
    {
    }

    public override bool canMove(int x, int y)
    {
        for (int i = 0; i < 8; i++)
        {
            if (((x == base.x + i || x == base.x - i) && (y == base.y + i || y == base.y - i)) && !isBlockedMovement(x, y)) //if same diagonal
            {
                return true;
            }
        }
        return false;
    }

    public override bool canCapture(int x, int y)
    {
        for (int i = 0; i < 8; i++)
        {
            if (((x == base.x + i || x == base.x - i) && (y == base.y + i || y == base.y - i)) && !isBlockedCapture(x, y)) //if same diagonal
            {
                return true;
            }
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

        if (Physics.Linecast(currentLocation, destination)) //if line doesn't hit anything before target
        {
            return true;
        }

        return false;
    }
}
