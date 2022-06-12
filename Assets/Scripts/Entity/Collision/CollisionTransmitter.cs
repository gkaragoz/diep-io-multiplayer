using System;
using UnityEngine;

namespace Entity.Collision
{
    public class CollisionTransmitter : MonoBehaviour
    {
        public Action<Collider2D> OnTriggerEnter2DCallback { get; set; }
        public Action<Collider2D> OnTriggerStay2DCallback { get; set; }
        public Action<Collider2D> OnTriggerExit2DCallback { get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter2DCallback?.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerStay2DCallback?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExit2DCallback?.Invoke(other);
        }
    }
}