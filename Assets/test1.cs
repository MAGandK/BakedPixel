using UnityEngine;

public class test1 : MonoBehaviour
{
 
           // public int counter = 0;
           //
           // private void Start()
           // {
           //     while(counter < 3)
           //     {
           //         Debug.Log($"BLOCK: counter = {counter}");
           //         counter++;
           //     }
           //     
           //     Debug.Log($"MAIN: counter = {counter}");
           //
           // }
           private byte[] array = { };

           private void Start()
           {
               byte[] ageList = { 23, 30, 35 };
               int index = 0;

               while (index < 3)
               {
                   byte age = ageList[index];
                   Debug.Log(age);

                   index++;
               }


           }
}
