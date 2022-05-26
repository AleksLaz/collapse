// Created by Lazarevich Aleksei in year 2020.
// Proprietary software (c) 2020 Lazarevich Aleksei.
//
using LaserGames.Collapse.Level;
using LaserGames.Framework;
using UnityEngine;


namespace LaserGames.Collapse
{
	class S_MouseLeftButtonDown : ASignalParam<Vector3> { }

	class S_LevelStateExit : ASignalNoParam { }
	class S_LevelElementsMatched : ASignalParam<(ElementType, ElementColor)[]> { }
	class S_LevelScoresChanged : ASignalParam<int> { }
	class S_LevelTopRowReached : ASignalNoParam { }
	class S_LevelRowSpawned : ASignalNoParam { }
	class S_LevelGoalProgressChanged : ASignalParam<int> { }
	class S_LevelTimeTick : ASignalParam<int> { }
	class S_LeveltimeIsUp : ASignalNoParam { }
	class S_LevelGoal : ASignalNoParam { }
	class S_LevelGameplayStart : ASignalNoParam { }
	class S_LevelBombApplied : ASignalParam<(ElementType, ElementColor)[]> { }
}