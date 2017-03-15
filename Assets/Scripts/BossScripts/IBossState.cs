using UnityEngine;
using System.Collections;

public interface IBossState
{

    void UpdateState();

    void ToJumpAttackState();

    void ToSpikesHorAttackState();

    void ToSpikesVertAttackState();
}
