using UnityEngine;

public abstract class TamashiBaseState
{
  public abstract void EnterState(TamashiStateManager sm);
  public abstract void UpdateState(TamashiStateManager sm);
}