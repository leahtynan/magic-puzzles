using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceViewer : MonoBehaviour {
	public PuzzlePieceManager puzzlePieceManager;
	public Image image;
	public Sprite[] slots; // As the user clicks on the piece, the images rotate in order

	// Use this for initialization
	void Start () {
		image.sprite = slots[puzzlePieceManager.initialSlot];
	}
	
	// Update is called once per frame
	void Update () {
		image.sprite = slots[puzzlePieceManager.currentSlot];
	}
}
