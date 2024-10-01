using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Banmen : MonoBehaviour
{
	[SerializeField] GameObject cellPrefab;

	enum Turn
	{
		Player,
		Ai
	}

	private Turn turn = Turn.Player;

	private bool firstMove = true;

	List<BanmenCell> allCell = new List<BanmenCell>();

	bool isGame;

	[SerializeField] GameObject winFirst;
	[SerializeField] GameObject winSecond;

	// Start is called before the first frame update
	void Start()
	{
		for (var x = 0; x < 3; x++)
		{
			for (var y = 0; y < 3; y++)
			{
				var cellGb = Instantiate(cellPrefab);
				cellGb.gameObject.transform.parent = this.transform;
				cellGb.gameObject.transform.localPosition = new Vector3(x, 0, y);

				var cell = cellGb.GetComponent<BanmenCell>();
				cell.Init(OnCellClick, x, y);
				allCell.Add(cell);
			}
		}



	}

	private bool isAlignSide()
	{
		var align = allCell.Where(c => c.X == 0).All(c => c.State(firstMove)) ||
			allCell.Where(c => c.X == 1).All(c => c.State(firstMove)) ||
			allCell.Where(c => c.X == 2).All(c => c.State(firstMove));
		return align;
	}

	private bool isAlignVertical()
	{
		return allCell.Where(c => c.Y == 0).All(c => c.State(firstMove)) ||
			allCell.Where(c => c.Y == 1).All(c => c.State(firstMove)) ||
			allCell.Where(c => c.Y == 2).All(c => c.State(firstMove));
	}

	private bool isDiagonal()
	{
		return (allCell.Find(c => c.X == 0 && c.Y == 0).State(firstMove) &&
			allCell.Find(c => c.X == 1 && c.Y == 1).State(firstMove) &&
			allCell.Find(c => c.X == 2 && c.Y == 2).State(firstMove)) ||
			(allCell.Find(c => c.X == 2 && c.Y == 0).State(firstMove) &&
			allCell.Find(c => c.X == 1 && c.Y == 1).State(firstMove) &&
			allCell.Find(c => c.X == 0 && c.Y == 2).State(firstMove));
	}

	private void SetTeban()
	{
		UnityEngine.Random.Range(0, 1);
	}

	private void OnCellClick(BanmenCell clickCell)
	{
		if (isGame)
		{
			return;
		}

		clickCell.SetMark(firstMove);

		if (isAlignSide() || isAlignVertical() || isDiagonal())
		{
			isGame = true;
			winFirst.SetActive(firstMove);
			winSecond.SetActive(!firstMove);
			Debug.Log(firstMove + "Win!");
		}

		TurnEnd();
	}

	private void TurnEnd()
	{

		turn = turn == Turn.Player ? Turn.Ai : Turn.Player;
		firstMove = !firstMove;
	}


}
