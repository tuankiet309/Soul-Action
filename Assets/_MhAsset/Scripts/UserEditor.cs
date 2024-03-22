

#if(UNITY_EDITOR)
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UserEditor : MonoBehaviour
{
    public static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/_MhAsset/Scenes/" + sceneName + ".unity");
        }
    }

    public static void OpenScene_02(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
        }
    }

    [MenuItem("[ Open Scene ]/1_Mh_Lobby")]
    public static void OpenScene_Lobby()
    {
        OpenScene("Mh_Main Menu");
    }


    [MenuItem("[ Open Scene ]/2_Mh_GamePlay")]
    public static void OpenScene_ShopVsInventory()
    {
        OpenScene("Mh_Main");
    }

    [MenuItem("[ Open Scene ]/3_Kiet_MainMenu")]
    public static void Open_SceneLobby()
    {
        OpenScene_02("Main Menu");
    }

    [MenuItem("[ Open Scene ]/4_Kiet_GamePlay")]
    public static void Open_SceneGamePlay()
    {
        OpenScene_02("Endless Level");
    }
}

#endif
