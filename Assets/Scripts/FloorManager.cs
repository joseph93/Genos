using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {
	/*
	static FloorManager Instance;
	// Use this for initialization
	void Start () {

		if(Instance != null)
		{
			//if instance already exists then destroy it because we do not want to duplicate GameManager object
			GameObject.Destroy(gameObject);
		}
		else
		{
			//if instance does not exist then create it
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this; //set current Instance equal to this
		}
	
	}


	// Update is called once per frame
	void Update () {
	
		//switch to first scene
		if(Input.GetKeyUp(KeyCode.Keypad1))
		{
			Application.LoadLevel("Floor2");
		}
		
		//switch to second scene
		if(Input.GetKeyUp(KeyCode.Keypad2))
		{
			Application.LoadLevel("Floor3");
		}
	}
	*/


	public void loadFloor2()
	{
		Application.LoadLevel ("Floor2");
	}

	public void loadFloor3()
	{
		Application.LoadLevel ("Floor3");
	}

}
