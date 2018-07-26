using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGamePlay_1 : MonoBehaviour {

	public Slider processBar; //用于展示进度条的UI组件
	private AsyncOperation async; //异步处理对象
	private int nowProcess; //当前的进度
	public GameObject loadingPicture; //loading界面的图片


	public void ToGamePlay_1()
	{
		loadingPicture.SetActive(true);
		processBar.gameObject.SetActive(true);
		StartCoroutine(LoadScene_1());
	}
	
	IEnumerator LoadScene_1()
	{
		async = SceneManager.LoadSceneAsync(1);
		async.allowSceneActivation = false;
		yield return async;
	}

	// Update is called once per frame
	void Update () {
		if(async == null)
		{
			return;
		}
		int toProcess;
		//async.progress表示正在读取的场景的进度
		//如果当前的进度小于0.9，说明它还没有加载完成
		//如果加载完成，async.progress应该等于0.9
		if (async.progress < 0.9f)
		{
			toProcess = (int)async.progress * 100;
		}
		else
		{
			toProcess = 100;
		}
		if(nowProcess < toProcess)
		{
			nowProcess++;
		}
		processBar.value = nowProcess / 100f;
		if(nowProcess == 100)
		{
			async.allowSceneActivation = true;
		}

	}
}
