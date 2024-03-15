//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class HS_HandSelectionManager : MonoBehaviour
//{
//    [SerializeField] private XRRayInteractor _interactor;

//    private void Awake()
//    {
//        _interactor.selectEntered.AddListener(OnSelect);
//    }
//    public void OnSelect(SelectEnterEventArgs args)
//    {
//        Collider coll = args.interactableObject.colliders[0].GetComponent<Collider>();
//        if (coll.TryGetComponent<HS_IOnSelectAudioPlayer>(out HS_IOnSelectAudioPlayer component))
//        {
//        }

//    }

//}
