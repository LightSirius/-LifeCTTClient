using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Manager{
    public class InputManager : MonoBehaviour
    {
        // ����Ű �������� �߻��ϴ� �̺�Ʈ
        public static KeyEvent_Vector2 arrowKeyEvent = new KeyEvent_Vector2();

        // ���콺 ���������� ������ �ִ� �̺�Ʈ
        public static KeyEvent_Vector2 mousePosEvent = new KeyEvent_Vector2();

        // Ű�� ������ �� �߻��ϴ� �̺�Ʈ
        public  static KeyEvents keyDownEvents = new KeyEvents();
        // Ű�� ������ ���� �� �߻��ϴ� �̺�Ʈ
        public static KeyEvents keyEvents = new KeyEvents();
        // Ű�� ���� �� �߻��ϴ� �̺�Ʈ
        public static KeyEvents keyUpEvents = new KeyEvents();

        private static bool isHolding = false;

        private void OnDestroy() {
            keyEvents.Clear();
            keyDownEvents.Clear();
            keyUpEvents.Clear();
            
            arrowKeyEvent.RemoveAllListeners();
        }

        private void Update() {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            mousePosEvent.Invoke(new Vector2(x, y));

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            arrowKeyEvent.Invoke(new Vector2(h, v));

            if (Input.anyKeyDown){                                      // Ű���尡 ���ȴ��� Ȯ���Ѵ�.
                for (int i = 0; i < keyDownEvents.Count; i++){          // Ű �ٿ��̺�Ʈ�� ���鼭 � Ű�� ���ȴ��� Ȯ��
                    if (Input.GetKeyDown(keyDownEvents[i].keyCode)){    // Ư��Ű �Է��� �߰ߵǸ� �̺�Ʈ�� �����Ѵ�.
                        keyDownEvents[i].keyEvent.Invoke();         
                        break;                                          // Ű�� ������ �������� �������� �ʴ´�. �̰� �ٸ��� ó���� �������
                    }
                }

            }

            if (Input.anyKey){
                // Ű���� ����Ű �Է�

                isHolding = true;                               // keyUp�̺�Ʈ�� ���� ����
                for (int i = 0; i < keyEvents.Count; i++){
                    if (Input.GetKey(keyEvents[i].keyCode)){
                        keyEvents[i].keyEvent.Invoke();

                        // �ٸ� Ű�� ���ÿ� ������ ��쵵 �����Ƿ� break�� ���� ����
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

        // public static void AddKeyDownListenr(KeyCode keycode, UnityAction func){
        //     KeyEvent temp = keyDownEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
        //     if (temp == null){
        //         temp = new KeyEvent();
        //         temp.keyCode = keycode;
        //         keyDownEvents.Add(temp);
        //     }

        //     temp.keyEvent.AddListener(func);
        // }

        // public static void RemoveKeyDownListenr(KeyCode keycode, UnityAction func){
        //     KeyEvent temp = keyDownEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
        //     if (temp == null){
        //         Debug.LogError("���� Ű ������ ����");
        //         return;
        //     }
        //     temp.keyEvent.RemoveListener(func);
        // }

        // public static void AddKeyListenr(KeyCode keycode, UnityAction func){
        //     KeyEvent temp = keyEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
        //     if (temp == null){
        //         temp = new KeyEvent();
        //         temp.keyCode = keycode;
        //         keyEvents.Add(temp);
        //     }
        //     temp.keyEvent.AddListener(func);
        // }

        // public static void RemoveKeyListenr(KeyCode keycode, UnityAction func){
        //     KeyEvent temp = keyEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
        //     if (temp == null){
        //         Debug.LogError("���� Ű ������ ����");
        //         return;
        //     }
        //     temp.keyEvent.RemoveListener(func);
        // }

        // public static void AddKeyUpListenr(KeyCode keycode, UnityAction func){
        //     KeyEvent temp = keyUpEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
        //     if (temp == null){
        //         temp = new KeyEvent();
        //         temp.keyCode = keycode;
        //         keyUpEvents.Add(temp);
        //     }
        //     temp.keyEvent.AddListener(func);
        // }

        // public static void RemoveKeyUpListenr(KeyCode keycode, UnityAction func){
        //     KeyEvent temp = keyUpEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
        //     if (temp == null){
        //         Debug.LogError("���� Ű ������ ����");
        //         return;
        //     }
        //     temp.keyEvent.RemoveListener(func);
        // }

        public class KeyEvents{
            public List<KeyEvent> keyEvents = new List<KeyEvent>();
            public int Count => keyEvents.Count;
            public KeyEvent this[int i]{
                get{ return keyEvents[i]; }
            }

            public void AddListener(KeyCode keycode, UnityAction func){
                KeyEvent temp = keyEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
                if (temp == null){
                    temp = new KeyEvent();
                    temp.keyCode = keycode;
                    keyEvents.Add(temp);
                }
                temp.keyEvent.AddListener(func);
            }

            public void RemoveListenr(KeyCode keycode, UnityAction func){
                KeyEvent temp = keyEvents.Find((KeyEvent keyEvent) => { return keyEvent.keyCode == keycode; });
                if (temp == null){
                    Debug.LogError("���� Ű ������ ����");
                    return;
                }
                temp.keyEvent.RemoveListener(func);
            }

            public void Clear() => keyEvents.Clear();
        }

        // Ű���� �Է°��� �޴� �̺�Ʈ Ŭ����
        public class KeyEvent{
            public KeyCode keyCode;
            public UnityEvent keyEvent = new UnityEvent();
        }

        // ����Ű �̺�Ʈ�� �޴� Ŭ����
        [System.Serializable]
        public class KeyEvent_Vector2 : UnityEvent<Vector2> {}
    }
}
