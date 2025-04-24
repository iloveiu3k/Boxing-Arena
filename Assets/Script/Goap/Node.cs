using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public Node parent;
    public float g, h;
    public Dictionary<string, object> state;
    public GoapAction action;
    public float F => g + h;

    public Node(Node parent, float g, float h, Dictionary<string, object> state, GoapAction action) {
        this.parent = parent;
        this.g = g;
        this.h = h;
        this.state = new Dictionary<string, object>(state);
        this.action = action;
    }
}