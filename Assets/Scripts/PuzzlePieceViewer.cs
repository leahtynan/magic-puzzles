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
		Debug.Log("Mouse enter");
		ToggleRotationUI(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Mouse exit");
		ToggleRotationUI(false);
	}

	public void Rotate() {
		Debug.Log ("Rotating to: " + puzzlePieceManager.angle);
		RectTransform rectTransform = art.GetComponent<RectTransform>();
		rectTransform.Rotate(new Vector3(0, 0, puzzlePieceManager.angle));
	}
		
	void ToggleRotationUI(bool isShowing) {
		rotateLeftButton.enabled = isShowing;
		rotateRightButton.enabled = isShowing;
	}

}
