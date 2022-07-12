//using System;
//namespace SpectrumTV.iOS.Effects
//{
//    public class SafeAreaEffect
//    {
//        public SafeAreaEffect()
//        {
//        }
//    }
//}


using System;
using System.Linq;
using SpectrumTV.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(SpectrumTV.iOS.Effects.SafeAreaEffect), SafeAreaEffect.EFFECT_NAME)]
namespace SpectrumTV.iOS.Effects
{
    public class SafeAreaEffect : PlatformEffect
    {
        private Thickness _padding;

        protected override void OnAttached()
        {
            try
            {
                if (Element is Layout layout)
                {
                    var effect = Element.Effects.FirstOrDefault(x => x is SpectrumTV.Effects.SafeAreaEffect) as SpectrumTV.Effects.SafeAreaEffect;

                    if (effect != null)
                    {
                        _padding = layout.Padding;

                        if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                        {
                            var safeArea = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;

                            double left = layout.Padding.Left;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Left))
                            {
                                left += safeArea.Left;
                            }

                            double top = layout.Padding.Top;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Top))
                            {
                                top += safeArea.Top;
                            }

                            double right = layout.Padding.Right;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Right))
                            {
                                right += safeArea.Right;
                            }

                            double bottom = layout.Padding.Bottom;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Bottom))
                            {
                                bottom += safeArea.Bottom;
                            }

                            layout.Padding = new Thickness(left, top, right, bottom);
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[SafeAreaPaddingEffect] Cannot be attached to type {Element.GetType()}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SafeAreaPaddingEffect] Exception: {ex.Message}\n{ex.StackTrace}");
            }
        }

        protected override void OnDetached()
        {
            if (Element is Layout layout)
            {
                layout.Padding = _padding;
            }
        }
    }
}
