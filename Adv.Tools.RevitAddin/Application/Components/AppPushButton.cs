using System;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;
using Autodesk.Revit.UI;
using System.Drawing;
using System.Configuration.Assemblies;
using System.Windows.Controls.Primitives;
using System.Security.Policy;

namespace Adv.Tools.RevitAddin.Application.Components
{
    public class AppPushButton
    {

        public PushButtonData ButtonData { get { return CreateNewPushButtonData(); } }
        
        private readonly IAppPushButton _appPushButton;

        public AppPushButton(IAppPushButton appPushButton)
        {
            _appPushButton = appPushButton;
        }

        private PushButtonData CreateNewPushButtonData()
        {

            // Required
            string buttonName = _appPushButton.ButtonName;
            string Description = _appPushButton.ButtonDescription;
            string AssemplyPath = _appPushButton.AssemblyPath;
            string ClassName = _appPushButton.TriggerClassName;

            // Icon
            var bitMapicon = _appPushButton.ButtonIcon.ToBitmap().GetHbitmap();
            var options = BitmapSizeOptions.FromEmptyOptions();
            var imgSource = Imaging.CreateBitmapSourceFromHBitmap(bitMapicon, IntPtr.Zero, Int32Rect.Empty, options);

            return new PushButtonData(buttonName, Description, AssemplyPath, ClassName)
            { 
                 LargeImage = imgSource,
            };
        }
    }
}
