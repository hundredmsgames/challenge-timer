using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour {
   public GameObject canvas;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
	}

    private void OnDestroy()
    {
        canvas.SetActive(true);
    }
}
