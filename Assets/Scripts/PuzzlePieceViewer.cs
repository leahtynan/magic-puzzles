using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceViewer : MonoBehaviour {
	public PuzzlePieceManager puzzlePieceManager;
	public Image image;

	public void Rotate() {
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.Rotate(new Vector3(0, 0, puzzlePieceManager.angle));
	}

}
