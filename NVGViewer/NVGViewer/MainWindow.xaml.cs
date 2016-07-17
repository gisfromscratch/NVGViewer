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

using Esri.ArcGISRuntime.Controls;
using MahApps.Metro.Controls;
using NVGViewer.ViewModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace NVGViewer
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FocusMapViewLayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;

            Debug.WriteLine(string.Format("Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message));
        }

        private void FocusMapViewDropped(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            var filePaths = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (null == filePaths)
            {
                return;
            }

            e.Handled = true;

            var mainViewModel = DataContext as MainViewModel;
            if (null == mainViewModel)
            {
                return;
            }

            // Start a new task for loading the NVG files
            mainViewModel.BeginLoadingFiles();
            Task.Run(() =>
            {
                var loadNvgFileCommand = mainViewModel.LoadNvgFileCommand;
                foreach (var filePath in filePaths)
                {
                    if (loadNvgFileCommand.CanExecute(filePath))
                    {
                        loadNvgFileCommand.Execute(filePath);
                    }
                }
            }).ContinueWith((task) => {
                mainViewModel.EndLoadingFiles();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void FocusMapViewSpatialReferenceChanged(object sender, EventArgs e)
        {
            if (null != FocusMapView.SpatialReference)
            {
                // Create the message processor
            }
        }
    }
}
