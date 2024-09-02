using MelonLoader;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace StumbleGuysMod
{
    public class MainMod : MelonMod
    {
        private static GameObject? menu;
        private static bool menuVisible = false;
        private static bool UnlockAllCosmetics = false;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Mod Initialized");
            LoadSettings();
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F7))
            {
                ToggleMenu();
            }
        }

        private static void CreateMenu()
        {
            menu = new GameObject("Menu");
            var canvas = menu.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var canvasScaler = menu.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            var graphicRaycaster = menu.AddComponent<GraphicRaycaster>();

            var panel = new GameObject("Panel");
            panel.transform.SetParent(menu.transform);
            var panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0, 0, 0, 0.7f);
            var rectTransform = panel.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(400, Screen.height);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 0.5f);
            rectTransform.anchoredPosition = new Vector2(0, 0);

            AddButton(panel.transform, "Unlock All Cosmetics", new Vector2(10, 200), ToggleUnlockAllCosmetics);

            menu.SetActive(false);
        }

        private static void AddButton(Transform parent, string buttonText, Vector2 position, Action onClickAction)
        {
            GameObject button = new GameObject(buttonText.Replace(" ", "") + "Button");
            button.transform.SetParent(parent);
            Image buttonImage = button.AddComponent<Image>();
            buttonImage.color = new Color(0.2f, 0.2f, 0.2f, 0.7f);

            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.sizeDelta = new Vector2(380, 50);
            buttonRectTransform.anchoredPosition = position;

            Button buttonComponent = button.AddComponent<Button>();
            buttonComponent.onClick.AddListener(onClickAction);
            buttonComponent.interactable = true;

            GameObject text = new GameObject("ButtonText");
            text.transform.SetParent(button.transform);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = buttonText;
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleCenter;

            RectTransform textRectTransform = textComponent.GetComponent<RectTransform>();
            textRectTransform.sizeDelta = new Vector2(380, 50);
            textRectTransform.anchoredPosition = new Vector2(0, 0);
        }

        private static void ToggleMenu()
        {
            if (menu == null)
            {
                CreateMenu(); // Create the menu if it doesn't exist
            }

            menuVisible = !menuVisible;
            menu?.SetActive(menuVisible);
        }

        private static void ToggleUnlockAllCosmetics()
        {
            UnlockAllCosmetics = !UnlockAllCosmetics;
            MelonLogger.Msg($"Unlock All Cosmetics: {(UnlockAllCosmetics ? "Activated" : "Deactivated")}");
            SaveSettings();
        }

        private static void SaveSettings()
        {
            ConfigLoader.SaveSettings(UnlockAllCosmetics);
        }

        private static void LoadSettings()
        {
            var configData = ConfigLoader.LoadSettings();
            UnlockAllCosmetics = configData.UnlockAllCosmetics;

            MelonLogger.Msg($"Unlock All Cosmetics: {(UnlockAllCosmetics ? "Activated" : "Deactivated")}");
        }

        [HarmonyPatch(typeof(Il2CppCosmetics.CosmeticsService), "OwnsCosmetic")]
        public class OwnsCosmeticPatch
        {
            public static bool Prefix(ref bool __result)
            {
                if (UnlockAllCosmetics)
                {
                    __result = true; // Always return true to unlock all cosmetics
                    return false; // Skip the original method
                }
                return true; // Allow the original method to run if is not true
            }
        }

        [HarmonyPatch(typeof(Il2CppCosmetics.CosmeticData<GameObject>), "get_IsOwned")]
        public class IsOwnedPatch
        {
            public static bool Prefix(ref bool __result)
            {
                if (UnlockAllCosmetics)
                {
                    __result = true; // Always return true to unlock all cosmetics
                    return false; // Skip the original method
                }
                return true; // Allow the original method to run if is not true
            }
        }

        [HarmonyPatch(typeof(Il2CppCosmetics.CosmeticsService), "RefreshUserCsometicsPlayerPrefs")]
        public class RefreshUserCsometicsPlayerPrefsPatch
        {
            public static bool Prefix(ref bool userEquippedCosmetics)
            {
                if (UnlockAllCosmetics)
                {
                    userEquippedCosmetics = true; // Always return true to unlock all cosmetics
                    return false; // Skip the original method
                }
                return true; // Allow the original method to run if is not true
            }
        }
    }
}
