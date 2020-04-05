using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzlePieceViewer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public PuzzlePieceManager puzzlePieceManager;
	public Image art;
	public Image rotateRightButton;
	public Image rotateLeftButton;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!puzzlePieceManager.isSet) {
			ToggleRotationUI(true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ToggleRotationUI(false);
	}

	public IEnumerator Rotate(string turnType, float WaitTime) {
		RectTransform rectTransform = art.GetComponent<RectTransform>();
		// This method can branch into processes
		// a. Rotate a piece left or right as controled by the user
		// This produces an animation so they can see the turn
		if (turnType != "random") {
			// The piece turns 18 degrees at a time:
			// i. positive if left turn 
			// ii. negative if right turn
			// This is 90 degrees (total change in angle) divided by 5, 
			// where 5 is the amount of 0.1s increments in animation (total of 0.5)
			int increment = 18; 
			if (turnType == "right") { // Right turn angle increment
				increment = -18;
			} 
			for (int i = 0; i < 5; i++) {
				rectTransform.Rotate (new Vector3 (0, 0, increment));
				yield return new WaitForSeconds(0.1f);
			}
			if (puzzlePieceManager.isSet) {
				ChangeOpacity("dim");
			}
		// b. Rotate the piece a random angle at start 
		// TODO: May need to reset all pieces to angle 0  before doing this
		// When we have multiple puzzles solved in sequence
		} else {
			rectTransform.Rotate(new Vector3(0, 0, puzzlePieceManager.angle));
		}
	}
		
	public void ToggleRotationUI(bool isShowing) {
		rotateLeftButton.enabled = isShowing;
		rotateRightButton.enabled = isShowing;
	}

	public void ChangeOpacity(string state) {
		if (state == "dim") {
			Color temp = art.color;
			temp.a = 0.5f;
			art.color = temp;
		} else if (state == "hidden") {
			art.enabled = false;
		}
	}

}
