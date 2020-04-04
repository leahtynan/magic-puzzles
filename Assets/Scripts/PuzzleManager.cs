using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {
	public bool isLoaded;
	public GameObject puzzle; // Naming format: "ComposerColorAnimal"
	public string[] songNotes = new string[12];
	public AudioClip recordedSong;
	public PuzzlePieceManager[] puzzlePieces;
	public Image animation;
	public bool hasBeenPlayed; 
	// TODO: There might be a color variable so there could be logic in puzzle selection that prevents
	// picking a puzzle of the same color consecutively. Even if they were different shades of the same color
	// they could be labeled the same to keep colors well distributed.

	void Start() {
		Load();
	}

	void Update() {
		if (isLoaded) {
			foreach (PuzzlePieceManager piece in puzzlePieces) {
				piece.Inspect();
			}
		}
	}

	public void Load() {
		foreach (PuzzlePieceManager piece in puzzlePieces) {
			piece.RandomizeAngle();
		}
		isLoaded = true;
	}
}

