using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalker : MonoBehaviour
{
    public QuoteManager quoteManager;
    public LayerMask clickLayer;
    private RaycastHit[] hits = new RaycastHit[5];

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.RaycastNonAlloc(r, hits, 10, clickLayer) > 0)
            {
                RaycastHit closestHit = hits[0];
                foreach (RaycastHit rhit in hits)
                {
                    if (rhit.collider != null && rhit.distance < closestHit.distance)
                    {
                        closestHit = rhit;
                    }
                }
                quoteManager.ShowQuote(closestHit.collider.GetComponent<SpriteChar>());
            }
        }
    }
}
