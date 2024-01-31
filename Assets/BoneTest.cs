using UnityEngine;
using System;
using System.Collections.Generic;

public class BoneTest : MonoBehaviour {

	public string fileName = "";	// ���[�h����CSV�t�@�C���̃p�X
	public float updateInterval;	// �t���[�����X�V����Ԋu�i�b�j
	public GameObject Bone;			// ���̂�GameObject

	private BoneLoader loader;				// �{�[���f�[�^�̃��[�_�[
	private List<List<Vector3>> bonesList;	// �{�[���ʒu���i�[����

	private float time = 0.0f;
	private int frameNo = 0;

	void Start() {
		// �{�[���ʒu�̃f�[�^�����[�h
		loader = new BoneLoader();
		bonesList = loader.load_bone_from_csv(fileName);
	}

	void Update() {
		// ��莞�ԑҋ@�������ʂ��X�V����
		// �{�[���ʒu�ɋ��̂𐶐����A�O��z�u�������͍̂폜����
		if (time > updateInterval) {
			time = 0.0f;

			// ���̍폜
			int childrenNum = transform.childCount;
			for (int i = childrenNum - 1; i >= 0; --i)
				Destroy(transform.GetChild(i).gameObject);

			// ���̐���
			// bonesList�̃C���f�b�N�X�ɂ̓t���[���ԍ����w�肷��
			var bones = bonesList[frameNo];
			for (int i = 0; i < bones.Count; ++i) {
				var bone = bones[i];
				var sphere = Instantiate(Bone);
				sphere.transform.parent = gameObject.transform;
				sphere.transform.localPosition = new Vector3(bone[0], bone[1], bone[2]);
				sphere.name = sphere.name + i.ToString();

				// �I�ƂЂ��ɂ͐F��t���Ă݂�
				// bones�̃C���f�b�N�X�ɂ� BoneIdx�̒萔���g����
				// ��`���Ă��Ȃ��l������̂ŁA�ڂ����͈ȉ��̃T�C�g���Q��
				// https://developers.google.com/mediapipe/solutions/vision/pose_landmarker
				if (i == BoneIdx.ELBOW_R || i == BoneIdx.ELBOW_L || i == BoneIdx.KNEES_R || i == BoneIdx.KNEES_L)
					sphere.GetComponent<Renderer>().material.color = Color.red;
				if (i == BoneIdx.SHOULDER_R || i == BoneIdx.SHOULDER_L || i == BoneIdx.HIP_R || i == BoneIdx.HIP_L)
					sphere.GetComponent<Renderer>().material.color = Color.blue;
			}

			// �t���[���ԍ���i�߂�
			++frameNo;
			if (frameNo >= bonesList.Count)
				frameNo = 0;
		}

		// ���Ԍv��
		time += Time.deltaTime;
	}
}
