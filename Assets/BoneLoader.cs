using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BoneLoader{

	public BoneLoader() {}

	// �߂�l�� List<List<Vector3>> �ɂ���
	// 
	// Vector3�̓{�[���̈ʒu���W��\�� [0]: X, [1]: Y, [2]: Z
	//
	// List<Vector3> �ŁA���̃t���[���ɓo�ꂵ���{�[�����ׂĂ�\��
	// �C���f�b�N�X�͉��L�T�C�g�̐}���Q��
	// https://developers.google.com/mediapipe/solutions/vision/pose_landmarker
	// �Ⴆ�΃C���f�b�N�X 0 �̏ꍇ�A���̍��W�l�͕@�̈ʒu�������Ă���
	// �������͒萔�Ƃ��Ē�`���Ă�����
	//
	// List<List<Vector3>>�őS�t���[���̃{�[����\����
	// ���[�h����CSV�ɂ�1�l���̃f�[�^���������Ă��Ȃ�
	public List<List<Vector3>> load_bone_from_csv(string CsvFileName) {
		var boneList = new List<List<Vector3>>();
		if (!File.Exists(CsvFileName)) {
			Debug.Log("�w�肳�ꂽCSV�t�@�C����������܂���");
			return null;
		}
		var reader = new StreamReader(CsvFileName);
		reader.ReadLine(); // 1�s�ڂ͎̂Ă�
		while (!reader.EndOfStream) {
			string line = reader.ReadLine();
			if (line != null && line != "") {
				string[] tmp = line.Split(',');
				var bones = new List<Vector3>();
				for (int i = 0; i < tmp.Length; i += 3)
					bones.Add(new Vector3(
						float.Parse(tmp[i]),
						-1 * float.Parse(tmp[i + 1]),   // y�͏㉺�t���ۂ��H
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
