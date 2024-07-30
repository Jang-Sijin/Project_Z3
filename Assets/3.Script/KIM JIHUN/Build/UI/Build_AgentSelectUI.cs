using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_AgentSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject _agentSelectObj;

    public void OpenAgentSelectUI()
    {
        _agentSelectObj.SetActive(true);
    }

    public void CloseAgentSelectUI()
    {
        _agentSelectObj.SetActive(false);
    }
}
