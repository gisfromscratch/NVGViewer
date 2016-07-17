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
using GalaSoft.MvvmLight;
using NVGViewer.Commands;
using System.Windows.Input;

namespace NVGViewer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _statusLabel = @"Drag the NVG files on the map view . . .";
        private bool _isBusy;

        private MapView _focusMapView;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            // Create the view models
            MessageViewModel = new MessageViewModel(this);

            // Create the commands
            LoadNvgFileCommand = new LoadNvgFileCommand(this);
        }

        /// <summary>
        /// The message view model for processing messages.
        /// </summary>
        public MessageViewModel MessageViewModel { get; private set; }

        /// <summary>
        /// Command for loading NVG files.
        /// </summary>
        public ICommand LoadNvgFileCommand { get; private set; }

        /// <summary>
        /// The initialized focus map view of this application.
        /// </summary>
        public MapView FocusMapView
        {
            get { return _focusMapView; }
            set { Set(ref _focusMapView, value); }
        }

        /// <summary>
        /// The status label shown in the status bar.
        /// </summary>
        public string StatusLabel
        {
            get { return _statusLabel; }
            set { Set(ref _statusLabel, value); }
        }

        /// <summary>
        /// <c>true</c> if the application is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        /// <summary>
        /// Updates the status label and start the progress bar animation.
        /// </summary>
        public void BeginLoadingFiles()
        {
            StatusLabel = @"Loading files . . .";
            IsBusy = true;
        }

        /// <summary>
        /// Resets the status label and stops the progress bar animation.
        /// </summary>
        public void EndLoadingFiles()
        {
            StatusLabel = @"Drag the NVG files on the map view . . .";
            IsBusy = false;
        }
    }
}