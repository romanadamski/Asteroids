using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SaveTests
{
    [Test]
    [TestCase(10u)]
    [TestCase(100u)]
    [TestCase(200u)]
    public void SaveHighscore_ScoreIsHigherThanLowest_ScoreIsAdded(uint score)
    {
        //arrange
        SaveManager saveManager = CreateFakeSaveManager();

        //act
        saveManager.AddScoreToHighscores(score);

        //assert
        Assert.Contains(score, saveManager.GetHighscore());
    }

    [Test]
    [TestCase(100u)]
    [TestCase(99u)]
    [TestCase(10u)]
    public void SaveHighscore_ScoreIsLessThanLowestLessThanMaxCount_ScoreIsAdded(uint score)
    {
        //arrange
        SaveManager saveManager = CreateFakeSaveManager();
        FillSaveManagerWithHighscores(saveManager,
            100u, 200u, 500u);

        //act
        saveManager.AddScoreToHighscores(score);

        //assert
        Assert.IsTrue(saveManager.GetHighscore().Contains(score));
    }

    [Test]
    [TestCase(99u)]
    [TestCase(10u)]
    public void SaveHighscore_ScoreIsLessThanLowestMoreThanMaxCount_ScoreIsNotAdded(uint score)
    {
        //arrange
        SaveManager saveManager = CreateFakeSaveManager();
        FillSaveManagerWithHighscores(saveManager, 
            100u, 200u, 300u, 400u, 500u, 1100u, 1200u, 1300u, 1400u, 1500u);

        //act
        saveManager.AddScoreToHighscores(score);

        //assert
        var highscore = saveManager.GetHighscore();
        Assert.IsFalse(highscore.Contains(score));
    }

    [Test]
    [TestCase(1600u)]
    [TestCase(2000u)]
    [TestCase(1500u)]
    public void SaveHighscore_AddHighestScore_ScoreIsHighestScore(uint score)
    {
        //arrange
        SaveManager saveManager = CreateFakeSaveManager();
        FillSaveManagerWithHighscores(saveManager, 
            100u, 200u, 300u, 400u, 500u, 1100u, 1200u, 1300u, 1400u, 1500u);

        //act
        saveManager.AddScoreToHighscores(score);

        //assert
        Assert.AreEqual(saveManager.GetHighestScore(), score);
    }

    [Test]
    [TestCase(1499u)]
    [TestCase(1000u)]
    [TestCase(0u)]
    public void SaveHighscore_AddLessThanHighestScore_ScoreIsNotHighestScore(uint score)
    {
        //arrange
        SaveManager saveManager = CreateFakeSaveManager();
        FillSaveManagerWithHighscores(saveManager, 
            100u, 200u, 300u, 400u, 500u, 1100u, 1200u, 1300u, 1400u, 1500u);

        //act
        saveManager.AddScoreToHighscores(score);

        //assert
        Assert.AreNotEqual(saveManager.GetHighestScore(), score);
    }

    private SaveManager CreateFakeSaveManager()
    {
        GameObject gameObject = new GameObject();
        SaveManager saveManager = gameObject.AddComponent<SaveManager>();
        saveManager.LoadSave();
        saveManager.GetHighscore().Clear();
        return saveManager;
    }

    private void FillSaveManagerWithHighscores(SaveManager saveManager, params uint[] scores)
    {
        foreach (var score in scores)
        {
            saveManager.AddScoreToHighscores(score);
        }
    }
}
