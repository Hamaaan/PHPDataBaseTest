using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MySqlGetData : MonoBehaviour
{
    public Text ResultText_;    //結果を格納するテキスト
    public Text InputText_;     //idを入力するインプットフィールド

    public string uri = "http://hamgabu007.php.xdomain.jp/selecttest.php";  //selecttest.phpを指定　今回のアドレスはlocalhost

    //SendSignalボタンを押した時に実行されるメソッド
    public void SendSignal_Button_Push()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        dic.Add("date", InputText_.GetComponent<Text>().text);  //インプットフィールドからidの取得);
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい

        StartCoroutine(Post(uri, dic));  // POST
    }

    IEnumerator Post(string uri, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //送られてきたデータをテキストに反映
                ResultText_.GetComponent<Text>().text = www.downloadHandler.text;
                if (www.downloadHandler.text == "")
                {
                    ResultText_.GetComponent<Text>().text = "そんなデータねーぞ！！！";
                }
            }
        }
    }
}
