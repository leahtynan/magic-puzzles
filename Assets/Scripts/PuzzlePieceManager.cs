using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceManager : MonoBehaviour {
	public GameManager gameManager;
	public PuzzlePieceViewer viewer;
	private int[] wrongAngles = { 90, 180, 270 };
	public int angle; // Angle 0 means the piece is in the correct position
	public bool isSet; // Whether or not the piece has been rotated to the correct slot

	public void RandomizeAngle() {
		// Set the puzzle piece to any wrong angle 
		int angleToRotate = Random.Range(0, wrongAngles.Length);
		angle = wrongAngles[angleToRotate];
		viewer.Rotate();
	}

	public void RotateRight() {
		angle += 90;
		viewer.Rotate();
	}

	public void RotateLeft() {
		angle -= 90;
		viewer.Rotate();
	}

	public void Inspect() {
		if (angle == 0 && !isSet) {
			SetPiece();
		}
	}

	void SetPiece() {
		isSet = true; 
		gameManager.numberPiecesPlaced++;
		gameManager.PlayNote();
		viewer.Rotate();
		// TODO: In the future, may add a UI update to indicate the piece has been locked
		Debug.Log(">>> Piece was set");
	}

}
