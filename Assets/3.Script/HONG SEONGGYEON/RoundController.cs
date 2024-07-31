// RoundManager.cs
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public GameObject nextRound; // 다음 라운드 GameObject

    private void Update()
    {
        if (transform.childCount == 0)
        {
            OnAllMonstersDefeated();
        }
    }

    private void OnAllMonstersDefeated()
    {
        // 라운드의 모든 자식 개체가 파괴되었을 때 호출
        if (nextRound != null)
        {
            nextRound.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
