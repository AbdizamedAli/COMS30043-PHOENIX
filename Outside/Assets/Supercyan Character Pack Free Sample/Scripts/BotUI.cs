using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SparkyControl
{
    public class BotUI : MonoBehaviour
    {
        public GameObject contentDisplayObject;
        public InputField input;

        public GameObject userBubble;
        public GameObject botBubble;

        private const int messagePadding = 15;
        private int allMessagesHeight = messagePadding;
        public bool increaseContentOnjectHeight;

        public RasaManager rasaManager;

        public void UpdateDisplay(string sender, string message, string messageType)
        {
            GameObject chatBubbleChild = CreateChatBubble(sender);
            AddChatComponent(chatBubbleChild, message, messageType);

            userBubble.SetActive(true);
            botBubble.SetActive(true);

            StartCoroutine(SetChatBubblePosition(chatBubbleChild.transform.parent.GetComponent<RectTransform>(), sender));
        }

        private IEnumerator SetChatBubblePosition(RectTransform chatBubblePos, string sender)
        {
            yield return new WaitForEndOfFrame();

            int horizontalPos = 0;
            if (sender == "user")
            {
                horizontalPos = -50;
            } else if (sender == "bot")
            {
                horizontalPos = 50;
            }
            
            allMessagesHeight += 15 + (int)chatBubblePos.sizeDelta.y;
            chatBubblePos.anchoredPosition3D = new Vector3(horizontalPos, -allMessagesHeight, 0);

            if (allMessagesHeight > 340)
            {
                RectTransform contentRect = contentDisplayObject.GetComponent<RectTransform>();
                contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, allMessagesHeight + messagePadding);
                contentDisplayObject.transform.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;
            }
        }

        public IEnumerator RefreshChatBubblePosition ()
        {
            yield return new WaitForEndOfFrame();

            int localAllMessagesHeight = messagePadding;
            foreach (RectTransform chatBubbleRect in contentDisplayObject.GetComponent<RectTransform>())
            {
                if (chatBubbleRect.sizeDelta.y < 35)
                {
                    localAllMessagesHeight += 35 + messagePadding;
                } else
                {
                    localAllMessagesHeight += (int)chatBubbleRect.sizeDelta.y + messagePadding;
                }
                chatBubbleRect.anchoredPosition3D = new Vector3(chatBubbleRect.anchoredPosition3D.x, -localAllMessagesHeight, 0);
            }

            allMessagesHeight = localAllMessagesHeight;
            if (allMessagesHeight > 340)
            {
                RectTransform contentRect = contentDisplayObject.GetComponent<RectTransform>();
                contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, allMessagesHeight + messagePadding);
                contentDisplayObject.transform.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;
            }
        }

        private GameObject CreateChatBubble (string sender)
        {
            GameObject chat = null;
            if (sender == "user")
            {
                chat = Instantiate(userBubble);
                chat.transform.SetParent(contentDisplayObject.transform, false);
            } else if (sender == "bot")
            {
                chat = Instantiate(botBubble);
                chat.transform.SetParent(contentDisplayObject.transform, false);
            }

            ContentSizeFitter chatsize = chat.AddComponent<ContentSizeFitter>();
            chatsize.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            chatsize.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            VerticalLayoutGroup verticalLayout = chat.AddComponent<VerticalLayoutGroup>();
            if (sender == "user")
            {
                verticalLayout.padding = new RectOffset(10, 20, 5, 5);
            } else if (sender == "bot")
            {
                verticalLayout.padding = new RectOffset(20, 10, 5, 5);
            }
            verticalLayout.childAlignment = TextAnchor.MiddleCenter;

            return chat.transform.GetChild(0).gameObject;
        }

        private void AddChatComponent (GameObject chatBubbleObject, string message, string messageType)
        {
            switch (messageType)
            {
                case "text":
                    Text chatMessage = chatBubbleObject.AddComponent<Text>();
                    chatMessage.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                    chatMessage.fontSize = 14;
                    chatMessage.alignment = TextAnchor.MiddleLeft;
                    chatMessage.text = message;
                    break;
                case "image":
                    Image chatImage = chatBubbleObject.AddComponent<Image>();
                    StartCoroutine(rasaManager.SetImageTextureFromUrl(message, chatImage));
                    break;
                case "attachment":
                    break;
                case "buttons":
                    break;
                case "elements":
                    break;
                case "quick_replies":
                    break;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
          
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
