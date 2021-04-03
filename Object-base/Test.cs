using UnityEngine;

public class Test : MonoBehaviour {
    private void Start() {
        for (int i = 0; i < 10; i++){
            Random.seed = -574716537;
            Debug.Log("range : " + Random.Range(0, 10));
            Debug.Log("seed : " + Random.seed);
        }
    }
}