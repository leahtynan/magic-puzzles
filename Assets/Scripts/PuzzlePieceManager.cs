using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceManager : MonoBehaviour {
	public GameManager gameManager;
	public PuzzlePieceViewer viewer;
	private int[] wrongAngles = { 90, 180, 270 };
	public int angle; // The angle the puzzle piece is turned; 0 is the correct position
	public bool isSet; // Whether or not the piece has been rotated to the correct slot

	public void RandomizeAngle() {
		// Set the puzzle piece to any wrong angle 
		int angleToRotate = Random.Range(0, wrongAngles.Length);
		angle = wrongAngles[angleToRotate];
		viewer.Rotate("random");
	}

	public void RotateRight() {
		if (!isSet) {
			viewer.Rotate("right");
			angle -= 90;
			Inspect();
		}
	}

	public void RotateLeft() {
		if (!isSet) {
			viewer.Rotate("left");
			angle += 90;
			Inspect();
		}
	}

	public void Inspect() {
		if (angle%360 == 0 && !isSet) {
			Debug.Log ("Piece has been placed correctly");
			SetPiece();
		}
	}

	void SetPiece() {
		isSet = true; 
		gameManager.numberPiecesPlaced++;
		gameManager.PlayNote();
		// TODO: In the future, may add a UI update to indicate the piece has been locked
		Debug.Log(">>> Piece was set");
	}

}
