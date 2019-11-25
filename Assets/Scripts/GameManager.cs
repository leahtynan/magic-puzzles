using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[Header("Musical Notes")]
	public int numberPiecesPlaced = 0; 
	public AudioSource audioSource;
	public AudioClip[] notes = new AudioClip[12]; 
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
	string[] sampleSong = { "a", "c", "d", "g", "fs", "c", "d", "e", "e", "a", "b", "bf" }; // TODO: Store all songs somewhere else once they are created
	public Button playNoteTestButton;

	[Header("Puzzle Pieces")]
	public PuzzlePieceManager[] puzzlePieces;

	// Use this for initialization
	void Start () {
		Debug.Log ("Welcome to Magic Puzzles");
		//StartCoroutine(PlayScale(0.5f));
		// TODO: Start with loading just one puzzle. Once this is running smoothly, think through whether or not there should be a menu to choose puzzle before adding more puzzles.
	}
	
	// Update is called once per frame
	void Update () {
		if (numberPiecesPlaced == 12) {
			Debug.Log ("Puzzle completed!");
			//AnimateAndSing();
		}
	}

	IEnumerator AnimateAndSing(float WaitTime) {
		// TODO: Animate
		// Play song 3-5x
		// Reset to a new puzzle
		yield return new WaitForSeconds(WaitTime);
	}

	IEnumerator PlayScale(float WaitTime) {
		for (int i = 0; i < 12; i++) {
			audioSource.clip = notes[i];
			audioSource.Play();
			yield return new WaitForSeconds(WaitTime);
		}
	}

	public void PlayNote() {
		numberPiecesPlaced++;
		Debug.Log("Number pieces placed: " + numberPiecesPlaced);
		Debug.Log("The note playing is: " + sampleSong[numberPiecesPlaced - 1]);
		int noteToPlay = notesMapping[sampleSong[numberPiecesPlaced - 1]];
		Debug.Log(">>> Play audio clip #" + noteToPlay);
		audioSource.clip = notes[noteToPlay];
		audioSource.Play();
		if (numberPiecesPlaced == 12) {
			Debug.Log("<color=green>Puzzle was completed!</color>");
			playNoteTestButton.interactable = false;
		} 
	}
}
