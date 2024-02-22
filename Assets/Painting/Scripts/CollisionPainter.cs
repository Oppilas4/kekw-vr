using System.Linq;
using UnityEngine;

public class CollisionPainter : MonoBehaviour{
    public Color paintColor;
    
    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    private void OnCollisionStay(Collision other) {
        Paintable p = other.collider.GetComponent<Paintable>();
        if(p != null){
            Vector3 pos = other.GetContact(0).point;
            PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
            Debug.Log($"Collision: {pos} {radius} {hardness} {strength} {paintColor}");
        }
    }
}
