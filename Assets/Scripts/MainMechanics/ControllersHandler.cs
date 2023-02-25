using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersHandler : MonoBehaviour
{
    [SerializeField] private CollectorController[] _collectorControllersPlayer0;
    [SerializeField] private CollectorController[] collectorControllersPlayer1;

    public void SetControllersAnimator(Animator animator)
    {
        for (int i = 0; i < _collectorControllersPlayer0.Length; i++)
        {
          //  _collectorControllersPlayer0[i].SetAnimator(animator);
        }
    }
}
