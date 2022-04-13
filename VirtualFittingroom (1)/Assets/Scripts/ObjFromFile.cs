using Dummiesman;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Events;
using Obi;
using AnotherFileBrowser.Windows;

public class ObjFromFile : MonoBehaviour
{
    public string objPath;
    public GameObject loadedObject;
    //tarvitaan jotta päästään käsiksi dropdownista valittuihin vaatteisiin
    public GameObject eventSystem;
    public choosecloth choosecloth;
    
    public GameObject parent;

    public InputField inputField;
    public InputField urlField;

    private Vector3 resetposition;

    void Start()
    {
        choosecloth = eventSystem.GetComponent<choosecloth>();
    }

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        new FileBrowser().OpenFileBrowser(bp, path=> {
            AssetBundleFromLocal(path);
        });
    }

    public void OpenFileBrowserAvatar()
    {
        var bp = new BrowserProperties();
        new FileBrowser().OpenFileBrowser(bp, path => {
            AvatarFromLocal(path);
        });
    }

    public void AssetBundleFromLocal(string path) {

        objPath = path;
        parent = eventSystem.GetComponent<AdjustObjectLocation>().currentCloth;
        //haetaan valitun cloth holderin lapsi (vaate) jos sellainen on olemassa
        try
        {
            loadedObject = parent.transform.GetChild(0).gameObject;
        }
        catch
        {
            loadedObject = null;
        }
        //file path
        if (!File.Exists(objPath))
        {
            inputField.text = "File doesn't exist.";
        }else
        {
            if(loadedObject != null)            
                Destroy(loadedObject);

            //Ladataan assetbundle
            var assetbundle = AssetBundle.LoadFromFile(objPath);

            //virheentarkistus
            if (assetbundle == null)
            {
                inputField.text="Failed to load assetbundle at" + objPath;
                return;
            }
            string rootAssetPath = assetbundle.GetAllAssetNames()[0];
            loadedObject = Instantiate(assetbundle.LoadAsset(rootAssetPath) as GameObject);

            //tarpeellinen koska muuten samaa assetbundlea ei voi avata useampaa kertaa
            assetbundle.Unload(false);

            //ladattu objecti tulee keskelle riippumatta holder objektin sijainnista joten resetoidaan holder sijainti keskelle ettei säätimistä lopu liikkumavara
            resetposition.x = 0;
            resetposition.y = 0;
            resetposition.z = 0;

            parent.transform.position = resetposition;

            //Asetetaan parent object
            loadedObject.transform.SetParent(parent.transform);

            //jos ladatulla vaatteella on particle picker asetetaan obisolver
            if (loadedObject.GetComponent<ObiParticlePicker>() != null)
            {
                loadedObject.GetComponent<ObiParticlePicker>().solver = this.gameObject.GetComponent<ObiSolver>();
            }
            
            //lopetetaan fysiikkasimulaatiot
            eventSystem.GetComponent<AdjustObjectLocation>().StopSimulation();
        }
    }

    public void AvatarFromLocal(string path)
    {

        objPath = path;

        //file path
        if (!File.Exists(objPath))
        {
            inputField.text = "File doesn't exist.";
        }
        else
        {
            if (loadedObject != null)
                Destroy(loadedObject);

            //Ladataan assetbundle
            var assetbundle = AssetBundle.LoadFromFile(objPath);

            //virheentarkistus
            if (assetbundle == null)
            {
                inputField.text = "Failed to load assetbundle at" + objPath;
                return;
            }
            string rootAssetPath = assetbundle.GetAllAssetNames()[0];
            loadedObject = Instantiate(assetbundle.LoadAsset(rootAssetPath) as GameObject);

            //tarpeellinen koska muuten samaa assetbundlea ei voi avata useampaa kertaa
            assetbundle.Unload(false);

            //ladattu objecti tulee keskelle riippumatta holder objektin sijainnista joten resetoidaan holder sijainti keskelle ettei säätimistä lopu liikkumavara
            resetposition.x = 0;
            resetposition.y = 0;
            resetposition.z = 0;

            transform.position = resetposition;

            //Asetetaan parent object
            loadedObject.transform.SetParent(this.transform);

            ObiSolver solver = this.gameObject.GetComponent<ObiSolver>();
            if (solver != null)
            {
                solver.enabled = false;
            }
        }
    }
    //vanha tapa ladata objekti tekstikentän avulla
    /*public void loadobject()
    {
        objPath = "/model?filename=" + urlField.text + "-WebGL";
        LoadAssetBundle(objPath);
    }*/

    public void loadshirt()
    {
        objPath = "/model/torso?filename=" + choosecloth.chosenShirt + "-WebGL";
        LoadAssetBundle(objPath);
    }
    public void loadpants()
    {
        objPath = "/model/legs?filename=" + choosecloth.chosenPants + "-WebGL";
        LoadAssetBundle(objPath);
    }
    public void loadavataruser()
    {
        objPath = "/avatar";
        LoadAssetBundle(objPath);
    }
    public void loadavatarmale()
    {
        objPath = "/avatar/default?avatar=avatarmale-WebGL";
        LoadAssetBundle(objPath);
    }
    public void loadavatarfemale()
    {
        objPath = "/avatar/default?avatar=avatarfemale-WebGL";
        LoadAssetBundle(objPath);
    }

    public void LoadAssetBundle(string objPath)
    {
        //objPath = "https://ttkr5.azurewebsites.net/test?container=testcontainer&filename=" + urlField.text + "-WebGL";
        //luodaan url hakulauseke assetbundlen nimen perusteella
        //objPath= "/model?filename=" + urlField.text + "-WebGL";

        //tuhoaa kaikki holder objectin lapsiobjectit
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //aloitetaan coroutine jossa ladataan assetbundle ladataan
        StartCoroutine (FetchAssetBundle(objPath));

        IEnumerator FetchAssetBundle(string url)
        {
            //request the assetbundle
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            yield return request.SendWebRequest();

            //catch and log network errors
            if (request.isNetworkError)
            {
                Debug.Log("network error");
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                //catching null errors
                if (bundle != null)
                {
                    Debug.Log("bundle löytynyt");
                    string rootAssetPath = bundle.GetAllAssetNames()[0];
                    //instantiate the asset bundle as loadedObject
                    loadedObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject);
                    bundle.Unload(false);

                    Debug.Log("bundle avattu");

                    //ladattu objecti tulee keskelle riippumatta holder objektin sijainnista joten resetoidaan holder sijainti keskelle ettei säätimistä lopu liikkumavara
                    resetposition.x = 0;
                    resetposition.y = 0;
                    resetposition.z = 0;

                    transform.position = resetposition;

                    //korjataan ladatun objectin koko yms
                    loadedObject.transform.SetParent(this.gameObject.transform);
                    loadedObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    loadedObject.transform.position.Set(0, 0, 0);
                }
                else{
                    Debug.Log("bundle ei löydy");
                }
            }
        }
    }

    

    public void FetchFromUrl()
    {
        objPath = "https://ttkr5.azurewebsites.net/test?container=testcontainer&filename=" + urlField.text + ".obj";
        //you can do more test here to check if the url correct

        if (loadedObject != null)
            Destroy(loadedObject);

        StartCoroutine(LoadObjFromUrlCoroutine(objPath));

        IEnumerator LoadObjFromUrlCoroutine(string url)
        {


            UnityWebRequest www = UnityWebRequest.Get(objPath);
            yield return www.SendWebRequest();

            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text));
            loadedObject = new OBJLoader().Load(textStream);

            //GameObject obj = new GameObject();
            //MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
            //MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
            //meshFilter.mesh = mesh;

            //add a BoxCollider to the loaded object
            //obj.AddComponent<BoxCollider>();

            //fixing the scaling and position problems

            loadedObject.transform.SetParent(this.gameObject.transform);
            loadedObject.transform.localScale = new Vector3(1f, 1f, 1f);
            loadedObject.transform.position.Set(0, 0, 0);
        }
    }
}
