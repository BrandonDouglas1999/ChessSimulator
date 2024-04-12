using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    int clickedX;
    int clickedY;
    private Piece selectedPiece = null;
    private List<Piece> pieces  = new List<Piece>();
    public List<GameObject> chessPieces;
    private bool whiteTurn = true;
    public GameObject indicator;
    public Material whiteMat;
    public Material blackMat;
    private Vector3 lastBlackMovement;
    private Piece lastPieceBlackMoved = null;
    private Vector3 lastWhiteMovement;
    private Piece lastPieceWhiteMoved = null;

    private void Start()
    {
        spawnPieces();
    }
   
   void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) //if right clicked
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
            {
                clickedX = (int)hit.point.x;
                clickedY = (int)hit.point.z;
                Piece temp = hasPiece(clickedX, clickedY); //sets temp to piece on square if it exists

                if (temp != null) //if space has a piece 
                {
                    if (selectedPiece == null && correctMovementTurn(temp)) //if you don't have a selected piece
                    {
                        selectedPiece = temp; //set selected piece to piece on space
                    }

                    //castle by clicking on king then clicking on rook
                    else if ((temp.getPiece() == 7 || temp.getPiece() == 1) && (selectedPiece.getPiece() == 11 || selectedPiece.getPiece() == 5)) //castling
                    {
                        KingMovement king = (KingMovement)selectedPiece; //typecast piece to king
                        if (king.canCastle(clickedX, clickedY, pieces)) //if king can castle towards piece
                        {
                            if (clickedX == 0) //if castling left side
                            {
                                temp.setPosition(2, clickedY); //move rook
                                selectedPiece.setPosition(1, clickedY); //move king
                            }
                            else //if castling right sied
                            {
                                temp.setPosition(5, clickedY); //move rook
                                selectedPiece.setPosition(6, clickedY); //move king   }
                            }
                            if (whiteTurn)
                            {
                                float x = selectedPiece.getX() - temp.getX();
                                float y = selectedPiece.getY() - temp.getY();
                                lastWhiteMovement = new Vector3(x, 1f, y);
                                lastPieceWhiteMoved = selectedPiece;
                            }
                            else
                            {

                                float x = selectedPiece.getX() - temp.getX();
                                float y = selectedPiece.getY() - temp.getY();
                                lastBlackMovement = new Vector3(x, 1f, y);
                                lastPieceBlackMoved = selectedPiece;
                            }
                            selectedPiece = null; //deselect piece
                            whiteTurn = !whiteTurn; //change turn
                            changeIndicator();
                        }
                    }

                    else if (correctCaptureTurn(temp) && selectedPiece.canCapture(clickedX, clickedY)) //if on the correct turn and can capture on square
                    {
                        Destroy(temp.gameObject);
                        pieces.Remove(temp); //capture piece
                        selectedPiece.setPosition(clickedX, clickedY); //move piece
                        if (whiteTurn)
                        {

                            float x = selectedPiece.getX() - temp.getX();
                            float y = selectedPiece.getY() - temp.getY();
                            lastWhiteMovement = new Vector3(x, 1f, y);
                            lastPieceWhiteMoved = selectedPiece;
                        }
                        else
                        {
                            float x = selectedPiece.getX() - temp.getX();
                            float y = selectedPiece.getY() - temp.getY();
                            lastBlackMovement = new Vector3(x, 1f, y);
                            lastPieceBlackMoved = selectedPiece;
                        }

                        temp = null;
                        selectedPiece = null; //deselect piece
                        whiteTurn = !whiteTurn; //change turn
                        changeIndicator();
                    }
                }

                else if (temp == null && selectedPiece!= null && lastPieceBlackMoved!= null && lastPieceWhiteMoved!= null && selectedPiece.getPiece() == 0 || selectedPiece.getPiece() == 6) // if pawn
                {
                    PawnMovement pawn = (PawnMovement)selectedPiece;
                    if (selectedPiece.getPiece() == 0 && selectedPiece.getY() == 7 && selectedPiece.canMove(clickedX, clickedY)) //white pawn promotion
                    {
                        selectedPiece.setPosition(clickedX, clickedY); //move piece to square
                        pieces.Add(new QueenMovement(4, selectedPiece.getX(), 7, Instantiate(chessPieces[4], new Vector3(selectedPiece.getX() + 0.5F, 1f, 7 + 0.5F), Quaternion.identity)));
                        Destroy(selectedPiece.gameObject);
                        pieces.Remove(selectedPiece);
                        if (whiteTurn)
                    {
                        float x = selectedPiece.getX() - clickedX;
                        float y = selectedPiece.getY() - clickedY;
                        lastWhiteMovement = new Vector3(x, 1f, y);
                        lastPieceWhiteMoved = selectedPiece;
                    }
                    else
                    {
                        float x = selectedPiece.getX() - clickedX;
                        float y = selectedPiece.getY() - clickedY;
                        lastBlackMovement = new Vector3(x, 1f, y);
                        lastPieceBlackMoved = selectedPiece;
                    }
                    selectedPiece = null; //deselect piece
                    pawn = null;
                    whiteTurn = !whiteTurn;
                    changeIndicator();
                    }
                    else if (selectedPiece.getPiece() == 6 && selectedPiece.getY() == 0 && selectedPiece.canMove(clickedX, clickedY)) //black pawn promotion
                    {
                        selectedPiece.setPosition(clickedX, clickedY); //move piece to square
                        pieces.Add(new QueenMovement(10, selectedPiece.getX(), 0, Instantiate(chessPieces[10], new Vector3(selectedPiece.getX() + 0.5F, 1f, 0 + 0.5F), Quaternion.identity)));
                        Destroy(selectedPiece.gameObject);
                        pieces.Remove(selectedPiece);
                        if (whiteTurn)
                        {
                            float x = selectedPiece.getX() - clickedX;
                            float y = selectedPiece.getY() - clickedY;
                            lastWhiteMovement = new Vector3(x, 1f, y);
                            lastPieceWhiteMoved = selectedPiece;
                        }
                        else
                        {
                            float x = selectedPiece.getX() - clickedX;
                            float y = selectedPiece.getY() - clickedY;
                            lastBlackMovement = new Vector3(x, 1f, y);
                            lastPieceBlackMoved = selectedPiece;
                        }
                        selectedPiece = null; //deselect piece
                        pawn = null;
                        whiteTurn = !whiteTurn;
                        changeIndicator();

                    }

                    else if (selectedPiece.canMove(clickedX, clickedY))
                    {
                        selectedPiece.setPosition(clickedX, clickedY); //move piece to square
                        if (whiteTurn)
                        {
                            float x = selectedPiece.getX() - clickedX;
                            float y = selectedPiece.getY() - clickedY;
                            lastWhiteMovement = new Vector3(x, 1f, y);
                            lastPieceWhiteMoved = selectedPiece;
                        }
                        else
                        {
                            float x = selectedPiece.getX() - clickedX;
                            float y = selectedPiece.getY() - clickedY;
                            lastBlackMovement = new Vector3(x, 1f, y);
                            lastPieceBlackMoved = selectedPiece;
                        }
                        selectedPiece = null; //deselect piece
                        pawn = null;
                        whiteTurn = !whiteTurn;
                        changeIndicator();
                    }

                    else if (lastPieceWhiteMoved != null && lastPieceBlackMoved != null && pawn.canEnPassant(whiteTurn, lastWhiteMovement, lastPieceWhiteMoved, lastBlackMovement, lastPieceBlackMoved)) //en passant
                    {
                        if (whiteTurn)
                        {
                            if ((clickedX == pawn.getX() + 1 || clickedX == pawn.getX() - 1) && clickedY == pawn.getY() + 1) //In correct x and y
                            {
                                if (clickedX == lastPieceBlackMoved.getX() && clickedY == lastPieceBlackMoved.getY()+1)
                                {
                                    Destroy(lastPieceBlackMoved.gameObject);
                                    pieces.Remove(lastPieceBlackMoved); //capture piece
                                    lastPieceBlackMoved = null;
                                    pawn.setPosition(clickedX, clickedY); //move piece
                                }
                            }
                        }
                        else
                        {
                            if ((clickedX == lastPieceWhiteMoved.getX() + 1 || clickedX == lastPieceWhiteMoved.getX() - 1) && clickedY == lastPieceWhiteMoved.getY() - 1)
                            {
                                if ((pawn.getX() == lastPieceWhiteMoved.getX() + 1 || pawn.getX() == lastPieceWhiteMoved.getX() - 1) && pawn.getY() == lastPieceWhiteMoved.getY()) //In correct x and y
                                {
                                    Destroy(lastPieceWhiteMoved.gameObject);
                                    pieces.Remove(lastPieceWhiteMoved); //capture piece
                                    lastPieceWhiteMoved = null;
                                    pawn.setPosition(clickedX, clickedY); //move piece
                                }
                            }
                        }
                        if (whiteTurn)
                        {
                            float x = selectedPiece.getX() - clickedX;
                            float y = selectedPiece.getY() - clickedY;
                            lastWhiteMovement = new Vector3(x, 1f, y);
                            lastPieceWhiteMoved = selectedPiece;
                        }
                        else
                        {
                            float x = selectedPiece.getX() - clickedX;
                            float y = selectedPiece.getY() - clickedY;
                            lastBlackMovement = new Vector3(x, 1f, y);
                            lastPieceBlackMoved = selectedPiece;
                        }
                        selectedPiece = null; //deselect piece
                        pawn = null;
                        whiteTurn = !whiteTurn;
                        changeIndicator();
                    }
                }

                else if (temp == null && selectedPiece != null && selectedPiece.canMove(clickedX, clickedY)) //if you have selected piece, and piece can move to square
                {
                    selectedPiece.setPosition(clickedX, clickedY); //move piece to square
                    
                    if (whiteTurn)
                    {
                        float x = selectedPiece.getX() - clickedX;
                        float y = selectedPiece.getY() - clickedY;
                        lastWhiteMovement = new Vector3(x, 1f, y);
                        lastPieceWhiteMoved = selectedPiece;
                    }
                    else
                    {
                        float x = selectedPiece.getX() - clickedX;
                        float y = selectedPiece.getY() - clickedY;
                        lastBlackMovement = new Vector3(x, 1f, y);
                        lastPieceBlackMoved = selectedPiece;
                    }
                    selectedPiece = null; //deselect piece
                    whiteTurn = !whiteTurn;
                    changeIndicator();
                }
                else { selectedPiece = null; } //clear selected piece if not valid selection
            }
        }
        else //if click not right click, clear selectedPiece
        {
            selectedPiece = null;
        }
    }
    
    private void changeIndicator() //change colour of turn indicator
    {
        if (whiteTurn)
        {
            indicator.GetComponent<Renderer>().material = whiteMat;
        }
        else
        {
            indicator.GetComponent<Renderer>().material = blackMat;
        }
    }

    private bool correctMovementTurn(Piece temp) //if you're on the right turn to move the selected piece
    {
        if(whiteTurn && temp.getPiece() <= 5)
        {
            return true;
        }
        else if(!whiteTurn && temp.getPiece() > 5)
        {
            return true;
        }
        return false;
    }

    private bool correctCaptureTurn(Piece temp) //if you're on the right turn to capture the selected piece
    {
        if (!whiteTurn && temp.getPiece() <= 5)
        {
            return true;
        }
        else if (whiteTurn && temp.getPiece() > 5)
        {
            return true;
        }
        return false;
    }

    private Piece hasPiece(int x, int y) //if there's a piece on square, return true
    {
        foreach (Piece n in pieces)
        {
            if(n.inPosition(clickedX, clickedY))
            {
                return n;
            }
        }
        return null;
    }

    private void spawnPieces()
    {
        for (int i = 0; i < 8; i++)
        {
            pieces.Add(new PawnMovement(0, i, 1, Instantiate(chessPieces[0], new Vector3(i + 0.5F, 1f, 1 + 0.5F), Quaternion.identity), true)); //white pawns
        }

        pieces.Add(new RookMovement(1, 0, 0, Instantiate(chessPieces[1], new Vector3(0 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity))); // white rooks
        pieces.Add(new RookMovement(1, 7, 0, Instantiate(chessPieces[1], new Vector3(7 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity)));

        pieces.Add(new KnightMovement(2, 1, 0, Instantiate(chessPieces[2], new Vector3(1 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity)));//white knights
        pieces.Add(new KnightMovement(2, 6, 0, Instantiate(chessPieces[2], new Vector3(6 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity)));

        pieces.Add(new BishopMovement(1, 2, 0, Instantiate(chessPieces[3], new Vector3(2 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity))); //white bishops
        pieces.Add(new BishopMovement(1, 5, 0, Instantiate(chessPieces[3], new Vector3(5 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity)));

        pieces.Add(new KingMovement(5, 3, 0, Instantiate(chessPieces[5], new Vector3(3 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity))); //white king
        pieces.Add(new QueenMovement(4, 4, 0, Instantiate(chessPieces[4], new Vector3(4 + 0.5F, 1f, 0 + 0.5F), Quaternion.identity)));//white queen


        for (int j = 0; j < 8; j++)
        {
            pieces.Add(new PawnMovement(6, j, 6, Instantiate(chessPieces[6], new Vector3(j + 0.5F, 1f, 6 + 0.5F), Quaternion.identity), false)); //black pawns
        }
        pieces.Add(new RookMovement(7, 0, 7, Instantiate(chessPieces[7], new Vector3(0 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity))); // black rooks
        pieces.Add(new RookMovement(7, 7, 7, Instantiate(chessPieces[7], new Vector3(7 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity)));

        pieces.Add(new KnightMovement(8, 1, 7, Instantiate(chessPieces[8], new Vector3(1 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity))); //black knights
        pieces.Add(new KnightMovement(8, 6, 7, Instantiate(chessPieces[8], new Vector3(6 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity)));

        pieces.Add(new BishopMovement(9, 2, 7, Instantiate(chessPieces[9], new Vector3(2 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity))); //black bishops
        pieces.Add(new BishopMovement(9, 5, 7, Instantiate(chessPieces[9], new Vector3(5 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity)));

        pieces.Add(new KingMovement(11, 3, 7, Instantiate(chessPieces[11], new Vector3(3 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity))); // black King
        pieces.Add(new QueenMovement(10, 4, 7, Instantiate(chessPieces[10], new Vector3(4 + 0.5F, 1f, 7 + 0.5F), Quaternion.identity)));//black queen
    }
}
