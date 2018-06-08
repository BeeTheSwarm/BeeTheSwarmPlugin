using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;
using BlockSmash;
//using BTS;

public enum ChestRewardID
{

    Bees_HighPriority = 1,
    Bees_LowPriority1 = 2,
    Bees_LowPriority2 = 3

}

public class ChestsRewardController : MonoBehaviour
{
    public static event Action OnOpenChest = delegate { };

    [SerializeField] private SingleChest[] m_chests;

    [SerializeField] private Animator m_chestsAnimator; // used for shuffle chests
    [SerializeField] private Animator m_hintsAnimator; // used for hints
   
    private List<SingleChest> m_rewardedChests;
    private List<Vector3> m_startPositions;

    private int m_bees = 0;
    private bool isOpen = false;
    private bool isShowed = false;
    private bool beesReward = false;
    private bool chooseReward = false;
    private bool m_isChestsAvailable = false;

    void Start()
    {

                  // WebServerManager.OnOpenChestSuccessful += OnChestsRewardRecievedHandler;    //need to do
        //  WebServerManager.OnChooseChestSuccessful += ORewardRecivedHandler;         //need to do

        m_isChestsAvailable = false;
        isOpen = false;
        chooseReward = false;
        m_startPositions = new List<Vector3>();
        m_rewardedChests = new List<SingleChest>(m_chests);
        m_rewardedChests.Sort((x, y) => x.id.CompareTo(y.id));

        for (int i = 0; i < m_chests.Length; i++)
        {
            m_startPositions.Add(m_chests[i].m_transform.anchoredPosition);
        }

        for (int i = 0; i < m_chests.Length; i++)
        {
            m_chests[i].m_transform.anchoredPosition = RandomPosition();
        }
        
        OnOpenChest();   //!
    }

    private void OnDestroy()
    {
        // WebServerManager.OnOpenChestSuccessful -= OnChestsRewardRecievedHandler;          //need to do
        // WebServerManager.OnChooseChestSuccessful -= ORewardRecivedHandler;                //need to do
    }

    public void OpenChest()
    {
        isShowed = false;

        if (!isOpen)
        {
            isOpen = true;
        }
        if (chooseReward)
        {
            m_hintsAnimator.SetTrigger("ShowUserReward");
            HideChests();

             int id = PlayerProgress.Instance.Player.RewardID;      //
            //  new WS_ChooseChest(id).Send();                          //need to do
            chooseReward = false;
        }
    }


    private void OnChestsRewardRecievedHandler(RewardData[] reward)
    {
        
        m_hintsAnimator.SetTrigger("ShowPossibleRewards");
        // AudioManager.Instance.PlayAllChestsRewardsClip();          //need to do
        m_isChestsAvailable = true;
        m_bees = reward[0].reward.count;

        for (int i = 0; i < m_rewardedChests.Count; i++)
        {
            m_rewardedChests[i].m_CountText.text = reward[i].reward.count.ToString();

            StartCoroutine(ChestOpening(m_rewardedChests[i].m_transform, m_rewardedChests[i].m_simpleAnimation, m_rewardedChests[i].m_CountText.gameObject));
        }
        StartCoroutine(ShuffleChests());
    }

    private void ORewardRecivedHandler(int rewardType)
    {
        RewardData reward = PlayerProgress.Instance.Player.UserRewardData[rewardType - 1];
        m_isChestsAvailable = true;
        beesReward = false;

        SingleChest curChest = new SingleChest();

        switch (rewardType)
        {
            case 1:
                curChest = m_rewardedChests[0];
                m_bees = reward.reward.count;
                beesReward = true;
                break;

            case 2:
                curChest = m_rewardedChests[1];
                break;

            case 3:
                curChest = m_rewardedChests[2];
                //need to do
                break;
        }
        curChest.m_epicText.text = reward.reward.count.ToString();
        StartCoroutine(EpicChestOpening(curChest.m_epicTransform, curChest.m_epicAnimation, curChest.m_epicText.gameObject, beesReward));

    }

    private IEnumerator ChestOpening(RectTransform chestRect, SkeletonGraphic chestAnim, GameObject text)
    {

        chestRect.gameObject.SetActive(true);
        chestRect.DOScale(1, 0.2f);

        yield return new WaitWhile(() => !m_isChestsAvailable);
        chestAnim.freeze = false;

        yield return new WaitForSeconds(0.5f);
        text.SetActive(true);

        yield return new WaitForSeconds(2.3f);
        text.SetActive(false);
        isShowed = true;
    }

    private IEnumerator EpicChestOpening(RectTransform chestRect, SkeletonGraphic chestAnim, GameObject text, bool beesReward)
    {

        HideChests();

        chestRect.gameObject.SetActive(true);
        chestRect.DOScale(1, 0.2f);

        yield return new WaitWhile(() => !m_isChestsAvailable);
        chestAnim.freeze = false;
        //AudioManager.Instance.ReceiveChestReward();      //need to do
        yield return new WaitForSeconds(2.0f);

        if (beesReward)
        {
           // new BTS_AddReward(m_bees, m_bees).Send();     //SET REWARD BEES
        }

        text.SetActive(true);

        yield return new WaitForSeconds(1.8f);
        isShowed = true;

    }


    private IEnumerator ShuffleChests()
    {

        yield return new WaitWhile(() => !isShowed);
        m_chestsAnimator.SetTrigger("Shuffle");
        m_hintsAnimator.SetTrigger("ShuffleChests");
        yield return new WaitForSeconds(0.5f);
        //AudioManager.Instance.PlayShuffleChestsClip();    //need to do

        yield return new WaitForSeconds(1.8f);
        chooseReward = true;
        m_hintsAnimator.SetTrigger("ShowChooseChest");
    }

    private void HideChests()
    {
        foreach (SingleChest chest in m_rewardedChests)
        {
            chest.m_transform.DOScale(0, 0.2f);
        }
    }

    private Vector3 RandomPosition()
    {
        for (int i = 0; i < m_startPositions.Count; i++)
        {
            int randomIndex = Random.Range(0, m_startPositions.Count);
            Vector3 randomPosition = m_startPositions[randomIndex];
            m_startPositions.RemoveAt(randomIndex);
            return randomPosition;
        }
        return m_startPositions[0];
    }

}

[Serializable]
public class SingleChest {

    public ChestRewardID id = ChestRewardID.Bees_HighPriority;

    [Header("Simple chests")]
    public RectTransform m_transform;
    public SkeletonGraphic m_simpleAnimation;
    public Text m_CountText;

    [Header("Epic chests")]
    public RectTransform m_epicTransform;
    public SkeletonGraphic m_epicAnimation;
    public Text m_epicText;
}
