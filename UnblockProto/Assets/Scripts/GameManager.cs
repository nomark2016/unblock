using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public bool bGameOver = false;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 60), ""+Input.mousePosition);

        if(bGameOver)
        {
            GUI.Label(new Rect(200, 0, 200, 60), "CLEAR");



        }
    }
}
