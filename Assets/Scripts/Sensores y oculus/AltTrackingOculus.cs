// Copyright 2020, ALT LLC. All Rights Reserved.
// This file is part of Antilatency SDK.
// It is subject to the license terms in the LICENSE file found in the top-level directory
// of this distribution and at http://www.antilatency.com/eula
// You may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.XR;

using Antilatency.Integration;
using Antilatency.DeviceNetwork;

/// <summary>
/// %Antilatency %Alt tracking components and scripts specific for %Oculus HMD devices.
/// </summary>
namespace Antilatency.OculusSample {
    /// <summary>
    /// %Antilatency %Alt tracking sample implementation for %Oculus headsets.
    /// </summary>
    [RequireComponent(typeof(OVRCameraRig))]
    public class AltTrackingOculus : AltTracking {
        /// <summary>
        /// Link to OVRCameraRig component.
        /// </summary>
        protected OVRCameraRig _rig;

        private Antilatency.TrackingAlignment.ILibrary _alignmentLibrary;
        private Antilatency.TrackingAlignment.ITrackingAlignment _alignment;

        private bool anchorsUpdated = false;

        //Oculus has no tracking quality parameter, so we use average Alt tracking quality.
        private const float _bQuality = 0.4f;
        private bool _altInitialPositionApplied = false;

        /// <summary>
        /// Get node (ALT tracker device) to start tracking task.
        /// </summary>
        /// <returns>First idle ALT tracker node connected via USB.</returns>
        protected override NodeHandle GetAvailableTrackingNode() {
            return GetUsbConnectedFirstIdleTrackerNode();
        }

        protected override void OnEnable() {
            base.OnEnable();

            _rig = GetComponent<OVRCameraRig>();

            if (_rig != null) {
                _rig.UpdatedAnchors += OnUpdatedAnchors;
            } else {
                Debug.LogError("OVRCameraRig not found");
            }

            if (OVRManager.boundary != null) {
                OVRManager.boundary.SetVisible(false);
            }
        }

        protected override void OnDisable() {
            base.OnDisable();

            _rig.UpdatedAnchors -= OnUpdatedAnchors;
        }

        protected virtual void OnFocusChanged(bool focus) {
            if (focus) {
                StartTrackingAlignment();
            } else {
                StopTrackingAlignment();
            }
        }

        private void OnApplicationFocus(bool focus) {
            OnFocusChanged(focus);
        }

        private void OnApplicationPause(bool pause) {
            OnFocusChanged(!pause);
        }

        private void StartTrackingAlignment() {
            if (_alignment != null) {
                StopTrackingAlignment();
            }

            var placement = GetPlacement();
            _alignment = _alignmentLibrary.createTrackingAlignment(Antilatency.Math.doubleQ.FromQuaternion(placement.rotation), ExtrapolationTime);

            _altInitialPositionApplied = false;
        }

        private void StopTrackingAlignment() {
            if (_alignment == null) {
                return;
            }

            _alignment.Dispose();
            _alignment = null;
        }

        protected override void Awake() {
            base.Awake();

            _alignmentLibrary = Antilatency.TrackingAlignment.Library.load();

            var placement = GetPlacement();
            _alignment = _alignmentLibrary.createTrackingAlignment(Antilatency.Math.doubleQ.FromQuaternion(placement.rotation), ExtrapolationTime);
        }

        protected override void Update() {
            base.Update();

            anchorsUpdated = false;
        }

        /// <summary>
        /// Applies tracking data to CameraRig. We used %Oculus native rotation as base and then smoothly correct it with our tracking data 
        /// to avoid glitches that can be seen because %Oculus Asynchronous TimeWarp system uses only native rotation data provided by headset.
        /// </summary>
        /// <param name="rig">Pointer to OVRCameraRig</param>
        protected virtual void OnUpdatedAnchors(OVRCameraRig rig) {
            if (_rig != rig) {
                return;
            }

            // OnUpdatedAnchors calling every frame for 2 times, one at update and one before render. We need to make correction only once per frame.
            if (anchorsUpdated) {
                return;
            }
            anchorsUpdated = true;

            // Get Alt raw tracking state to correct Oculus rotation.
            Alt.Tracking.State trackingState;
            var altTrackingActive = GetRawTrackingState(out trackingState);

            // Correct Oculus rotation if we have good quality tracking data from Alt.
            if (altTrackingActive && (_alignment != null) && trackingState.stability.stage == Antilatency.Alt.Tracking.Stage.Tracking6Dof && trackingState.stability.value > 0.075f) {
                var result = _alignment.update(
                    Antilatency.Math.doubleQ.FromQuaternion(trackingState.pose.rotation),
                    Antilatency.Math.doubleQ.FromQuaternion(rig.centerEyeAnchor.localRotation),
                    Time.realtimeSinceStartup
                );

                ExtrapolationTime = (float)result.timeBAheadOfA;
                _placement.rotation = result.rotationARelativeToB.ToQuaternion();
                rig.trackingSpace.localRotation = result.rotationBSpace.ToQuaternion();
            }

            // Get Alt tracking extrapolated state to correct Oculus position.
            altTrackingActive = GetTrackingState(out trackingState);

            if (!altTrackingActive || trackingState.stability.stage == Antilatency.Alt.Tracking.Stage.InertialDataInitialization) {
                return;
            }

            // Correct Oculus position.
            if (OVRManager.instance.usePositionTracking) {
                if (trackingState.stability.stage == Antilatency.Alt.Tracking.Stage.Tracking6Dof) {
                    var a = trackingState.pose.position;
                    var bSpace = rig.trackingSpace.localPosition;
                    var b = rig.transform.InverseTransformPoint(rig.centerEyeAnchor.position);

                    Vector3 averagePositionInASpace;

                    if (!_altInitialPositionApplied) {
                        averagePositionInASpace = a;
                        _altInitialPositionApplied = true;
                    } else {
                        averagePositionInASpace = (b * _bQuality + a * trackingState.stability.value) / (trackingState.stability.value + _bQuality);
                    }

                    rig.trackingSpace.localPosition += averagePositionInASpace - b;
                }
            } else {
                rig.trackingSpace.localPosition = trackingState.pose.position;
                rig.leftEyeAnchor.localPosition = Vector3.zero;
                rig.rightEyeAnchor.localPosition = Vector3.zero;
                rig.centerEyeAnchor.localPosition = Vector3.zero;
            }
        }

        protected override Pose GetPlacement() {
            var result = Pose.identity;

            using (var localStorage = Integration.StorageClient.GetLocalStorage()) {

                if (localStorage == null) {
                    return result;
                }

                var placementCode = localStorage.read("placement", "default");

                if (string.IsNullOrEmpty(placementCode)) {
                    Debug.LogError("Failed to get placement code");
                } else {
                    result = _trackingLibrary.createPlacement(placementCode);
                }

                return result;
            }
        }
    }
}