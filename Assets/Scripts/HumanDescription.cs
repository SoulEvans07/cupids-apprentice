using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using HairStyle = DescriptionDefaults.HairStyle;
using Features = DescriptionDefaults.Features;
using SexEnum = DescriptionDefaults.SexEnum;
using HeightEnum = DescriptionDefaults.HeightEnum;

public class HumanDescription {
    public Color hairColor;
    public HairStyle hairStyle;    
    public HeightEnum height;
    public Vector2 scale;
    public SexEnum sex;
    
    // todo: features
    public Features features;
    
    // todo: accessories


    public HumanDescription(DescriptionDefaults defaults) {
        // todo: make a more natural hair color picker
        hairColor = new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1));
        sex = defaults.GetRandom<SexEnum>();
        height = defaults.GetRandom<HeightEnum>();
        scale = defaults.GetRandomHeight(this.height);
        hairStyle = defaults.GetRandomHairStyle(this.sex);
        //features = defaults.GetRandomFeatures(this.sex);
    }
}