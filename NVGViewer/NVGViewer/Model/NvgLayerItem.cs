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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology.Specialized;
using GalaSoft.MvvmLight;
using NVG.Data;

namespace NVGViewer.Model
{
    /// <summary>
    /// Represents a NVG layer item which is show on a map.
    /// </summary>
    public class NvgLayerItem : INotifyPropertyChanged
    {
        private readonly MessageLayer _messageLayer;

        /// <summary>
        /// Creates a new layer item instance using the specified layer.
        /// </summary>
        /// <param name="messageLayer">The layer showing the messages.</param>
        public NvgLayerItem(MessageLayer messageLayer)
        {
            _messageLayer = messageLayer;
        }

        internal MessageLayer MessageLayer
        {
            get { return _messageLayer; }
        }

        public bool IsVisible
        {
            get { return _messageLayer.IsVisible; }
            set
            {
                _messageLayer.IsVisible = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }

        public ulong MessageCount { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
