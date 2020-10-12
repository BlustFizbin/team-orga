using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterToGrass : MonoBehaviour
{
   public UnityEvent enter = new UnityEvent(), exit = new UnityEvent();
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("Hi");
      enter.Invoke();
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      exit.Invoke();
   }
}
