using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LedgerCultistPanel : MonoBehaviour
{
    public Human activeHuman;
    public HumanUiPrefab cultistSlot1;
    public HumanUiPrefab cultistSlot2;
    public HumanUiPrefab cultistSlot3;
    public HumanUiPrefab cultistSlot4;

    public TextMeshProUGUI noMembersText;
    public TextMeshProUGUI pageNumber;
    public GameObject pageination;
    public Button btnLeft;
    public Button btnRight;

    public int pageIndex;
    public List<Human> members;

    private void Start()
    {
        cultistSlot1.button.onClick.AddListener(() => HighlightButton(cultistSlot1));
        cultistSlot2.button.onClick.AddListener(() => HighlightButton(cultistSlot2));
        cultistSlot3.button.onClick.AddListener(() => HighlightButton(cultistSlot3));
        cultistSlot4.button.onClick.AddListener(() => HighlightButton(cultistSlot4));

        btnLeft.onClick.AddListener(() => BtnLeft());
        btnRight.onClick.AddListener(() => BtnRight());
    }
    private void OnEnable()
    {
        members = GameplayManager.dummyData?.cultMembers ?? new();
        activeHuman = null;
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        pageIndex = 0;

        if (members.Count == 0)
        {
            SetUpWithoutMembers();
        }
        else
        {
            noMembersText.gameObject.SetActive(false);

            if (members.Count < 5)
            {
                SetUpSinglePage();
            }
            else
            {
                UpdatePageNumber();
                pageination.SetActive(true);
                PrintPage(CreatePageFromIndex(0));
            }
        }
    }

    void SetUpWithoutMembers()
    {
        pageination.SetActive(false);

        cultistSlot1.gameObject.SetActive(false);
        cultistSlot2.gameObject.SetActive(false);
        cultistSlot3.gameObject.SetActive(false);
        cultistSlot4.gameObject.SetActive(false);

        noMembersText.gameObject.SetActive(true);
    }
    void SetUpSinglePage()
    {
        pageination.SetActive(false);
        PrintPage(CreatePageFromIndex(0));
    }

    LedgerCultistPage CreatePageFromIndex(int i)
    {
        LedgerCultistPage page = new();
        page.human1 = members.Count > i ? members[i] : null;
        page.human2 = members.Count > i + 1 ? members[i + 1] : null;
        page.human3 = members.Count > i + 2 ? members[i + 2] : null;
        page.human4 = members.Count > i + 3 ? members[i + 3] : null;

        return page;
    }

    void PrintPage(LedgerCultistPage page)
    {
        ClearActiveHuman();

        cultistSlot1.gameObject.SetActive(page.human1 != null);
        cultistSlot2.gameObject.SetActive(page.human2 != null);
        cultistSlot3.gameObject.SetActive(page.human3 != null);
        cultistSlot4.gameObject.SetActive(page.human4 != null);

        if (page.human1 != null)
        {
            cultistSlot1.SetDisplay(page.human1);
        }

        if (page.human2 != null)
        {
            cultistSlot2.SetDisplay(page.human2);
        }

        if (page.human3 != null)
        {
            cultistSlot3.SetDisplay(page.human3);
        }

        if (page.human4 != null)
        {
            cultistSlot4.SetDisplay(page.human4);
        }
    }

    void HighlightButton(HumanUiPrefab clickedObject)
    {

        if (clickedObject.highlight.activeInHierarchy)
        {
            clickedObject.Highlight(false);
            activeHuman = null;
            return;
        }

        cultistSlot1.Highlight(clickedObject == cultistSlot1);
        cultistSlot2.Highlight(clickedObject == cultistSlot2);
        cultistSlot3.Highlight(clickedObject == cultistSlot3);
        cultistSlot4.Highlight(clickedObject == cultistSlot4);

        activeHuman = clickedObject.human;
    }

    void ClearActiveHuman()
    {
        cultistSlot1.Highlight(false);
        cultistSlot2.Highlight(false);
        cultistSlot3.Highlight(false);
        cultistSlot4.Highlight(false);

        activeHuman = null;
    }

    void BtnLeft()
    {
        pageIndex -= 4;

        if (pageIndex < 0)
        {
            pageIndex = ((members.Count - 1) / 4) * 4;
        }

        UpdatePageNumber();
        PrintPage(CreatePageFromIndex(pageIndex));
    }

    void BtnRight()
    {
        pageIndex += 4;

        if (pageIndex >= members.Count)
        {
            pageIndex = 0;
        }

        UpdatePageNumber();
        PrintPage(CreatePageFromIndex(pageIndex));
    }

    void UpdatePageNumber()
    {
        int currentPage = (pageIndex / 4) + 1;
        int totalPages = (members.Count + 4 - 1) / 4;
        pageNumber.text = currentPage + "/" + totalPages;
    }

    public class LedgerCultistPage
    {
        public Human human1;
        public Human human2;
        public Human human3;
        public Human human4;
    }
}