using UnityEngine;


public class Lab1 : MonoBehaviour
{

    /*
     For this lab assignment, we will make a chess game interface that will not be functional.

    step1:
    This lab task takes place in the Editor; no need to run the app
    Note: attach code for gizmos onto empty game object
    Note: toggle to be able to see gizmos in window.
    Draw a chess play area (8x8) using Gizmos; use loops; be mindful of the index 0 starting point in an array

    step 2:
    Place one set of chess pieces on the play area (white)
    https://commons.wikimedia.org/wiki/Category:PNG_chess_pieces/Standard_transparentLinks to an external site.
    Using the inspector, we need to be able to change the piece type, color tint of the sprite
    Piece type changes the sprite and how the pieces can move
    When we select a piece in the scene, we want to see where the piece can move depending on the type of the piece using Gizmos

    step 3: (optional)
    Just as an example of a Handle, let’s also do the interactive border around the piece (but make one adjustment):
    https://docs.unity3d.com/ScriptReference/Editor.OnSceneGUI.htmlLinks to an external site.

    Notes:
    You do not need to do:
    Play chess
    Check if the move is valid
    Etc.*/

    public float squareSize = 1f; // size of each chess square
    public Vector3 origin = Vector3.zero; // bottom-left corner of the board

    //Draw the board checkered black & white
    //Draw outline for grid lines
    //Draw 8x8 board or grid
    private void OnDrawGizmos()
    {

        for (int x = 0; x <= 7; x++)
        {
            for (int y = 0; y <= 7; y++)
            {
                bool isWhite = (x + y) % 2 == 0;//bool to alternate colors
                if (isWhite)
                    Gizmos.color = Color.white;
                else
                    Gizmos.color = Color.black;

                //Draw squares from their centers
                Gizmos.DrawCube(GetTileCenter(x, y), new Vector3(squareSize, 0.01f, squareSize));

            }
        }
        Gizmos.color = Color.red;
        DrawGridLines();
    }

    //Function to compute the center of a tile
    public Vector3 GetTileCenter(int x, int y)
    {
        return origin + new Vector3((x + 0.5f) * squareSize, 0f, (y + 0.5f) * squareSize);

    }

    private void DrawGridLines()
    {
        //Draw vertical lines using x axis
        for (int x = 0; x <= 8; x++)
        {

            Vector3 start = origin + new Vector3(x * squareSize, 0f, 0f);
            Vector3 end = origin + new Vector3(x * squareSize, 0f, 8 * squareSize);
            Gizmos.DrawLine(start, end);
        }

        //Draw horizontal lines using y axis
        for (int y = 0; y <= 8; y++)
        {
            Vector3 start = origin + new Vector3(0f, 0f, y * squareSize);
            Vector3 end = origin + new Vector3(8 * squareSize, 0f, y * squareSize);
            Gizmos.DrawLine(start, end);
        }


    }


}


