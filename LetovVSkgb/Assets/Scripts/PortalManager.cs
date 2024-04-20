using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
        public GameObject portal;
        void Start()
        {
            portal.SetActive(false);
    }
        private void Update()
        {
            if (Letov.countVin == 3)
                portal.SetActive(true);
        }
}