using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : Piece
{
    private bool hasMoved = false;
    private bool isWhite;

    public PawnMovement(int piece, int x, int y, GameObject gameObject, bool isWhite) : base(piece, x, y, gameObject)
    {
        this.isWhite = isWhite;
    }

    public override bool canMove(int x, int y)
    {
        if (hasMoved) //if it has moved
        {
            if (isWhite && y == base.y + 1 && x == base.x) //if up one space for white
            {
                return true;
            }
            else if(!isWhite && y == base.y - 1 && x == base.x) //if down one space for black
            {
                return true;
            }
        }
        else if (!hasMoved) //if it hasn't moved
        {
            if (isWhite && (y == base.y + 1 || y == base.y + 2) && x == base.x) //if one or two forward for white
            {
                this.hasMoved = true; //set has moved to true
                return true;
            }
            else if(!isWhite && (y == base.y - 1 || y == base.y - 2) && x == base.x) //if one or two forward for black
            {
                this.hasMoved = true;
                return true;
            }
        }
        return false;
    }

    public override bool canCapture(int x, int y)
    {
        if (isWhite && (y == base.y + 1 && (x == base.x + 1 || x == base.x - 1))) //if up right or up left or enpessant for white
        {
            return true; //return true
        }
        else if(!isWhite && (y == base.y - 1 && (x == base.x + 1 || x == base.x - 1))) //if down right or down left or enpassant for black
        {
            return true;
        }
        return false;
    }

    public bool canEnPassant(bool whiteTurn, Vector3 lastWhiteMovement, Piece lastPieceWhiteMoved, Vector3 lastBlackMovement, Piece lastPieceBlackMoved)
    {
        if (whiteTurn) //if white's turn
        {
            if(lastPieceBlackMoved.getPiece() == 6) //if the last movement of black was a pawn
            {
                if (lastBlackMovement.y == 2) //If the pawn double moved
                {
                    return true;
                }
            }
        }
        else //if black's turn
        {
            if (lastPieceWhiteMoved.getPiece() == 0) //if the last movement of white was a pawn
            {
                if (lastWhiteMovement.y == -2) //If the pawn double moved
                {
                    return true;
                }
            }
        }
        return false;
    }
}
