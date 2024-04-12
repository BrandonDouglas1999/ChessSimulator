using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : Piece
{
    private bool hasMoved = false; //if true, then you can't castle

    public KingMovement(int piece, int x, int y, GameObject gameObject) : base(piece, x, y, gameObject)
    {
    }

    public override bool canMove(int x, int y)
    {
        if ((y == base.y + 1 && (x == base.x || x == base.x + 1 || x == base.x - 1)) || (y == base.y - 1 && (x == base.x || x == base.x + 1 || x == base.x - 1)) || (y == base.y && (x == base.x - 1 || x == base.x + 1))) //if square one block away
        {
            hasMoved = true;
            return true;
        }
        return false;
    }

    public bool canCastle(int x, int y, List<Piece> pieces)
    {
        if (!hasMoved && !isBlocked(x, y) && hasRook(x, y, pieces))
        {
            hasMoved = true;
            return true;
        }
        return false;
    }

    public override bool canCapture(int x, int y)
    {
        if (canMove(x, y)) //if it can move to square
        {
            hasMoved = true;
            return true; //return true
        }
        return false;
    }

    private bool hasRook(int x, int y, List<Piece> pieces)
    {
        foreach (Piece n in pieces)
        {
            if ((n.getPiece() == 7 || n.getPiece() == 1) && n.inPosition(x, y))
            {
                return true;
            }
        }
        return false;
    }

    private bool isBlocked(int x, int y)
    {
        Vector3 currentLocation = new Vector3(base.x + 0.5F, 1f, base.y + 0.5F);
        Vector3 destination = new Vector3(x + 0.5F, 1f, y + 0.5F);

        if (Physics.Linecast(currentLocation, destination)) //if line doesn't hit anything
        {
            return true;
        }

        return false;
    }
}
