using UnityEngine;
using System.Collections;

public interface IProducer<T>
{
    void Go(T data);
}
