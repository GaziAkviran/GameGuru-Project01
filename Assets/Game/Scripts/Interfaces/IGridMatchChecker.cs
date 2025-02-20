using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridMatchChecker
{
    void CheckAndClearMatches(int row, int col);
}
