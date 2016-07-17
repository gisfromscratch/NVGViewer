/*
 * Copyright 2016 Jan Tschada
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Esri.ArcGISRuntime.Symbology.Specialized;
using GalaSoft.MvvmLight;
using NVG.Data;

namespace NVGViewer.ViewModel
{
    /// <summary>
    /// The view model for the messages.
    /// </summary>
    public class MessageViewModel : ViewModelBase
    {
        public MessageLayer CreateMessageLayer(NvgElement nvgElement)
        {
            // TODO: Create the message layer containing all messages of the NVG element
            return null;
        }
    }
}
