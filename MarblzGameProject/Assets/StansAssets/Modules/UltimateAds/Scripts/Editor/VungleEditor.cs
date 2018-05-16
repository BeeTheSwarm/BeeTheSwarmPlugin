//#define VUNGLE_ENABLED

////////////////////////////////////////////////////////////////////////////////
//  
// @module Ultimate Ads
// @author Alexey Yaremenko && Konstantin Koretsky (Stan's Assets)
// @support support@stansassets.com
// @website https://stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SA.UltimateAds {
	
	[CustomEditor(typeof(VungleNetwork))]
	internal class VungleEditor : Editor {

		private VungleNetwork network;

		#if VUNGLE_ENABLED
			GUIContent PlacementNameLabel = new GUIContent("PlacementID[?]:", "A unique placementID that will be used for showing ads you think are appropriate.");
		#endif
		
		void Awake() {
			network = target as VungleNetwork;
		}

		public override void OnInspectorGUI() {			
			Settings ();
			
			EditorGUILayout.Space ();

			if (GUI.changed) {
				DirtyEditor ();
			}
		}

		private void Settings() {
			EditorGUILayout.Space ();
			GUIStyle style = EditorStyles.foldout;
			style.font = EditorStyles.boldFont;

			if (network.isOpen = EditorGUILayout.Foldout (network.isOpen, network.Name, style)) {
	
				#if VUNGLE_ENABLED
					//ANDROID
					EditorGUILayout.BeginVertical (GUI.skin.box);

					network.AndroidAppId = EditorGUILayout.TextField ("Android AppID", network.AndroidAppId);

					EditorGUI.indentLevel++;
					{
						EditorGUILayout.BeginHorizontal();
							network.ShowANPlacements = EditorGUILayout.Foldout(network.ShowANPlacements, "Placements");
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();

						if(network.ShowANPlacements) {

							foreach(ANVunglePlacement placement in network.ANPlacements) {

								EditorGUILayout.BeginVertical (GUI.skin.box);

								EditorGUILayout.BeginHorizontal();
								EditorGUILayout.LabelField(PlacementNameLabel);
								placement.ID = EditorGUILayout.TextField(placement.ID);

								bool ItemWasRemoved = DrawSortingButtons((object) placement, network.ANPlacements);
								if(ItemWasRemoved) {
									return;
								}

								EditorGUILayout.EndHorizontal();

								EditorGUILayout.EndVertical();
							}

							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.Space();
							if(GUILayout.Button("Add new", EditorStyles.miniButton, GUILayout.Width(250))) {
								ANVunglePlacement placement = new ANVunglePlacement("New placementID");
								network.ANPlacements.Add(placement);
							}

							EditorGUILayout.Space();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						} 
					}
					EditorGUI.indentLevel--;

					EditorGUILayout.EndVertical();

					//IOS
					EditorGUILayout.BeginVertical (GUI.skin.box);

					network.iOSAppId = EditorGUILayout.TextField ("iOS App Id", network.iOSAppId);

					EditorGUI.indentLevel++;
					{
						EditorGUILayout.BeginHorizontal();
						network.ShowIOSPlacements = EditorGUILayout.Foldout(network.ShowIOSPlacements, "Placements");

						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();

						if(network.ShowIOSPlacements) {

							foreach(IOSVunglePlacement placement in network.IOSPlacements) {

								EditorGUILayout.BeginVertical (GUI.skin.box);

								EditorGUILayout.BeginHorizontal();

								EditorGUILayout.LabelField(PlacementNameLabel);
								placement.ID = EditorGUILayout.TextField(placement.ID);

								bool ItemWasRemoved = DrawSortingButtons((object) placement, network.IOSPlacements);
								if(ItemWasRemoved) {
									return;
								}

								EditorGUILayout.EndHorizontal();

								EditorGUILayout.EndVertical();
							}

							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.Space();
							if(GUILayout.Button("Add new", EditorStyles.miniButton, GUILayout.Width(250))) {
								IOSVunglePlacement placement = new IOSVunglePlacement("New placementID");
								network.IOSPlacements.Add(placement);
							}

							EditorGUILayout.Space();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						} 
					}
					EditorGUI.indentLevel--;

					EditorGUILayout.EndVertical();

					//WINDOWS
					EditorGUILayout.BeginVertical (GUI.skin.box);

					network.WinAppId = EditorGUILayout.TextField ("Windows App Id", network.WinAppId);

					EditorGUI.indentLevel++;
					{
						EditorGUILayout.BeginHorizontal();
						network.ShowWINPlacements = EditorGUILayout.Foldout(network.ShowWINPlacements, "Placements");

						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();

						if(network.ShowWINPlacements) {

							foreach(WinVunglePlacement placement in network.WinPlacements) {

								EditorGUILayout.BeginVertical (GUI.skin.box);

								EditorGUILayout.BeginHorizontal();

								EditorGUILayout.LabelField(PlacementNameLabel);
								placement.ID = EditorGUILayout.TextField(placement.ID);

								bool ItemWasRemoved = DrawSortingButtons((object) placement, network.WinPlacements);
								if(ItemWasRemoved) {
									return;
								}

								EditorGUILayout.EndHorizontal();

								EditorGUILayout.EndVertical();
							}

							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.Space();
							if(GUILayout.Button("Add new", EditorStyles.miniButton, GUILayout.Width(250))) {
								WinVunglePlacement placement = new WinVunglePlacement("New placementID");
								network.WinPlacements.Add(placement);
							}

							EditorGUILayout.Space();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						} 
					}
					EditorGUI.indentLevel--;

					EditorGUILayout.EndVertical();
				#else
					EditorGUILayout.HelpBox ("Vungle SDK DOESN'T exist", MessageType.Warning);				
				#endif
			}
		}

		private bool DrawSortingButtons(object currentObject, IList ObjectsList) {

			int ObjectIndex = ObjectsList.IndexOf(currentObject);
			if(ObjectIndex == 0) {
				GUI.enabled = false;
			} 

			bool up 		= GUILayout.Button("↑", EditorStyles.miniButtonLeft, GUILayout.Width(20));
			if(up) {
				object c = currentObject;
				ObjectsList[ObjectIndex]  		= ObjectsList[ObjectIndex - 1];
				ObjectsList[ObjectIndex - 1] 	=  c;
			}


			if(ObjectIndex >= ObjectsList.Count -1) {
				GUI.enabled = false;
			} else {
				GUI.enabled = true;
			}

			bool down 		= GUILayout.Button("↓", EditorStyles.miniButtonMid, GUILayout.Width(20));
			if(down) {
				object c = currentObject;
				ObjectsList[ObjectIndex] =  ObjectsList[ObjectIndex + 1];
				ObjectsList[ObjectIndex + 1] = c;
			}


			GUI.enabled = true;
			bool r 			= GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(20));
			if(r) {
				ObjectsList.Remove(currentObject);
			}

			return r;
		}

		private void DirtyEditor() {
			#if UNITY_EDITOR
				EditorUtility.SetDirty(network);
			#endif
		}
	}
}
#endif
