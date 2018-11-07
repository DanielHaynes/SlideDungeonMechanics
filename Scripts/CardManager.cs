using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using DG.Tweening;

public class CardManager : MonoBehaviour {
    const int MAX_GRID_WIDTH = 4;
    const int MAX_GRID_HEIGHT = 4;
    private float Grid_Spacer_Width = 170.0f;
    private float Grid_Spacer_Height = 267.0f;
    private Vector2 startPoint = new Vector2(-340.0f, 175.0f);

    private float cardTransitionDelay = 0.25f;
    private float cardTransitionDistance = 550.0f;

    [SerializeField]
    private Card cardTemplate;
    Card[,] activeCards;

    private void Awake() {
        SwipeDetector.OnSwipe += HandlePlayerSwipe;
        SwipeDetector.OnPreSwipe += HandlePlayerPotentialSwipe;

        KeyboardDetector.OnInput += HandlePlayerInputFromKeyboard;
        activeCards = new Card[MAX_GRID_WIDTH, MAX_GRID_HEIGHT];
    }

    void HandlePlayerInputFromKeyboard(KeyboardDirection direction) {
        SwipeData data = new SwipeData();
        data.Direction = ConvertKeyBoardDirection(direction);
        HandlePlayerSwipe(data);
    }
    /// <summary>
    /// Converts keyboard direction to swipe direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    SwipeDirection ConvertKeyBoardDirection(KeyboardDirection direction) {
        switch (direction) {
            case KeyboardDirection.Up: return SwipeDirection.Up;
            case KeyboardDirection.Right: return SwipeDirection.Right;
            case KeyboardDirection.Down: return SwipeDirection.Down;
            case KeyboardDirection.Left: return SwipeDirection.Left;
        }
        return SwipeDirection.Up;
    }

    void AddCardToBoard(SwipeDirection direction) {
        if (GameManager.instance.IsDeckActive()) {
            bool set = false;
            switch (direction) {
                case SwipeDirection.Up: {
                        do {
                            int randIndex = Random.Range(0, MAX_GRID_WIDTH);
                            if (activeCards[randIndex, MAX_GRID_HEIGHT - 1].IsCardActive() == false) {
                                set = true;
                                activeCards[randIndex, MAX_GRID_HEIGHT - 1].SetCardActiveState(true);
                                activeCards[randIndex, MAX_GRID_HEIGHT - 1].SetCardData(GameManager.instance.GetNextCard());

                                Vector2 tempPos = activeCards[randIndex, MAX_GRID_HEIGHT - 1].GetGridLocation();
                                activeCards[randIndex, MAX_GRID_HEIGHT - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(tempPos.x, tempPos.y - cardTransitionDistance);
                                activeCards[randIndex, MAX_GRID_HEIGHT - 1].GetComponent<RectTransform>().DOAnchorPos(tempPos, cardTransitionDelay);
                            }
                        } while (set == false);
                        break;
                    }
                case SwipeDirection.Right: {
                        do {
                            int randIndex = Random.Range(0, MAX_GRID_WIDTH);
                            if (activeCards[0, randIndex].IsCardActive() == false) {
                                set = true;
                                activeCards[0, randIndex].SetCardActiveState(true);
                                activeCards[0, randIndex].SetCardData(GameManager.instance.GetNextCard());

                                Vector2 tempPos = activeCards[0, randIndex].GetGridLocation();
                                activeCards[0, randIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(tempPos.x - cardTransitionDistance, tempPos.y);
                                activeCards[0, randIndex].GetComponent<RectTransform>().DOAnchorPos(tempPos, cardTransitionDelay);
                            }
                        } while (set == false);
                        break;
                    }
                case SwipeDirection.Down: {
                        do {
                            int randIndex = Random.Range(0, MAX_GRID_WIDTH);
                            if (activeCards[randIndex, 0].IsCardActive() == false) {
                                set = true;
                                activeCards[randIndex, 0].SetCardActiveState(true);
                                activeCards[randIndex, 0].SetCardData(GameManager.instance.GetNextCard());

                                Vector2 tempPos = activeCards[randIndex, 0].GetGridLocation();
                                activeCards[randIndex, 0].GetComponent<RectTransform>().anchoredPosition = new Vector2(tempPos.x, tempPos.y + cardTransitionDistance);
                                activeCards[randIndex, 0].GetComponent<RectTransform>().DOAnchorPos(tempPos, cardTransitionDelay);
                            }
                        } while (set == false);
                        break;
                    }
                case SwipeDirection.Left: {
                        do {
                            int randIndex = Random.Range(0, MAX_GRID_HEIGHT);
                            if (activeCards[MAX_GRID_WIDTH - 1, randIndex].IsCardActive() == false) {
                                set = true;
                                activeCards[MAX_GRID_WIDTH - 1, randIndex].SetCardActiveState(true);
                                activeCards[MAX_GRID_WIDTH - 1, randIndex].SetCardData(GameManager.instance.GetNextCard());

                                Vector2 tempPos = activeCards[MAX_GRID_WIDTH - 1, randIndex].GetGridLocation();
                                activeCards[MAX_GRID_WIDTH - 1, randIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(tempPos.x + cardTransitionDistance, tempPos.y);
                                activeCards[MAX_GRID_WIDTH - 1, randIndex].GetComponent<RectTransform>().DOAnchorPos(tempPos, cardTransitionDelay);
                            }
                        } while (set == false);
                        break;
                    }
            }
            //GameManager.instance.CreateNextCard();
        }
    }

    void SwapCards(Card cardOne, Card cardTwo) {
        cardOne.SetCardActiveState(true);
        cardOne.SetCardData(cardTwo);
        cardTwo.SetCardActiveState(false);
    }

    void HandlePlayerPotentialSwipe(SwipeData data) {
        //switch (data.Direction) {
        //    case SwipeDirection.Up:
        //        if (HandleUpMovementTest()) {
        //            for (int row = 0; row < MAX_GRID_WIDTH; row++) {
        //                for (int col = 0; col < MAX_GRID_HEIGHT; col++) {
        //                    if (activeCards[row, col].IsCardActive() && col != 0) {
        //                        activeCards[row, col].SetCardActiveState(true);
        //                        activeCards[row, col].transform.DOMove(new Vector2(activeCards[row, col].transform.position.x, activeCards[row, col].transform.position.y + Grid_Spacer_Height / 3), 1);
        //                    }
        //                }
        //            }
        //        }
        //        break;
        //    case SwipeDirection.Right:
        //        if (HandleRightMovementTest()) {
                   
        //        }
        //        break;
        //    case SwipeDirection.Down:
        //        if (HandleDownMovementTest()) {
                    
        //        }
        //        break;
        //    case SwipeDirection.Left:
        //        if (HandleLeftMovementTest()) {
                    
        //        }
        //        break;
        //}
    }

    void HandlePlayerSwipe(SwipeData data) {
        switch (data.Direction) {
            case SwipeDirection.Up:
                if (HandleUpMovementTest()) {
                    HandleUpMovement();
                    AddCardToBoard(SwipeDirection.Up);
                }
                break;
            case SwipeDirection.Right:
                if (HandleRightMovementTest()) {
                    HandleRightMovement();
                    AddCardToBoard(SwipeDirection.Right);
                }
                break;
            case SwipeDirection.Down:
                if (HandleDownMovementTest()) {
                    HandleDownMovement();
                    AddCardToBoard(SwipeDirection.Down);
                }
                break;
            case SwipeDirection.Left:
                if (HandleLeftMovementTest()) {
                    HandleLeftMovement();
                    AddCardToBoard(SwipeDirection.Left);
                }
                break;
        }
    }

    void HandleCardInteration(Card cardOne, Card cardTwo) {
        if (cardOne.GetCardType() != cardTwo.GetCardType()) {
            switch (cardOne.GetCardType()) {
                case CardType.player: {
                    switch (cardTwo.GetCardType()) {
                        case CardType.enemy:
                            if (cardOne.SurviveDamage(cardTwo.cardValue) == true) {
                                cardOne.TakeDamage(cardTwo.cardValue);
                                cardTwo.SetCardActiveState(false);
                                SwapCards(cardTwo, cardOne);
                            } else {
                                cardTwo.SetCardActiveState(false);
                                SwapCards(cardTwo, cardOne);
                            }
                            break;
                        case CardType.potion:
                            cardOne.AddValue(cardTwo.cardValue);
                            cardTwo.SetCardActiveState(false);
                            SwapCards(cardTwo, cardOne);
                            break;
                        case CardType.coin:
                            GameManager.instance.UpdatePlayerScore(cardTwo.cardValue);
                            cardTwo.SetCardActiveState(false);
                            SwapCards(cardTwo, cardOne);
                            break;
                    }
                    break;
                }
                case CardType.enemy: {
                    switch (cardTwo.GetCardType()) {
                        case CardType.player:
                            if (cardTwo.SurviveDamage(cardOne.cardValue)) {
                                cardTwo.TakeDamage(cardOne.cardValue);
                                cardOne.SetCardActiveState(false);
                            } else {
                                cardOne.SetCardActiveState(false);
                            }
                            break;
                    case CardType.shield:
                            if (cardOne.GetCardValue() > cardTwo.GetCardValue()) {
                                cardOne.TakeDamage(cardTwo.cardValue);
                                cardTwo.SetCardActiveState(false);
                                SwapCards(cardTwo, cardOne);
                            } else if (cardOne.GetCardValue() < cardTwo.GetCardValue()) {
                                cardTwo.TakeDamage(cardOne.cardValue);
                                cardOne.SetCardActiveState(false);
                            } else {
                                cardOne.SetCardActiveState(false);
                                cardTwo.SetCardActiveState(false);
                            }
                            break;
                    case CardType.weapon:
                            if (cardOne.GetCardValue() > cardTwo.GetCardValue()) {
                                cardOne.TakeDamage(cardTwo.cardValue);
                                cardTwo.SetCardActiveState(false);
                                SwapCards(cardTwo, cardOne);
                            } else if (cardOne.GetCardValue() < cardTwo.GetCardValue()) {
                                cardTwo.TakeDamage(cardOne.cardValue);
                                cardOne.SetCardActiveState(false);
                            } else {
                                cardOne.SetCardActiveState(false);
                                cardTwo.SetCardActiveState(false);
                            }
                            break;
                    }
                    break;
                }
                case CardType.coin:{
                    switch (cardTwo.GetCardType()) {
                        case CardType.player:
                            GameManager.instance.UpdatePlayerScore(cardOne.cardValue);
                            cardOne.SetCardActiveState(false);
                            break;
                    }
                    break;
                }
                case CardType.potion:{
                    switch (cardTwo.GetCardType()) {
                        case CardType.player:
                            cardTwo.AddValue(cardOne.cardValue);
                            cardOne.SetCardActiveState(false);
                            break;
                    }
                    break;
                }
                case CardType.shield:{
                    switch (cardTwo.GetCardType()) {
                        case CardType.enemy:
                            if (cardOne.GetCardValue() > cardTwo.GetCardValue()) {
                                cardOne.TakeDamage(cardTwo.cardValue);
                                cardTwo.SetCardActiveState(false);
                                SwapCards(cardTwo, cardOne);
                            } else if (cardOne.GetCardValue() < cardTwo.GetCardValue()) {
                                cardTwo.TakeDamage(cardOne.cardValue);
                                cardOne.SetCardActiveState(false);
                            } else {
                                cardOne.SetCardActiveState(false);
                                cardTwo.SetCardActiveState(false);
                            }
                            break;
                    }
                    break;
                }
                case CardType.weapon:{
                    switch (cardTwo.GetCardType()) {
                        case CardType.enemy:
                            if (cardOne.GetCardValue() > cardTwo.GetCardValue()) {
                                cardOne.TakeDamage(cardTwo.cardValue);
                                cardTwo.SetCardActiveState(false);
                                SwapCards(cardTwo, cardOne);
                            }else if (cardOne.GetCardValue() < cardTwo.GetCardValue()) {
                                cardTwo.TakeDamage(cardOne.cardValue);
                                cardOne.SetCardActiveState(false);
                            }else {
                                cardOne.SetCardActiveState(false);
                                cardTwo.SetCardActiveState(false);
                            }
                            break;
                    }
                    break;
                }
            }
        }
    }

    bool HandleCardInterationTest(Card cardOne, Card cardTwo) {
        if (cardOne.GetCardType() != cardTwo.GetCardType()) {
            switch (cardOne.GetCardType()) {
                case CardType.player: 
                    switch (cardTwo.GetCardType()) {
                        case CardType.enemy:    return true;
                        case CardType.potion:   return true;
                        case CardType.coin:     return true;
                        default:                return false;
                    }
                case CardType.enemy: 
                    switch (cardTwo.GetCardType()) {
                        case CardType.player:   return true;
                        case CardType.shield:   return true;
                        case CardType.weapon:   return false;
                        default:                return false;
                    }
                case CardType.coin:
                    switch (cardTwo.GetCardType()) {
                        case CardType.player:   return true;
                        default:                return false;
                    }
                case CardType.potion:
                    switch (cardTwo.GetCardType()) {
                        case CardType.player:   return true;
                        default:                return false;
                    }
                case CardType.shield:
                    switch (cardTwo.GetCardType()) {
                        case CardType.enemy:    return true;
                        default:                return false;
                    }
                case CardType.weapon:
                    switch (cardTwo.GetCardType()) {
                        case CardType.enemy:    return true;
                        default:                return false;
                    }
                default:
                    return false;
            }
        }
        return false;
    }

    public bool HandleUpMovementTest() {
        for (int row = 0; row < MAX_GRID_WIDTH; row++) {
            for (int col = 0; col < MAX_GRID_HEIGHT; col++) {
                if (activeCards[row, col].IsCardActive()) {
                    if (col != 0) {
                        if (activeCards[row, col - 1].IsCardActive()) {
                            if (HandleCardInterationTest(activeCards[row, col], activeCards[row, col - 1]) == true) {
                                return true;
                            }
                        } else {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void HandleUpMovement() {
        for (int row = 0; row < MAX_GRID_WIDTH; row++) {
            for (int col = 0; col < MAX_GRID_HEIGHT; col++) {
                if (activeCards[row, col].IsCardActive()) {
                    if (col != 0) {
                        if (activeCards[row, col - 1].IsCardActive()) {
                            HandleCardInteration(activeCards[row, col], activeCards[row, col - 1]);
                        } else {
                            SwapCards(activeCards[row, col - 1], activeCards[row, col]);
                        }
                    }
                }
            }
        }
    }

    public bool HandleRightMovementTest() {
        for (int row = MAX_GRID_WIDTH - 1; row >= 0; row--) {
            for (int col = MAX_GRID_HEIGHT - 1; col >= 0; col--) {
                if (activeCards[row, col].IsCardActive()) {
                    if (row != MAX_GRID_WIDTH - 1) {
                        if (activeCards[row + 1, col].IsCardActive()) {
                            if (HandleCardInterationTest(activeCards[row, col], activeCards[row + 1, col]) == true) return true;
                        } else {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void HandleRightMovement() {
        for (int row = MAX_GRID_WIDTH - 1; row >= 0; row--) {
            for (int col = MAX_GRID_HEIGHT - 1; col >= 0; col--) {
                if (activeCards[row, col].IsCardActive()) {
                    if (row != MAX_GRID_WIDTH - 1) {
                        if (activeCards[row + 1, col].IsCardActive()) {
                            HandleCardInteration(activeCards[row, col], activeCards[row + 1, col]);
                        } else {
                            SwapCards(activeCards[row + 1, col], activeCards[row, col]);
                        }
                    }
                }
            }
        }
    }

    public bool HandleDownMovementTest() {
        for (int row = MAX_GRID_WIDTH - 1; row >= 0; row--) {
            for (int col = MAX_GRID_HEIGHT - 1; col >= 0; col--) {
                if (activeCards[row, col].IsCardActive()) {
                    if (col != MAX_GRID_HEIGHT - 1) {
                        if (activeCards[row, col + 1].IsCardActive()) {
                            if (HandleCardInterationTest(activeCards[row, col], activeCards[row, col + 1]) == true) return true;
                        } else {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void HandleDownMovement()  {
        for (int row = MAX_GRID_WIDTH - 1; row >= 0; row--)  {
            for (int col = MAX_GRID_HEIGHT - 1; col >= 0; col--) {
                if (activeCards[row, col].IsCardActive()) {
                    if (col != MAX_GRID_HEIGHT - 1) {
                        if (activeCards[row, col + 1].IsCardActive()) {
                            HandleCardInteration(activeCards[row, col], activeCards[row, col + 1]);
                        } else {
                            SwapCards(activeCards[row, col + 1], activeCards[row, col]);
                        }
                    }
                }
            }
        }
    }

    public bool HandleLeftMovementTest() {
        for (int row = 0; row < MAX_GRID_WIDTH; row++) {
            for (int col = 0; col < MAX_GRID_HEIGHT; col++) {
                if (activeCards[row, col].IsCardActive()) {
                    if (row != 0) {
                        if (activeCards[row - 1, col].IsCardActive()) {
                            if (HandleCardInterationTest(activeCards[row, col], activeCards[row - 1, col]) == true) return true;
                        } else  {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void HandleLeftMovement() {
        for (int row = 0; row < MAX_GRID_WIDTH; row++) {
            for (int col = 0; col < MAX_GRID_HEIGHT; col++) {
                if (activeCards[row, col].IsCardActive()) {
                    if (row != 0)  {
                        if (activeCards[row - 1, col].IsCardActive()) {
                            HandleCardInteration(activeCards[row, col], activeCards[row - 1, col]);
                        }else {
                            SwapCards(activeCards[row - 1, col], activeCards[row, col]);
                        }
                    }
                }
            }
        }
    }

    public void FillGameBoard(int numberToStart) {
        for (int row = 0; row < MAX_GRID_WIDTH; row++) {
            for (int col = 0; col < MAX_GRID_HEIGHT; col++) {
                activeCards[row, col] = Instantiate(cardTemplate, cardTemplate.transform.position, cardTemplate.transform.rotation);
                activeCards[row, col].transform.SetParent(this.transform, false);
                float xPos = startPoint.x + (row * Grid_Spacer_Width);
                float yPos = startPoint.y - (col * Grid_Spacer_Height);
                Vector2 startLocation = new Vector2(xPos, yPos);
                activeCards[row, col].GetComponent<RectTransform>().anchoredPosition = startLocation;
                activeCards[row, col].SetCardActiveState(false);
                activeCards[row, col].SetGridLocation(startLocation);
            }
        }

        int setActive = 0;
        do {
            int randomRow = Random.Range(0, MAX_GRID_WIDTH);
            int randomCol = Random.Range(0, MAX_GRID_HEIGHT);

            if (setActive == 0) {
                activeCards[randomRow, randomCol].SetCardActiveState(true);
                activeCards[randomRow, randomCol].SetCardData(CardType.player, 15);
            } else {
                if (activeCards[randomRow, randomCol].IsCardActive() == false) {
                    activeCards[randomRow, randomCol].SetCardActiveState(true);
                    activeCards[randomRow, randomCol].SetCardData(GameManager.instance.GetNextCard());
                }
            }
            setActive++;
        } while (setActive < numberToStart);
    }
}