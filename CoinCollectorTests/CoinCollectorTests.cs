using NUnit.Framework;
//using AltTester.AltDriver;
using Altom.AltDriver;
using System.Threading;
//using UnityEngine;

namespace CoinCollectorTests;

public class AltTester_NIS_Tests
{   
    public AltDriver altDriver;
    //altDriver = new AltDriver();

    // Before any test it connects with the socket
    [SetUp]
    public void SetUp()
    {
        altDriver = new AltDriver();
    }

    // At the end of the test closes the connection with the socket
    [TearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    // scroll
    [Test]
    public void TestPlayerJumpsOnScroll()
    {
        altDriver.LoadScene("GameScene");
        Thread.Sleep(1000);

        // jump to collect the coin using scroll
        altDriver.Scroll(2000, 0.1f, true);

        // verify that the coin was collected
        Thread.Sleep(500);
        var coinValue = altDriver.FindObject(By.PATH,"//GameController/GameView/Coin/CoinValueText");
        Assert.AreEqual("1", coinValue.GetText());
    }
    
    // click object
    [Test]
    public void TestPlayerJumpsOnClick()
    {
        altDriver.LoadScene("GameScene");
        Thread.Sleep(1000);

        // jump to collect the coin using left click
        var player = altDriver.FindObject(By.NAME, "Player");
        player.Click();

        // verify that the coin was collected
        Thread.Sleep(500);
        var coinValue = altDriver.FindObject(By.PATH,"//GameController/GameView/Coin/CoinValueText");
        Assert.AreEqual("1", coinValue.GetText());
    }
    
    // tap object
    [Test]
    public void TestPlayerJumpsOnTap()
    {
        altDriver.LoadScene("GameScene");
        Thread.Sleep(1000);

        // jump to collect the coin using one tap
        var player = altDriver.FindObject(By.NAME, "Player");
        player.Tap();

        // verify that the coin was collected
        Thread.Sleep(500);
        var coinValue = altDriver.FindObject(By.PATH,"//GameController/GameView/Coin/CoinValueText");
        Assert.AreEqual("1", coinValue.GetText());
    }

    // swipe object
    [Test]
    public void TestPlayerJumpsOnSwipe()
    {
        altDriver.LoadScene("GameScene");
        Thread.Sleep(1000);

        // jump to collect the coin using swipe
        var playerPosition = altDriver.FindObject(By.NAME,"Player");
        altDriver.Swipe(new AltVector2(playerPosition.x + 1, playerPosition.y + 1), new AltVector2(playerPosition.x + 1, playerPosition.y + 1), 0.1f); //simulating a tap
        
        // verify that the coin was collected
        Thread.Sleep(500);
        var coinValue = altDriver.FindObject(By.PATH,"//GameController/GameView/Coin/CoinValueText");
        Assert.AreEqual("1", coinValue.GetText());
    }

    // key down + key up
    [Test]
    public void TestPlayerMovesOnKeyDownKeyUp()
    {
        altDriver.LoadScene("GameScene");        
        Thread.Sleep(1000);

        // move the player to the left until he arrives at the coin
        var playerPosition = altDriver.FindObject(By.NAME, "Player");
        var coinPosition = altDriver.FindObject(By.NAME, "SpawnPoint (2)");
        altDriver.KeyDown(AltKeyCode.LeftArrow);
        while(playerPosition.worldX - 3 > coinPosition.worldX) {
            playerPosition = altDriver.FindObject(By.NAME, "Player");
        }
        altDriver.KeyUp(AltKeyCode.LeftArrow);

        // jump to collect the coin using key down and key up on left click
        altDriver.KeyDown(AltKeyCode.Mouse0);
        Thread.Sleep(100);
        altDriver.KeyUp(AltKeyCode.Mouse0);

        // verify that the coin was collected
        Thread.Sleep(500);
        var coinValue = altDriver.FindObject(By.PATH,"//GameController/GameView/Coin/CoinValueText");
        Assert.AreEqual("1", coinValue.GetText());
    }

    // key down + key up + press key
    [Test]
    public void TestPlayerJumpsOnPressKey()
    {
        altDriver.LoadScene("GameScene");        
        Thread.Sleep(1000);

        // move the player down until he arrives at the coin
        var playerPosition = altDriver.FindObject(By.NAME, "Player");
        var coinPosition = altDriver.FindObject(By.NAME, "SpawnPoint (1)");
        altDriver.KeyDown(AltKeyCode.DownArrow);
        while(playerPosition.worldZ - 3 > coinPosition.worldZ) {
            playerPosition = altDriver.FindObject(By.NAME, "Player");
        }
        altDriver.KeyUp(AltKeyCode.DownArrow);

        // jump to collect the coin using press key on left click
        altDriver.PressKey(AltKeyCode.Mouse0);
        
        // verify that the coin was collected
        Thread.Sleep(500);
        var coinValue = altDriver.FindObject(By.PATH,"//GameController/GameView/Coin/CoinValueText");
        Assert.AreEqual("1", coinValue.GetText());
    }
    
    // begin touch + end touch
    [Test]
    public void TestPlayerJumpsOnBeginEndTouch()
    {
        altDriver.LoadScene("GameScene");
        Thread.Sleep(1000);

        // jump to collect the coin using touch
        var player = altDriver.FindObject(By.NAME,"Player");
        var playerPosition = player.getScreenPosition();
        var fingerId = altDriver.BeginTouch(playerPosition);
        altDriver.EndTouch(fingerId);

        // verify that the coin was collected
        Thread.Sleep(500);
        var coinValue = altDriver.FindObject(By.PATH,"//GameController/GameView/Coin/CoinValueText");
        Assert.AreEqual("1", coinValue.GetText());
    }

       [Test]

    public void TestCountDownWorks()
    {
        altDriver.LoadScene("GameScene");
        Thread.Sleep(500);

        var initialClock = altDriver.FindObject(By.PATH, "/Canvas/GameController/GameView/Time/TimeValueText");
        var initialClockValue = initialClock.GetText();

        Thread.Sleep(3000);

        var finalClock = altDriver.FindObject(By.PATH, "/Canvas/GameController/GameView/Time/TimeValueText");
        var finalClockValue = finalClock.GetText();

        Assert.AreNotEqual(initialClockValue, finalClockValue);

    }

    [Test]
    public void TestGameStopsWhenCountdownReachesZero()
    {
        altDriver.LoadScene("GameScene");
        Thread.Sleep(60000);

        var resultView = altDriver.FindObject(By.PATH, "/Canvas/GameController/ResultView");
        var finalCoin = altDriver.FindObject(By.PATH, "/Canvas/GameController/ResultView/FinalCoinText");

        var finalCoinText = finalCoin.GetText();
        Assert.AreEqual("0", finalCoinText);

    }
}

