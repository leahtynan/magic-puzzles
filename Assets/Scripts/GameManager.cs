using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[Header("Musical Notes")]
	public AudioSource audioSource;
	public AudioClip[] notes = new AudioClip[kNumberPieces]; 
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
		// TODO: Start with loading just one puzzle. Once this is running smoothly, think through whether or not there should be a menu to choose puzzle before adding more puzzles.
	}
	
	// Update is called once per frame
	void Update () {
		CheckPuzzleCompletion();
	}

	public void PlayNote() {
		int noteToPlay = notesMapping[puzzles[0].songNotes[numberPiecesPlaced - 1]];
		audioSource.clip = notes[noteToPlay];
		audioSource.Play();
		if (numberPiecesPlaced == kNumberPieces) {
			playNoteTestButton.interactable = false;
		} 
	}

	void CheckPuzzleCompletion() {
		if (numberPiecesPlaced == kNumberPieces) {
			Debug.Log ("Puzzle completed!");
			StartCoroutine(AnimateAndSing(1f));
			numberPiecesPlaced = 0;
		}
	}

	// TODO: Right now, just working with our one puzzle StephPurpleWhale which is index 0.
	// Eventually, puzzle access will be dynamic (i.e. puzzles[currentPuzzle])

	IEnumerator AnimateAndSing(float WaitTime) {
		puzzles[0].animation.enabled = true;
		for (int i = 0; i < kNumberPieces; i++) {
			audioSource.clip = puzzles[0].recordedSong;
			audioSource.Play();
			yield return new WaitForSeconds(10f);
		}
		puzzles[0].hasBeenPlayed = true;
		// TODO: 
		// 1. Fade out the current animation
		// 2. Reset puzzle (all pieces should have isSet set to false, etc.)
		// 3. Select a new puzzle (random puzzle in set that hasn't been played yet)
		// 3. Fade in the new puzzle
	}

	IEnumerator PlayScale(float WaitTime) {
		// This isn't used for anything in the interactive. It was a quick test for musical note file access.
		for (int i = 0; i < kNumberPieces; i++) {
			audioSource.clip = notes[i];
			audioSource.Play();
			yield return new WaitForSeconds(WaitTime);
		}
	}
		
}
