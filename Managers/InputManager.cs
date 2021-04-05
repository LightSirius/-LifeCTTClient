using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Manager{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField]
        private List<KeyEvent> keyDownEvents = new List<KeyEvent>();
        [SerializeField]
        private List<KeyEvent> keyEvents = new List<KeyEvent>();
        [SerializeField]
        private List<KeyEvent> keyUpEvents = new List<KeyEvent>();

        private bool isHolding = false;

        private void Update() {
            if (Input.anyKeyDown){                                      // Ű���尡 ���ȴ��� Ȯ���Ѵ�.
                for (int i = 0; i < keyDownEvents.Count; i++){          // Ű �ٿ��̺�Ʈ�� ���鼭 � Ű�� ���ȴ��� Ȯ��
                    if (Input.GetKeyDown(keyDownEvents[i].keyCode)){    // Ư��Ű �Է��� �߰ߵǸ� �̺�Ʈ�� �����Ѵ�.
                        keyDownEvents[i].keyEvent.Invoke();         
                        break;                                          // Ű�� ������ �������� �������� �ʴ´�. �̰� �ٸ��� ó���� �������
                    }
                }

            }

            if (Input.anyKey){
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

        public void TestDown(int value){
            Debug.Log("TestDown");
        }

        public void TestUp(){
            Debug.Log("TestUp");
        }

        public void Test(){
            Debug.Log("Test");
        }

        [System.Serializable]
        public class KeyEvent{
            public KeyCode keyCode;
            public UnityEvent keyEvent;
        }
    }
}
