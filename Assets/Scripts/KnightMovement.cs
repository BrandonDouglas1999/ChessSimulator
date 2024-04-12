using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : Piece
{
    public KnightMovement(int piece, int x, int y, GameObject gameObject) : base(piece, x, y, gameObject)
    {
    }

    public override bool canMove(int x, int y)
    {
        if (((x == base.x+1 || x == base.x-1) && (y == base.y + 2 || y == base.y - 2)) || ((x == base.x + 2 || x == base.x - 2) && (y == base.y + 1 || y == base.y - 1))) //if knight can move to square
        {
            return true;
        }
        return false;
    }

    public override bool canCapture(int x, int y)
    {
        if (canMove(x, y)) //if it can move to square
        {
            return true; //return true
        }
        return false;
    }
}
