using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Manager{
    public class InputManager : Singleton<InputManager>
    {
        public ArrowKeyEvent arrowKeyEvent = new ArrowKeyEvent();
        [SerializeField]
        private List<KeyEvent> keyDownEvents = new List<KeyEvent>();
        [SerializeField]
        private List<KeyEvent> keyEvents = new List<KeyEvent>();
        [SerializeField]
        private List<KeyEvent> keyUpEvents = new List<KeyEvent>();

        private bool isHolding = false;

        private void OnDestroy() {
            keyEvents.Clear();
            keyDownEvents.Clear();
            keyUpEvents.Clear();
            
            arrowKeyEvent.RemoveAllListeners();
        }

        private void Update() {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            arrowKeyEvent.Invoke(new Vector2(h, v));

            if (Input.anyKeyDown){                                      // 키보드가 눌렸는지 확인한다.
                for (int i = 0; i < keyDownEvents.Count; i++){          // 키 다운이벤트를 돌면서 어떤 키가 눌렸는지 확인
                    if (Input.GetKeyDown(keyDownEvents[i].keyCode)){    // 특정키 입력이 발견되면 이벤트를 실행한다.
                        keyDownEvents[i].keyEvent.Invoke();         
                        break;                                          // 키가 동시의 눌릴경우는 상정하지 않는다. 이건 다르게 처리를 해줘야함
                    }
                }

            }

            if (Input.anyKey){
                // 키보드 방향키 입력

                isHolding = true;                               // keyUp이벤트를 위한 변수
                for (int i = 0; i < keyEvents.Count; i++){
                    if (Input.GetKey(keyEvents[i].keyCode)){
                        keyEvents[i].keyEvent.Invoke();

                        // 다른 키가 동시에 눌렸을 경우도 있으므로 break를 걸지 않음
                    }
                }
            }

            if (!Input.anyKey && isHolding){
                isHolding = false;
                for (int i = 0; i < keyUpEvents.Count; i++){
                    if (Input.GetKeyUp(keyUpEvents[i].keyCode)){
                        keyUpEvents[i].keyEvent.Invoke();
                    }
                }
            }
        }

        public void AddKeyDownListenr(KeyCode keycode, UnityAction func){
            KeyEvent temp = keyDownEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
            if (temp == null){
                temp = new KeyEvent();
                temp.keyCode = keycode;
                keyDownEvents.Add(temp);
            }

            temp.keyEvent.AddListener(func);
        }

        public void RemoveKeyDownListenr(KeyCode keycode, UnityAction func){
            KeyEvent temp = keyDownEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
            if (temp == null){
                Debug.LogError("현재 키 미지정 상태");
                return;
            }
            temp.keyEvent.RemoveListener(func);
        }

        public void AddKeyListenr(KeyCode keycode, UnityAction func){
            KeyEvent temp = keyEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
            if (temp == null){
                temp = new KeyEvent();
                temp.keyCode = keycode;
                keyEvents.Add(temp);
            }
            temp.keyEvent.AddListener(func);
        }

        public void RemoveKeyListenr(KeyCode keycode, UnityAction func){
            KeyEvent temp = keyEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
            if (temp == null){
                Debug.LogError("현재 키 미지정 상태");
                return;
            }
            temp.keyEvent.RemoveListener(func);
        }

        public void AddKeyUpListenr(KeyCode keycode, UnityAction func){
            KeyEvent temp = keyUpEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
            if (temp == null){
                temp = new KeyEvent();
                temp.keyCode = keycode;
                keyUpEvents.Add(temp);
            }
            temp.keyEvent.AddListener(func);
        }

        public void RemoveKeyUpListenr(KeyCode keycode, UnityAction func){
            KeyEvent temp = keyUpEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
            if (temp == null){
                Debug.LogError("현재 키 미지정 상태");
                return;
            }
            temp.keyEvent.RemoveListener(func);
        }

        // 키보드 입력값을 받는 이벤트 클래스
        [System.Serializable]
        public class KeyEvent{
            public KeyCode keyCode;
            public UnityEvent keyEvent = new UnityEvent();
        }

        // 방향키 이벤트를 받는 클래스
        [System.Serializable]
        public class ArrowKeyEvent : UnityEvent<Vector2> {}
    }
}
