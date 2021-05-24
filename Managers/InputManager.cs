using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Manager{
    public class InputManager : Singleton<InputManager>
    {
        // ����Ű �������� �߻��ϴ� �̺�Ʈ
        public Vector2KeyEvent arrowKeyEvent = new Vector2KeyEvent();

        // ���콺 ���������� ������ �ִ� �̺�Ʈ
        public Vector2KeyEvent mousePosEvent = new Vector2KeyEvent();

        // Ű�� ������ �� �߻��ϴ� �̺�Ʈ
        [SerializeField]
        private List<KeyEvent> keyDownEvents = new List<KeyEvent>();
        // Ű�� ������ ���� �� �߻��ϴ� �̺�Ʈ
        [SerializeField]
        private List<KeyEvent> keyEvents = new List<KeyEvent>();
        // Ű�� ���� �� �߻��ϴ� �̺�Ʈ
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
                Debug.LogError("���� Ű ������ ����");
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
                Debug.LogError("���� Ű ������ ����");
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
                Debug.LogError("���� Ű ������ ����");
                return;
            }
            temp.keyEvent.RemoveListener(func);
        }

        // Ű���� �Է°��� �޴� �̺�Ʈ Ŭ����
        [System.Serializable]
        public class KeyEvent{
            public KeyCode keyCode;
            public UnityEvent keyEvent = new UnityEvent();
        }

        // ����Ű �̺�Ʈ�� �޴� Ŭ����
        [System.Serializable]
        public class Vector2KeyEvent : UnityEvent<Vector2> {}
    }
}
