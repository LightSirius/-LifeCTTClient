using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
   protected static T instance;
 
   //Returns the instance of this singleton.
   public static T Instance
   {
      get
      {
         if(instance == null)
         {
            instance = (T) FindObjectOfType(typeof(T));
 
            if (instance == null)
            {
               Debug.LogError("An instance of " + typeof(T) + 
                  " is needed in the scene, but there is none.");
            }
         }
 
         return instance;
      }
   }
}
// [출처] [Tip] 유니티 작업을 위한 50가지 팁 (2016 Edition) : 한글 번역본|작성자 두야