﻿/*
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace NVGViewer.ViewModel
{
    /// <summary>
    /// The view model for the messages.
    /// </summary>
    public class MessageViewModel : ViewModelBase
    {
        private readonly MainViewModel _viewModel;
        private readonly ObservableCollection<INvgElement> _nvgElements;

        /// <summary>
        /// Creates a new message view model instance using the specifies main view model.
        /// </summary>
        /// <param name="viewModel">The main view model.</param>
        public MessageViewModel(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            _nvgElements = new ObservableCollection<INvgElement>();
        }

        /// <summary>
        /// Adds a NVG element to this view model.
        /// </summary>
        /// <param name="nvgElement">The NVG element which should be added.</param>
        public void AddElement(INvgElement nvgElement)
        {
            if (null == nvgElement)
            {
                throw new ArgumentNullException(nameof(nvgElement));
            }

            _nvgElements.Add(nvgElement);
        }

        /// <summary>
        /// Processes all added NVG elements using the registered map view.
        /// Make sure this method is called from the UI thread!
        /// </summary>
        public async void ProcessAllMessages()
        {
            var mapView = _viewModel.FocusMapView;
            if (null == mapView)
            {
                return;
            }

            // Create a new message layer and wait till the layer was loaded
            var messageLayer = new MessageLayer();
            mapView.Map.Layers.Add(messageLayer);
            await mapView.LayersLoadedAsync(new[] { messageLayer });

            // Process all messages
            foreach (var nvgElement in _nvgElements)
            {
                ProcessAllMessages(nvgElement, messageLayer);
            }

            // Clear the elements
            _nvgElements.Clear();
        }

        private void ProcessAllMessages(INvgElement nvgElement, MessageLayer messageLayer)
        {
            foreach (var nvgChildElement in nvgElement.Children)
            {
                var nvgPointElement = nvgChildElement as NvgPointElement;
                if (null != nvgPointElement)
                {
                    if (!nvgPointElement.IsEmpty)
                    {
                        var message = CreateMessage(nvgPointElement);
                        if (!messageLayer.ProcessMessage(message))
                        {
                            // TODO: Log the message error!
                        }
                    }
                }
            }
        }

        private static Message CreateMessage(NvgPointElement pointElement)
        {
            var messageProperties = new Dictionary<string, string>();
            messageProperties.Add(@"_type", @"position_report");
            messageProperties.Add(@"_action", @"update");
            messageProperties.Add(@"_id", pointElement.Id);
            messageProperties.Add(@"_control_points", string.Format(CultureInfo.InvariantCulture, @"{0},{1}", pointElement.X, pointElement.Y));
            messageProperties.Add(@"_wkid", @"4326");
            messageProperties.Add(@"sic", pointElement.SymbolCode);
            messageProperties.Add(@"uniquedesignation", pointElement.Label);

            return new Message(messageProperties);
        }
    }
}