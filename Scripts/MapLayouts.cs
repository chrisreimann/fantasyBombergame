using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapLayouts : MonoBehaviour
{
    public List<Vector2> layout1 = new List<Vector2>();
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2[] blanks1 = {new Vector2(-6,5),
                                new Vector2(-5,5),
                                new Vector2(-2,5),
                                new Vector2(0,5),
                                new Vector2(5,5),
                                new Vector2(6,5),
                                new Vector2(6,4),
                                new Vector2(-6,4),
                                new Vector2(-6,3),
                                new Vector2(6,3),
                                new Vector2(3,1),
                                new Vector2(-2,0),
                                new Vector2(-1,-1),
                                new Vector2(-6,-3),
                                new Vector2(-3,-3),
                                new Vector2(2,-3),
                                new Vector2(6,-3),
                                new Vector2(-6,-4),
                                new Vector2(6,-4),
                                new Vector2(-6,-5),
                                new Vector2(-5,-5),
                                new Vector2(2,-5),
                                new Vector2(5,-5),
                                new Vector2(6,-5)};

        for (int i = -6; i < 7; i++)
            {
                for (int j = 5; j > -6; j--)
                {
                    Vector2 pos = new Vector2(i,j);
                    if (!blanks1.Contains(pos) & !(j % 2 == 0 & (i % 2 == 1 | i % 2 == -1)))
                    {
                        layout1.Add(pos);
                    }

                }
            }                        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
