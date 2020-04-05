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
	private int currentPuzzle; // TODO: Choose a random puzzle at Start and set value for this int
	public List<int> availablePuzzles; // TODO: Load available puzzles on Start, remove puzzles as they are played
	private const int kNumberPieces = 12;
	public int numberPiecesPlaced = 0; 


	// Use this for initialization
	void Start () {
		Debug.Log ("Welcome to Magic Puzzles");
		//StartCoroutine(PlayScale(0.5f));
		LoadNotes();
		// TODO: Start with loading just one puzzle. Once this is running smoothly, think through whether or not there should be a menu to choose puzzle before adding more puzzles.
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
		string[] noteToPlay = MapNoteToPlay(puzzles[0].songNotes[numberPiecesPlaced - 1]);
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
		// 1. Show the animation and hide the pieces underneath
		puzzles[0].animation.enabled = true;
		foreach (PuzzlePieceManager piece in puzzles[0].puzzlePieces) {
			piece.viewer.ChangeOpacity("hidden");
		}
		// 2. Play the song twice
		for (int i = 0; i < 2; i++) {
			audioSource.clip = puzzles[0].recordedSong;
			audioSource.Play();
			yield return new WaitForSeconds(audioSource.clip.length);
		}
		puzzles[0].hasBeenPlayed = true;
		// TODO: 
		// 3. Fade out the current animation
		StartCoroutine(FadeOutAnimation(4f));
		// 4. Reset puzzle (all pieces should have isSet set to false, etc.)
		// 5. Select a new puzzle (random puzzle in set that hasn't been played yet)
		// 6. Fade in the new puzzle pieces
		// 7. Hide the new puzzle's animation
	}

	IEnumerator FadeOutAnimation(float WaitTime) {
		Debug.Log ("Fading out animation");
		for(int i = 0; i < 40; i++) {
			Color temp = puzzles[0].animation.color;
			temp.a -= 0.1f;
			puzzles[0].animation.color = temp;
			yield return new WaitForSeconds(0.1f);
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
