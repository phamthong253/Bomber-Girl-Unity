// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections;

// public class Singleton : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     private static T instance;
//     public stactic T Instance {get{return instance;}}
//     protected virtual void Awake(){
//         if(instance != null && this.gameObject != null){
//             Destroy(this.gameObject);
//         }else{
//             instance = (T)this;
//         }
//         DontDestroyOnLoad(GameObject)
//     }
// }
