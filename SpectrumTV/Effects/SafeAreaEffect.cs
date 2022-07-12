using System;
using Xamarin.Forms;

namespace SpectrumTV.Effects
{
    public class SafeAreaEffect : RoutingEffect
    {
        public const string EFFECT_NAME = "SafeAreaEffect";
        public SafeAreaFlags Flags { get; set; }
        public SafeAreaEffect() : base($"{ResolutionGroupName.SpectrumTV}.{EFFECT_NAME}") { }
    }
}


[Flags]
public enum SafeAreaFlags
{
    None = 0,
    Left = 1,
    Top = 2,
    Right = 4,
    Bottom = 8
}