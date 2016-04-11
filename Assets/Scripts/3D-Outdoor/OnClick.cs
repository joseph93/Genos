using UnityEngine;
using System.Collections;

public class OnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        OnMouseEnter();
	}

    void OnMouseEnter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel("Menu");
        }
    }
}
