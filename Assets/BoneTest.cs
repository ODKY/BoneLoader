using UnityEngine;
using System;
using System.Collections.Generic;

public class BoneTest : MonoBehaviour {

	public string fileName = "";	// ロードするCSVファイルのパス
	public float updateInterval;	// フレームを更新する間隔（秒）
	public GameObject Bone;			// 球体のGameObject

	private BoneLoader loader;				// ボーンデータのローダー
	private List<List<Vector3>> bonesList;	// ボーン位置を格納する

	private float time = 0.0f;
	private int frameNo = 0;

	void Start() {
		// ボーン位置のデータをロード
		loader = new BoneLoader();
		bonesList = loader.load_bone_from_csv(fileName);
	}

	void Update() {
		// 一定時間待機したら画面を更新する
		// ボーン位置に球体を生成し、前回配置した球体は削除する
		if (time > updateInterval) {
			time = 0.0f;

			// 球体削除
			int childrenNum = transform.childCount;
			for (int i = childrenNum - 1; i >= 0; --i)
				Destroy(transform.GetChild(i).gameObject);

			// 球体生成
			// bonesListのインデックスにはフレーム番号を指定する
			var bones = bonesList[frameNo];
			for (int i = 0; i < bones.Count; ++i) {
				var bone = bones[i];
				var sphere = Instantiate(Bone);
				sphere.transform.parent = gameObject.transform;
				sphere.transform.localPosition = new Vector3(bone[0], bone[1], bone[2]);
				sphere.name = sphere.name + i.ToString();

				// 肘とひざには色を付けてみた
				// bonesのインデックスには BoneIdxの定数が使える
				// 定義していない値もあるので、詳しくは以下のサイトを参照
				// https://developers.google.com/mediapipe/solutions/vision/pose_landmarker
				if (i == BoneIdx.ELBOW_R || i == BoneIdx.ELBOW_L || i == BoneIdx.KNEES_R || i == BoneIdx.KNEES_L)
					sphere.GetComponent<Renderer>().material.color = Color.red;
				if (i == BoneIdx.SHOULDER_R || i == BoneIdx.SHOULDER_L || i == BoneIdx.HIP_R || i == BoneIdx.HIP_L)
					sphere.GetComponent<Renderer>().material.color = Color.blue;
			}

			// フレーム番号を進める
			++frameNo;
			if (frameNo >= bonesList.Count)
				frameNo = 0;
		}

		// 時間計測
		time += Time.deltaTime;
	}
}
