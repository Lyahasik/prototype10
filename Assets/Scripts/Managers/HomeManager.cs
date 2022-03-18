using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    [SerializeField] private GameObject _mainKnife;
    
    [SerializeField] private GameObject _knifesMenu;
    
    [Space]
    [SerializeField] private Text _recordScore;
    [SerializeField] private Text _recordLevel;
    [SerializeField] private Text _countApples;

    public void SetDataAcc()
    {
        _countApples.text = DataGame.GetCountApples().ToString();
        _recordScore.text = "score " + DataGame.GetRecordNumber();
        _recordLevel.text = DataGame.GetRecordLevel() + " disk";
        
        _mainKnife.GetComponent<MeshRenderer>().material.mainTexture = DataGame.GetCurrentTextureKnife();
    }

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        DataGame.ResetIdMaxOpenKnife();
        
        _gameManager.LoadData();
    }

    public void OpenKnifesMenu()
    {
        _knifesMenu.SetActive(true);
    }

    public void CloseKnifesMenu()
    {
        _knifesMenu.SetActive(false);
    }

    public void SwitchKnife(Texture texture)
    {
        _gameManager.SetIdCurrentTexture(texture);
        
        _mainKnife.GetComponent<MeshRenderer>().material.mainTexture = DataGame.GetCurrentTextureKnife();
    }
}
