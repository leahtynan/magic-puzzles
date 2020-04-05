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

	public void Rotate(string turnType) {
		// Pass in increments of positive or negative 90 degrees
		// This is not the angle of the piece, it is the vector value the piece should turn
		// See PuzzlePieceManager for value of the angle
		int angle = 90; // Left turn angle increment, default to overwrite if needed
		if (turnType == "right") { // Right turn angle increment
			angle = -90;
		} else if (turnType == "random") {
			// At start, use the value of the angle rather than turn increment when shuffling pieces
			angle = puzzlePieceManager.angle;
		}
		RectTransform rectTransform = art.GetComponent<RectTransform>();
		rectTransform.Rotate(new Vector3(0, 0, angle));
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
