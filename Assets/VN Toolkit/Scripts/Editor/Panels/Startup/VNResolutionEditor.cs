﻿using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Collections;
using VNToolkit.VNUtility;
using VNToolkit.VNEditor.VNUtility;

namespace VNToolkit.VNEditor {

	public class VNResolutionEditor : VNPanelAbstract {

		// Public Variables

		// Private Variables
		private string widthText;
		private string heightText;

		// Static Variables

		# region Panel Editor Abstract
		public override string PanelTitle {
			get { return VNPanelInfo.PANEL_RESOLUTION_NAME; }
		}

		public override string PanelControlName {
			get { return VNControlName.FOCUSED_PANEL_RESOLUTION; }
		}

		protected override bool IsPanelFoldable {
			get { return true; }
		}

		protected override bool IsPanelFlexible {
			get { return true; }
		}

		protected override bool IsRefreshable {
			get { return true; }
		}

		protected override bool IsScrollable {
			get { return false; }
		}

		protected override float PanelWidth {
			get { return 0f; }
		}

		protected override System.Action<Rect> OnPanelGUI {
			get { return ResolutionWindow; }
		}

		public override void OnPanelEnable(UnityAction repaint) {
			base.OnPanelEnable(repaint);
			widthText = string.Empty;
			heightText = string.Empty;
		}

		protected override void PanelRefresh() {
			widthText = string.Empty;
			heightText = string.Empty;

			base.PanelRefresh();
		}

		protected override void PanelOpen() {
			base.PanelOpen();
		}

		protected override void PanelClose() {
			base.PanelClose();
		}

		protected override void PanelSave() {
			base.PanelSave();
			VNDataManager.SharedInstance.VnProjectData.projectWidth = int.Parse(widthText);
			VNDataManager.SharedInstance.VnProjectData.projectHeight = int.Parse(heightText);
		}

		protected override void PanelLoad() {
			base.PanelLoad();
			widthText = VNDataManager.SharedInstance.VnProjectData.projectWidth.ToString();
			heightText = VNDataManager.SharedInstance.VnProjectData.projectHeight.ToString();
		}

		protected override void PanelClear() {
			base.PanelClear();
			widthText = string.Empty;
			heightText = string.Empty;
		}

		protected override void PanelReset() {
			base.PanelReset();
			widthText = VNConstants.CAMERA_DEFAULT_WIDTH.ToString();
			heightText = VNConstants.CAMERA_DEFAULT_HEIGHT.ToString();
		}

		private void ResolutionWindow(Rect position) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Width", GUILayout.Width(VNConstants.EDITOR_LABEL_WIDTH));
			widthText = EditorGUILayout.TextField(widthText, EditorStyles.textField);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Height", GUILayout.Width(VNConstants.EDITOR_LABEL_WIDTH));
			heightText = EditorGUILayout.TextField(heightText, EditorStyles.textField);
			EditorGUILayout.EndHorizontal();
		}
		# endregion Panel Editor Abstract
	}
}