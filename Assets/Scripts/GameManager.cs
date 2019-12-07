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
	string[] stephsSong = { "g", "bf", "d", "f", "g", "d", "ef", "c", "d", "f", "g", "g" }; // TODO: Store all songs somewhere else once they are created
	public Button playNoteTestButton;

	[Header("Puzzle Pieces")]
	public PuzzlePieceManager[] puzzlePieces;

	[Header("Animations")]
	public Image animation;

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

	void CheckPuzzleCompletion() {
		if (numberPiecesPlaced == 12) {
			Debug.Log ("Puzzle completed!");
			StartCoroutine(AnimateAndSing(1f));
			numberPiecesPlaced = 0;
		}
	}

	IEnumerator AnimateAndSing(float WaitTime) {
		Debug.Log ("animate and sing");
		animation.enabled = true;
		// Play song 3-5x
		// TODO: Idea. When playing the song, instead of playing each note audio file in sequence, record the entire song and have it saved as its own mp3. That way the pacing can stay true to the musician's intent and we avoid the sequence feeling choppy. Individual files work better for piece locking because it is not continuous.
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
		Debug.Log("Number pieces placed: " + numberPiecesPlaced);
		Debug.Log("The note playing is: " + stephsSong[numberPiecesPlaced - 1]);
		int noteToPlay = notesMapping[stephsSong[numberPiecesPlaced - 1]];
		Debug.Log(">>> Play audio clip #" + noteToPlay);
		audioSource.clip = notes[noteToPlay];
		audioSource.Play();
		if (numberPiecesPlaced == 12) {
			Debug.Log("<color=green>Puzzle was completed!</color>");
			playNoteTestButton.interactable = false;
		} 
	}
}
