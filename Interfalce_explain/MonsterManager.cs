using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
    public List<MonsterOrigin> mosnter;

    private void Start() {
        for (int i = 0; i < mosnter.Count; i++){
            mosnter[i].Attack();

            Mosnter _mon = mosnter[i] as Mosnter;
            if (_mon != null){
                _mon.Defence();
            }
        }
    }
}