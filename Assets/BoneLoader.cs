using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BoneLoader{

	public BoneLoader() {}

	// 戻り値の List<List<Vector3>> について
	// 
	// Vector3はボーンの位置座標を表す [0]: X, [1]: Y, [2]: Z
	//
	// List<Vector3> で、そのフレームに登場したボーンすべてを表す
	// インデックスは下記サイトの図を参照
	// https://developers.google.com/mediapipe/solutions/vision/pose_landmarker
	// 例えばインデックス 0 の場合、その座標値は鼻の位置を示している
	// いくつかは定数として定義しておいた
	//
	// List<List<Vector3>>で全フレームのボーンを表せる
	// ロードするCSVには1人分のデータしか入っていない
	public List<List<Vector3>> load_bone_from_csv(string CsvFileName) {
		var boneList = new List<List<Vector3>>();
		if (!File.Exists(CsvFileName)) {
			Debug.Log("指定されたCSVファイルが見つかりません");
			return null;
		}
		var reader = new StreamReader(CsvFileName);
		reader.ReadLine(); // 1行目は捨てる
		while (!reader.EndOfStream) {
			string line = reader.ReadLine();
			if (line != null && line != "") {
				string[] tmp = line.Split(',');
				var bones = new List<Vector3>();
				for (int i = 0; i < tmp.Length; i += 3)
					bones.Add(new Vector3(
						float.Parse(tmp[i]),
						-1 * float.Parse(tmp[i + 1]),   // yは上下逆っぽい？
						float.Parse(tmp[i + 2])
					));
				boneList.Add(bones);
			}
		}
		return boneList;
	}
}

public class BoneIdx {
	public const int NOSE = 0;
	public const int SHOULDER_R = 12;
	public const int SHOULDER_L = 11;
	public const int HIP_R = 24;
	public const int HIP_L = 23;
	public const int ELBOW_R = 14;
	public const int ELBOW_L = 13;
	public const int WRIST_R = 16;
	public const int WRIST_L = 15;
	public const int KNEES_R = 26;
	public const int KNEES_L = 25;
	public const int ANKLE_R = 28;
	public const int ANKLE_L = 27;
}
