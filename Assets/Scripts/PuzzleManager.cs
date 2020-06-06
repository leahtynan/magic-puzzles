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
	public Color color;

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

