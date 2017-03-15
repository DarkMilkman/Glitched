using UnityEngine;
using System.Collections;

public interface IDesState
{

	void UpdateState();

    void ToLightBulbAttackState();

    void ToDieAttackState();

    void ToCreateLevelAttackState();
	
	void ToFloatState();
}
