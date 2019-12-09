using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceViewer : MonoBehaviour {
	public PuzzlePieceManager puzzlePieceManager;
	public Image image;
	public Sprite[] slots = new Sprite[3]; // As the user clicks on the piece, the images rotate in order.

	// TODO: Simply swapping out images is good for now. But at some point, it might be interesting to look into creating the illusion of rotating planes in a drum like a slot machine.

	// Use this for initialization
	void Start () {
		//image.sprite = slots[puzzlePieceManager.initialSlot];
	}
	
	// Update is called once per frame
	void Update () {
		//image.sprite = slots[puzzlePieceManager.currentSlot];
	}

	public void SetPiece() {
		//TODO: Disabling of the entire UI Image is just temporary so I can see my progress
		image.enabled = false;
	}
}
