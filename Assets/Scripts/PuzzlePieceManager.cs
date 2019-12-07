using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceManager : MonoBehaviour {
	public GameManager gameManager;
	public bool isSet; // Whether or not the piece has been rotated to the correct slot
	public int initialSlot; // The image when the puzzle loads, never the correct image. These will be curated and set on the Inspector.
	public int currentSlot; // The image currently visible to the user
	public int correctSlot; // The image in the series of rotating slots that is the correct fit for the puzzle. This should also be set in the Inspector.

	// Use this for initialization
	void Start () {
		currentSlot = initialSlot;
	}
	
	// Update is called once per frame
	void Update () {
		InspectSlot();
	}

	public void RotateSlots() { // TODO: Associate this with click event
		if (currentSlot < 5) { // I am thinking 5 will be the number of slots, but might change depending how it feels in action
			currentSlot++;
		} else {
			currentSlot = 0;
		}
	}

	void InspectSlot() {
		if (currentSlot == correctSlot) {
			SetPiece();
		}
	}

	void SetPiece() {
		isSet = true; 
		gameManager.PlayNote();
		gameManager.numberPiecesPlaced++;
		Debug.Log("Piece was set");
		// TODO: Visually indicate the piece is locked in the Viewer
	}

}
