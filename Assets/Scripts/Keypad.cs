using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public KeypadBackground Background;

    private List<int> buttonsEntered;
    private Combination combination;

    // Start is called before the first frame update
    void Start()
    {
        combination = new Combination();
        ResetButtonEntries();
    }

    public void RegisterButtonClick(int buttonValue)
    {
        print("Keypad received :" + buttonValue);
        buttonsEntered.Add(buttonValue);
    }

    public void TryToUnlock()
    {
        if (IsCorrectCombination())
            Unlock();
        else
            FailToUnlock();

        ResetButtonEntries();
    }

    private bool IsCorrectCombination()
    {
        if (HaveNoButtonsBeenClicked())
            return false;

        if (HaveWrongNumberOfButtonsBeenClicked())
            return false;

        return CheckCombination();
    }

    private bool CheckCombination()
    {
        for (int buttonIndex = 0; buttonIndex < buttonsEntered.Count; buttonIndex++)
        {
            if (IsCorrectButton(buttonIndex) == false)
                return false;
        }
        return true;
    }

    private bool HaveNoButtonsBeenClicked()
    {
        return (buttonsEntered.Count == 0);
    }

    private bool HaveWrongNumberOfButtonsBeenClicked()
    {
        if (buttonsEntered.Count == combination.GetCombinationLength())
            return false;
        else
            return true;
    }

    private bool IsCorrectButton(int buttonIndex)
    {
        if (IsWrongEntry(buttonIndex))
            return false;
        return true;
    }

    private bool IsWrongEntry(int buttonIndex)
    {
        if (buttonsEntered[buttonIndex] == combination.GetCombinationDigit(buttonIndex))
            return false;
        return true;
    }

    private void Unlock()
    {
        Background.HideUnlockButton();
        Background.ChangeToSuccessColor();
    }

    private void FailToUnlock()
    {
        Background.ChangeToFailedColor();
        StartCoroutine(ResetBackgroundColor());
    }

    private IEnumerator ResetBackgroundColor()
    {
        yield return new WaitForSeconds(.25f);
        Background.ChangeToDefaultColor();
    }

    private void ResetButtonEntries()
    {
        buttonsEntered = new List<int>();
    }
}
