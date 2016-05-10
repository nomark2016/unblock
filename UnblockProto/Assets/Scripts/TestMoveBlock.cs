using UnityEngine;
using System.Collections;

public class TestMoveBlock : MonoBehaviour {

    public bool bPick = false;
    public Vector3 _MousePickPos;
    public _BLOCK_TYPE _type;

    [System.NonSerialized]
    protected UIRoot mRoot;
    [System.NonSerialized]
    protected bool mDragging = false;

    bool bCollison = false;
    public float MoveSpeed = 2;
    public GameManager _Manager;

	// Use this for initialization
	void Start () {

        mRoot = NGUITools.FindInParents<UIRoot>(transform);
        Debug.Log(mRoot.name);
        Invoke("ColliderSizeSmall", 0.2f);

        _Manager = GetComponentInParent<GameManager>();
	}

    void ColliderSizeSmall()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.size = col.size * 0.95f;
    }
	
	// Update is called once per frame
	void Update () {
        if(!bPick)
            CheckPosition();
	
	}

    protected virtual void OnDrag(Vector2 delta)
    {
        if (!bPick) return;
        if (mRoot != null) OnDragDropMove(delta * mRoot.pixelSizeAdjustment * 1.78f);
        else OnDragDropMove(delta);
    }

    void OnDragDropMove(Vector3 delta)
    {
        Vector3 old_Pos = transform.localPosition;

        Vector3 Ret_Pos = transform.localPosition + Vector3.Normalize(delta) * Time.deltaTime * MoveSpeed;



        if(_type == _BLOCK_TYPE._VERTICAL)
            transform.localPosition = new Vector3(old_Pos.x,Ret_Pos.y,0);
        else if (_type == _BLOCK_TYPE._HORIZONTAL)
            transform.localPosition = new Vector3(Ret_Pos.x, old_Pos.y, 0);
        else if (_type == _BLOCK_TYPE.BOTH)
            transform.localPosition = new Vector3(Ret_Pos.x, Ret_Pos.y, 0);

        

    }

    void CheckPosition()
    {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        int _MAX_X = ClassDefine._SIZE_X_LIST.Length;
        int _MAX_Y = ClassDefine._SIZE_Y_LIST.Length;

        if (x < ClassDefine._SIZE_X_LIST[0])
            x = ClassDefine._SIZE_X_LIST[0];
        if (x > ClassDefine._SIZE_X_LIST[_MAX_X-1])
            x = ClassDefine._SIZE_X_LIST[_MAX_X-1];
        if (y > ClassDefine._SIZE_Y_LIST[0])
            y = ClassDefine._SIZE_Y_LIST[0];
        if (y < ClassDefine._SIZE_Y_LIST[_MAX_Y - 1])
            y = ClassDefine._SIZE_Y_LIST[_MAX_Y - 1];

        for (int i = 0; i < _MAX_X; i++)
        {
            float m = Mathf.Abs(x - ClassDefine._SIZE_X_LIST[i]);
            if(m < ClassDefine._SIZE_W)
            {
                if(m < ClassDefine._SIZE_W/2)
                {
                    x = ClassDefine._SIZE_X_LIST[i];
                }
                else
                {
                    if(i+1 < ClassDefine._SIZE_X_LIST.Length)
                        x = ClassDefine._SIZE_X_LIST[i+1];
                    else
                        x = ClassDefine._SIZE_X_LIST[i];
                }
                break;
            }
        }

        for (int i = 0; i < _MAX_Y; i++)
        {
            float m = Mathf.Abs(y - ClassDefine._SIZE_Y_LIST[i]);
            if (m < ClassDefine._SIZE_H)
            {
                if (m < ClassDefine._SIZE_H / 2)
                {
                    y = ClassDefine._SIZE_Y_LIST[i];
                }
                else
                {
                    if (i + 1 < ClassDefine._SIZE_Y_LIST.Length)
                        y = ClassDefine._SIZE_Y_LIST[i + 1];
                    else
                        y = ClassDefine._SIZE_Y_LIST[i];
                }

                break;
            }
        }

        transform.localPosition = new Vector3(x, y, 0);
    }

    public void SetPick()
    {
        
    }

    protected virtual void OnDragStart ()
    {
        Debug.Log("Drag!!");

        bPick = true;
        mDragging = true;
        _MousePickPos = Input.mousePosition - transform.localPosition;
    }

    protected virtual void OnDragEnd()
    {
        Debug.Log("Drag Stop!!");

        bPick = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("BLOCK"))
        {
            bCollison = true;
            bPick = false;
        }
        //Debug.Log(other.name + " : " + Time.time);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("BLOCK") || other.tag.Equals("WALL") || other.tag.Equals("Player"))
        {
            bCollison = true;
            bPick = false;
        }

        if(other.tag.Equals("GOAL"))
        {
            if (gameObject.tag.Equals("Player"))
            {
                _Manager.bGameOver = true;
                Invoke("GameEnd", 1);
            }
            bCollison = true;
            bPick = false;

            
        }
        //Debug.Log(other.name + " : " + Time.time);
    }

    void GameEnd()
    {
        Application.LoadLevel(0);
    }

}
