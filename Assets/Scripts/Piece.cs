using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    protected int piece;
    protected int x;
    protected int y;
    public GameObject gameObject;

    public Piece(int piece, int x, int y, GameObject gameObject)
    {
        this.piece = piece;
        this.x = x;
        this.y = y;
        this.gameObject = gameObject;
    }

    public int getPiece() { return this.piece; }
    public int getX() { return this.x; }
    public int getY() { return this.y; }

    public bool inPosition(int x, int y) //returns true if piece is in the given position
    {
        if(this.x == x && this.y == y)
        {
            return true;
        }
        return false;
    }

    public void setPosition(int x, int y) //moves piece
    {
        this.gameObject.transform.position = new Vector3(x + 0.5f, this.gameObject.transform.position.y, y + 0.5f);
        this.x = x;
        this.y = y;
    }

    public virtual bool canMove(int x, int y) //returns true if piece can move to given position
    {
        return true;
    }

    public virtual bool canCapture(int x, int y) //returns true if piece can capture at given position
    {
        return true;
    }
}
