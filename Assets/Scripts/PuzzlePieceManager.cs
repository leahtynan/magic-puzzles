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
		StartCoroutine(viewer.Rotate("random", 1f));
	}

	public void RotateRight() {
		if (!isSet) {
			StartCoroutine(viewer.Rotate("right", 1f));
			angle -= 90;
			Inspect();
		}
	}

	public void RotateLeft() {
		if (!isSet) {
			StartCoroutine(viewer.Rotate("left", 1f));
			angle += 90;
			Inspect();
		}
	}

	public void Inspect() {
		if (angle%360 == 0 && !isSet) {
			SetPiece();
		}
	}
		
	void SetPiece() {
		Debug.Log("Setting the piece");
		gameManager.numberPiecesPlaced++;
		gameManager.PlayNote();
		isSet = true; 
		viewer.ToggleRotationUI(false);
	}

}
