using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RaycastManager : MonoBehaviour
    {
        public static System.Func<(bool isHit,RaycastHit hitData)> GetRaycastHit;
        public static System.Func<(bool isHit,RaycastHit hitData)> GetRaycastHitFronCam;
        public static System.Func<(bool isHit,RaycastHit hitData)> GetRaycastHitTempPlane;

        [SerializeField] private Transform playerCamera;
        [SerializeField] private LayerMask maskTempAndTriggers;
        [SerializeField] private LayerMask maskTemporaryparent;

        private void Awake()
        {
            GetRaycastHit = TryAndGetRaycastHit;
            GetRaycastHitTempPlane = TryAndGetRaycastHitOnTempPlane;
            GetRaycastHitFronCam = TryAndGetRaycastHitFrontCamera;
        }

        private (bool, RaycastHit) TryAndGetRaycastHitFrontCamera()
        {
            bool isHit = false;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if(Physics.Raycast(ray, out RaycastHit hitData, 2, maskTempAndTriggers))
                isHit = true;
            return (isHit, hitData);
        }
        private (bool, RaycastHit) TryAndGetRaycastHit()
        {
            bool isHit = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitData, 2, maskTempAndTriggers))
                isHit = true;
            return (isHit, hitData);
        }
        private (bool, RaycastHit) TryAndGetRaycastHitOnTempPlane()
        {
            bool isHit = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitData, 2, maskTemporaryparent))
                isHit = true;
            return (isHit, hitData);
        }

#if UNITY_EDITOR
        void Update()
        {
            if(GameManager.Game == GameManager.GameState.Play || GameManager.Game == GameManager.GameState.Minigame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
            } 
        }
#endif

    }
}