using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class SpiralPlacer : PipeItemGenerator
{
    public PipeItem[] ItemPrefabs;

    public override void GenerateItem(Pipe pipe)
    {
        float start = (Random.Range(0, pipe.PipeSegmentCount) + 0.5f);
        float direction = Random.value < 0.5f ? 1f : -1f;

        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
        for (int i = 0; i < pipe.CurveSegmentCount; i++)
        {
            PipeItem item = Instantiate(ItemPrefabs[Random.Range(0,ItemPrefabs.Length)]);
            float pipeRotation = (start + i * direction) * 360f / pipe.PipeSegmentCount;
            item.Position(pipe, i * angleStep, pipeRotation);
        }
    }
}
