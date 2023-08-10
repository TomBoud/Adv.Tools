using System;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;
using Autodesk.Revit.UI;
using System.Drawing;


namespace Adv.Tools.RevitAddin.Application.Components
{
    public class AppPushButton : IAppPushButton
    {
        public string AssemblyPath => _classToTrigger.Assembly.Location;

        public string TriggerClassName => _classToTrigger.FullName;

        private readonly Type _classToTrigger;
        private readonly Icon _icon;

        public AppPushButton(Type classToTrigger, Icon icon)
        {
            _classToTrigger = classToTrigger;
            _icon = icon;
        }

        public void SetIcon(PushButton button)
        {
            var bitMapicon = _icon.ToBitmap().GetHbitmap();
            var options = BitmapSizeOptions.FromEmptyOptions();
            var imgSource = Imaging.CreateBitmapSourceFromHBitmap(bitMapicon, IntPtr.Zero, Int32Rect.Empty, options);
            button.LargeImage = imgSource;
        }

        public PushButton AddToPanel(RibbonPanel panel, string buttonName, string buttonDescription)
        {
            var data = new PushButtonData(buttonName, buttonDescription, AssemblyPath, TriggerClassName);
            return panel.AddItem(data) as PushButton;
        }

      
    }
}
