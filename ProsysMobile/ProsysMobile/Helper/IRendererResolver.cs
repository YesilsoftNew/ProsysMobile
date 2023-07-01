using System;
using Xamarin.Forms;

namespace ProsysMobile.Helper
{
    public interface IRendererResolver
    {
        object GetRenderer(VisualElement element);
        bool HasRenderer(VisualElement element);
    }
}
