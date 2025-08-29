using UnityEngine;

public enum ChessPieceType
{
    Pawn, Knight, Rook, Bishop, Queen, King

}
public class Lab1ChessPiece : MonoBehaviour
{
    /* Place one set of chess pieces on the play area (white)
    https://commons.wikimedia.org/wiki/Category:PNG_chess_pieces/Standard_transparentLinks to an external site.
Using the inspector, we need to be able to change the piece type, color tint of the sprite
Piece type changes the sprite and how the pieces can move
When we select a piece in the scene, we want to see where the piece can move depending on the type of the piece using Gizmos
    */
    //Inspector Header for adjusting chess piece settings: tint and type
    [Header("Piece Settings")]
    public ChessPieceType pieceType;//select type in Inspector
    public Color colorTint = Color.blue;//select tint in Inspector

    [Header("Piece Icons")]
    public Sprite pawn;
    public Sprite knight;
    public Sprite rook;
    public Sprite bishop;
    public Sprite queen;
    public Sprite king;

    private SpriteRenderer spriteRenderer;//variable to control appearance of piece

    //Make changes in editor appear automatically when change occurs in the Inspector
    private void OnValidate()
    {
        if (spriteRenderer == null)//null check
            spriteRenderer = GetComponent<SpriteRenderer>();//get component

        UpdatePiece();//call function for updating piece type

    }

    private void UpdatePiece()
    {
        //Assign each sprite based on chess piece type
        switch (pieceType)
        {
            case ChessPieceType.Pawn: spriteRenderer.sprite = pawn; break;
            case ChessPieceType.Knight: spriteRenderer.sprite = knight; break;
            case ChessPieceType.Rook: spriteRenderer.sprite = rook; break;
            case ChessPieceType.Bishop: spriteRenderer.sprite = bishop; break;
            case ChessPieceType.Queen: spriteRenderer.sprite = queen; break;
            case ChessPieceType.King: spriteRenderer.sprite = king; break;
        }

        //Apply tint
        spriteRenderer.color = colorTint;


    }

    //Draw gizmos based on the rules for each chess piece
    private void OnDrawGizmosSelected()
    {
        Lab1 board = Object.FindAnyObjectByType<Lab1>();//get the board from class Lab1
        if (board == null) return;
        Gizmos.color = Color.yellow;
        int currentX = 0; //piece’s board position on x axis
        int currentZ = 0; //piece’s board position on z axis

        //Apply different rules for each chess piece type using a switch for organization
        switch (pieceType)
        {
            //Pawns in chess can generally move forward one space only
            case ChessPieceType.Pawn:
                int nextZ = currentZ + 1;
                if (nextZ < 8)
                {
                    Vector3 center = board.GetTileCenter(currentX, nextZ);
                    Gizmos.DrawCube(center + Vector3.up * 0.01f, new Vector3(board.squareSize, 0.01f, board.squareSize));//highlight cube
                }
                break;

            //General movement for the knight including backwards which isn't possible from current location.
            case ChessPieceType.Knight:
                Vector2Int[] knightMoves = new Vector2Int[]
            {
                new Vector2Int(1, 2),  // move right 1, forward 2
                new Vector2Int(2, 1),  // move right 2, forward 1
                new Vector2Int(2, -1), // move right 2, backward 1
                new Vector2Int(1, -2), // move right 1, backward 2
                new Vector2Int(-1, -2),// move left 1, backward 2
                new Vector2Int(-2, -1),// move left 2, backward 1
                new Vector2Int(-2, 1), // move left 2, forward 1
                new Vector2Int(-1, 2)  // move left 1, forward 2
            };
                //Loop through each of these possibilities
                foreach (Vector2Int move in knightMoves)
                {
                    int newX = currentX + move.x;//new position on horizontal axis
                    int newZ = currentZ + move.y;//new position forward/backwards

                    //Is the movement available on the board check
                    if (newX >= 0 && newX < 8 && newZ >= 0 && newZ < 8)
                    {
                        Vector3 center = board.GetTileCenter(newX, newZ);//convert from board coordinates to world
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));//highlight cube aka draw gizmo cubes
                    }
                }
                break;

            //Rooks moves horizontally and vertically across the board in any direction
            case ChessPieceType.Rook:
                for (int i = 1; i < 8; i++)
                {
                    //Rooks can iterate forwards directionally
                    if (currentZ + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX, currentZ + i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Rooks can iterate backwards directionally
                    if (currentZ - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX, currentZ - i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Rooks can iterate along squares to the right of current position
                    if (currentX + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX + i, currentZ);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Rooks can iterate along squares to the left of current position
                    if (currentX - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX - i, currentZ);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                }
                break;

            //Bishops can move diagonally across the chess board in any direction
            case ChessPieceType.Bishop:
                for (int i = 1; i < 8; i++)
                {
                    //forward-right iteration
                    if (currentX + i < 8 && currentZ + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX + i, currentZ + i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //forward-left iteration
                    if (currentX - i >= 0 && currentZ + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX - i, currentZ + i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //backward-right iteration
                    if (currentX + i < 8 && currentZ - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX + i, currentZ - i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //backward-left iteration
                    if (currentX - i >= 0 && currentZ - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX - i, currentZ - i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                }
                break;

            //Queens move in any direction as many spaces as they like; for gizmo visualization, rook & bishop rules can be combined
            //Note: Queens cannot move like the Knight
            case ChessPieceType.Queen:
                for (int i = 1; i < 8; i++)
                {
                    //---ROOK RULES---
                    //Forward movement
                    if (currentZ + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX, currentZ + i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Backward movement
                    if (currentZ - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX, currentZ - i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Right movement
                    if (currentX + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX + i, currentZ);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Left movement
                    if (currentX - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX - i, currentZ);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }

                    //---BISHOP RULES---
                    //Forward-right movement
                    if (currentX + i < 8 && currentZ + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX + i, currentZ + i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Forward-left movement
                    if (currentX - i >= 0 && currentZ + i < 8)
                    {
                        Vector3 center = board.GetTileCenter(currentX - i, currentZ + i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Backward-right movement
                    if (currentX + i < 8 && currentZ - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX + i, currentZ - i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                    //Backward-left movement
                    if (currentX - i >= 0 && currentZ - i >= 0)
                    {
                        Vector3 center = board.GetTileCenter(currentX - i, currentZ - i);
                        Gizmos.DrawCube(center + Vector3.up * 0.01f,
                            new Vector3(board.squareSize, 0.01f, board.squareSize));
                    }
                }
                break;

            //Kings can move one space in any direction
            case ChessPieceType.King:
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        //skips the square the king is currently on
                        if (i == 0 && j == 0) continue;

                        int newX = currentX + i;
                        int newZ = currentZ + j;

                        //checks if the possible move is on the board
                        if (newX >= 0 && newX < 8 && newZ >= 0 && newZ < 8)
                        {
                            Vector3 center = board.GetTileCenter(newX, newZ);
                            Gizmos.DrawCube(center + Vector3.up * 0.01f,
                                new Vector3(board.squareSize, 0.01f, board.squareSize));
                        }
                    }
                }
                break;

        }


    }
}
