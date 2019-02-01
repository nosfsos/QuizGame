using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetPositions : MonoBehaviour
{
    public List<Transform> Movables;

    public List<MovableValues> Values;

    public static ResetPositions Instance;


    private void Awake()
    {
        Instance = this;
    }

    public void GatherInitialPositions()
    {
        Movables = GameObject.FindGameObjectsWithTag("Movable").Select(go => go.transform).ToList();

        foreach (var movable in Movables)
        {

            Values.Add(
                new MovableValues
                {
                    Position = movable.localPosition,
                    Rotation = movable.localRotation,
                    Scale = movable.localScale
                });
        }
    }
    public void ResetValues()
    {
        for (var i = 0; i < Movables.Count; i++)
        {
            var movable = Movables[i];
            var values = Values[i];

            movable.localPosition = values.Position;
            movable.localRotation = values.Rotation;
            movable.localScale = values.Scale;
        }
    }


    [Serializable]
    public struct MovableValues
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }
}