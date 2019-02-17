using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DescriptionDefaults : MonoBehaviour {
    public List<HairStyle> maleHairStyles;
    public List<HairStyle> femaleHairStyles;
    public List<Features> maleFeatures;
    public List<Features> femaleFeatures;
    public float heightDiff = 0.15f;

    public HairStyle GetRandomHairStyle(SexEnum _sex) {
        return _sex == SexEnum.MALE
            ? maleHairStyles[Random.Range(0, maleHairStyles.Count)]
            : femaleHairStyles[Random.Range(0, femaleHairStyles.Count)];
    }

    public Features GetRandomFeatures(SexEnum _sex) {
        return _sex == SexEnum.MALE
            ? maleFeatures[Random.Range(0, maleFeatures.Count)]
            : femaleFeatures[Random.Range(0, femaleFeatures.Count)];
    }

    public Vector2 GetRandomHeight(HeightEnum height) {
        switch (height) {
            case HeightEnum.TALL: return new Vector2(1, 1.2f + Random.Range(-heightDiff, heightDiff));
            case HeightEnum.NORMAL: return new Vector2(1, 1 + Random.Range(-heightDiff, heightDiff));
            case HeightEnum.SHORT: return new Vector2(1, 0.8f + Random.Range(-heightDiff, heightDiff));
            default: return Vector2.one;          
        }
    }

    [Serializable]
    public class HairStyle {
        public string name;
        public Sprite sprite;
    }

    [Serializable]
    public class Features {
    }

    public T GetRandom<T>() {
        var v = Enum.GetValues(typeof(T));
        return (T) v.GetValue(Random.Range(0, v.Length));
    }


    [Serializable]
    public enum HeightEnum {
        TALL,
        NORMAL,
        SHORT
    }

    [Serializable]
    public enum SexEnum {
        MALE,
        FEMALE
    }
}