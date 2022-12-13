using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanmenCell : MonoBehaviour
{
    [SerializeField] GameObject maru;
    [SerializeField] GameObject batu;

    private System.Action<BanmenCell> OnClick;

    private int x;
    private int y;
    public int X { get { return x; } }
    public int Y { get { return y; } }

    public bool State(bool firstMove) {
        return firstMove ? IsMaru : IsBatu;
    }

    public bool IsMaru {
        get {
            return maru.activeInHierarchy;
        }
    }

    public bool IsBatu
    {
        get
        {
            return batu.activeInHierarchy;
        }
    }


    private void OnMouseDown()
	{
        if (maru.activeInHierarchy || batu.activeInHierarchy)
        {
            return;
        }

        if (this.OnClick != null) {
            OnClick(this);
        }
	}

    public void Init(System.Action<BanmenCell> onClick,int x,int y)
    {
        this.x = x;
        this.y = y;

        this.OnClick += onClick;
    }


    public void SetMark(bool firstMove) {
        maru.SetActive(firstMove);
        batu.SetActive(!firstMove);
    }


}
