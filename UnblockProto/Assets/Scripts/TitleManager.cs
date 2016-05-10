using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

    int Stage = 0;
    public UIButton _Left;
    public UIButton _Right;
    public UILabel _StageLabel;
    public int _MAX_STAGE = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        _StageLabel.text = string.Format("STAGE {0}", Stage + 1);
	}

    public void Stage_Left()
    {
        Stage--;
        if (Stage < 0)
            Stage = _MAX_STAGE-1;
    }
    public void Stage_Right()
    {
        Stage++;
        if (Stage >= _MAX_STAGE)
            Stage = 0;
    }

    public void Stage_Start()
    {
        Application.LoadLevel(Stage+1);
    }
}
