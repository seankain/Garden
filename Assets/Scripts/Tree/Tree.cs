using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tree<T>
{
    private T data;
    private List<Tree<T>> children;

    public Tree(T data) {
        this.data = data;
        children = new List<Tree<T>>();
    }

    public void AddChild(T data) {
        this.children.Add(new Tree<T>(data));
    }

    public Tree<T> GetChild(int childIndex) {
        return children[childIndex];
    }
}
