﻿#region License
/*
Microsoft Public License (Ms-PL)
XnaTouch - Copyright © 2009 The XnaTouch Team

All rights reserved.

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not
accept the license, do not use the software.

1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under 
U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
your patent license from such contributor to the software ends automatically.
(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
notices that are present in the software.
(D) If you distribute any portion of the software in source code form, you may do so only under this license by including 
a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object 
code form, you may only do so under a license that complies with this license.
(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees
or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent
permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular
purpose and non-infringement.
*/
#endregion License

using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Windows.Devices.Input;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Input;

namespace Microsoft.Xna.Framework
{
    public partial class MetroGameWindow : GameWindow
    {
        private float _dipFactor;

        void InitializeTouch()
        {
            // Receives mouse, touch and stylus input events
            _coreWindow.PointerPressed += CoreWindow_PointerPressed;
            _coreWindow.PointerReleased += CoreWindow_PointerReleased;
            _coreWindow.PointerMoved += CoreWindow_PointerMoved;
            _coreWindow.PointerWheelChanged += CoreWindow_PointerWheelChanged;

            // To convert from DIPs (that all touch and pointer events are returned in) to pixels (that XNA APIs expect)
            _dipFactor = DisplayProperties.LogicalDpi / 96.0f;
        }

        private void CoreWindow_PointerWheelChanged(CoreWindow sender, PointerEventArgs args)
        {
            // Wheel events always go to the mouse state.
            Mouse.State.Update(args);
        }
        
        void CoreWindow_PointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            var pos = new Vector2((float)args.CurrentPoint.Position.X, (float)args.CurrentPoint.Position.Y) * _dipFactor;
            var isTouch = args.CurrentPoint.PointerDevice.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch;
            if (isTouch)
                TouchPanel.AddEvent(new TouchLocation((int)args.CurrentPoint.PointerId, TouchLocationState.Moved, pos));

            if (!isTouch || args.CurrentPoint.Properties.IsPrimary)
            {
                // Mouse or stylus event or the primary touch event (simulated as mouse input)
                Mouse.State.Update(args);
            }
        }

        void CoreWindow_PointerReleased(CoreWindow sender, PointerEventArgs args)
        {
            var pos = new Vector2((float)args.CurrentPoint.Position.X, (float)args.CurrentPoint.Position.Y) * _dipFactor;
            var isTouch = args.CurrentPoint.PointerDevice.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch;
            if (isTouch)
                TouchPanel.AddEvent(new TouchLocation((int)args.CurrentPoint.PointerId, TouchLocationState.Released, pos));

            if (!isTouch || args.CurrentPoint.Properties.IsPrimary)
            {
                // Mouse or stylus event or the primary touch event (simulated as mouse input)
                Mouse.State.Update(args);
            }
        }

        void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            var pos = new Vector2((float)args.CurrentPoint.Position.X, (float)args.CurrentPoint.Position.Y) * _dipFactor;
            var isTouch = args.CurrentPoint.PointerDevice.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch;
            if (isTouch)
                TouchPanel.AddEvent(new TouchLocation((int)args.CurrentPoint.PointerId, TouchLocationState.Pressed, pos));

            if (!isTouch || args.CurrentPoint.Properties.IsPrimary)
            {
                // Mouse or stylus event or the primary touch event (simulated as mouse input)
                Mouse.State.Update(args);
            }
        }

    }
}