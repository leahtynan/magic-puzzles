﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceManager : MonoBehaviour {
	public GameManager gameManager;
	public PuzzlePieceViewer viewer;
	public bool isSet; // Whether or not the piece has been rotated to the correct slot
	public int initialSlot; // The image when the puzzle loads, never the correct image. These will be curated and set on the Inspector.
	public int currentSlot; // The image currently visible to the user. Note: Value here on the Inspector is meaningless, whereas initialSlot and correctSlot assignments are. This is public so the viewer can access it.
	public int correctSlot; // The image in the series of rotating slots that is the correct fit for the puzzle. This should also be set in the Inspector.

	// Use this for initialization
	void Start () {
		currentSlot = initialSlot;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void RotateSlots() { 
		Debug.Log("Current slot is: " + currentSlot);
		Debug.Log("Rotating slot...");
		if (!isSet) {
			if (currentSlot < 2) { 
				currentSlot++;
			} else {
				currentSlot = 0;
			}
			InspectSlot();
		}
	}

	void InspectSlot() {
		if (currentSlot == correctSlot) {
			SetPiece();
		}
	}

	void SetPiece() {
		isSet = true; 
		gameManager.numberPiecesPlaced++;
		gameManager.PlayNote();
		// viewer.SetPiece(); TODO: In the future, may add a UI update to indicate the piece has been locked
		Debug.Log(">>> Piece was set");
	}

}
