using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    void InitAI(PathFinding pathFinding, GenCube startingCube);
    void HandleOnPlayerMove(GenCube targetCube);
    void UpdateAI();
}
