/*==============================================================================
Copyright (c) 2021, PTC Inc. All rights reserved.
Vuforia is a trademark of PTC Inc., registered in the United States and other countries.
==============================================================================*/
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MultiArea : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public bool hideAugmentationsWhenNotTracked = true;

    public TextMeshProUGUI tmpLog;

    #endregion PUBLIC_MEMBER_VARIABLES



    #region PRIVATE_MEMBER_VARS

    /// <summary>
    /// Trackable poses relative to the MultiArea root
    /// </summary>
    private readonly Dictionary<string, Matrix4x4> mPoses = new Dictionary<string, Matrix4x4>();
    private bool m_Tracked = false;


    private string logText;

    #endregion PRIVATE_MEMBER_VARS



    #region UNITY_MONOBEHAVIOUR_METHODS

    // Start is called before the first frame update
    void Start()
    {
        var areaTargets = GetComponentsInChildren<AreaTargetBehaviour>(includeInactive: true);
        foreach (var at in areaTargets)
        {
            // Remember the relative pose of each AT to the group root node
            var matrix = GetFromToMatrix(at.transform, transform);
            mPoses[at.TrackableName] = matrix;
            Debug.Log("Original pose: " + at.TrackableName + "\n" + matrix.ToString(""));

            // Detach augmentation and re-parent it under the group root node
            for (int i = at.transform.childCount - 1; i >= 0; i--)
            {
                var child = at.transform.GetChild(i);
                child.SetParent(transform, worldPositionStays: true);
            }

            if (hideAugmentationsWhenNotTracked)
            {
                ShowAugmentations(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        logText = "";

        if (!VuforiaARController.Instance.HasStarted)
        {
            tmpLog.text = "01: Vuforia not started";
            return;
        }

        // Find current "best tracked" Area Target
        var atb = GetBestTrackedAreaTarget();
        if (!atb)
        {
            logText += "02: ATB is null. ";

            if (m_Tracked)
            {
                logText += "m_Tracked being set to false. hideAugmentationsWhenNotTracked is $hideAugmentationsWhenNotTracked. ";

                m_Tracked = false;
                if (hideAugmentationsWhenNotTracked)
                {
                    ShowAugmentations(false);
                }
            }

            logText += "End of Update Loop";
            tmpLog.text = logText;

            return;
        }
        logText += "03; ATB returned. ";
        if (!m_Tracked)
        {
            m_Tracked = true;
            ShowAugmentations(true);
            logText += "m_Tracked set to true, showAugmentations(true). ";
        }

        if (GetGroupPoseFromAreaTarget(atb, out Matrix4x4 groupPose))
        {
            // set new group pose
            transform.position = groupPose.GetColumn(3);
            transform.rotation = Quaternion.LookRotation(groupPose.GetColumn(2), groupPose.GetColumn(1));
            logText += "Set new group pose. ";
        }

        tmpLog.text = logText;
    }

    #endregion UNITY_MONOBEHAVIOUR_METHODS



    #region PRIVATE_METHODS

    private void ShowAugmentations(bool show)
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var rnd in renderers)
        {
            rnd.enabled = show;
        }
    }

    private AreaTargetBehaviour GetBestTrackedAreaTarget()
    {
        var trackedAreaTargets = GetTrackedAreaTargets(includeLimited: true);
        if (trackedAreaTargets.Count == 0)
        {
            return null;
        }

        // look for extended/tracked targets
        foreach (var tb in trackedAreaTargets)
        {
            if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED ||
                tb.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                return tb;
            }
        }

        // if no target in EXT/TRACKED was found,
        // then fallback to any other target
        // i.e. including LIMITED ones;
        // just report the first in the list
        return trackedAreaTargets[0];
    }

    private List<AreaTargetBehaviour> GetTrackedAreaTargets(bool includeLimited = false)
    {
        List<AreaTargetBehaviour> trackedTargets = new List<AreaTargetBehaviour>();
        StateManager sm = TrackerManager.Instance.GetStateManager();
        var activeTrackables = sm.GetActiveTrackableBehaviours();
        foreach (var tb in activeTrackables)
        {
            if (!(tb is AreaTargetBehaviour))
                continue;//skip non-area-targets

            if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED ||
                tb.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ||
                (includeLimited && tb.CurrentStatus == TrackableBehaviour.Status.LIMITED))
            {
                trackedTargets.Add(tb as AreaTargetBehaviour);
            }
        }
        return trackedTargets;
    }

    private bool GetGroupPoseFromAreaTarget(AreaTargetBehaviour atb, out Matrix4x4 groupPose)
    {
        groupPose = Matrix4x4.identity;
        if (mPoses.TryGetValue(atb.TrackableName, out Matrix4x4 areaTargetToGroup))
        {
            // Matrix of group root node w.r.t. AT
            var groupToAreaTarget = areaTargetToGroup.inverse;

            // Current atb matrix
            var areaTargetToWorld = atb.transform.localToWorldMatrix;
            groupPose = areaTargetToWorld * groupToAreaTarget;
            return true;
        }
        return false;
    }

    private static Matrix4x4 GetFromToMatrix(Transform from, Transform to)
    {
        var m1 = from ? from.localToWorldMatrix : Matrix4x4.identity;
        var m2 = to ? to.worldToLocalMatrix : Matrix4x4.identity;
        return m2 * m1;
    }

    #endregion PRIVATE_METHODS


    //Output the new state of the Toggle into Text
    public void ToggleValueChanged(Toggle change)
    {
        hideAugmentationsWhenNotTracked = change.isOn;
    }
}