using Android.Content;
using Android.Views;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace PanRangeSlider;

public class ThumbBorderHandler : BorderHandler
{
    // NOTE: Ripped from source https://github.com/dotnet/maui/blob/aa3f9868ac2f0a285b3e91af9d1f540cca5fd82a/src/Core/src/Handlers/Border/BorderHandler.Android.cs#L7.
    protected override ContentViewGroup CreatePlatformView()
    {
        if (VirtualView == null)
        {
            throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a {nameof(ContentViewGroup)}");
        }

        ThumbBorderViewGroup viewGroup = new(Context)
        {
            CrossPlatformLayout = VirtualView
        };

        viewGroup.SetLayerType(LayerType.Hardware, null);
        return viewGroup;
    }

    private class ThumbBorderViewGroup(Context context) : ContentViewGroup(context)
    {
        public override bool OnInterceptTouchEvent(MotionEvent? e)
        {
            switch (e?.ActionMasked)
            {
                case MotionEventActions.Down:
                    Parent?.RequestDisallowInterceptTouchEvent(true);
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    Parent?.RequestDisallowInterceptTouchEvent(false);
                    break;
            }

            return base.OnInterceptTouchEvent(e);
        }
    }
}