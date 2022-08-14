using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ChipDelete : MonoBehaviour
{
    new string name;
    const string fileExtension = ".txt";
    Manager _manager;
    public GameObject deleteConfirmation;
    public TextMeshProUGUI confirmText;
    public GameObject create;

    void Start() {
        _manager = GameObject.FindWithTag("Manager").GetComponent<Manager>();
    }

    public void ConfirmDelete(string _name) {
        deleteConfirmation.SetActive(true);
        Localiation transl = GameObject.Find("Translation").GetComponent<Localiation>(); // This doesn't work
        string text = string.Format(transl.GetText("Delete_Description"), _name);
        confirmText.text = text;
        name = _name;
    }

    public void DeleteLocal()
    {
        _manager.SaveWorkspace();
        string deletePath = SaveSystem.GetPathToSaveFile(name);
        string wireDeletePath = SaveSystem.GetPathToWireSaveFile(name);
        DeleteFile(deletePath);
        DeleteFile(wireDeletePath);
        create.GetComponent<CreateMenu>().FinishCreation();
        deleteConfirmation.SetActive(false);
        DeleteFile(SaveSystem.GetPathToSaveFile(""));
        
    }
   
    public void DeleteGlobal()
    {
        _manager.SaveWorkspace();
        string deleteGlobalPath = SaveSystem.GetPathToGlobalSaveFile(name);
        string wireDeleteGlobalPath = SaveSystem.GetPathToGlobalWireSaveFile(name);

        DeleteFile(deleteGlobalPath);
        DeleteFile(wireDeleteGlobalPath);

        DeleteLocal();
        deleteConfirmation.SetActive(false);
        DeleteFile(SaveSystem.GetPathToSaveFile(""));
    }

    void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);

        }
    }
}
