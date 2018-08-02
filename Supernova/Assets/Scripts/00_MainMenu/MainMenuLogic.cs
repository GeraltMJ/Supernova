using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {

	public GameObject gameRoom; //游戏大厅的 Sprite
	public GameObject mainMenu; //刚开始的游戏界面

	public GameObject[] P; //表示房间里显示的 4 个角色立绘
	public GameObject[] readyPic;//表示4个角色的Ready图片
	public Sprite[] sprites; //4 个角色的立绘图像

	public int playerIndex = 0; // 表示本地的该玩家是几号,如果是0表示那么表示第一个进入房间，他就是房主
	public int playerTotal; // 表示当前房间的玩家数
	public int picIndex = 0; // 表示玩家选的是第几个立绘，默认是第一个立绘

	public GameObject characterSelect;
	public GameObject mapSelect;

	public int mapIndex;//TODO
	public GameObject readyBtn;
	public GameObject goBtn;

	public bool isReady = false;//是否准备

	//载入游戏需要的数据成员
	public Slider processBar;
	private AsyncOperation async;
	private int nowProcess;//当前进度
	public GameObject loadingPicture;//Lodading界面图片
	private int sceneIndex = 0;//表示需要载入的游戏场景的Index
	private int sceneTotal;//表示总共可以载入的游戏场景关卡

	// Use this for initialization
	void Start () {
		//TODO:进入房间，将4个玩家的人物显示出来
		//如果之前有玩家进入，默认初始化玩家的头像
		/*
		while(playerTotal >= 0)
		{
			P[playerTotal].GetComponent<Image>().sprite = sprites[0];
			playerTotal--;
		}
		*/

		if(playerIndex == 0)
		{
			goBtn.SetActive(true);
		}
		else
		{
			readyBtn.SetActive(true);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(async == null)
		{
			return;
		}
		int toProcess;
		if(async.progress < 0.9f)
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

	//改变制定位置的立绘图像
	public void Change(int playerIndex, int picIndex)
	{
		P[playerIndex].GetComponent<Image>().sprite = sprites[picIndex];
	}

	//右选人按钮的点击事件
	public void OnCharRightBtnDown()
	{
		picIndex = (picIndex + 1) % 4;
		characterSelect.GetComponent<Image>().sprite = sprites[picIndex];
		Change(playerIndex, picIndex);
		//return picIndex;
	}

	//左选人按钮的点击事件
	public void OnCharLeftBtnDown()
	{
		picIndex = (picIndex + 3) % 4;
		characterSelect.GetComponent<Image>().sprite = sprites[picIndex];
		Change(playerIndex, picIndex);
		//return picIndex;
	}

	//TODO:地图按钮
	/*
	public int OnMapLeftBtnDown()
	{
		
	}

	public int OnMapRightBtnDown()
	{

	}
	*/

	//当除房主外另外3个玩家准备完毕后，开始按钮点击事件
	public void OnGoBtnDown(int sceneIndex)
	{
		loadingPicture.SetActive(true);
		processBar.gameObject.SetActive(true);
		StartCoroutine(LoadGame(sceneIndex));
	}

	//退出按钮的点击事件
	public void OnExitBtnDown()
	{
		gameRoom.SetActive(false);
		mainMenu.SetActive(true);
	}

	//准备按钮的点击事件
	public void OnReadyBtnDown(int playerIndex)
	{
		readyPic[playerIndex].SetActive(true);
		isReady = true;
	}

	IEnumerator LoadGame(int sceneIndex)
	{
		async = SceneManager.LoadSceneAsync(sceneIndex);
		async.allowSceneActivation = false;
		yield return async;
	}

	public void OnMatchBtnDown()
	{
		gameRoom.SetActive(true);
		mainMenu.SetActive(false);
	}

}
