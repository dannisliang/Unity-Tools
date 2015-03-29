﻿using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.AnimatedValues;
using System.Collections.Generic;
using System.Collections;

namespace VNToolkit {
	public class VNToolkitEditor : EditorWindow {

		// Public Variables

		// Private Variables
		private string versionTitle;

		private int selectedIndx;
		private string[] windowToolbar;

		private enum WindowOptions {
			WINDOW_LOAD = 0,
			WINDOW_SAVE = 1
		}

		private WindowOptions windowOptions;
		private VNToolkitEditorDataCache vnToolkitDataCache;

		// Static Variables

		[MenuItem("Toolkit/VN Toolkit")]
		private static void Initialize() {
			VNToolkitEditor window = (VNToolkitEditor)EditorWindow.GetWindow(typeof(VNToolkitEditor));
			window.title = VNToolkitConstants.WINDOW_TITLE;
			window.position = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, VNToolkitConstants.WINDOW_WIDTH, VNToolkitConstants.WINDOW_HEIGHT);
			window.minSize = new Vector2(VNToolkitConstants.WINDOW_WIDTH, VNToolkitConstants.WINDOW_HEIGHT);

			window.Show();
		}

		private void OnEnable() {
			hideFlags = HideFlags.HideAndDontSave;
			versionTitle = File.ReadAllText(Application.dataPath + "/VN Toolkit/Version Number.txt");

			selectedIndx = -1;
			windowToolbar = new string[] { "Load", "Save" };

			vnToolkitDataCache = new VNToolkitEditorDataCache();
			vnToolkitDataCache.Initialize();

			VNToolkitPanelManager.SharedInstance.Initialize(Repaint, position);
		}

		private void OnGUI() {
			WindowHeader();

			EditorGUILayout.BeginVertical("box", GUILayout.MaxHeight(position.height - 50f));
			EditorGUILayout.BeginHorizontal();

			VNToolkitPanelManager.SharedInstance.DrawPanels();

			//VNToolkitChapterEditor.ChapterWindow(Repaint);
			//VNToolkitSceneEditor.SceneWindow(Repaint);
			//VNToolkitPhysicalDataEditor.PhysicalDataWindow(Repaint);
			//VNToolkitChapterEditor.ChapterInformationWindow();
			//vnToolkitChapterEditor.DrawGUI();

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();

			WindowFooter();

			//EditorGUILayout.BeginVertical("Box");
			//EditorGUILayout.BeginHorizontal("Box");
			//if (GUILayout.Button("Test", EditorStyles.boldLabel)) {
			//    VNToolkitEditorBase.CreateLoadBase();
			//}
			//EditorGUILayout.EndHorizontal();
			//EditorGUILayout.EndVertical();

			Event e = Event.current;
			if (e.button == 0 && e.type == EventType.mouseDown) {
				GUI.FocusControl(VNToolkitControlName.FOCUSED_NONE);
			}

			Repaint();
		}

		private void WindowHeader() {
			GUILayout.Space(5f);
			selectedIndx = GUILayout.Toolbar(selectedIndx, windowToolbar, EditorStyles.toolbarButton);

			windowOptions = (WindowOptions)selectedIndx;
			if (windowOptions == WindowOptions.WINDOW_LOAD) {
				Load();
			}
			else if (windowOptions == WindowOptions.WINDOW_SAVE) {
				Save();
			}
			
			selectedIndx = -1;
		}

		private void Save() {
			Debug.Log("VN Editor: Saved");
			vnToolkitDataCache.chapterData.AddRange(VNToolkitChapterEditor.chapterInfo);

			vnToolkitDataCache.Save();
			AssetDatabase.Refresh();

			VNToolkitPanelManager.SharedInstance.SavePanels();

			VNToolkitNotifierEditor.TriggerNotification("Data Saved!", VNToolkitConstants.NOTIFICATION_TIMEOUT);
		}

		private void Load() {
			Debug.Log("VN Editor: Load");

			vnToolkitDataCache.Load();
			ArrayUtility.Clear<VNToolkitChapterData>(ref VNToolkitChapterEditor.chapterInfo);
			ArrayUtility.AddRange<VNToolkitChapterData>(ref VNToolkitChapterEditor.chapterInfo, vnToolkitDataCache.chapterData.ToArray());
			VNToolkitPanelManager.SharedInstance.RepaintPanels(true);

			VNToolkitNotifierEditor.TriggerNotification("Data Loading", VNToolkitConstants.NOTIFICATION_TIMEOUT);
		}

		private void WindowFooter() {
			GUI.Box(new Rect(0f, position.height - 20f, position.width, 20f), string.Empty, EditorStyles.textField);

			GUI.Box(new Rect(0f, position.height - 20f, 100f, 20f), string.Empty, EditorStyles.textField);
			EditorGUI.DropShadowLabel(new Rect(0f, position.height - 15f, 100f, 10f), versionTitle);

			VNToolkitNotifierEditor.NotifierWindow(Repaint, position);

			GUI.Box(new Rect(position.width - 150f, position.height - 20f, 150f, 20f), string.Empty, EditorStyles.textField);
			EditorGUI.ProgressBar(new Rect(position.width - 150f, position.height - 20f, 150f, 20f), 1, "Loading");
		}
	}
}