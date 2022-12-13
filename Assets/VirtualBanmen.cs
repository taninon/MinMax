using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirtualBanmen {

	List<VirtualCell> cells;

	VirtualBanmen() {
		cells = new List<VirtualCell>();
        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                var cell = new VirtualCell();
                cell.X = x;
                cell.Y = y;
            }
        }
    }


    private IEnumerable<VirtualCell> GetSideCell(int x) {
        return cells.Where(c => c.X == x);
    }

    private IEnumerable<VirtualCell> GetVerticalCell(int y)
    {
        return cells.Where(c => c.Y == y);
    }

    private int GetFirstMoveOnlyCount(IEnumerable<VirtualCell> target,bool firstMove) {
        return target.Any(c => c.State(!firstMove)) ? 0 : target.Where(c => c.State(firstMove)).Count(); ;
    }

    public int GetEvaluation(bool firstMove) {


        int evaluation = 0;
        for(int x = 0 ; x < 3; x++)
		{
            evaluation += GetFirstMoveOnlyCount(GetSideCell(x), firstMove);
            evaluation += GetFirstMoveOnlyCount(GetVerticalCell(x), firstMove);
        }






        return evaluation;
	}
}
