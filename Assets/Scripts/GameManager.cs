using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	[Header("Musical Notes")]
	public AudioSource audioSource; // Plays the audio
	public AudioClip[] lowOctave = new AudioClip[12]; 
	public AudioClip[] middleOctave = new AudioClip[12]; 
	public AudioClip[] highOctave = new AudioClip[12]; 
	private AudioClip[,] notes = new AudioClip[3, 12]; // Notes in all octaves, loaded at Start
	Dictionary <string, int> notesMapping = new Dictionary<string, int>()
	{
		{ "c", 0 },
		{ "cs", 1 },
		{ "d", 2 },
		{ "ef", 3 },
		{ "e", 4 },
		{ "f", 5 },
		{ "fs", 6 },
		{ "g", 7 },
		{ "af",8 },
		{ "a", 9 },
		{ "bf", 10 },
		{ "b", 11 }
	};
	public Button playNoteTestButton;

	[Header("Puzzles")]
	public PuzzleManager[] puzzles;
	public int puzzleNumber = 0; // Which puzzle the user is on
	private const int kNumberPieces = 12;
	public int numberPiecesPlaced = 0; 

	// Use this for initialization
	void Start () {
		Debug.Log ("Welcome to Magic Puzzles");
		//StartCoroutine(PlayScale(0.5f));
		LoadNotes();
		ResetPiecesStatus();
		HideInactivePuzzles();
	}

	void HideInactivePuzzles() {
		// Each puzzle (except for the first one) should have its pieces at alpha 0 
		// so they can be faded in as a transition when they are loaded later on
		// The entire puzzle game object must be de-activated, otherwise
		// mouse in/out events won't target the current puzzle if there are 
		// puzzles active (even if invisible via transparently) with a higher z-index
		for (int i = 1; i < puzzles.Length; i++) {
			puzzles[i].gameObject.SetActive(false);
			puzzles[i].animation.enabled = false;
			foreach (PuzzlePieceManager piece in puzzles[i].puzzlePieces) {
				Color temp = piece.viewer.art.color;
				temp.a = 0;
				piece.viewer.art.color = temp;
			}
		}
	}

	void ResetPiecesStatus() {
		// Set the pieces in every puzzle to not set
		foreach (PuzzleManager puzzle in puzzles) {
			for (int i = 0; i < kNumberPieces; i++) {
				puzzle.puzzlePieces[i].isSet = false;
			}
		}
	}

	void LoadNotes() {
		// Loads three octaves of musical notes into a 2D array
		for (int i = 0; i < lowOctave.Length; i++) {
			notes[0, i] = lowOctave[i];
		}
		for (int i = 0; i < middleOctave.Length; i++) {
			notes[1, i] = middleOctave[i];
		}
		for (int i = 0; i < highOctave.Length; i++) {
			notes[2, i] = highOctave[i];
		}
	}

	// Update is called once per frame
	void Update () {
		CheckPuzzleCompletion();
	}

	public void PlayNote() {
		string[] noteToPlay = MapNoteToPlay(puzzles[puzzleNumber].songNotes[numberPiecesPlaced - 1]);
		int octave = Convert.ToInt32(noteToPlay[0]);
		int note = Convert.ToInt32(notesMapping[noteToPlay[1]]);
		audioSource.clip = notes[octave, note];
		audioSource.Play();
		if (numberPiecesPlaced == kNumberPieces) {
			playNoteTestButton.interactable = false;
		} 
	}

	string[] MapNoteToPlay(string shortHand) {
		// Converts shorthand for note (e.g. 1-af, middle octave's A flat) into octave number and musical note
		string[] mapping = shortHand.Split('-');
		return mapping;
	}

	void CheckPuzzleCompletion() {
		if (numberPiecesPlaced == kNumberPieces) {
			Debug.Log ("Puzzle completed!");
			StartCoroutine(AnimateAndSing(1f));
			numberPiecesPlaced = 0;
		}
	}
		
	IEnumerator AnimateAndSing(float WaitTime) {
		// 1. Wait a moment for the last note of the song to play
		yield return new WaitForSeconds(1f);
		// 2. Show the animation and hide the pieces underneath
		puzzles[puzzleNumber].animation.enabled = true;
		foreach (PuzzlePieceManager piece in puzzles[puzzleNumber].puzzlePieces) {
			piece.viewer.ChangeOpacity("hidden");
		}
		// 3. Play the song twice
		for (int i = 0; i < 2; i++) {
			audioSource.clip = puzzles[puzzleNumber].recordedSong;
			audioSource.Play();
			yield return new WaitForSeconds(audioSource.clip.length);
		}
		puzzles[puzzleNumber].hasBeenPlayed = true;
		// 4. Transition to the next puzzle
		StartCoroutine(TransitionPuzzles(4f));
	}
		
	IEnumerator TransitionPuzzles(float WaitTime) {
		Debug.Log ("Fading out animation");
		for(int i = 0; i < 80; i++) {
			Debug.Log ("Puzzle number: " + puzzleNumber);
			Color temp = puzzles[puzzleNumber].animation.color;
			temp.a -= 0.05f;
			puzzles[puzzleNumber].animation.color = temp;
			yield return new WaitForSeconds(0.05f);
		}
		puzzles[puzzleNumber].gameObject.SetActive(false);
		puzzleNumber++;
		Debug.Log ("Puzzle number: " + puzzleNumber);
		StartCoroutine(SetupNewPuzzle(4f));
	}

	IEnumerator SetupNewPuzzle(float WaitTime) {
		Debug.Log ("Showing new puzzle");
		// 1. Activate the puzzle game object
		puzzles[puzzleNumber].gameObject.SetActive(true);
		// 2. Fade in each puzzle piece
		for(int i = 0; i < 80; i++) {
			foreach (PuzzlePieceManager piece in puzzles[puzzleNumber].puzzlePieces) {
				Color temp = piece.viewer.art.color;
				temp.a += 0.05f;
				piece.viewer.art.color = temp;
			}
			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator PlayScale(float WaitTime) {
		// This isn't used for anything in the interactive. It was a quick test for musical note file access.
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 12; j++) {
				audioSource.clip = notes[i, j];
				audioSource.Play();
				yield return new WaitForSeconds(WaitTime);
			}
		}
	}
		
}
