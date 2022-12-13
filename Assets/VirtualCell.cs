using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCell
{
	public int X;
	public int Y;

	public bool maru;
	public bool batu;

	public bool State(bool firstMove)
	{
		return firstMove ? maru : batu;
	}

	public bool IsEnpty {
		get { 
			return !maru && !batu;
		}
	}
}
